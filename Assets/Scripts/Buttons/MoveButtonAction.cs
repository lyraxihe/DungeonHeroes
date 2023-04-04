using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButtonAction : MonoBehaviour
{
    public GameObject[] Positions;       // Array de posiciones del combate
    public GameObject CharacterPosition; // Posici�n actual del personaje
    public GameObject Character;         // Personaje asociado

    public int[] PositionsToMove;        // Array de posiciones conectadas a la posici�n actual del personaje

    private void Start()
    {
        PositionsToMove = CharacterPosition.GetComponent<CombatPosition>().PositionsToMove; // Alamcena el array de posiciones conectadas a la posici�n actual del personaje
    }

    /****************************************************************************************
     * Funci�n: OnClicked                                                                   *
     * Uso: Indica las posiciones a las que un personaje se puede mover al pulasar el bot�n *
     * Variables entrada: Nada                                                              *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void OnClicked()
    {
        for (int i = 0; i < PositionsToMove.Length; i++) // Recorre el array de posiciones conectadas a la posici�n actual del personaje
        {
            if (Positions[PositionsToMove[i]].GetComponent<CombatPosition>().Occupied == false)           // Si la posici�n no est� ocupada
            {
                Positions[PositionsToMove[i]].GetComponent<CombatPosition>().SelectedToMove = true;       // Habilita la posici�n para que el personaje se pueda mover a ella
                Positions[PositionsToMove[i]].GetComponent<CombatPosition>().Vibrate = true;              // Habilita que la posici�n vibre (Est�tica)
                Positions[PositionsToMove[i]].GetComponent<CombatPosition>().CharacterToMove = Character; // Almacena el personaje que va a moverse a dicha posici�n
            }
        }
    }
}
