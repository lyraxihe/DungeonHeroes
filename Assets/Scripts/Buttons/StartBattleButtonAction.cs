using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleButtonAction : MonoBehaviour
{
    public bool[] AliadosPositionStatus; // Array de booleanos para comprobar que todos los personajes del Jugador éstán colocados en el mapa de combate
    public bool Activated = false;       // Booleano que controla si el botón ha sido activado cuando todos los personajes del Jugador estaban colocados en el mapa de combate

    /****************************************************************************************
     * Función: OnClicked                                                                   *
     * Uso: "Activa" el botón si este es clicado                                            *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void OnClicked()
    {
        if (CheckAliadosPosition()) // Si al hacer click todos los personajes del Jugador están colocados en el mapa de combate
            Activated = true;       // Activa el botón
        else                        // Si al hacer click no todos los personajes del Jugador están colocados en el mapa de combate
            Activated = false;      // Sigue sin activar el botón, sin esto si diésemos al botón sin estar todos colocados no se activaría,
                                    // pero al colocar al último de los personajes se activaría sólo sin tener que volver a dar a l botón
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
}
