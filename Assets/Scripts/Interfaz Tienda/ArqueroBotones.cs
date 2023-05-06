using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArqueroBotones : MonoBehaviour
{
    //texto de money
    public TMP_Text MoneyText;

    //botones del Arquero
    public Button HpArqueroButton;
    public Button ATQArqueroButton;
    public Button DEFArqueroButton;

    //texto del Arquero
    public TMP_Text HPActualArquero;
    public TMP_Text ATQActualArquero;
    public TMP_Text DEFActualArquero;

    //variables generales entre los personajes
    public int AtaqueMax;               // Ataque máximo de los personajes
    public int DefensaMax;              // Defensa máxima de los personajes
    public int Money;                   // Dinero actual del jugador

    //variables del Arquero
    public int VidaMaxArquero;                 // Vida máxima del personaje


    void Start()
    {
        //tomo los valores asignados en variables globales para todos los personajes
        AtaqueMax = VariablesGlobales.instance.AtaqueMax;
        DefensaMax = VariablesGlobales.instance.DefensaMax;
        Money = VariablesGlobales.instance.Money;

        //tomo los valores asignados en variables globales para el Arquero
        VidaMaxArquero = VariablesGlobales.instance.MageVidaTotal;

    }

    // Update is called once per frame
    void Update()
    {
        //actualiza el valor de la moneda
        MoneyText.text = "" + VariablesGlobales.instance.Money;

        //actualiza los valores del arquero
        HPActualArquero.text = "" + VariablesGlobales.instance.MageVidaActual + "/" + VidaMaxArquero;
        ATQActualArquero.text = "" + VariablesGlobales.instance.MageAtaqueActual + "/" + AtaqueMax;
        DEFActualArquero.text = "" + VariablesGlobales.instance.MageDefensaActual * 5 + "%/" + DefensaMax * 5 + "%";
    }

    public void SumarHPArquero()
    {

        if (VariablesGlobales.instance.Money >= 10 & VariablesGlobales.instance.MageVidaActual < VidaMaxArquero)
        {
            HpArqueroButton.interactable = true; //creo que así hago que el boton solo sea interactuable al tener menos vida que la vida máxima
            VariablesGlobales.instance.MageVidaActual += 10;
            HPActualArquero.text = "" + VariablesGlobales.instance.MageVidaActual + "/" + VidaMaxArquero;
            VariablesGlobales.instance.Money -= 10;
            MoneyText.text = "" + VariablesGlobales.instance.Money;

            if (VariablesGlobales.instance.MageVidaActual > VidaMaxArquero)
            {
                VariablesGlobales.instance.MageVidaActual = VidaMaxArquero;
            }
        }
        else if (VariablesGlobales.instance.MageVidaActual == VidaMaxArquero)
        {   
            HpArqueroButton.interactable = false;
        }
        else { return; }
    }
    public void SumarATQArquero()
    {
        if (VariablesGlobales.instance.Money >= 20 & VariablesGlobales.instance.MageAtaqueActual < AtaqueMax)
        {
            ATQArqueroButton.interactable = true;
            VariablesGlobales.instance.Money -= 20;
            VariablesGlobales.instance.MageAtaqueActual += 5;
            ATQActualArquero.text = "" + VariablesGlobales.instance.MageAtaqueActual + "/" + AtaqueMax;
            MoneyText.text = "" + VariablesGlobales.instance.Money;
            if (VariablesGlobales.instance.MageAtaqueActual > AtaqueMax)
            {
                VariablesGlobales.instance.MageAtaqueActual = AtaqueMax;
            }
        }
        else if (VariablesGlobales.instance.MageAtaqueActual == AtaqueMax)
        {
            ATQArqueroButton.interactable = false;
        }
        else { return; }
    }
    public void SumarDEFArquero()
    {
        if (VariablesGlobales.instance.Money >= 15 & VariablesGlobales.instance.MageDefensaActual < DefensaMax)
        {
            DEFArqueroButton.interactable = true;
            VariablesGlobales.instance.Money -= 15;
            VariablesGlobales.instance.MageDefensaActual += 1;
            DEFActualArquero.text = "" + VariablesGlobales.instance.MageDefensaActual * 5 + "%/" + DefensaMax * 5 + "%";
            MoneyText.text = "" + VariablesGlobales.instance.Money;

            if (VariablesGlobales.instance.MageDefensaActual > DefensaMax)
            {
               
                VariablesGlobales.instance.MageDefensaActual = DefensaMax;
            }
        
        }
        else if (VariablesGlobales.instance.MageDefensaActual == DefensaMax)
        {
            DEFArqueroButton.interactable = false;
        }
        else { return; }

    }
}
