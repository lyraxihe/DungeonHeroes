using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public GameObject UIEstadisticaVida;
    public GameObject UIEstadisticaAtaque;
    public GameObject UIEstadisticaDefensa;

    public void OnMouseEnter()
    {
        if (gameObject.tag == "VidaImagen")
            UIEstadisticaVida.SetActive(true);
        else if (gameObject.tag == "AtaqueImagen")
            UIEstadisticaAtaque.SetActive(true);
        else if (gameObject.tag == "DefensaImagen")
            UIEstadisticaDefensa.SetActive(true);

        //UIEstadisticaVida.SetActive(true);
        //UIEstadisticaAtaque.SetActive(true);
        //UIEstadisticaDefensa.SetActive(true);
    }

    public void OnMouseExit()
    {
        UIEstadisticaVida.SetActive(false);
        UIEstadisticaAtaque.SetActive(false);
        UIEstadisticaDefensa.SetActive(false);
    }
}
