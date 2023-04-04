using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleButtonAction : MonoBehaviour
{
    public bool[] AliadosPositionStatus; // Array de booleanos para comprobar que todos los personajes del Jugador �st�n colocados en el mapa de combate
    public bool Activated = false;       // Booleano que controla si el bot�n ha sido activado cuando todos los personajes del Jugador estaban colocados en el mapa de combate

    /****************************************************************************************
     * Funci�n: OnClicked                                                                   *
     * Uso: "Activa" el bot�n si este es clicado                                            *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void OnClicked()
    {
        if (CheckAliadosPosition()) // Si al hacer click todos los personajes del Jugador est�n colocados en el mapa de combate
            Activated = true;       // Activa el bot�n
        else                        // Si al hacer click no todos los personajes del Jugador est�n colocados en el mapa de combate
            Activated = false;      // Sigue sin activar el bot�n, sin esto si di�semos al bot�n sin estar todos colocados no se activar�a,
                                    // pero al colocar al �ltimo de los personajes se activar�a s�lo sin tener que volver a dar a l bot�n
    }

    /****************************************************************************************
     * Funci�n: SetAliadosPositionStatus                                                    *
     * Uso: Obtiene el array de booleanos que indica si todos los personajes del Jugador    *
     *      est�n colocados en el mapa de combate                                           *
     * Variables entrada: aliadoPositionStatus - Array de booleanos que indica si todos los *
     *                                           personajes del Jugador est�n colocados en  *
     *                                           el mapa de batalla                         *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void SetAliadosPositionStatus(bool[] aliadoPositionStatus)
    {
        AliadosPositionStatus = aliadoPositionStatus; // Guarda en la variable local el array de booleanos
    }

    /****************************************************************************************
     * Funci�n: CheckAliadosPosition                                                        *
     * Uso: Comprueba que todos los personajes del Jugador est�n colocados en el mapa de    *
     *      combate                                                                         *
     * Variables entrada: Nada                                                              *
     * Return: True - Si todos los personajes est�n colocados en el mapa de combate         *
     *         False - Si no todos los personajes est�n colocados en el mapa de combate     *
     ****************************************************************************************/
    private bool CheckAliadosPosition()
    {
        int contador = 0;                                      // Inicializa el contador a 0

        for (int i = 0; i < AliadosPositionStatus.Length; i++) // Recorre el array de booleanos
        {
            if (AliadosPositionStatus[i] == true)              // Si el personaje del Jugador est� colocado en el mapa de combate
                contador++;                                    // +1 al contador
        }

        if (contador == 4)                                     // Si los 4 personajes est�n colocados en el mapa de combate
            return true;                                       // Devuelve true
        else                                                   // Si no est�n los 4 colocados
            return false;                                      // Devuelve false
    }
}
