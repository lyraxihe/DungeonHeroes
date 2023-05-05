using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlPausa : MonoBehaviour
{
    public void ContinuarPausa()
    {
        VariablesGlobales.instance.EscPressed = false;
        VariablesGlobales.instance.canvas.SetActive(!VariablesGlobales.instance.canvas.activeSelf);
    }

    public void SalirPausa()
    {
        VariablesGlobales.instance.EscPressed = false;
        VariablesGlobales.instance.canvas.SetActive(!VariablesGlobales.instance.canvas.activeSelf);
        SceneManager.LoadScene("MainMenu"); //abre la escena
    }
}
