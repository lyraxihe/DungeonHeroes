using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class VariablesGlobales : MonoBehaviour
{
    public static VariablesGlobales instance;
    
    // MENÚ PAUSA
    public GameObject PrefabMenuPausaBorder;
    public GameObject PrefabMenuPausaContainer;
    public GameObject PrefabPausaText;
    public GameObject PrefabContinuarButton;
    public GameObject PrefabContinuarButtonText;
    public GameObject PrefabSalirButton;
    public GameObject PrefabSalirButtonText;

    public GameObject ClonMenuPausaBorder;
    public GameObject ClonMenuPausaContainer;
    //public GameObject ClonContinuarButton;
    //public GameObject ClonSalirButton;

    public GameObject PausaText;
    public GameObject ContinuarButton;
    public GameObject ContinuarButtonText;
    public GameObject SalirButton;
    public GameObject SalirButtonText;
    public bool EscPressed;

    // BOSS
    public bool Boss;

    private void Awake() //carga los datos guardados
    {
        instance = this;

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        Boss = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!EscPressed)
            {
                MenuPausa();
            }
            else
            {
                EscPressed = false;

                Destroy(ClonMenuPausaBorder);
                Destroy(ClonMenuPausaContainer);

                Destroy(PausaText);
                Destroy(ContinuarButton);
                Destroy(ContinuarButtonText);
                Destroy(SalirButton);
                Destroy(SalirButtonText);
            }
        }
    }
    public void ContinuarPausa()
    {
        EscPressed = false;

        Destroy(ClonMenuPausaBorder);
        Destroy(ClonMenuPausaContainer);

        Destroy(PausaText);
        Destroy(ContinuarButton);
        Destroy(ContinuarButtonText);
        Destroy(SalirButton);
        Destroy(SalirButtonText);
    }
    public void MenuPausa()
    {
        EscPressed = true;

        // MENU PAUSA
        ClonMenuPausaBorder = Instantiate(PrefabMenuPausaBorder);
        ClonMenuPausaBorder.transform.position = new Vector3(0, 0, -1);
        ClonMenuPausaBorder.transform.localScale = new Vector3(9, 10, 1);
        //ClonMenuPausaBorder.GetComponent<SpriteRenderer>().enabled = false;

        ClonMenuPausaContainer = Instantiate(PrefabMenuPausaContainer);
        ClonMenuPausaContainer.transform.position = new Vector3(0, 0, -2);
        ClonMenuPausaContainer.transform.localScale = new Vector3(8.5f, 9.5f, 1);
        //ClonMenuPausaContainer.GetComponent<SpriteRenderer>().enabled = false;

        PausaText = Instantiate(PrefabPausaText);
        //PausaText.GetComponent<TextMeshPro>().enabled = true;
        ContinuarButton = Instantiate(PrefabContinuarButton);
        //ContinuarButton.GetComponent<Image>().enabled = true;
        ContinuarButtonText = Instantiate(PrefabContinuarButtonText);
        //ContinuarButtonText.GetComponent<TextMeshPro>().enabled = true;
        SalirButton = Instantiate(PrefabSalirButton);
        //SalirButton.GetComponent<Image>().enabled = true;
        SalirButtonText = Instantiate(PrefabSalirButtonText);
        //SalirButtonText.GetComponent<TextMeshPro>().enabled = true;
    }
    public void SalirPausa()
    {
        SceneManager.LoadScene("MainMenu"); //abre la escena
    }

    //private static Background instance = null;
    //public static Background Instance { get { return instance; } } //creo el Singletons
}

   

   

