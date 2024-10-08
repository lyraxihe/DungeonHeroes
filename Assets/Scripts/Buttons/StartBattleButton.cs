using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartBattleButton : MonoBehaviour
{
    public Button _Button;                // Bot�n asociado al Canva "StartBattleButton"
    public GameObject Border;             // Borde del bot�n (est�tica)
    public bool[] AliadosPositionStatus;  // Array de booleanos para comprobar que todos los personajes del Jugador �st�n colocados en el mapa de combate
    public bool StarBattleStatus = false; // Booleano que indica si se puede comenzar el combate y ha terminado la fase de colocaci�n
    public GameObject[] Enemies;

    //UI ENEMIGOS
    public GameObject UIEnemigo1;
    public GameObject UIEnemigo2;
    public GameObject UIEnemigo3;
    public GameObject UIEnemigo4;
    public int NumEnemigos;

    public GameObject BorderUIEnemigo;
    public GameObject ContainerUIEnemigo;
    public GameObject UIEnemigoKnight;
    public GameObject UIEnemigoHealer;
    public GameObject UIEnemigoSlime;
    public GameObject UIEnemigoMage;
    public GameObject UIEnemigoBoss;
    public GameObject UIEnemigoCorazon;
    public GameObject UIEnemigoEspada;
    public GameObject UIEnemigoEscudo;

    public GameObject UIEstadisticaVidaEnemy1;
    public GameObject UIEstadisticaAtaqueEnemy1;
    public GameObject UIEstadisticaDefensaEnemy1;
    public GameObject UIEstadisticaVidaEnemy2;
    public GameObject UIEstadisticaAtaqueEnemy2;
    public GameObject UIEstadisticaDefensaEnemy2;
    public GameObject UIEstadisticaVidaEnemy3;
    public GameObject UIEstadisticaAtaqueEnemy3;
    public GameObject UIEstadisticaDefensaEnemy3;
    public GameObject UIEstadisticaVidaEnemy4;
    public GameObject UIEstadisticaAtaqueEnemy4;
    public GameObject UIEstadisticaDefensaEnemy4;

    public void Start()
    {
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigo1 = UIEnemigo1;
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigo2 = UIEnemigo2;
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigo3 = UIEnemigo3;
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigo4 = UIEnemigo4;
        _Button.GetComponent<StartBattleButtonAction>().NumEnemigos = NumEnemigos;

        _Button.GetComponent<StartBattleButtonAction>().BorderUIEnemigo = BorderUIEnemigo;
        _Button.GetComponent<StartBattleButtonAction>().ContainerUIEnemigo = ContainerUIEnemigo;
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigoKnight = UIEnemigoKnight;
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigoHealer = UIEnemigoHealer;
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigoSlime = UIEnemigoSlime;
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigoMage = UIEnemigoMage;
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigoBoss = UIEnemigoBoss;
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigoCorazon = UIEnemigoCorazon;
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigoEspada = UIEnemigoEspada;
        _Button.GetComponent<StartBattleButtonAction>().UIEnemigoEscudo = UIEnemigoEscudo;

        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaVidaEnemy1 = UIEstadisticaVidaEnemy1;
        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaAtaqueEnemy1 = UIEstadisticaAtaqueEnemy1;
        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaDefensaEnemy1 = UIEstadisticaDefensaEnemy1;
        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaVidaEnemy2 = UIEstadisticaVidaEnemy2;
        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaAtaqueEnemy2 = UIEstadisticaAtaqueEnemy2;
        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaDefensaEnemy2 = UIEstadisticaDefensaEnemy2;
        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaVidaEnemy3 = UIEstadisticaVidaEnemy3;
        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaAtaqueEnemy3 = UIEstadisticaAtaqueEnemy3;
        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaDefensaEnemy3 = UIEstadisticaDefensaEnemy3;
        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaVidaEnemy4 = UIEstadisticaVidaEnemy4;
        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaAtaqueEnemy4 = UIEstadisticaAtaqueEnemy4;
        _Button.GetComponent<StartBattleButtonAction>().UIEstadisticaDefensaEnemy4 = UIEstadisticaDefensaEnemy4;

        _Button.GetComponent<StartBattleButtonAction>().Enemies = Enemies;
    }

    private void Update()
    {
        ButtonStatus();                    // Actualiza el estado del bot�n
        StartBattle();                     // Cada frame comprueba si puede comenzar el combate
        _Button.GetComponent<StartBattleButtonAction>().SetAliadosPositionStatus(AliadosPositionStatus); // Cada frame obtiene el array de booleanos que indica si todos los personajes del Jugador est�n colocados en el mapa
    }

    /****************************************************************************************
     * Funci�n: StartBattle                                                                 *
     * Uso: Cuando el bot�n �Comenzar Batalla! es pulsado estando todos los personajes del  *
     *      Jugador colocados en el campo de batalla, cambia el booleano StartBattle para   *
     *      que el script del combate lo sepa que el bot�n ha sido pulsado                  *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void StartBattle()
    {
        if (CheckAliadosPosition() && _Button.GetComponent<StartBattleButtonAction>().Activated) // Si los 4 personajes del Jugador est�n colocados en el mapa de combate y el bot�n ha sido clicado
            StarBattleStatus = true;                                                             // Indica que puede comenzar el combate
    }

    /****************************************************************************************
     * Funci�n: ButtonStatus                                                                *
     * Uso: Actualiza las caracter�sticas del bot�n                                         *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void ButtonStatus()
    {
        if (CheckAliadosPosition())                                                      // Si los 4 personajes del Jugador est�n colocados en el mapa de combate
        {
            _Button.GetComponent<Image>().color = new Color(0.66f, 0, 0);                // Cambia el color del bot�n a un rojo oscuro
            _Button.GetComponent<Button>().transition = Selectable.Transition.ColorTint; // Cambia la caracter�stica para que el bot�n se sombree al pulsarlo
            Border.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);            // Cambia el color del borde del bot�n a un rojo puro
        }
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

        if (contador == VariablesGlobales.instance.NumPersonajes) // Si los 4 personajes est�n colocados en el mapa de combate
            return true;                                          // Devuelve true
        else                                                      // Si no est�n los 4 colocados
            return false;                                         // Devuelve false
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
     * Funci�n: GetStartBattleStatus                                                        *
     * Uso: Env�a el booleano que indica si el combate puede comenzar                       *
     * Variables entrada: Nada                                                              *
     * Return: StarBattleStatus(true) - Si puede comenzar el combate                        *
     *         StarBattleStatus(false) - Si puede comenzar el combate                       *
     ****************************************************************************************/
    public bool GetStartBattleStatus()
    {
        return StarBattleStatus;
    }
}
