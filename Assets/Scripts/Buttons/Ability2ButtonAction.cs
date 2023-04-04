using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability2ButtonAction : MonoBehaviour
{
    public GameObject[] Positions;       // Array de posiciones del combate
    public GameObject[] Enemies;         // Array de enemigos del combate
    public GameObject[] Aliados;         // Array de aliados del combate
    public GameObject CharacterPosition; // Posición actual del personaje
    public GameObject Character;         // Personaje asociado

    public int[] PositionsToAttack;        // Array de posiciones conectadas a la posición actual del personaje

    // Start is called before the first frame update
    private void Start()
    {
        PositionsToAttack = CharacterPosition.GetComponent<CombatPosition>().PositionsToMove; // Alamcena el array de posiciones conectadas a la posición actual del personaje
    }

    /****************************************************************************************
     * Función: OnClicked                                                                   *
     * Uso: Indica los enemigos a los que un personaje se puede atacar al pulasar el botón  *
     * Variables entrada: Nada                                                              *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void OnClicked()
    {
        if (Character.GetComponent<GeneralPlayer>().CharacterType == 1)     // Si el personaje es un Knight
        {
            if (Character.GetComponent<PlayerKnight>().Invencible != true)
            {
                Character.GetComponent<PlayerKnight>().Invencible = true;              // Activa la invulnerabilidad durante 3 turnos
                Character.GetComponent<PlayerKnight>().PlayerKnightInvencibleCont = 0; // Inicia el contador

                Character.transform.localScale = Character.GetComponent<GeneralPlayer>().MinTam;                         // Devuelve al personaje a su tamaño original
                Character.GetComponent<GeneralPlayer>().DestroyCharacterInfo();                                          // Destruye la interfaz de información del personaje
                Character.GetComponent<GeneralPlayer>()._CombatBackground.GetComponent<CombatBackground>().ChangeTurn(); // Tras la acción, cambia el turno de la partida
            }
        }
        else if (Character.GetComponent<GeneralPlayer>().CharacterType == 2)                                                         // Si el personaje es un Healer
        {
            if (Character.GetComponent<PlayerHealer>().UsedAbility != true)                                                          // Si puede usar su habilidad
            {
                for (int i = 0; i < Aliados.Length; i++)                                                                             // Bucle para recorrer los aliados del combate
                {
                    Aliados[i].GetComponent<GeneralPlayer>().Action = 1;                                                             // Indica que la acción es la habilidad del Slime
                    Aliados[i].GetComponent<GeneralPlayer>().Selected = true;                                                        // Indica que el personaje puede ser atacado
                    Aliados[i].GetComponent<GeneralPlayer>().Vibrate = true;                                                         // Indica que el personaje puede vibrar
                    Aliados[i].GetComponent<GeneralPlayer>().PlayerUsingAbility = Character.GetComponent<GeneralPlayer>().Character; // Indica el personaje del Jugador que le ha seleccionado
                }
            }
        }
        else if(Character.GetComponent<GeneralPlayer>().CharacterType == 3) // Si el personaje es un Slime
        {
            if (Character.GetComponent<PlayerSlime>().UsedAbility != true)
            {
                for (int i = 0; i < Enemies.Length; i++)                    // Blucle que hace seleccionables a todos los enemigos del combate
                {
                    if (Enemies[i] != null)
                    {
                        Enemies[i].GetComponent<GeneralEnemy>().Action = 2;                                                              // Indica que la acción es la habilidad del Slime
                        Enemies[i].GetComponent<GeneralEnemy>().SelectedToAttack = true;                                                 // Indica que el enemigo puede ser atacado
                        Enemies[i].GetComponent<GeneralEnemy>().Vibrate = true;                                                          // Indica que el enemigo puede vibrar
                        Enemies[i].GetComponent<GeneralEnemy>().PlayerAttacking = Character.GetComponent<GeneralPlayer>().Character;     // Indica el personaje del Jugador que le puede atacar
                    }
                }
            }
        }
        else                                                                                                                         // Si el personaje es un Mage
        {
            if (Character.GetComponent<PlayerMage>().UsedAbility != true)                                                          // Si puede usar su habilidad
            {
                for (int i = 0; i < Aliados.Length; i++)                                                                             // Bucle para recorrer los aliados del combate
                {
                    Aliados[i].GetComponent<GeneralPlayer>().Action = 2;                                                             // Indica que la acción es la habilidad del Slime
                    Aliados[i].GetComponent<GeneralPlayer>().Selected = true;                                                        // Indica que el personaje puede ser atacado
                    Aliados[i].GetComponent<GeneralPlayer>().Vibrate = true;                                                         // Indica que el personaje puede vibrar
                    Aliados[i].GetComponent<GeneralPlayer>().PlayerUsingAbility = Character.GetComponent<GeneralPlayer>().Character; // Indica el personaje del Jugador que le ha seleccionado
                }
            }
        }
    }
}
