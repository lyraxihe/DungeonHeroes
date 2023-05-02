using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyKnight : MonoBehaviour
{
    public GameObject _CombatBackground; // Combate

    public GameObject EnemyPosition; // Posición del enemigo
    public GameObject[] Positions;   // Array de posiciones del combate

    // ESTADÍSTICAS DEL PERSONAJE
    public int VidaTotal;     // Vida máxima del personaje
    public int VidaActual;    // Vida actual del personaje
    public int AtaqueActual;  // Ataque actual del personaje
    public int AtaqueMax;     // Ataque máximo del personaje
    public int DefensaActual; // Defensa actual del personaje
    public int DefensaMax;    // Defensa máxima del personaje
    public int DefensaActualPercentage; // Porcentaje de defensa del personaje

    public GameObject PrefabHealthbar; // Prefab Healthbar
    public GameObject ClonHealthbar;   // Clon del prefab Healthbar

    public bool HabilidadSlime;    // Booleano para controlar si la habilidad del Slime está activa sobre este personaje

    //UI ENEMIGOS
    public GameObject UIEnemigo;
    public TMP_Text VidaEnemigo;
    public TMP_Text AtaqueEnemigo;
    public TMP_Text DefensaEnemigo;

    // Start is called before the first frame update
    void Start()
    {
        // Establece los atributos del personaje
        VidaTotal = VariablesGlobales.instance.KnightVidaTotal;
        VidaActual = VariablesGlobales.instance.KnightVidaActual;
        AtaqueActual = VariablesGlobales.instance.KnightAtaqueActual;
        AtaqueMax = VariablesGlobales.instance.KnightAtaqueMax;
        DefensaActual = VariablesGlobales.instance.KnightDefensaActual;
        DefensaMax = VariablesGlobales.instance.KnightDefensaMax;
        DefensaActualPercentage = VariablesGlobales.instance.KnightDefensaActualPercentage;

        HabilidadSlime = false;

        CreateHealthbar();                    // Crea la barra de vida del personaje
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthbarPosition();                                                          // Cada frame actualiza la posición de la barra de vida por si el personaje se mueve
        ClonHealthbar.GetComponent<Healthbar>().SetHealth(VidaActual);                      // Actualiza constantemente la Healthbar con la vida actual del personaje
        ClonHealthbar.GetComponent<Healthbar>().ChangeText(VidaActual + " / " + VidaTotal); // Actualiza el texto de la Healthbar a "VidaActual / VidaTotal"

        ControlAtributos();                                                                 // Controla cada frame que los valores de los atributos sean correctos
        ControlSlimeAbility();                                                              // Controla la duración de la habilidad del Slime
        ControlUI();
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
        int index;
        int[] indexNotValid = new int[4];
        bool repeatIndex;
        bool noPositions;
        bool fail = false;
        int i;
        int cont;

        if (GetComponent<GeneralEnemy>().Atacar)
        {
            do
            {
                attack = false;
                noPositions = false;
                cont = 0;

                do
                {
                    repeatIndex = true;
                    index = EnemyPosition.GetComponent<CombatPosition>().PositionsToMove[Random.Range(0, EnemyPosition.GetComponent<CombatPosition>().PositionsToMove.Length)];
                    for (i = 0; i < indexNotValid.Length; i++)
                    {
                        if (index == (indexNotValid[i] - 1))
                            repeatIndex = false;
                    }

                    for(i = 0; i < indexNotValid.Length; i++)
                    {
                        if (indexNotValid[i] != 0)
                            cont++;
                    }

                    if (cont == 4)
                        fail = true;
                } while (!repeatIndex && !fail);

                position = Positions[index];                           // Selecciona una posición aleatoria de las accesibles desde la posición del enemigo

                if (position.GetComponent<CombatPosition>().CharacterType == 2) // Si la posición están ocuapada por un personaje del Jugador
                {
                    attack = true;
                    if (position.GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterType == 1)    // Si la posición está ocupada por un Knight
                        if (position.GetComponent<CombatPosition>().Character.GetComponent<PlayerKnight>().Invencible == true) // Si está en modo invencible
                        {
                            for (i = 0; i < 4; i++)
                            {
                                if (indexNotValid[i] == 0)
                                {
                                    indexNotValid[i] = index + 1;
                                    break;
                                }
                            }

                            if (i == 3)
                                noPositions = true;

                            attack = false;                                                                                    // No puede atacar esa posición
                        }
                }
                else
                {
                    for (i = 0; i < 4; i++)
                    {
                        if (indexNotValid[i] == 0)
                        {
                            indexNotValid[i] = index + 1;
                            break;
                        }
                    }

                    if (i == 3)
                        noPositions = true;
                }

            } while (!attack && !noPositions && !fail);                                                                                 // Repite hasta que la posción seleccionada esté ocupada por un personaje del Jugador

            if (!noPositions && !fail)
            {
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
                else                                                                                                                    // Si el objetivo es un Mage
                {
                    position.GetComponent<CombatPosition>().Character.GetComponent<Animator>().SetTrigger("danho");
                    position.GetComponent<CombatPosition>().Character.GetComponent<PlayerMage>().VidaActual -= (damage - ((position.GetComponent<CombatPosition>().Character.GetComponent<PlayerMage>().DefensePercentage() * damage) / 100));
                }
            }

            GetComponent<GeneralEnemy>().Atacar = false; // Indica que el enemigo no puede volver a atacar
        }
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
        if(VidaActual > VidaTotal)
            VidaActual = VidaTotal;

        if (AtaqueActual < 0)
            AtaqueActual = 0;
        if(AtaqueActual > AtaqueMax)
            AtaqueActual = AtaqueMax;

        if (DefensaActual < 0)
            DefensaActual = 0;
        if(DefensaActual > DefensaMax)
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
        if(HabilidadSlime == true)
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

        if (AtaqueActual < VariablesGlobales.instance.KnightAtaqueActual)
        {
            AtaqueEnemigo.text = "<color=red>" + AtaqueActual + "</color> / " + AtaqueMax;
        }
        else if (AtaqueActual > VariablesGlobales.instance.KnightAtaqueActual)
        {
            AtaqueEnemigo.text = "<color=green>" + AtaqueActual + "</color> / " + AtaqueMax;
        }
        else
        {
            AtaqueEnemigo.text = AtaqueActual + " / " + AtaqueMax;
        }
        
        if(DefensaActual < VariablesGlobales.instance.KnightDefensaActual)
        {
            DefensaEnemigo.text = "<color=red>" + DefensaActualPercentage + "%</color> / 50%";
        }
        else if (DefensaActual > VariablesGlobales.instance.KnightDefensaActual)
        {
            DefensaEnemigo.text = "<color=green>" + DefensaActualPercentage + "%</color> / 50%";
        }
        else
        {
            DefensaEnemigo.text = DefensaActualPercentage + "% / 50%";
        }
    }
}
