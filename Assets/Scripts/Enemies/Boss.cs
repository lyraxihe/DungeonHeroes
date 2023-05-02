using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    public GameObject _CombatBackground; // Combate
    public GameObject[] Positions;   // Array de posiciones del combate
    public GameObject EnemyPosition; // Posición del enemigo
    public GameObject[] Enemies; // Array de enemigos
    public GameObject Enemy;             // Enemigo asociado

    public int Index;                // Índice que determina el tipo de enemigo que es

    public bool Atacar = false;      // Indica si se puede atacar o no

    public GameObject PlayerAttacking;             // Tipo de personaje del Jugador atacando este enemigo

    public GameObject UIEstadisticasPersonaje;

    // ESTADÍSTICAS DEL PERSONAJE
    public int VidaTotal;               // Vida máxima del personaje
    public int VidaActual;              // Vida actual del personaje
    public int AtaqueActual;            // Ataque actual del personaje
    public int AtaqueMax;               // Ataque máximo del personaje
    public int DefensaActual;           // Defensa actual del personaje
    public int DefensaMax;              // Defensa máxima del personaje
    public int DefensaActualPercentage; // Porcentaje de defensa del personaje

    //UI ENEMIGOS
    public GameObject UIEnemigo;
    public TMP_Text VidaEnemigo;
    public TMP_Text AtaqueEnemigo;
    public TMP_Text DefensaEnemigo;

    public GameObject UIEnemigoImagen;
    public GameObject UIEnemigoCorazon;
    public GameObject UIEnemigoEspada;
    public GameObject UIEnemigoEscudo;

    public GameObject PrefabHealthbar; // Prefab Healthbar
    public GameObject ClonHealthbar;  // Clon del prefab Healthbar

    public bool HabilidadSlime;    // Booleano para controlar si la habilidad del Slime está activa sobre este personaje

    public bool SelectedToAttack = false;          // Booleano que indica si el enemigo es seleccionable para ser atacado
    public bool Vibrate = false;                   // Booleano que controla si el enemigo seleccionable para ser atacado puede vibrar o no (Estética)
    public bool Down = false;                      // Blooleano que indica cuando puede empezar a reducir de tamaño el enemigo (Estética)
    public Vector2 MinTam;                        // Tamaño máximo que puede llegar a tener el enemigo
    public Vector2 MaxTam;                        // Tamaño mínimo que puede llegar a tener el enemigo
    public int Action;                             // 0 - ninguna acción, 1 - ataque, 2 - habilidad del Slime

    // Start is called before the first frame update
    void Start()
    {
        MinTam = new Vector2(transform.localScale.x, transform.localScale.y);         // Establece el tamaño máximo que puede llegar a tener la posición
        MaxTam = new Vector2(transform.localScale.x * 2, transform.localScale.y * 2); // Establece el tamaño mínimo que puede llegar a tener la posición
        Action = 0;                                                                   // Ninguna acción

        // Establece los atributos del personaje
        VidaTotal = VariablesGlobales.instance.BossVidaTotal;
        VidaActual = VariablesGlobales.instance.BossVidaActual;
        AtaqueActual = VariablesGlobales.instance.BossAtaqueActual;
        AtaqueMax = VariablesGlobales.instance.BossAtaqueMax;
        DefensaActual = VariablesGlobales.instance.BossDefensaActual;
        DefensaMax = VariablesGlobales.instance.BossDefensaMax;
        DefensaActualPercentage = VariablesGlobales.instance.BossDefensaActualPercentage;

        HabilidadSlime = false;

        CreateHealthbar();                    // Crea la barra de vida del personaje
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedToAttack && (PlayerAttacking.GetComponent<GeneralPlayer>().Atacando || PlayerAttacking.GetComponent<GeneralPlayer>().Habilidad2))                                                // Si el enemigo puede ser atacado
        {
            if (Vibrate)                                                     // Si vibrar es true
                ScaleUpPositionBecauseSelected();                            // Empieza la animación para indicar que el enemjigo puede ser atacado
        }

        UpdateHealthbarPosition();                                                          // Cada frame actualiza la posición de la barra de vida por si el personaje se mueve
        ClonHealthbar.GetComponent<Healthbar>().SetHealth(VidaActual);                      // Actualiza constantemente la Healthbar con la vida actual del personaje
        ClonHealthbar.GetComponent<Healthbar>().ChangeText(VidaActual + " / " + VidaTotal); // Actualiza el texto de la Healthbar a "VidaActual / VidaTotal"

        ControlAtributos();                                                                 // Controla cada frame que los valores de los atributos sean correctos
        ControlSlimeAbility();                                                              // Controla la duración de la habilidad del Slime
        ControlUI();
    }

    /****************************************************************************************
     * Función: ScalePositionBecauseSelected                                                *
     * Uso: Modifica el tamaño del enemigo para que vibre al poder ser atacado              *
     * Variables entrada: Nada                                                              *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void ScaleUpPositionBecauseSelected()
    {
        float velocidad = 1.5f;                                                                            // Velocidad a la que variará el tamaño de la posición

        if (Vector2.Distance(transform.localScale, MaxTam) < 0.2f)                                         // Si la posición ha alcanzado el tamño máximo, indica que puede empezar a reducirse
            Down = true;                                                                                   // Indica que puede empezar a reducir el tamaño

        if (Vector2.Distance(transform.localScale, MinTam) < 0.2f)                                         // Si la posición ha alcanzado el tamaño mínimo, indica que puede empezar a aumentar
            Down = false;                                                                                  // INdica que puede empezar a aumentar el tamaño

        if (!Down)                                                                                          // Si puede aumentar de tamaño
            transform.localScale = Vector2.Lerp(transform.localScale, MaxTam, velocidad * Time.deltaTime);  // Aumenta el tamaño progresivamente hasta el tamaño máximo
        else                                                                                                // Si puede reducir de tamaño
            transform.localScale = Vector2.Lerp(transform.localScale, MinTam, velocidad * Time.deltaTime);  // Reduce el tamaño progresivamentehasta el tamaño mínimo
    }

    /****************************************************************************************
     * Función: OnMouseOver                                                                 *
     * Uso: Al hacer click en el enemigo realiza una serie de acciones                      *
     * Variables entrada: Nada                                                              *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void OnMouseDown()
    {
        int i;

        for (i = 0; i < Enemies.Length; i++)                                   // Recorre el array de enemigos
        {
            if (Enemies[i] != null)
            {
                if (Enemies[i].GetComponent<GeneralEnemy>().Enemy != Enemy)           // Para el resto de enemigos que no son este
                {
                    Enemies[i].GetComponent<GeneralEnemy>().SelectedToAttack = false;                 // Inhabilita que el personaje pueda atacarlos
                    Enemies[i].transform.localScale = Enemies[i].GetComponent<GeneralEnemy>().MinTam; // Dichos enemigos modificados vuelven a su tamaño original después de vibrar
                }
            }
        }

        if (SelectedToAttack && (PlayerAttacking.GetComponent<GeneralPlayer>().Atacando || PlayerAttacking.GetComponent<GeneralPlayer>().Habilidad2))                                                // Si el enemigo puede ser atacado
        {
            Vibrate = false;                                                                                                                            // El resto de posiciones dejan de vibrar
            transform.localScale = MinTam;                                                                                                              // La posición vuelve a su tamaño original

            if (PlayerAttacking.GetComponent<GeneralPlayer>().CharacterType != 1)
                PlayerAttacking.GetComponent<Animator>().SetTrigger("ataque");

            if (Action == 1)                                                                  // Si la acción es la de atacar
            {
                if (PlayerAttacking.GetComponent<GeneralPlayer>().CharacterType == 1)         // El personaje atacando es un Knight
                    EnemyAttacked(PlayerAttacking.GetComponent<PlayerKnight>().AtaqueActual); // Quita el daño del atacante a la vida actual del enemigo atacado
                else if (PlayerAttacking.GetComponent<GeneralPlayer>().CharacterType == 2)    // El personaje atacando es un Healer
                    EnemyAttacked(PlayerAttacking.GetComponent<PlayerHealer>().AtaqueActual); // Quita el daño del atacante a la vida actual del enemigo atacado
                else if (PlayerAttacking.GetComponent<GeneralPlayer>().CharacterType == 3)    // El personaje atacando es un Slime
                    EnemyAttacked(PlayerAttacking.GetComponent<PlayerSlime>().AtaqueActual);  // Quita el daño del atacante a la vida actual del enemigo atacado
                else                                                                          // El personaje atacando es un Mage
                    EnemyAttacked(PlayerAttacking.GetComponent<PlayerMage>().AtaqueActual);   // Quita el daño del atacante a la vida actual del enemigo atacado

                PlayerAttacking.GetComponent<GeneralPlayer>().Atacando = false;
            }
            else if (Action == 2)                                                             // Si la acción es la habilidad del Slime
            {
                if (!VariablesGlobales.instance.Boss)
                {
                    if (Index == 1)       // Si el enemigo elegido es un Knight
                    {
                        Enemy.GetComponent<EnemyKnight>().DefensaActual -= 3;
                        Enemy.GetComponent<EnemyKnight>().HabilidadSlime = true;
                        _CombatBackground.GetComponent<CombatBackground>().ContHabilidadSlime = 0;
                    }
                    else if (Index == 2) // Si el enemigo elegido es un Healer
                    {
                        Enemy.GetComponent<EnemyHealer>().DefensaActual -= 3;
                        Enemy.GetComponent<EnemyHealer>().HabilidadSlime = true;
                        _CombatBackground.GetComponent<CombatBackground>().ContHabilidadSlime = 0;
                    }
                    else if (Index == 3) // Si el enemigo elegido es un Slime
                    {
                        Enemy.GetComponent<EnemySlime>().DefensaActual -= 3;
                        Enemy.GetComponent<EnemySlime>().HabilidadSlime = true;
                        _CombatBackground.GetComponent<CombatBackground>().ContHabilidadSlime = 0;
                    }
                    else                 // Si el enemigo elegido es un Mage
                    {
                        Enemy.GetComponent<EnemyMage>().DefensaActual -= 3;
                        Enemy.GetComponent<EnemyMage>().HabilidadSlime = true;
                        _CombatBackground.GetComponent<CombatBackground>().ContHabilidadSlime = 0;
                    }
                }
                else
                {
                    Enemy.GetComponent<Boss>().DefensaActual -= 3;
                    Enemy.GetComponent<Boss>().HabilidadSlime = true;
                    _CombatBackground.GetComponent<CombatBackground>().ContHabilidadSlime = 0;
                }
                PlayerAttacking.GetComponent<PlayerSlime>().UsedAbility = true;
                PlayerAttacking.GetComponent<GeneralPlayer>().Habilidad2 = false;
            }

            SelectedToAttack = false;                                                                    // Hace que esta acción sólo se pueda realizar una vez
            PlayerAttacking.transform.localScale = PlayerAttacking.GetComponent<GeneralPlayer>().MinTam; // Devuelve al atacante a su tamaño original
            transform.localScale = MinTam;                                                               // Devuelve al enemigo a su tamaño original

            PlayerAttacking.GetComponent<GeneralPlayer>().DestroyCharacterInfo();         // Destruye la interfaz de información del personaje
            _CombatBackground.GetComponent<CombatBackground>().ChangeTurn();              // Tras la acción del movimiento, cambia el turno de la partida

            Atacar = true;

            Action = 0;                                                                        // Indica que ya no se realiza ninguna acción
            _CombatBackground.GetComponent<CombatBackground>().EnemigoParaAtacar = true;
        }
        UIEstadisticasPersonaje.SetActive(false);
    }

    /****************************************************************************************
     * Función: EnemyAttacked                                                               *
     * Uso: Determina que el tipo de enemigo asociado para quitarle vida                    *
     * Variables entrada: damage - Daño del atacante hacia el enemigo                       *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void EnemyAttacked(int damage)
    {
        VidaActual -= (damage - ((DefensePercentage() * damage) / 100));
    }

    /****************************************************************************************
     * Función: CreateHealthbar                                                             *
     * Uso: Crea la barra de vida del personaje                                             *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void CreateHealthbar()
    {
        ClonHealthbar = Instantiate(PrefabHealthbar);                                                                                             // Instancia el prefab Healthbar en el clon del prefab Healthbar
        ClonHealthbar.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z); // Lo coloca justo debajo del sprite del enemigo
        ClonHealthbar.GetComponent<Healthbar>().SetMaxHealth(VidaTotal);                                                                          // Inicializa la Healthbar con la vida máxima del personaje
        ClonHealthbar.GetComponent<Healthbar>().ChangeText(VidaActual + " / " + VidaTotal);                                                       // Cambia el texto de la Healthbar a "VidaActual / VidaTotal"
    }

    /****************************************************************************************
     * Función: UpdateHealthbarPosition                                                     *
     * Uso: Actualiza la posición de la barra de vida del personaje                         *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void UpdateHealthbarPosition()
    {
        ClonHealthbar.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z); // Coloca la barra de vida debajo del personaje
    }

    /****************************************************************************************
     * Función: EnemyAtack                                                                  *
     * Uso: Ataque del personaje, de entre las posiciones cercanas selecciona una aleatoria *
     *      que contenga un aliado y lo ataca                                               *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void EnemyAtack()
    {
        GameObject position;       // Posición a la que atacará el personaje
        int damage = AtaqueActual; // El daño es el ataque actual del enemigo
        bool attack;

        if (Atacar)
        {
            do
            {
                attack = false;
                position = Positions[Random.Range(0, Positions.Length)];                                                       // Selecciona una posición aleatoria de las accesibles desde la posición del enemigo

                if (position.GetComponent<CombatPosition>().CharacterType == 2)                                                 // Si la posición están ocuapada por un personaje del Jugador
                {
                    attack = true;
                    if (position.GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterType == 1)    // Si la posición está ocupada por un Knight
                        if (position.GetComponent<CombatPosition>().Character.GetComponent<PlayerKnight>().Invencible == true) // Si está en modo invencible
                            attack = false;                                                                                    // No puede atacar esa posición
                }
            } while (!attack);
            
            if (position.GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterType == 1)      // Si el objetivo es un Knight
            {
                position.GetComponent<CombatPosition>().Character.GetComponent<PlayerKnight>().VidaActual -= (damage - ((position.GetComponent<CombatPosition>().Character.GetComponent<PlayerKnight>().DefensePercentage() * damage) / 100));
            }
            else if (position.GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterType == 2) // Si el objeivo es un Healer
            {
                position.GetComponent<CombatPosition>().Character.GetComponent<Animator>().SetTrigger("danho");
                position.GetComponent<CombatPosition>().Character.GetComponent<PlayerHealer>().VidaActual -= (damage - ((position.GetComponent<CombatPosition>().Character.GetComponent<PlayerHealer>().DefensePercentage() * damage) / 100));
            }
            else if (position.GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterType == 3) // Si el objetivo es un Slime
            {
                position.GetComponent<CombatPosition>().Character.GetComponent<Animator>().SetTrigger("danho");
                position.GetComponent<CombatPosition>().Character.GetComponent<PlayerSlime>().VidaActual -= (damage - ((position.GetComponent<CombatPosition>().Character.GetComponent<PlayerSlime>().DefensePercentage() * damage) / 100));
            }
            else                                                                                                         // Si el objetivo es un Mage
            {
                position.GetComponent<CombatPosition>().Character.GetComponent<Animator>().SetTrigger("danho");
                position.GetComponent<CombatPosition>().Character.GetComponent<PlayerMage>().VidaActual -= (damage - ((position.GetComponent<CombatPosition>().Character.GetComponent<PlayerMage>().DefensePercentage() * damage) / 100));
            }

            Atacar = false; // Indica que el enemigo no puede volver a atacar
        }

        //GameObject position;       // Posición a la que atacará el personaje
        //int damage = AtaqueActual; // El daño es el ataque actual del enemigo
        //bool attack;
        //int index;
        //int[] indexNotValid = new int[Positions.Length];
        //bool repeatIndex;
        //bool noPositions;
        //int i;

        //if (Atacar)
        //{
        //    do
        //    {
        //        attack = false;
        //        repeatIndex = true;
        //        noPositions = false;

        //        do
        //        {
        //            index = Random.Range(0, Positions.Length);                                                       // Selecciona una posición aleatoria de las accesibles desde la posición del enemigo
        //            for (i = 0; i < indexNotValid.Length; i++)
        //            {
        //                if (index == (indexNotValid[i] - 1))
        //                    repeatIndex = false;
        //            }
        //        } while (!repeatIndex);

        //        position = Positions[index];
        //        if (position.GetComponent<CombatPosition>().CharacterType == 2)                                                 // Si la posición están ocuapada por un personaje del Jugador
        //        {
        //            attack = true;
        //            if (position.GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterType == 1)    // Si la posición está ocupada por un Knight
        //                if (position.GetComponent<CombatPosition>().Character.GetComponent<PlayerKnight>().Invencible == true) // Si está en modo invencible
        //                    attack = false;                                                                                    // No puede atacar esa posición
        //        }
        //        else
        //        {
        //            for(i = 0; i < indexNotValid.Length; i++)
        //            {
        //                if (indexNotValid[i] == 0)
        //                {
        //                    indexNotValid[i] = index + 1;
        //                    break;
        //                }
        //            }

        //            if(i == indexNotValid.Length - 1)
        //                noPositions = true;
        //        }

        //    } while (!attack && !noPositions);                                                                                  // Repite hasta que la posción seleccionada esté ocupada por un personaje del Jugador

        //    if (position.GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterType == 1)      // Si el objetivo es un Knight
        //        position.GetComponent<CombatPosition>().Character.GetComponent<PlayerKnight>().VidaActual -= (damage - ((position.GetComponent<CombatPosition>().Character.GetComponent<PlayerKnight>().DefensePercentage() * damage) / 100));
        //    else if (position.GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterType == 2) // Si el objeivo es un Healer
        //        position.GetComponent<CombatPosition>().Character.GetComponent<PlayerHealer>().VidaActual -= (damage - ((position.GetComponent<CombatPosition>().Character.GetComponent<PlayerHealer>().DefensePercentage() * damage) / 100));
        //    else if (position.GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterType == 3) // Si el objetivo es un Slime
        //        position.GetComponent<CombatPosition>().Character.GetComponent<PlayerSlime>().VidaActual -= (damage - ((position.GetComponent<CombatPosition>().Character.GetComponent<PlayerSlime>().DefensePercentage() * damage) / 100));
        //    else                                                                                                         // Si el objetivo es un Mage
        //        position.GetComponent<CombatPosition>().Character.GetComponent<PlayerMage>().VidaActual -= (damage - ((position.GetComponent<CombatPosition>().Character.GetComponent<PlayerMage>().DefensePercentage() * damage) / 100));

        //    Atacar = false; // Indica que el enemigo no puede volver a atacar
        //}
    }

    /****************************************************************************************
     * Función: DefensePercentage                                                           *
     * Uso: Establece el porcentage de defensa del enemigo en función de su defensa actual  *
     * Variables entrada: Ninguno                                                           *
     * Return: int - Porcentaje de defensa del enemigo                                      *
     ****************************************************************************************/
    public int DefensePercentage()
    {
        if (DefensaActual == 1)
            return 5;
        else if (DefensaActual == 2)
            return 10;
        else if (DefensaActual == 3)
            return 15;
        else if (DefensaActual == 4)
            return 20;
        else if (DefensaActual == 5)
            return 25;
        else if (DefensaActual == 6)
            return 30;
        else if (DefensaActual == 7)
            return 35;
        else if (DefensaActual == 8)
            return 40;
        else if (DefensaActual == 9)
            return 45;
        else
            return 50;
    }

    /****************************************************************************************
     * Función: ControlAtributes                                                            *
     * Uso: Controla que si algún atributo del personaje es negativo, pasa a ser 0. Y si    *
     *      algún atributo es mayor que su máximo, pasa a ser su máximo                     *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void ControlAtributos()
    {
        if (VidaActual < 0)
            VidaActual = 0;
        if (VidaActual > VidaTotal)
            VidaActual = VidaTotal;

        if (AtaqueActual < 0)
            AtaqueActual = 0;
        if (AtaqueActual > AtaqueMax)
            AtaqueActual = AtaqueMax;

        if (DefensaActual < 0)
            DefensaActual = 0;
        if (DefensaActual > DefensaMax)
            DefensaActual = DefensaMax;
    }

    /****************************************************************************************
     * Función: ControlSlimeAbility                                                         *
     * Uso: Controla el tiempo que dura la habilidad del slime                              *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void ControlSlimeAbility()
    {
        if (HabilidadSlime == true)
        {
            if (_CombatBackground.GetComponent<CombatBackground>().ContHabilidadSlime >= 3) // Si ya han pasado dos o más turnos
            {
                HabilidadSlime = false;                                                     // Inhabilita la habilidad del Slime
                DefensaActual += 3;                                                         // Devuelve la defensa perdida
                _CombatBackground.GetComponent<CombatBackground>().ContHabilidadSlime = 0;  // Reinicia el contador

                // Indica al Slime del Jugador que ya puede usar de nuevo su habilidad
                for (int i = 0; i < _CombatBackground.GetComponent<CombatBackground>().Aliados.Length; i++)
                {
                    if (_CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 3)
                        _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().UsedAbility = false;
                }
            }
        }
    }

    private void ControlUI()
    {
        if (VidaActual <= (20 * VidaTotal) / 100)
        {
            VidaEnemigo.text = "<color=red>" + VidaActual + "</color> / " + VidaTotal;
        }
        else if (VidaActual <= (50 * VidaTotal) / 100)
        {
            VidaEnemigo.text = "<color=yellow>" + VidaActual + "</color> / " + VidaTotal;
        }
        else
        {
            VidaEnemigo.text = VidaActual + " / " + VidaTotal;
        }

        if (AtaqueActual < VariablesGlobales.instance.BossAtaqueActual)
        {
            AtaqueEnemigo.text = "<color=red>" + AtaqueActual + "</color> / " + AtaqueMax;
        }
        else if (AtaqueActual > VariablesGlobales.instance.BossAtaqueActual)
        {
            AtaqueEnemigo.text = "<color=green>" + AtaqueActual + "</color> / " + AtaqueMax;
        }
        else
        {
            AtaqueEnemigo.text = AtaqueActual + " / " + AtaqueMax;
        }

        if (DefensaActual < VariablesGlobales.instance.BossDefensaActual)
        {
            DefensaEnemigo.text = "<color=red>" + DefensaActualPercentage + "%</color> / 50%";
        }
        else if (DefensaActual > VariablesGlobales.instance.BossDefensaActual)
        {
            DefensaEnemigo.text = "<color=green>" + DefensaActualPercentage + "%</color> / 50%";
        }
        else
        {
            DefensaEnemigo.text = DefensaActualPercentage + "% / 50%";
        }
    }
}
