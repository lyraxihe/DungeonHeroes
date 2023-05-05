using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoriaDerrotaButtonAction : MonoBehaviour
{
    public GameObject _CombatBackground;
    public TMP_Text Texto;

    /****************************************************************************************
      * Función: OnClicked                                                                   *
      * Uso: "Activa" el botón si este es clicado                                            *
      * Variables entrada: Ninguno                                                           *
      * Return: Nada                                                                         *
      ****************************************************************************************/
    public void OnClicked()
    {
        if (_CombatBackground.GetComponent<CombatBackground>().Victoria)
        {
            VariablesGlobales.instance.NumPersonajes = 0;
            
            for (int i = 0; i < _CombatBackground.GetComponent<CombatBackground>().Aliados.Length; i++)
            {
                if (_CombatBackground.GetComponent<CombatBackground>().Aliados[i] != null)
                {
                    if (i == 0)
                    {
                        //VariablesGlobales.instance.KnightVidaTotal = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().VidaTotal;
                        VariablesGlobales.instance.KnightVidaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().VidaActual;
                        //VariablesGlobales.instance.KnightAtaqueActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().AtaqueActual;
                        //VariablesGlobales.instance.KnightAtaqueMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().AtaqueMax;
                        //VariablesGlobales.instance.KnightDefensaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().DefensaActual;
                        //VariablesGlobales.instance.KnightDefensaMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().DefensaMax;
                    }
                    else if (i == 1)
                    {
                        //VariablesGlobales.instance.HealerVidaTotal = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().VidaTotal;
                        VariablesGlobales.instance.HealerVidaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().VidaActual;
                        //VariablesGlobales.instance.HealerAtaqueActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().AtaqueActual;
                        //VariablesGlobales.instance.HealerAtaqueMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().AtaqueMax;
                        //VariablesGlobales.instance.HealerDefensaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().DefensaActual;
                        //VariablesGlobales.instance.HealerDefensaMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().DefensaMax;
                    }
                    else if (i == 2)
                    {
                        //VariablesGlobales.instance.SlimeVidaTotal = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().VidaTotal;
                        VariablesGlobales.instance.SlimeVidaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().VidaActual;
                        //VariablesGlobales.instance.SlimeAtaqueActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().AtaqueActual;
                        //VariablesGlobales.instance.SlimeAtaqueMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().AtaqueMax;
                        //VariablesGlobales.instance.SlimeDefensaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().DefensaActual;
                        //VariablesGlobales.instance.SlimeDefensaMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().DefensaMax;
                    }
                    else
                    {
                        //VariablesGlobales.instance.MageVidaTotal = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().VidaTotal;
                        VariablesGlobales.instance.MageVidaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().VidaActual;
                        //VariablesGlobales.instance.MageAtaqueActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().AtaqueActual;
                        //VariablesGlobales.instance.MageAtaqueMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().AtaqueMax;
                        //VariablesGlobales.instance.MageDefensaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().DefensaActual;
                        //VariablesGlobales.instance.MageDefensaMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().DefensaMax;
                    }
                    VariablesGlobales.instance.NumPersonajes++;
                }
                else
                {
                    if (i == 0)
                    {
                        VariablesGlobales.instance.KnightVidaTotal = 0;
                        VariablesGlobales.instance.KnightVidaActual = 0;
                        VariablesGlobales.instance.KnightAtaqueActual = 0;
                        VariablesGlobales.instance.KnightAtaqueMax = 0;
                        VariablesGlobales.instance.KnightDefensaActual = 0;
                        VariablesGlobales.instance.KnightDefensaMax = 0;
                        VariablesGlobales.instance.KnightDefensaActualPercentage = 0;
                        VariablesGlobales.instance.KnightVivo = false;
                    }
                    else if (i == 1)
                    {
                        VariablesGlobales.instance.HealerVidaTotal = 0;
                        VariablesGlobales.instance.HealerVidaActual = 0;
                        VariablesGlobales.instance.HealerAtaqueActual = 0;
                        VariablesGlobales.instance.HealerAtaqueMax = 0;
                        VariablesGlobales.instance.HealerDefensaActual = 0;
                        VariablesGlobales.instance.HealerDefensaMax = 0;
                        VariablesGlobales.instance.HealerDefensaActualPercentage = 0;
                        VariablesGlobales.instance.HealerVivo = false;
                    }
                    else if (i == 2)
                    {
                        VariablesGlobales.instance.SlimeVidaTotal = 0;
                        VariablesGlobales.instance.SlimeVidaActual = 0;
                        VariablesGlobales.instance.SlimeAtaqueActual = 0;
                        VariablesGlobales.instance.SlimeAtaqueMax = 0;
                        VariablesGlobales.instance.SlimeDefensaActual = 0;
                        VariablesGlobales.instance.SlimeDefensaMax = 0;
                        VariablesGlobales.instance.SlimeDefensaActualPercentage = 0;
                        VariablesGlobales.instance.SlimeVivo = false;
                    }
                    else
                    {
                        VariablesGlobales.instance.MageVidaTotal = 0;
                        VariablesGlobales.instance.MageVidaActual = 0;
                        VariablesGlobales.instance.MageAtaqueActual = 0;
                        VariablesGlobales.instance.MageAtaqueMax = 0;
                        VariablesGlobales.instance.MageDefensaActual = 0;
                        VariablesGlobales.instance.MageDefensaMax = 0;
                        VariablesGlobales.instance.MageDefensaActualPercentage = 0;
                        VariablesGlobales.instance.MageVivo = false;
                    }
                }
            }
        }

        //VariablesGlobales.instance.KnightVidaActual = 30;

        if (VariablesGlobales.instance.Boss && _CombatBackground.GetComponent<CombatBackground>().Victoria)
        {
            VariablesGlobales.instance.Boss = false;
            VariablesGlobales.instance.canvasCreditos.SetActive(!VariablesGlobales.instance.canvasCreditos.activeSelf);
            //SceneManager.LoadScene("MainMenu"); //abre la escena
        }
        else if (_CombatBackground.GetComponent<CombatBackground>().Derrota)
        {
            VariablesGlobales.instance.Boss = false;
            SceneManager.LoadScene("MainMenu"); //abre la escena
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }
}
