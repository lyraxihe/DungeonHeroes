using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButtonAction : MonoBehaviour
{
    public GameObject _CombatBackground; // Combate
    public GameObject[] Positions;       // Array de posiciones del combate
    public GameObject CharacterPosition; // Posici�n actual del personaje
    public GameObject Character;         // Personaje asociado

    public GameObject UIMover;

    public int[] PositionsToMove;        // Array de posiciones conectadas a la posici�n actual del personaje

    public void Start()
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
        GameObject[] Enemies = _CombatBackground.GetComponent<CombatBackground>().Enemies;
        GameObject[] Aliados = _CombatBackground.GetComponent<CombatBackground>().Aliados;

        // TEXTO EXPLICACI�N
        /************************************************************************************************************************/
        _CombatBackground.GetComponent<CombatBackground>().ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeText("Selecciona una posicion a la que moverte");
        /************************************************************************************************************************/

        for (int i = 0; i < PositionsToMove.Length; i++) // Recorre el array de posiciones conectadas a la posici�n actual del personaje
        {
            Character.GetComponent<GeneralPlayer>().Moviendo = true;                                  // Indica que el personaje se est� moviendo
            Character.GetComponent<GeneralPlayer>().Atacando = false;                                 // Indica que el personaje deja de atacar
            for (int j = 0; j < _CombatBackground.GetComponent<CombatBackground>().Enemies.Length; j++)
                if (_CombatBackground.GetComponent<CombatBackground>().Enemies[j] != null)
                {
                    if (!VariablesGlobales.instance.Boss)
                    {
                        _CombatBackground.GetComponent<CombatBackground>().Enemies[j].GetComponent<GeneralEnemy>().transform.localScale = _CombatBackground.GetComponent<CombatBackground>().Enemies[j].GetComponent<GeneralEnemy>().MinTam;
                    }
                    else
                    {
                        _CombatBackground.GetComponent<CombatBackground>().Enemies[j].GetComponent<Boss>().transform.localScale = _CombatBackground.GetComponent<CombatBackground>().Enemies[j].GetComponent<Boss>().MinTam;
                    }
                }
            Character.GetComponent<GeneralPlayer>().Habilidad2 = false;                               // Indica que el personaje deja de usar su habilidad
            for (int k = 0; k < _CombatBackground.GetComponent<CombatBackground>().Aliados.Length; k++)
            {
                if (_CombatBackground.GetComponent<CombatBackground>().Aliados[k] != null)
                {
                    if (_CombatBackground.GetComponent<CombatBackground>().Aliados[k] != Character)
                        _CombatBackground.GetComponent<CombatBackground>().Aliados[k].GetComponent<GeneralPlayer>().transform.localScale = _CombatBackground.GetComponent<CombatBackground>().Aliados[k].GetComponent<GeneralPlayer>().MinTam;
                }
            }

            if (Positions[PositionsToMove[i]].GetComponent<CombatPosition>().Occupied == false)           // Si la posici�n no est� ocupada
            {
                Positions[PositionsToMove[i]].GetComponent<CombatPosition>().SelectedToMove = true;       // Habilita la posici�n para que el personaje se pueda mover a ella
                Positions[PositionsToMove[i]].GetComponent<CombatPosition>().Vibrate = true;              // Habilita que la posici�n vibre (Est�tica)
                Positions[PositionsToMove[i]].GetComponent<CombatPosition>().CharacterToMove = Character; // Almacena el personaje que va a moverse a dicha posici�n   
            }
        }
    }

    public void OnMouseEnter()
    {
        UIMover.SetActive(true);
    }

    public void OnMouseExit()
    {
        UIMover.SetActive(false);
    }
}
