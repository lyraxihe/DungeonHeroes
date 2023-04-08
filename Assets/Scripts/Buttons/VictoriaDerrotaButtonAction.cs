using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoriaDerrotaButtonAction : MonoBehaviour
{
    /****************************************************************************************
      * Función: OnClicked                                                                   *
      * Uso: "Activa" el botón si este es clicado                                            *
      * Variables entrada: Ninguno                                                           *
      * Return: Nada                                                                         *
      ****************************************************************************************/
    public void OnClicked()
    {
        SceneManager.LoadScene("Main"); //abre la escena
    }
}
