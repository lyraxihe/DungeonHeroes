using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class VariablesGlobales : MonoBehaviour
{
    public static VariablesGlobales instance;

    public GameObject canvas;
    
    //// MENÚ PAUSA
    //public GameObject PrefabMenuPausaBorder;
    //public GameObject PrefabMenuPausaContainer;
    //public GameObject PrefabPausaText;
    //public GameObject PrefabContinuarButton;
    //public GameObject PrefabContinuarButtonText;
    //public GameObject PrefabSalirButton;
    //public GameObject PrefabSalirButtonText;

    //public GameObject ClonMenuPausaBorder;
    //public GameObject ClonMenuPausaContainer;
    ////public GameObject ClonContinuarButton;
    ////public GameObject ClonSalirButton;

    //public GameObject PausaText;
    //public GameObject ContinuarButton;
    //public GameObject ContinuarButtonText;
    //public GameObject SalirButton;
    //public GameObject SalirButtonText;
    public bool EscPressed;

    // BOSS
    public bool Boss;

    // ESTADÍSTICAS DEL KNIGHT
    public int KnightVidaTotal;              // Vida máxima del personaje
    public int KnightVidaActual;             // Vida actual del personaje
    public int KnightAtaqueActual;           // Ataque actual del personaje
    public int KnightAtaqueMax;              // Ataque máximo del personaje
    public int KnightDefensaActual;          // Defensa actual del personaje
    public int KnightDefensaMax;             // Defensa máxima del personaje

    // ESTADÍSTICAS DEL HEALER
    public int HealerVidaTotal;              // Vida máxima del personaje
    public int HealerVidaActual;             // Vida actual del personaje
    public int HealerAtaqueActual;           // Ataque actual del personaje
    public int HealerAtaqueMax;              // Ataque máximo del personaje
    public int HealerDefensaActual;          // Defensa actual del personaje
    public int HealerDefensaMax;             // Defensa máxima del personaje

    // ESTADÍSTICAS DEL SLIME
    public int SlimeVidaTotal;              // Vida máxima del personaje
    public int SlimeVidaActual;             // Vida actual del personaje
    public int SlimeAtaqueActual;           // Ataque actual del personaje
    public int SlimeAtaqueMax;              // Ataque máximo del personaje
    public int SlimeDefensaActual;          // Defensa actual del personaje
    public int SlimeDefensaMax;             // Defensa máxima del personaje

    // ESTADÍSTICAS DEL MAGE
    public int MageVidaTotal;              // Vida máxima del personaje
    public int MageVidaActual;             // Vida actual del personaje
    public int MageAtaqueActual;           // Ataque actual del personaje
    public int MageAtaqueMax;              // Ataque máximo del personaje
    public int MageDefensaActual;          // Defensa actual del personaje
    public int MageDefensaMax;             // Defensa máxima del personaje

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
        EscPressed = false;

        // Establece los atributos del Knight
        KnightVidaTotal = 75;
        KnightVidaActual = KnightVidaTotal;
        KnightAtaqueActual = 30;
        KnightAtaqueMax = 50;
        KnightDefensaActual = 5;
        KnightDefensaMax = 10;

        // Establece los atributos del Healer
        HealerVidaTotal = 100;
        HealerVidaActual = 100;
        HealerAtaqueActual = 5;
        HealerAtaqueMax = 50;
        HealerDefensaActual = 5;
        HealerDefensaMax = 10;

        // Establece los atributos del Slime
        SlimeVidaTotal = 150;
        SlimeVidaActual = 150;
        SlimeAtaqueActual = 20;
        SlimeAtaqueMax = 50;
        SlimeDefensaActual = 2;
        SlimeDefensaMax = 10;

        // Establece los atributos del Mage
        MageVidaTotal = 100;
        MageVidaActual = 100;
        MageAtaqueActual = 10;
        MageAtaqueMax = 50;
        MageDefensaActual = 7;
        MageDefensaMax = 10;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(!canvas.activeSelf);
            EscPressed = !EscPressed;

            //if (!EscPressed)
            //{
            //    MenuPausa();
            //}
            //else
            //{
            //    EscPressed = false;

            //    canvas.GetComponent<Canvas>().enabled = false;
            //}
        }
    }

    public void ContinuarPausa()
    {
        EscPressed = false;
        canvas.SetActive(!canvas.activeSelf);
    }
    //public void MenuPausa()
    //{
    //    EscPressed = true;

    //    //// MENU PAUSA
    //    //ClonMenuPausaBorder = Instantiate(PrefabMenuPausaBorder);
    //    //ClonMenuPausaBorder.transform.position = new Vector3(0, 0, -1);
    //    //ClonMenuPausaBorder.transform.localScale = new Vector3(9, 10, 1);
    //    ////ClonMenuPausaBorder.GetComponent<SpriteRenderer>().enabled = false;

    //    //ClonMenuPausaContainer = Instantiate(PrefabMenuPausaContainer);
    //    //ClonMenuPausaContainer.transform.position = new Vector3(0, 0, -2);
    //    //ClonMenuPausaContainer.transform.localScale = new Vector3(8.5f, 9.5f, 1);
    //    ////ClonMenuPausaContainer.GetComponent<SpriteRenderer>().enabled = false;

    //    //PausaText = Instantiate(PrefabPausaText);
    //    //PausaText.transform.parent = canvas.transform;
    //    ////PausaText.GetComponent<TextMeshPro>().enabled = true;
    //    //ContinuarButton = Instantiate(PrefabContinuarButton);
    //    //ContinuarButton.transform.parent = canvas.transform;
    //    ////ContinuarButton.GetComponent<Image>().enabled = true;
    //    //ContinuarButtonText = Instantiate(PrefabContinuarButtonText);
    //    //ContinuarButtonText.transform.parent = canvas.transform;
    //    ////ContinuarButtonText.GetComponent<TextMeshPro>().enabled = true;
    //    //SalirButton = Instantiate(PrefabSalirButton);
    //    //SalirButton.transform.parent = canvas.transform;
    //    ////SalirButton.GetComponent<Image>().enabled = true;
    //    //SalirButtonText = Instantiate(PrefabSalirButtonText);
    //    //SalirButtonText.transform.parent = canvas.transform;
    //    ////SalirButtonText.GetComponent<TextMeshPro>().enabled = true;
    //    canvas.GetComponent<Canvas>().enabled = true;
    //}
    public void SalirPausa()
    {
        EscPressed = false;
        canvas.SetActive(!canvas.activeSelf);
        SceneManager.LoadScene("MainMenu"); //abre la escena
    }

    ////private static Background instance = null;
    ////public static Background Instance { get { return instance; } } //creo el Singletons
}

   

   

