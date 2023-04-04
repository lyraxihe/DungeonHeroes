using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartBattleButton : MonoBehaviour
{
    public Button _Button;                // Botón asociado al Canva "StartBattleButton"
    public GameObject Border;             // Borde del botón (estética)
    public bool[] AliadosPositionStatus;  // Array de booleanos para comprobar que todos los personajes del Jugador éstán colocados en el mapa de combate
    public bool StarBattleStatus = false; // Booleano que indica si se puede comenzar el combate y ha terminado la fase de colocación

    private void Update()
    {
        ButtonStatus();                    // Actualiza el estado del botón
        StartBattle();                     // Cada frame comprueba si puede comenzar el combate
        _Button.GetComponent<StartBattleButtonAction>().SetAliadosPositionStatus(AliadosPositionStatus); // Cada frame obtiene el array de booleanos que indica si todos los personajes del Jugador están colocados en el mapa
    }

    /****************************************************************************************
     * Función: StartBattle                                                                 *
     * Uso: Cuando el botón ¡Comenzar Batalla! es pulsado estando todos los personajes del  *
     *      Jugador colocados en el campo de batalla, cambia el booleano StartBattle para   *
     *      que el script del combate lo sepa que el botón ha sido pulsado                  *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void StartBattle()
    {
        if (CheckAliadosPosition() && _Button.GetComponent<StartBattleButtonAction>().Activated) // Si los 4 personajes del Jugador están colocados en el mapa de combate y el botón ha sido clicado
            StarBattleStatus = true;                                                             // Indica que puede comenzar el combate
    }

    /****************************************************************************************
     * Función: ButtonStatus                                                                *
     * Uso: Actualiza las características del botón                                         *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void ButtonStatus()
    {
        if (CheckAliadosPosition())                                                      // Si los 4 personajes del Jugador están colocados en el mapa de combate
        {
            _Button.GetComponent<Image>().color = new Color(0.66f, 0, 0);                // Cambia el color del botón a un rojo oscuro
            _Button.GetComponent<Button>().transition = Selectable.Transition.ColorTint; // Cambia la característica para que el botón se sombree al pulsarlo
            Border.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);            // Cambia el color del borde del botón a un rojo puro
        }
    }

    /****************************************************************************************
     * Función: CheckAliadosPosition                                                        *
     * Uso: Comprueba que todos los personajes del Jugador están colocados en el mapa de    *
     *      combate                                                                         *
     * Variables entrada: Nada                                                              *
     * Return: True - Si todos los personajes están colocados en el mapa de combate         *
     *         False - Si no todos los personajes están colocados en el mapa de combate     *
     ****************************************************************************************/
    private bool CheckAliadosPosition()
    {
        int contador = 0;                                      // Inicializa el contador a 0

        for (int i = 0; i < AliadosPositionStatus.Length; i++) // Recorre el array de booleanos
        {
            if (AliadosPositionStatus[i] == true)              // Si el personaje del Jugador está colocado en el mapa de combate
                contador++;                                    // +1 al contador
        }

        if (contador == 4)                                     // Si los 4 personajes están colocados en el mapa de combate
            return true;                                       // Devuelve true
        else                                                   // Si no están los 4 colocados
            return false;                                      // Devuelve false
    }

    /****************************************************************************************
     * Función: SetAliadosPositionStatus                                                    *
     * Uso: Obtiene el array de booleanos que indica si todos los personajes del Jugador    *
     *      están colocados en el mapa de combate                                           *
     * Variables entrada: aliadoPositionStatus - Array de booleanos que indica si todos los *
     *                                           personajes del Jugador están colocados en  *
     *                                           el mapa de batalla                         *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void SetAliadosPositionStatus(bool[] aliadoPositionStatus)
    {
        AliadosPositionStatus = aliadoPositionStatus; // Guarda en la variable local el array de booleanos
    }

    /****************************************************************************************
     * Función: GetStartBattleStatus                                                        *
     * Uso: Envía el booleano que indica si el combate puede comenzar                       *
     * Variables entrada: Nada                                                              *
     * Return: StarBattleStatus(true) - Si puede comenzar el combate                        *
     *         StarBattleStatus(false) - Si puede comenzar el combate                       *
     ****************************************************************************************/
    public bool GetStartBattleStatus()
    {
        return StarBattleStatus;
    }
}
