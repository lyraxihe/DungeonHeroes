using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VariablesTienda : MonoBehaviour
{
    //botones del caballero
    public Button HpCaballeroButton;
    public Button ATQCaballeroButton;
    public Button DEFCaballeroButton;

    //botones de la Bruja
    public Button HpBrujaButton;
    public Button ATQBrujaButton;
    public Button DEFBrujaButton;

    //botones del Arquero
    public Button HpArqueroButton;
    public Button ATQArqueroButton;
    public Button DEFArqueroButton;

    //botones del Hada
    public Button HpHadaButton;
    public Button ATQHadaButton;
    public Button DEFHadaButton;

    /*  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
    //texto de money
    public TMP_Text MoneyText;

    //texto del Caballero
    public TMP_Text HPActualCaballero;
    public TMP_Text ATQActualCaballero;
    public TMP_Text DEFActualCaballero;

    //texto de la Bruja
    public TMP_Text HPActualBruja;
    public TMP_Text ATQActualBruja;
    public TMP_Text DEFActualBruja;

    //texto del Arquero
    public TMP_Text HPActualArquero;
    public TMP_Text ATQActualArquero;
    public TMP_Text DEFActualArquero;

    //texto del Hada
    public TMP_Text HPActualHada;
    public TMP_Text ATQActualHada;
    public TMP_Text DEFActualHada;

    /*  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    //variables generales entre los personajes
    public int AtaqueMax;               // Ataque m�ximo de los personajes
    public int DefensaMax;              // Defensa m�xima de los personajes
    public int Money;                   // Dinero actual del jugador

    //variables del Caballero
    public int VidaActualCaballero;              // Vida actual del personaje
    public int VidaMaxCaballero;                 // Vida m�xima del personaje
    public int AtaqueActualCaballero;            // Ataque actual del personaje
    public int DefensaActualCaballero;           // Defensa actual del personaje

    //variables del Bruja
    public int VidaActualBruja;              // Vida actual del personaje
    public int VidaMaxBruja;                 // Vida m�xima del personaje
    public int AtaqueActualBruja;            // Ataque actual del personaje
    public int DefensaActualBruja;           // Defensa actual del personaje

    //variables del Arquero
    public int VidaActualArquero;              // Vida actual del personaje
    public int VidaMaxArquero;                 // Vida m�xima del personaje
    public int AtaqueActualArquero;            // Ataque actual del personaje
    public int DefensaActualArquero;           // Defensa actual del personaje

    //variables del hada
    public int VidaActualHada;              // Vida actual del personaje
    public int VidaMaxHada;                 // Vida m�xima del personaje
    public int AtaqueActualHada;            // Ataque actual del personaje
    public int DefensaActualHada;           // Defensa actual del personaje

    /*  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Start()
    {
        //tomo los valores asignados en variables globales para todos los personajes
        AtaqueMax = VariablesGlobales.instance.AtaqueMax;
        DefensaMax = VariablesGlobales.instance.DefensaMax;
        Money = VariablesGlobales.instance.Money;


        //tomo los valores asignados en variables globales para el caballero
        VidaActualCaballero = VariablesGlobales.instance.KnightVidaActual;
        VidaMaxCaballero = VariablesGlobales.instance.KnightVidaTotal;
        AtaqueActualCaballero = VariablesGlobales.instance.KnightAtaqueActual;
        DefensaActualCaballero = VariablesGlobales.instance.KnightDefensaActual;

        //tomo los valores asignados en variables globales para la Bruja
        VidaActualBruja = VariablesGlobales.instance.SlimeVidaActual;
        VidaMaxBruja = VariablesGlobales.instance.SlimeVidaTotal;
        AtaqueActualBruja = VariablesGlobales.instance.SlimeAtaqueActual;
        DefensaActualBruja = VariablesGlobales.instance.SlimeDefensaActual;

        //tomo los valores asignados en variables globales para el Arquero
        VidaActualArquero = VariablesGlobales.instance.MageVidaActual;
        VidaMaxArquero = VariablesGlobales.instance.MageVidaTotal;
        AtaqueActualArquero = VariablesGlobales.instance.MageAtaqueActual;
        DefensaActualArquero = VariablesGlobales.instance.MageDefensaActual;

        //tomo los valores asignados en variables globales para el hada
        VidaActualHada = VariablesGlobales.instance.HealerVidaActual;
        VidaMaxHada = VariablesGlobales.instance.HealerVidaTotal;
        AtaqueActualHada = VariablesGlobales.instance.HealerAtaqueActual;
        DefensaActualHada = VariablesGlobales.instance.HealerDefensaActual;
        
    }

    // Update is called once per frame
    void Update()
    {
     

        //actualiza el valor de la moneda
        MoneyText.text = "" + Money;

        //actualiza los valores del caballero
        HPActualCaballero.text = "" + VidaActualCaballero + "/" + VidaMaxCaballero;
        ATQActualCaballero.text = "" + AtaqueActualCaballero + "/" + AtaqueMax;
        DEFActualCaballero.text = "" + DefensaActualCaballero + "/" + DefensaMax;

        //actualiza los valores de la bruja
        HPActualBruja.text = "" + VidaActualBruja + "/" + VidaMaxBruja;
        ATQActualBruja.text = "" + AtaqueActualBruja + "/" + AtaqueMax;
        DEFActualBruja.text = "" + DefensaActualBruja + "/" + DefensaMax;

        //actualiza los valores del arquero
        HPActualArquero.text = "" + VidaActualArquero + "/" + VidaMaxArquero;
        ATQActualArquero.text = "" + AtaqueActualArquero + "/" + AtaqueMax;
        DEFActualArquero.text = "" + DefensaActualArquero + "/" + DefensaMax;


        //actualiza los valores del hada
        HPActualHada.text = "" + VidaActualHada + "/" + VidaMaxHada;
        ATQActualHada.text = "" + AtaqueActualHada + "/" + AtaqueMax;
        DEFActualHada.text = "" + DefensaActualHada + "/" + DefensaMax;


    }

    public void SumarHPCaballero()
    {
        
        if (Money >= 10 & VidaActualCaballero <VidaMaxCaballero) 
        {
            HpCaballeroButton.interactable = true; //creo que as� hago que el boton solo sea interactuable al tener menos vida que la vida m�xima
            HPActualCaballero.text = "" + VidaActualCaballero;
            Money -= 10;
            VidaActualCaballero += 10;
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
           ATQActualCaballero.text = "" + AtaqueActualCaballero;
           Money -= 20;
           AtaqueActualCaballero += 5;
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
        if (Money>=15 & DefensaActualCaballero < DefensaMax)
        {
            DEFActualCaballero.text = "" + DefensaActualCaballero;
            Money -= 15;
            DefensaActualCaballero += 5;
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
    //faltan los botones para la bruja, el arquero y el hada +
    //mensajes cuando se lleg� al m�ximo y opci�n para que en la vida no se apriete el boton
    //cuando tiene su vida maxima pero que cuando ya tenga menos vida si que se pueda

}
