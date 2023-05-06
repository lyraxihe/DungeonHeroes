using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CaballeroBotones : MonoBehaviour
{

    //botones del caballero
    public Button HpCaballeroButton;
    public Button ATQCaballeroButton;
    public Button DEFCaballeroButton;

    //texto de money
    public TMP_Text MoneyText;

    //texto del Caballero
    public TMP_Text HPActualCaballero;
    public TMP_Text ATQActualCaballero;
    public TMP_Text DEFActualCaballero;

    //variables generales entre los personajes
    public int AtaqueMax;               // Ataque máximo de los personajes
    public int DefensaMax;              // Defensa máxima de los personajes
    public int Money;                   // Dinero actual del jugador

    //variables del Caballero
    public int VidaActualCaballero;              // Vida actual del personaje
    public int VidaMaxCaballero;                 // Vida máxima del personaje
    public int AtaqueActualCaballero;            // Ataque actual del personaje
    public int DefensaActualCaballero;           // Defensa actual del personaje

    void Start()
    {

        //tomo los valores asignados en variables globales para todos los personajes
        AtaqueMax = VariablesGlobales.instance.AtaqueMax;
        DefensaMax = VariablesGlobales.instance.DefensaMax * 5;
        Money = VariablesGlobales.instance.Money;

        //tomo los valores asignados en variables globales para el caballero
        VidaActualCaballero = VariablesGlobales.instance.KnightVidaActual;
        VidaMaxCaballero = VariablesGlobales.instance.KnightVidaTotal;
        AtaqueActualCaballero = VariablesGlobales.instance.KnightAtaqueActual;
        DefensaActualCaballero = VariablesGlobales.instance.KnightDefensaActual * 5;

    }

    // Update is called once per frame
    void Update()
    {

        //actualiza el valor de la moneda
        MoneyText.text = "" + Money;

        //actualiza los valores del caballero
        HPActualCaballero.text = "" + VidaActualCaballero + "/" + VidaMaxCaballero;
        ATQActualCaballero.text = "" + AtaqueActualCaballero + "/" + AtaqueMax;
        DEFActualCaballero.text = "" + DefensaActualCaballero + "%/" + DefensaMax + "%";
    }

    public void SumarHPCaballero()
    {

        if (Money >= 10 & VidaActualCaballero < VidaMaxCaballero)
        {
            HpCaballeroButton.interactable = true; //creo que así hago que el boton solo sea interactuable al tener menos vida que la vida máxima
            VidaActualCaballero += 10;
            HPActualCaballero.text = "" + VidaActualCaballero + "/" + VidaMaxCaballero;
            Money -= 10;
            MoneyText.text = "" + Money;
           
            if (VidaActualCaballero > VidaMaxCaballero)
            {
                VidaActualCaballero = VidaMaxCaballero;
            }
        }
        else if (VidaActualCaballero == VidaMaxCaballero)
        {
            HpCaballeroButton.interactable = false;
        }
        else { return; }
    }
    public void SumarATQCaballero()
    {
        if (Money >= 20 & AtaqueActualCaballero < AtaqueMax)
        {
            ATQCaballeroButton.interactable = true;
            Money -= 20;
            AtaqueActualCaballero += 5;
            ATQActualCaballero.text = "" + AtaqueActualCaballero + "/" + AtaqueMax;
            MoneyText.text = "" + Money;
            if (AtaqueActualCaballero > AtaqueMax)
            {
                AtaqueActualCaballero = AtaqueMax;
            }
        }
        else if (AtaqueActualCaballero == AtaqueMax)
        {
            ATQCaballeroButton.interactable = false;
        }
        else { return; }

    }
    public void SumardDEFCaballero()
    {
        if (Money >= 15 & DefensaActualCaballero < DefensaMax)
        {
            DEFCaballeroButton.interactable = true;
            Money -= 15;
            DefensaActualCaballero += 5;
            DEFActualCaballero.text = "" + DefensaActualCaballero + "%/" + DefensaMax + "%";
            MoneyText.text = "" + Money;

            if (DefensaActualCaballero > DefensaMax)
            {
                DefensaActualCaballero = DefensaMax;
            }
        }
        else if (DefensaActualCaballero == DefensaMax)
        {
            DEFCaballeroButton.interactable = false;
        }
        else { return; }

    }
}
