using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Ability1ButtonAction : MonoBehaviour
{
    public GameObject _CombatBackground; // Combate
    public GameObject[] Positions;       // Array de posiciones del combate
    public GameObject[] Enemies;         // Array de enemigos del combate
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
        // TEXTO EXPLICACIÓN
        /************************************************************************************************************************/
        _CombatBackground.GetComponent<CombatBackground>().ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeText("Selecciona un enemigo al que atacar");
        /************************************************************************************************************************/

        if (Character.GetComponent<GeneralPlayer>().CharacterType == 4) // Si el personaje seleccionado es el mago no habrá rango de ataque
        {
            for(int i = 0; i < Enemies.Length; i++)                    // Blucle para que, al no tener rango de ataque, todos los enemigos del combate sean seleccionados para ser atacados
            {
                if (Enemies[i] != null)
                {
                    Enemies[i].GetComponent<GeneralEnemy>().Action = 1;                                                               // Indica que la acción es la de atacar
                    Enemies[i].GetComponent<GeneralEnemy>().SelectedToAttack = true;                                                  // Indica que el enemigo puede ser atacado
                    Enemies[i].GetComponent<GeneralEnemy>().Vibrate = true;                                                           // Indica que el enemigo puede vibrar
                    Enemies[i].GetComponent<GeneralEnemy>().PlayerAttacking = Character.GetComponent<GeneralPlayer>().Character;      // Indica el personaje del Jugador que le puede atacar
                }
            }
        }
        else                                                           // Para el resto de personajes que tienen rango de ataque
        {
            for(int i = 0; i < PositionsToAttack.Length; i++)          // Recorre el array de posiciones conectadas a la posición del personaje seleccionado
            {
                if (Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().CharacterType == 1)
                {
                    Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<GeneralEnemy>().Action = 1;                                                          // Indica que la acción es la de atacar
                    Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<GeneralEnemy>().SelectedToAttack = true;                                             // Indica que el enemigo puede ser atacado
                    Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<GeneralEnemy>().Vibrate = true;                                                      // Indica que el enemigo puede vibrar
                    Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<GeneralEnemy>().PlayerAttacking = Character.GetComponent<GeneralPlayer>().Character; // Indica el personaje del Jugador que le puede atacar
                }
            }
        }
    }
}
