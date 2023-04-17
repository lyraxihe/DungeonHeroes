using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject CombatBackground;

    // MENÚ PAUSA
    public GameObject PrefabMenuPausaBorder;
    public GameObject PrefabMenuPausaContainer;

    public GameObject ClonMenuPausaBorder;
    public GameObject ClonMenuPausaContainer;

    public TMP_Text PausaText;
    public Button ContinuarButton;
    public TMP_Text ContinuarButtonText;
    public Button SalirButton;
    public TMP_Text SalirButtonText;

    public bool EscPressed;

    // Start is called before the first frame update
    void Start()
    {
        GameObject clon = Instantiate(CombatBackground); // Crea el fondo que tendrá el Combate
        clon.transform.position = new Vector3(0, 0, 0); // Coloca el fondo en (0, 0, 0)

        // MENU PAUSA
        EscPressed = false;
        
        ClonMenuPausaBorder = Instantiate(PrefabMenuPausaBorder);
        ClonMenuPausaBorder.transform.position = new Vector3(0, 0, -1);
        ClonMenuPausaBorder.transform.localScale = new Vector3(8, 9, 1);
        ClonMenuPausaBorder.GetComponent<SpriteRenderer>().enabled = false;

        ClonMenuPausaContainer = Instantiate(PrefabMenuPausaContainer);
        ClonMenuPausaContainer.transform.position = new Vector3(0, 0, -2);
        ClonMenuPausaContainer.transform.localScale = new Vector3(7.5f, 8.5f, 1);
        ClonMenuPausaContainer.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // MENU DE PAUSA
        /***********************************/
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!EscPressed)
            {
                MenuPausa();
            }
            else
            {
                EscPressed = false;

                ClonMenuPausaBorder.GetComponent<SpriteRenderer>().enabled = false;

                ClonMenuPausaContainer.GetComponent<SpriteRenderer>().enabled = false;

                PausaText.enabled = false;
                ContinuarButton.image.enabled = false;
                ContinuarButtonText.enabled = false;
                SalirButton.image.enabled = false;
                SalirButtonText.enabled = false;
            }
        }
    }

    public void MenuPausa()
    {
        EscPressed = true;

        ClonMenuPausaBorder.GetComponent<SpriteRenderer>().enabled = true;

        ClonMenuPausaContainer.GetComponent<SpriteRenderer>().enabled = true;

        PausaText.enabled = true;
        ContinuarButton.image.enabled = true;
        ContinuarButtonText.enabled = true;
        SalirButton.image.enabled = true;
        SalirButtonText.enabled = true;
    }

    public void ContinuarPausa()
    {
        EscPressed = false;

        ClonMenuPausaBorder.GetComponent<SpriteRenderer>().enabled = false;

        ClonMenuPausaContainer.GetComponent<SpriteRenderer>().enabled = false;

        PausaText.enabled = false;
        ContinuarButton.image.enabled = false;
        ContinuarButtonText.enabled = false;
        SalirButton.image.enabled = false;
        SalirButtonText.enabled = false;
    }

    public void SalirPausa()
    {
        SceneManager.LoadScene("MainMenu"); //abre la escena
    }
}
