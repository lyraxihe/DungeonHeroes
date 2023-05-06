using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BrujaBotones : MonoBehaviour
{
    //texto de money
    public TMP_Text MoneyText;

    //botones de la Bruja
    public Button HpBrujaButton;
    public Button ATQBrujaButton;
    public Button DEFBrujaButton;

    //texto de la Bruja
    public TMP_Text HPActualBruja;
    public TMP_Text ATQActualBruja;
    public TMP_Text DEFActualBruja;

    //variables generales entre los personajes
    public int AtaqueMax;               // Ataque máximo de los personajes
    public int DefensaMax;              // Defensa máxima de los personajes
    public int Money;                   // Dinero actual del jugador

    //variables del Bruja

    public int VidaMaxBruja;                 // Vida máxima del personaje




    void Start()
    {

        //tomo los valores asignados en variables globales para todos los personajes
        AtaqueMax = VariablesGlobales.instance.AtaqueMax;
        DefensaMax = VariablesGlobales.instance.DefensaMax;
        Money = VariablesGlobales.instance.Money;

        //tomo los valores asignados en variables globales para la Bruja
      
        VidaMaxBruja = VariablesGlobales.instance.SlimeVidaTotal;

    }

    // Update is called once per frame
    void Update()
    {
        //actualiza el valor de la moneda
        MoneyText.text = "" + VariablesGlobales.instance.Money;

        //actualiza los valores de la bruja
        HPActualBruja.text = "" + VariablesGlobales.instance.SlimeVidaActual + "/" + VidaMaxBruja;
        ATQActualBruja.text = "" + VariablesGlobales.instance.SlimeAtaqueActual + "/" + AtaqueMax;
        DEFActualBruja.text = "" + VariablesGlobales.instance.SlimeDefensaActual * 5 + "%/" + DefensaMax * 5 + "%";

    }

    public void SumarHPBruja()
    {

        if (VariablesGlobales.instance.Money >= 10 & VariablesGlobales.instance.SlimeVidaActual < VidaMaxBruja)
        {
            HpBrujaButton.interactable = true; //creo que así hago que el boton solo sea interactuable al tener menos vida que la vida máxima
            VariablesGlobales.instance.SlimeVidaActual += 10;
            HPActualBruja.text = "" + VariablesGlobales.instance.SlimeVidaActual + "/" + VidaMaxBruja;
            VariablesGlobales.instance.Money -= 10;
            MoneyText.text = "" + VariablesGlobales.instance.Money;

            if (VariablesGlobales.instance.SlimeVidaActual > VidaMaxBruja)
            {
                VariablesGlobales.instance.SlimeVidaActual = VidaMaxBruja;
                HpBrujaButton.interactable = false;
            }
        }
        else if (VariablesGlobales.instance.SlimeVidaActual == VidaMaxBruja)
        {
            HpBrujaButton.interactable = false;
        }
        else { return; }
    }
    public void SumarATQBruja()
    {
        if (VariablesGlobales.instance.Money >= 20 & VariablesGlobales.instance.SlimeAtaqueActual < AtaqueMax)
        {
            ATQBrujaButton.interactable = true;
            VariablesGlobales.instance.Money -= 20;
            VariablesGlobales.instance.SlimeAtaqueActual += 5;
            ATQActualBruja.text = "" + VariablesGlobales.instance.SlimeAtaqueActual + "/" + AtaqueMax;
            MoneyText.text = "" + VariablesGlobales.instance.Money;
            if (VariablesGlobales.instance.SlimeAtaqueActual > AtaqueMax)
            {
                VariablesGlobales.instance.SlimeAtaqueActual = AtaqueMax;
                ATQBrujaButton.interactable = false;
            }
        }
        else if (VariablesGlobales.instance.SlimeAtaqueActual == AtaqueMax)
        {
            ATQBrujaButton.interactable = false;
        }
        else { return; }
    }
    public void SumarDEFBruja()
    {
        
        if (VariablesGlobales.instance.Money >= 15 & VariablesGlobales.instance.SlimeDefensaActual < DefensaMax)
        {
            DEFBrujaButton.interactable = true;
            VariablesGlobales.instance.Money -= 15;
            VariablesGlobales.instance.SlimeDefensaActual += 1;
            DEFActualBruja.text = "" + VariablesGlobales.instance.SlimeDefensaActual * 5 + "%/" + DefensaMax * 5 + "%";
            MoneyText.text = "" + VariablesGlobales.instance.Money;

            if (VariablesGlobales.instance.SlimeDefensaActual > DefensaMax)
            {
              
                VariablesGlobales.instance.SlimeDefensaActual = DefensaMax;
                DEFBrujaButton.interactable = false;
            }
        }
        else if (VariablesGlobales.instance.SlimeDefensaActual == DefensaMax)
        {
            DEFBrujaButton.interactable = false;
        }

        else { return; }

    }
}

