using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HadaBotones : MonoBehaviour
{
    //texto de money
    public TMP_Text MoneyText;

    //botones del Hada
    public Button HpHadaButton;
    public Button ATQHadaButton;
    public Button DEFHadaButton;

    //texto del Hada
    public TMP_Text HPActualHada;
    public TMP_Text ATQActualHada;
    public TMP_Text DEFActualHada;

    //variables generales entre los personajes
    public int AtaqueMax;               // Ataque máximo de los personajes
    public int DefensaMax;              // Defensa máxima de los personajes
    public int Money;                   // Dinero actual del jugador

    //variables del hada

    public int VidaMaxHada;                 // Vida máxima del personaje

   

    void Start()
    {
        //tomo los valores asignados en variables globales para todos los personajes
        AtaqueMax = VariablesGlobales.instance.AtaqueMax;
        DefensaMax = VariablesGlobales.instance.DefensaMax;
        Money = VariablesGlobales.instance.Money;

        //tomo los valores asignados en variables globales para el hada

        VidaMaxHada = VariablesGlobales.instance.HealerVidaTotal;

       

    }

    // Update is called once per frame
    void Update()
    {
        //actualiza los valores del hada
        HPActualHada.text = "" + VariablesGlobales.instance.HealerVidaActual + "/" + VidaMaxHada;
        ATQActualHada.text = "" + VariablesGlobales.instance.HealerAtaqueActual + "/" + AtaqueMax;
        DEFActualHada.text = "" + VariablesGlobales.instance.HealerDefensaActual * 5 + "%/" + DefensaMax * 5 + "%";
    }

    public void SumarHPHada()
    {

        if (VariablesGlobales.instance.Money >= 10 & VariablesGlobales.instance.HealerVidaActual < VidaMaxHada)
        {
            HpHadaButton.interactable = true; //creo que así hago que el boton solo sea interactuable al tener menos vida que la vida máxima
            VariablesGlobales.instance.HealerVidaActual += 10;
            HPActualHada.text = "" + VariablesGlobales.instance.HealerVidaActual + "/" + VidaMaxHada;
            VariablesGlobales.instance.Money -= 10;
            MoneyText.text = "" + VariablesGlobales.instance.Money;

            if (VariablesGlobales.instance.HealerVidaActual > VidaMaxHada)
            {
                VariablesGlobales.instance.HealerVidaActual = VidaMaxHada;
            }
        }
        else if (VariablesGlobales.instance.HealerVidaActual == VidaMaxHada)
        {
            HpHadaButton.interactable = false;
        }
        else { return; }
    }
    public void SumarATQHada()
    {
        if (VariablesGlobales.instance.Money >= 20 & VariablesGlobales.instance.HealerAtaqueActual < AtaqueMax)
        {
            ATQHadaButton.interactable = true;
            VariablesGlobales.instance.Money -= 20;
            VariablesGlobales.instance.HealerAtaqueActual += 5;
            ATQActualHada.text = "" + VariablesGlobales.instance.HealerAtaqueActual + "/" + AtaqueMax;
            MoneyText.text = "" + VariablesGlobales.instance.Money;
            if (VariablesGlobales.instance.HealerAtaqueActual > AtaqueMax)
            {
                VariablesGlobales.instance.HealerAtaqueActual = AtaqueMax;
            }
        }
        else if (VariablesGlobales.instance.HealerAtaqueActual == VariablesGlobales.instance.AtaqueMax)
        {
            ATQHadaButton.interactable = false;
        }
        else { return; }

    }
    public void SumarDEFCaballero()
    {
        if (VariablesGlobales.instance.Money >= 15 & VariablesGlobales.instance.HealerDefensaActual < DefensaMax)
        {
            DEFHadaButton.interactable = true;
            VariablesGlobales.instance.Money -= 15;
            VariablesGlobales.instance.HealerDefensaActual += 1;
            DEFActualHada.text = "" + VariablesGlobales.instance.HealerDefensaActual * 5  + "%/" + DefensaMax * 5 + "%";
            MoneyText.text = "" + VariablesGlobales.instance.Money;

            if (VariablesGlobales.instance.HealerDefensaActual > DefensaMax)
            {
                VariablesGlobales.instance.HealerDefensaActual = DefensaMax;
            }
        }
        else if (VariablesGlobales.instance.HealerDefensaActual == DefensaMax)
        {
            DEFHadaButton.interactable = false;
        }
        else { return; }

    }
}
