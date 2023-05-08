using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Unity.VisualScripting;

public class ArqueroBotones : MonoBehaviour
{
    //texto de money
    public TMP_Text MoneyText;

    //cartel de Dinero Insuficiente
    public GameObject CartelDineroInsuficiente;

    //canva
    public RectTransform canvas;

    //botones del Arquero
    public Button HpArqueroButton;
    public Button ATQArqueroButton;
    public Button DEFArqueroButton;

    //texto del Arquero
    public TMP_Text HPActualArquero;
    public TMP_Text ATQActualArquero;
    public TMP_Text DEFActualArquero;

    //variables generales entre los personajes
    public int AtaqueMax;               // Ataque m�ximo de los personajes
    public int DefensaMax;              // Defensa m�xima de los personajes
    public int Money;                   // Dinero actual del jugador

    //variables del Arquero
    public int VidaMaxArquero;                 // Vida m�xima del personaje


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
            HpArqueroButton.interactable = true; //creo que as� hago que el boton solo sea interactuable al tener menos vida que la vida m�xima
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

        else
        {
            //return;
            StartCoroutine(Esperar());
        }
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
            DEFArqueroButton.interactable = false;
        }
        else
        {
            //return;
            StartCoroutine(Esperar());
        }


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
        else
        {
            //return;
            StartCoroutine(Esperar());
        }

    }

    IEnumerator Esperar()
    {
        CartelDineroInsuficiente.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        CartelDineroInsuficiente.SetActive(false);
    }
  
}
