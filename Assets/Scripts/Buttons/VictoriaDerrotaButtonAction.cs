using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoriaDerrotaButtonAction : MonoBehaviour
{
    /****************************************************************************************
      * Funci�n: OnClicked                                                                   *
      * Uso: "Activa" el bot�n si este es clicado                                            *
      * Variables entrada: Ninguno                                                           *
      * Return: Nada                                                                         *
      ****************************************************************************************/
    public void OnClicked()
    {
        SceneManager.LoadScene("Main"); //abre la escena
    }
}
