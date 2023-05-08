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

    //cartel de Dinero Insuficiente
    public GameObject CartelDineroInsuficiente;

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
  
    public int VidaMaxCaballero;                 // Vida máxima del personaje
  

    void Start()
    {

        //tomo los valores asignados en variables globales para todos los personajes
        AtaqueMax = VariablesGlobales.instance.AtaqueMax;
        DefensaMax = VariablesGlobales.instance.DefensaMax;
        Money = VariablesGlobales.instance.Money;

        //tomo los valores asignados en variables globales para el caballero
       
        VidaMaxCaballero = VariablesGlobales.instance.KnightVidaTotal;
      

    }

    // Update is called once per frame
    void Update()
    {

        //actualiza el valor de la moneda
        MoneyText.text = "" + VariablesGlobales.instance.Money;

        //actualiza los valores del caballero
        HPActualCaballero.text = "" + VariablesGlobales.instance.KnightVidaActual + "/" + VidaMaxCaballero;
        ATQActualCaballero.text = "" + VariablesGlobales.instance.KnightAtaqueActual + "/" + AtaqueMax;
        DEFActualCaballero.text = "" + (VariablesGlobales.instance.KnightDefensaActual * 5) + "%/" + DefensaMax * 5 + "%";
    }

    public void SumarHPCaballero()
    {
        
        if (VariablesGlobales.instance.Money >= 10 & VariablesGlobales.instance.KnightVidaActual < VidaMaxCaballero)
        {
            HpCaballeroButton.interactable = true; //creo que así hago que el boton solo sea interactuable al tener menos vida que la vida máxima
            VariablesGlobales.instance.KnightVidaActual += 10;
            HPActualCaballero.text = "" + VariablesGlobales.instance.KnightVidaActual + "/" + VidaMaxCaballero;
            VariablesGlobales.instance.Money -= 10;
            MoneyText.text = "" + VariablesGlobales.instance.Money;
           
            if (VariablesGlobales.instance.KnightVidaActual > VidaMaxCaballero)
            {
                VariablesGlobales.instance.KnightVidaActual = VidaMaxCaballero;
            }
        }
        else if (VariablesGlobales.instance.KnightVidaActual == VidaMaxCaballero)
        {
            HpCaballeroButton.interactable = false;
        }
        else 
        {
            StartCoroutine(Esperar());
        }
    }
    public void SumarATQCaballero()
    {
       
        if (VariablesGlobales.instance.Money >= 20 & VariablesGlobales.instance.KnightAtaqueActual < AtaqueMax)
        {
            ATQCaballeroButton.interactable = true;
            VariablesGlobales.instance.Money -= 20;
            VariablesGlobales.instance.KnightAtaqueActual += 5;
            ATQActualCaballero.text = "" + VariablesGlobales.instance.KnightAtaqueActual + "/" + AtaqueMax;
            MoneyText.text = "" + VariablesGlobales.instance.Money;
            if (VariablesGlobales.instance.KnightAtaqueActual > AtaqueMax)
            {
                VariablesGlobales.instance.KnightAtaqueActual = AtaqueMax;
            }
        }
        else if (VariablesGlobales.instance.KnightAtaqueActual == VariablesGlobales.instance.AtaqueMax)
        {
            ATQCaballeroButton.interactable = false;
        }
        else { StartCoroutine(Esperar()); }

    }
    public void SumarDEFCaballero()
    {
        if (VariablesGlobales.instance.Money >= 15 & VariablesGlobales.instance.KnightDefensaActual < DefensaMax)
        {
            DEFCaballeroButton.interactable = true;
            VariablesGlobales.instance.Money -= 15;
            VariablesGlobales.instance.KnightDefensaActual += 1;
            DEFActualCaballero.text = "" + (VariablesGlobales.instance.KnightDefensaActual * 5 )+ "%/" + DefensaMax * 5 + "%";
            MoneyText.text = "" + VariablesGlobales.instance.Money;

            if (VariablesGlobales.instance.KnightDefensaActual > DefensaMax)
            {
                VariablesGlobales.instance.KnightDefensaActual = DefensaMax;
            }
        }
        else if (VariablesGlobales.instance.KnightDefensaActual == DefensaMax)
        {
            DEFCaballeroButton.interactable = false;
        }
        else { StartCoroutine(Esperar()); }

    }

    IEnumerator Esperar()
    {
        CartelDineroInsuficiente.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        CartelDineroInsuficiente.SetActive(false);
    }
}
