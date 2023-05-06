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



    void Start()
    {

        if (!VariablesGlobales.instance.KnightVivo)
        {
            HpCaballeroButton.interactable = false;
            ATQCaballeroButton.interactable = false;
            DEFCaballeroButton.interactable = false;
        }
        if (!VariablesGlobales.instance.SlimeVivo)
        {
            HpBrujaButton.interactable = false;
            ATQBrujaButton.interactable = false;
            DEFBrujaButton.interactable = false;
        }
        if (!VariablesGlobales.instance.MageVivo)
        {
            HpArqueroButton.interactable = false;
            ATQArqueroButton.interactable = false;
            DEFArqueroButton.interactable = false;
        }
        if (!VariablesGlobales.instance.HealerVivo)
        {
            HpHadaButton.interactable = false;
            ATQHadaButton.interactable = false;
            DEFHadaButton.interactable = false;
        }
        //botones del caballero
        if (VariablesGlobales.instance.KnightVidaActual == VariablesGlobales.instance.KnightVidaTotal)
        {
            HpCaballeroButton.interactable = false;
        }
        if (VariablesGlobales.instance.KnightAtaqueActual == VariablesGlobales.instance.AtaqueMax)
        {
            ATQCaballeroButton.interactable = false;
        }
        if (VariablesGlobales.instance.KnightDefensaActual == VariablesGlobales.instance.DefensaMax)
        {
            DEFCaballeroButton.interactable = false;
        }

        //botones de la bruja
        if (VariablesGlobales.instance.SlimeVidaActual == VariablesGlobales.instance.SlimeVidaTotal)
        {
            HpBrujaButton.interactable = false;
        }
        if (VariablesGlobales.instance.SlimeAtaqueActual == VariablesGlobales.instance.AtaqueMax)
        {
            ATQBrujaButton.interactable = false;
        }
        if (VariablesGlobales.instance.SlimeDefensaActual == VariablesGlobales.instance.DefensaMax)
        {
            DEFBrujaButton.interactable = false;
        }

        //botones del arquero
        if (VariablesGlobales.instance.MageVidaActual == VariablesGlobales.instance.MageVidaTotal)
        {
            HpArqueroButton.interactable = false;
        }
        if (VariablesGlobales.instance.MageAtaqueActual == VariablesGlobales.instance.AtaqueMax)
        {
            ATQArqueroButton.interactable = false;
        }
        if (VariablesGlobales.instance.MageDefensaActual == VariablesGlobales.instance.DefensaMax)
        {
            DEFArqueroButton.interactable = false;
        }

        //botones del hada
        if (VariablesGlobales.instance.HealerVidaActual == VariablesGlobales.instance.HealerVidaTotal)
        {
            HpHadaButton.interactable = false;
        }
        if (VariablesGlobales.instance.HealerAtaqueActual == VariablesGlobales.instance.AtaqueMax)
        {
            ATQHadaButton.interactable = false;
        }
        if (VariablesGlobales.instance.HealerDefensaActual == VariablesGlobales.instance.DefensaMax)
        {
            DEFHadaButton.interactable = false;
        }
    }

    void Update()
    {


    }

  
}
