using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlPausa : MonoBehaviour
{
    public GameObject _CombatBackground;

    public GameObject ClonMenuPausaBorder;
    public GameObject ClonMenuPausaContainer;

    public TMP_Text PausaText;
    public Button ContinuarButton;
    public TMP_Text ContinuarButtonText;
    public Button SalirButton;
    public TMP_Text SalirButtonText;

    //public void ContinuarPausa()
    //{
    //    _CombatBackground.GetComponent<CombatBackground>().EscPressed = false;

    //    ClonMenuPausaBorder.GetComponent<SpriteRenderer>().enabled = false;

    //    ClonMenuPausaContainer.GetComponent<SpriteRenderer>().enabled = false;

    //    PausaText.enabled = false;
    //    ContinuarButton.image.enabled = false;
    //    ContinuarButtonText.enabled = false;
    //    SalirButton.image.enabled = false;
    //    SalirButtonText.enabled = false;
    //}

    //public void SalirPausa()
    //{
    //    SceneManager.LoadScene("MainMenu"); //abre la escena
    //}
}
