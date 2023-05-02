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
    public GameObject EnemyPosition; // Posici�n del enemigo
    public GameObject[] Enemies; // Array de enemigos
    public GameObject Enemy;             // Enemigo asociado

    public int Index;                // �ndice que determina el tipo de enemigo que es

    public bool Atacar = false;      // Indica si se puede atacar o no

    public GameObject PlayerAttacking;             // Tipo de personaje del Jugador atacando este enemigo

    public GameObject UIEstadisticasPersonaje;

    // ESTAD�STICAS DEL PERSONAJE
    public int VidaTotal;               // Vida m�xima del personaje
    public int VidaActual;              // Vida actual del personaje
    public int AtaqueActual;            // Ataque actual del personaje
    public int AtaqueMax;               // Ataque m�ximo del personaje
    public int DefensaActual;           // Defensa actual del personaje
    public int DefensaMax;              // Defensa m�xima del personaje
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

    public bool HabilidadSlime;    // Booleano para controlar si la habilidad del Slime est� activa sobre este personaje

    public bool SelectedToAttack = false;          // Booleano que indica si el enemigo es seleccionable para ser atacado
    public bool Vibrate = false;                   // Booleano que controla si el enemigo seleccionable para ser atacado puede vibrar o no (Est�tica)
    public bool Down = false;                      // Blooleano que indica cuando puede empezar a reducir de tama�o el enemigo (Est�tica)
    public Vector2 MinTam;                        // Tama�o m�ximo que puede llegar a tener el enemigo
    public Vector2 MaxTam;                        // Tama�o m�nimo que puede llegar a tener el enemigo
    public int Action;                             // 0 - ninguna acci�n, 1 - ataque, 2 - habilidad del Slime

    // Start is called before the first frame update
    void Start()
    {
        MinTam = new Vector2(transform.localScale.x, transform.localScale.y);         // Establece el tama�o m�ximo que puede llegar a tener la posici�n
        MaxTam = new Vector2(transform.localScale.x * 2, transform.localScale.y * 2); // Establece el tama�o m�nimo que puede llegar a tener la posici�n
        Action = 0;                                                                   // Ninguna acci�n

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
                ScaleUpPositionBecauseSelected();                            // Empieza la animaci�n para indicar que el enemjigo puede ser atacado
        }

        UpdateHealthbarPosition();                                                          // Cada frame actualiza la posici�n de la barra de vida por si el personaje se mueve
        ClonHealthbar.GetComponent<Healthbar>().SetHealth(VidaActual);                      // Actualiza constantemente la Healthbar con la vida actual del personaje
        ClonHealthbar.GetComponent<Healthbar>().ChangeText(VidaActual + " / " + VidaTotal); // Actualiza el texto de la Healthbar a "VidaActual / VidaTotal"

        ControlAtributos();                                                                 // Controla cada frame que los valores de los atributos sean correctos
        ControlSlimeAbility();                                                              // Controla la duraci�n de la habilidad del Slime
        ControlUI();
    }

    /****************************************************************************************
     * Funci�n: ScalePositionBecauseSelected                                                *
     * Uso: Modifica el tama�o del enemigo para que vibre al poder ser atacado              *
     * Variables entrada: Nada                                                              *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void ScaleUpPositionBecauseSelected()
    {
        float velocidad = 1.5f;                                                                            // Velocidad a la que variar� el tama�o de la posici�n

        if (Vector2.Distance(transform.localScale, MaxTam) < 0.2f)                                         // Si la posici�n ha alcanzado el tam�o m�ximo, indica que puede empezar a reducirse
            Down = true;                                                                                   // Indica que puede empezar a reducir el tama�o

        if (Vector2.Distance(transform.localScale, MinTam) < 0.2f)                                         // Si la posici�n ha alcanzado el tama�o m�nimo, indica que puede empezar a aumentar
            Down = false;                                                                                  // INdica que puede empezar a aumentar el tama�o

        if (!Down)                                                                                          // Si puede aumentar de tama�o
            transform.localScale = Vector2.Lerp(transform.localScale, MaxTam, velocidad * Time.deltaTime);  // Aumenta el tama�o progresivamente hasta el tama�o m�ximo
        else                                                                                                // Si puede reducir de tama�o
            transform.localScale = Vector2.Lerp(transform.localScale, MinTam, velocidad * Time.deltaTime);  // Reduce el tama�o progresivamentehasta el tama�o m�nimo
    }

    /****************************************************************************************
     * Funci�n: OnMouseOver                                                                 *
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
                    Enemies[i].transform.localScale = Enemies[i].GetComponent<GeneralEnemy>().MinTam; // Dichos enemigos modificados vuelven a su tama�o original despu�s de vibrar
                }
            }
        }

        if (SelectedToAttack && (PlayerAttacking.GetComponent<GeneralPlayer>().Atacando || PlayerAttacking.GetComponent<GeneralPlayer>().Habilidad2))                                                // Si el enemigo puede ser atacado
        {
            Vibrate = false;                                                                                                                            // El resto de posiciones dejan de vibrar
            transform.localScale = MinTam;                                                                                                              // La posici�n vuelve a su tama�o original

            if (PlayerAttacking.GetComponent<GeneralPlayer>().CharacterType != 1)
                PlayerAttacking.GetComponent<Animator>().SetTrigger("ataque");

            if (Action == 1)                                                                  // Si la acci�n es la de atacar
            {
                if (PlayerAttacking.GetComponent<GeneralPlayer>().CharacterType == 1)         // El personaje atacando es un Knight
                    EnemyAttacked(PlayerAttacking.GetComponent<PlayerKnight>().AtaqueActual); // Quita el da�o del atacante a la vida actual del enemigo atacado
                else if (PlayerAttacking.GetComponent<GeneralPlayer>().CharacterType == 2)    // El personaje atacando es un Healer
                    EnemyAttacked(PlayerAttacking.GetComponent<PlayerHealer>().AtaqueActual); // Quita el da�o del atacante a la vida actual del enemigo atacado
                else if (PlayerAttacking.GetComponent<GeneralPlayer>().CharacterType == 3)    // El personaje atacando es un Slime
                    EnemyAttacked(PlayerAttacking.GetComponent<PlayerSlime>().AtaqueActual);  // Quita el da�o del atacante a la vida actual del enemigo atacado
                else                                                                          // El personaje atacando es un Mage
                    EnemyAttacked(PlayerAttacking.GetComponent<PlayerMage>().AtaqueActual);   // Quita el da�o del atacante a la vida actual del enemigo atacado

                PlayerAttacking.GetComponent<GeneralPlayer>().Atacando = false;
            }
            else if (Action == 2)                                                             // Si la acci�n es la habilidad del Slime
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

            SelectedToAttack = false;                                                                    // Hace que esta acci�n s�lo se pueda realizar una vez
            PlayerAttacking.transform.localScale = PlayerAttacking.GetComponent<GeneralPlayer>().MinTam; // Devuelve al atacante a su tama�o original
            transform.localScale = MinTam;                                                               // Devuelve al enemigo a su tama�o original

            PlayerAttacking.GetComponent<GeneralPlayer>().DestroyCharacterInfo();         // Destruye la interfaz de informaci�n del personaje
            _CombatBackground.GetComponent<CombatBackground>().ChangeTurn();              // Tras la acci�n del movimiento, cambia el turno de la partida

            Atacar = true;

            Action = 0;                                                                        // Indica que ya no se realiza ninguna acci�n
            _CombatBackground.GetComponent<CombatBackground>().EnemigoParaAtacar = true;
        }
        UIEstadisticasPersonaje.SetActive(false);
    }

    /****************************************************************************************
     * Funci�n: EnemyAttacked                                                               *
     * Uso: Determina que el tipo de enemigo asociado para quitarle vida                    *
     * Variables entrada: damage - Da�o del atacante hacia el enemigo                       *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void EnemyAttacked(int damage)
    {
        VidaActual -= (damage - ((DefensePercentage() * damage) / 100));
    }

    /****************************************************************************************
     * Funci�n: CreateHealthbar                                                             *
     * Uso: Crea la barra de vida del personaje                                             *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void CreateHealthbar()
    {
        ClonHealthbar = Instantiate(PrefabHealthbar);                                                                                             // Instancia el prefab Healthbar en el clon del prefab Healthbar
        ClonHealthbar.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z); // Lo coloca justo debajo del sprite del enemigo
        ClonHealthbar.GetComponent<Healthbar>().SetMaxHealth(VidaTotal);                                                                          // Inicializa la Healthbar con la vida m�xima del personaje
        ClonHealthbar.GetComponent<Healthbar>().ChangeText(VidaActual + " / " + VidaTotal);                                                       // Cambia el texto de la Healthbar a "VidaActual / VidaTotal"
    }

    /****************************************************************************************
     * Funci�n: UpdateHealthbarPosition                                                     *
     * Uso: Actualiza la posici�n de la barra de vida del personaje                         *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void UpdateHealthbarPosition()
    {
        ClonHealthbar.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z); // Coloca la barra de vida debajo del personaje
    }

    /****************************************************************************************
     * Funci�n: EnemyAtack                                                                  *
     * Uso: Ataque del personaje, de entre las posiciones cercanas selecciona una aleatoria *
     *      que contenga un aliado y lo ataca                                               *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void EnemyAtack()
    {
        GameObject position;       // Posici�n a la que atacar� el personaje
        int damage = AtaqueActual; // El da�o es el ataque actual del enemigo
        bool attack;

        if (Atacar)
        {
            do
            {
                attack = false;
                position = Positions[Random.Range(0, Positions.Length)];                                                       // Selecciona una posici�n aleatoria de las accesibles desde la posici�n del enemigo

                if (position.GetComponent<CombatPosition>().CharacterType == 2)                                                 // Si la posici�n est�n ocuapada por un personaje del Jugador
                {
                    attack = true;
                    if (position.GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterType == 1)    // Si la posici�n est� ocupada por un Knight
                        if (position.GetComponent<CombatPosition>().Character.GetComponent<PlayerKnight>().Invencible == true) // Si est� en modo invencible
                            attack = false;                                                                                    // No puede atacar esa posici�n
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

        //GameObject position;       // Posici�n a la que atacar� el personaje
        //int damage = AtaqueActual; // El da�o es el ataque actual del enemigo
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
        //            index = Random.Range(0, Positions.Length);                                                       // Selecciona una posici�n aleatoria de las accesibles desde la posici�n del enemigo
        //            for (i = 0; i < indexNotValid.Length; i++)
        //            {
        //                if (index == (indexNotValid[i] - 1))
        //                    repeatIndex = false;
        //            }
        //        } while (!repeatIndex);

        //        position = Positions[index];
        //        if (position.GetComponent<CombatPosition>().CharacterType == 2)                                                 // Si la posici�n est�n ocuapada por un personaje del Jugador
        //        {
        //            attack = true;
        //            if (position.GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterType == 1)    // Si la posici�n est� ocupada por un Knight
        //                if (position.GetComponent<CombatPosition>().Character.GetComponent<PlayerKnight>().Invencible == true) // Si est� en modo invencible
        //                    attack = false;                                                                                    // No puede atacar esa posici�n
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

        //    } while (!attack && !noPositions);                                                                                  // Repite hasta que la posci�n seleccionada est� ocupada por un personaje del Jugador

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
     * Funci�n: DefensePercentage                                                           *
     * Uso: Establece el porcentage de defensa del enemigo en funci�n de su defensa actual  *
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
     * Funci�n: ControlAtributes                                                            *
     * Uso: Controla que si alg�n atributo del personaje es negativo, pasa a ser 0. Y si    *
     *      alg�n atributo es mayor que su m�ximo, pasa a ser su m�ximo                     *
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
     * Funci�n: ControlSlimeAbility                                                         *
     * Uso: Controla el tiempo que dura la habilidad del slime                              *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void ControlSlimeAbility()
    {
        if (HabilidadSlime == true)
        {
            if (_CombatBackground.GetComponent<CombatBackground>().ContHabilidadSlime >= 3) // Si ya han pasado dos o m�s turnos
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
