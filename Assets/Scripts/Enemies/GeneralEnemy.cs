using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GeneralEnemy : MonoBehaviour
{
    public GameObject Enemy;             // Enemigo asociado
    public GameObject _CombatBackground; // Combate

    public int Index;                // Índice que determina el tipo de enemigo que es
    public bool Atacar = false;      // Indica si se puede atacar o no
    public GameObject EnemyPosition; // Posición del enemigo

    public GameObject[] Enemies; // Array de enemigos

    public bool SelectedToAttack = false;          // Booleano que indica si el enemigo es seleccionable para ser atacado
    public bool Down = false;                      // Blooleano que indica cuando puede empezar a reducir de tamaño el enemigo (Estética)
    public bool Vibrate = false;                   // Booleano que controla si el enemigo seleccionable para ser atacado puede vibrar o no (Estética)
    public Vector2 MinTam;                        // Tamaño máximo que puede llegar a tener el enemigo
    public Vector2 MaxTam;                        // Tamaño mínimo que puede llegar a tener el enemigo

    public GameObject PlayerAttacking;             // Tipo de personaje del Jugador atacando este enemigo

    public int Action;                             // 0 - ninguna acción, 1 - ataque, 2 - habilidad del Slime

    public GameObject UIEstadisticasPersonaje;

    // Start is called before the first frame update
    void Start()
    {
        MinTam = new Vector2(transform.localScale.x, transform.localScale.y);         // Establece el tamaño máximo que puede llegar a tener la posición
        MaxTam = new Vector2(transform.localScale.x * 2, transform.localScale.y * 2); // Establece el tamaño mínimo que puede llegar a tener la posición
        Action = 0;                                                                   // Ninguna acción
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedToAttack && (PlayerAttacking.GetComponent<GeneralPlayer>().Atacando || PlayerAttacking.GetComponent<GeneralPlayer>().Habilidad2))                                                // Si el enemigo puede ser atacado
        {
            if (Vibrate)                                                     // Si vibrar es true
                ScaleUpPositionBecauseSelected();                            // Empieza la animación para indicar que el enemjigo puede ser atacado

            //OnMouseOver();                                                   // Cuando se hace click en el enemigo, este es atacado
        }
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

        if (SelectedToAttack)
        {

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
        }

        if (SelectedToAttack && (PlayerAttacking.GetComponent<GeneralPlayer>().Atacando || PlayerAttacking.GetComponent<GeneralPlayer>().Habilidad2))                                                // Si el enemigo puede ser atacado
        {
            Vibrate = false;                                                                                                                            // El resto de posiciones dejan de vibrar
            transform.localScale = MinTam;                                                                                                              // La posición vuelve a su tamaño original

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

                PlayerAttacking.GetComponent<PlayerSlime>().UsedAbility = true;
                PlayerAttacking.GetComponent<GeneralPlayer>().Habilidad2 = false;
            }

            SelectedToAttack = false;                                                                    // Hace que esta acción sólo se pueda realizar una vez
            PlayerAttacking.transform.localScale = PlayerAttacking.GetComponent<GeneralPlayer>().MinTam; // Devuelve al atacante a su tamaño original
            transform.localScale = MinTam;                                                               // Devuelve al enemigo a su tamaño original

            PlayerAttacking.GetComponent<GeneralPlayer>().DestroyCharacterInfo();         // Destruye la interfaz de información del personaje
            _CombatBackground.GetComponent<CombatBackground>().ChangeTurn();              // Tras la acción del movimiento, cambia el turno de la partida

            for (i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i] != null)
                {
                    if (!VariablesGlobales.instance.Boss)
                        Enemies[i].GetComponent<GeneralEnemy>().Atacar = true;
                    else
                        Enemies[i].GetComponent<Boss>().Atacar = true;
                }
            }

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
        if (Index == 1)      // El enemigo es un Knight
            Enemy.GetComponent<EnemyKnight>().VidaActual -= (damage - ((Enemy.GetComponent<EnemyKnight>().DefensePercentage() * damage) / 100));
        else if (Index == 2) // El enemigo es un Healer
            Enemy.GetComponent<EnemyHealer>().VidaActual -= (damage - ((Enemy.GetComponent<EnemyHealer>().DefensePercentage() * damage) / 100));
        else if (Index == 3) // El enemigo es un Slime
            Enemy.GetComponent<EnemySlime>().VidaActual -= (damage - ((Enemy.GetComponent<EnemySlime>().DefensePercentage() * damage) / 100));
        else                 // El enemigo es un Mage
            Enemy.GetComponent<EnemyMage>().VidaActual -= (damage - ((Enemy.GetComponent<EnemyMage>().DefensePercentage() * damage) / 100));
    }
}
