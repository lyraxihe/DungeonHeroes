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
    public int VidaActualBruja;              // Vida actual del personaje
    public int VidaMaxBruja;                 // Vida máxima del personaje
    public int AtaqueActualBruja;            // Ataque actual del personaje
    public int DefensaActualBruja;           // Defensa actual del personaje



    void Start()
    {

        //tomo los valores asignados en variables globales para todos los personajes
        AtaqueMax = VariablesGlobales.instance.AtaqueMax;
        DefensaMax = VariablesGlobales.instance.DefensaMax * 5;
        Money = VariablesGlobales.instance.Money;

        //tomo los valores asignados en variables globales para la Bruja
        VidaActualBruja = VariablesGlobales.instance.SlimeVidaActual;
        VidaMaxBruja = VariablesGlobales.instance.SlimeVidaTotal;
        AtaqueActualBruja = VariablesGlobales.instance.SlimeAtaqueActual;
        DefensaActualBruja = VariablesGlobales.instance.SlimeDefensaActual * 5;
    }

    // Update is called once per frame
    void Update()
    {
        //actualiza el valor de la moneda
        MoneyText.text = "" + Money;

        //actualiza los valores de la bruja
        HPActualBruja.text = "" + VidaActualBruja + "/" + VidaMaxBruja;
        ATQActualBruja.text = "" + AtaqueActualBruja + "/" + AtaqueMax;
        DEFActualBruja.text = "" + DefensaActualBruja + "%/" + DefensaMax + "%";

    }

    public void SumarHPBruja()
    {

        if (Money >= 10 & VidaActualBruja < VidaMaxBruja)
        {
            HpBrujaButton.interactable = true; //creo que así hago que el boton solo sea interactuable al tener menos vida que la vida máxima
            VidaActualBruja += 10;
            HPActualBruja.text = "" + VidaActualBruja + "/" + VidaMaxBruja;
            Money -= 10;
            MoneyText.text = "" + Money;

            if (VidaActualBruja > VidaMaxBruja)
            {
                VidaActualBruja = VidaMaxBruja;
            }
        }
        else if (VidaActualBruja == VidaMaxBruja)
        {
            HpBrujaButton.interactable = false;
        }
        else { return; }
    }
    public void SumarATQBruja()
    {
        if (Money >= 20 & AtaqueActualBruja < AtaqueMax)
        {
            ATQBrujaButton.interactable = true;
            Money -= 20;
            AtaqueActualBruja += 5;
            ATQActualBruja.text = "" + AtaqueActualBruja + "/" + AtaqueMax;
            MoneyText.text = "" + Money;
            if (AtaqueActualBruja > AtaqueMax)
            {
                AtaqueActualBruja = AtaqueMax;
            }
        }
        else if (AtaqueActualBruja == AtaqueMax)
        {
            ATQBrujaButton.interactable = false;
        }
        else { return; }

    }
    public void SumardDEFBruja()
    {
        if (Money >= 15 & DefensaActualBruja < DefensaMax)
        {
            DEFBrujaButton.interactable = true;
            Money -= 15;
            DefensaActualBruja += 5;
            DEFActualBruja.text = "" + DefensaActualBruja + "%/" + DefensaMax + "%";
            MoneyText.text = "" + Money;

            if (DefensaActualBruja > DefensaMax)
            {
                DefensaActualBruja = DefensaMax;
            }
        }
        else if (DefensaActualBruja == DefensaMax)
        {
            DEFBrujaButton.interactable = false;
        }
        else { return; }

    }
}

