using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GeneralEnemy : MonoBehaviour
{
    public GameObject Enemy;             // Enemigo asociado
    public GameObject _CombatBackground; // Combate

    public int Index;                // �ndice que determina el tipo de enemigo que es
    public bool Atacar = false;      // Indica si se puede atacar o no
    public GameObject EnemyPosition; // Posici�n del enemigo

    public GameObject[] Enemies; // Array de enemigos

    public bool SelectedToAttack = false;          // Booleano que indica si el enemigo es seleccionable para ser atacado
    public bool Down = false;                      // Blooleano que indica cuando puede empezar a reducir de tama�o el enemigo (Est�tica)
    public bool Vibrate = false;                   // Booleano que controla si el enemigo seleccionable para ser atacado puede vibrar o no (Est�tica)
    public Vector2 MinTam;                        // Tama�o m�ximo que puede llegar a tener el enemigo
    public Vector2 MaxTam;                        // Tama�o m�nimo que puede llegar a tener el enemigo

    public GameObject PlayerAttacking;             // Tipo de personaje del Jugador atacando este enemigo

    public int Action;                             // 0 - ninguna acci�n, 1 - ataque, 2 - habilidad del Slime

    public GameObject UIEstadisticasPersonaje;

    // Start is called before the first frame update
    void Start()
    {
        MinTam = new Vector2(transform.localScale.x, transform.localScale.y);         // Establece el tama�o m�ximo que puede llegar a tener la posici�n
        MaxTam = new Vector2(transform.localScale.x * 2, transform.localScale.y * 2); // Establece el tama�o m�nimo que puede llegar a tener la posici�n
        Action = 0;                                                                   // Ninguna acci�n
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedToAttack && (PlayerAttacking.GetComponent<GeneralPlayer>().Atacando || PlayerAttacking.GetComponent<GeneralPlayer>().Habilidad2))                                                // Si el enemigo puede ser atacado
        {
            if (Vibrate)                                                     // Si vibrar es true
                ScaleUpPositionBecauseSelected();                            // Empieza la animaci�n para indicar que el enemjigo puede ser atacado

            //OnMouseOver();                                                   // Cuando se hace click en el enemigo, este es atacado
        }
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

        if (SelectedToAttack)
        {

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
        }

        if (SelectedToAttack && (PlayerAttacking.GetComponent<GeneralPlayer>().Atacando || PlayerAttacking.GetComponent<GeneralPlayer>().Habilidad2))                                                // Si el enemigo puede ser atacado
        {
            Vibrate = false;                                                                                                                            // El resto de posiciones dejan de vibrar
            transform.localScale = MinTam;                                                                                                              // La posici�n vuelve a su tama�o original

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

            SelectedToAttack = false;                                                                    // Hace que esta acci�n s�lo se pueda realizar una vez
            PlayerAttacking.transform.localScale = PlayerAttacking.GetComponent<GeneralPlayer>().MinTam; // Devuelve al atacante a su tama�o original
            transform.localScale = MinTam;                                                               // Devuelve al enemigo a su tama�o original

            PlayerAttacking.GetComponent<GeneralPlayer>().DestroyCharacterInfo();         // Destruye la interfaz de informaci�n del personaje
            _CombatBackground.GetComponent<CombatBackground>().ChangeTurn();              // Tras la acci�n del movimiento, cambia el turno de la partida

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
