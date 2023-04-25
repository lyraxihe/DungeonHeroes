using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoriaDerrotaButtonAction : MonoBehaviour
{
    public GameObject _CombatBackground;

    /****************************************************************************************
      * Función: OnClicked                                                                   *
      * Uso: "Activa" el botón si este es clicado                                            *
      * Variables entrada: Ninguno                                                           *
      * Return: Nada                                                                         *
      ****************************************************************************************/
    public void OnClicked()
    {
        //for (int i = 0; i < _CombatBackground.GetComponent<CombatBackground>().Aliados.Length; i++)
        //{
        //    if (i == 1)
        //    {
        //        VariablesGlobales.instance.KnightVidaTotal = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().VidaTotal;
        //        VariablesGlobales.instance.KnightVidaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().VidaActual;
        //        VariablesGlobales.instance.KnightAtaqueActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().AtaqueActual;
        //        VariablesGlobales.instance.KnightAtaqueMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().AtaqueMax;
        //        VariablesGlobales.instance.KnightDefensaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().DefensaActual;
        //        VariablesGlobales.instance.KnightDefensaMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerKnight>().DefensaMax;
        //    }
        //    else if (i == 2)
        //    {
        //        VariablesGlobales.instance.HealerVidaTotal = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().VidaTotal;
        //        VariablesGlobales.instance.HealerVidaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().VidaActual;
        //        VariablesGlobales.instance.HealerAtaqueActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().AtaqueActual;
        //        VariablesGlobales.instance.HealerAtaqueMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().AtaqueMax;
        //        VariablesGlobales.instance.HealerDefensaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().DefensaActual;
        //        VariablesGlobales.instance.HealerDefensaMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerHealer>().DefensaMax;
        //    }
        //    else if (i == 3)
        //    {
        //        VariablesGlobales.instance.SlimeVidaTotal = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().VidaTotal;
        //        VariablesGlobales.instance.SlimeVidaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().VidaActual;
        //        VariablesGlobales.instance.SlimeAtaqueActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().AtaqueActual;
        //        VariablesGlobales.instance.SlimeAtaqueMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().AtaqueMax;
        //        VariablesGlobales.instance.SlimeDefensaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().DefensaActual;
        //        VariablesGlobales.instance.SlimeDefensaMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerSlime>().DefensaMax;
        //    }
        //    else
        //    {
        //        VariablesGlobales.instance.MageVidaTotal = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().VidaTotal;
        //        VariablesGlobales.instance.MageVidaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().VidaActual;
        //        VariablesGlobales.instance.MageAtaqueActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().AtaqueActual;
        //        VariablesGlobales.instance.MageAtaqueMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().AtaqueMax;
        //        VariablesGlobales.instance.MageDefensaActual = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().DefensaActual;
        //        VariablesGlobales.instance.MageDefensaMax = _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().DefensaMax;
        //    }
        //}

        //VariablesGlobales.instance.KnightVidaActual = 30;

        SceneManager.LoadScene("Main"); //abre la escena
    }
}
