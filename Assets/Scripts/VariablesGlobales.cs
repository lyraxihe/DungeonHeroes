using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;
using System;
using Random = UnityEngine.Random;

public class VariablesGlobales : MonoBehaviour
{
    public static VariablesGlobales instance;

    public GameObject canvas;
    public GameObject canvasCreditos;
    public RectTransform _canvas;
    public GameObject RoomCanvas;
   // private string SceneMain;
    

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

    public GameObject RoomsPosition;          // Prefab RoomsPosition
    public GameObject PrefabCombate;          // Prefab sala 1 - combate
    public GameObject PrefabTienda;           // Prefab sala 2 - tienda
    public GameObject PrefabJefe;             // Prefab sala del medio - jefe
    
    private float[] PositionsX = { 0, -3.5f, 0, 3.5f, 0 }; // Array de posiciones Coordenadas X del prefab RoomPosition
    private float[] PositionsY = { 3.5f, 0, 0, 0, -3.5f };  // Array de posiciones Coordenadas Y del prefab RoomPosition
    private GameObject[] Positions;           // Array de prefabs CombatPosition

    public bool EscPressed;

    // BOSS
    public bool Boss;

    //objetos y estadísticas generales
    public int Money;
    public int AtaqueMax;
    public int DefensaMax;


    // ESTADÍSTICAS DEL KNIGHT
    public int KnightVidaTotal;               // Vida máxima del personaje
    public int KnightVidaActual;              // Vida actual del personaje
    public int KnightAtaqueActual;            // Ataque actual del personaje
    public int KnightAtaqueMax;               // Ataque máximo del personaje
    public int KnightDefensaActual;           // Defensa actual del personaje
    public int KnightDefensaMax;              // Defensa máxima del personaje
    public int KnightDefensaActualPercentage; // Porcentaje de defensa actual del personaje

    // ESTADÍSTICAS DEL HEALER
    public int HealerVidaTotal;               // Vida máxima del personaje
    public int HealerVidaActual;              // Vida actual del personaje
    public int HealerAtaqueActual;            // Ataque actual del personaje
    public int HealerAtaqueMax;               // Ataque máximo del personaje
    public int HealerDefensaActual;           // Defensa actual del personaje
    public int HealerDefensaMax;              // Defensa máxima del personaje
    public int HealerDefensaActualPercentage; // Porcentaje de defensa actual del personaje

    // ESTADÍSTICAS DEL SLIME
    public int SlimeVidaTotal;               // Vida máxima del personaje
    public int SlimeVidaActual;              // Vida actual del personaje
    public int SlimeAtaqueActual;            // Ataque actual del personaje
    public int SlimeAtaqueMax;               // Ataque máximo del personaje
    public int SlimeDefensaActual;           // Defensa actual del personaje
    public int SlimeDefensaMax;              // Defensa máxima del personaje
    public int SlimeDefensaActualPercentage; // Porcentaje de defensa actual del personaje

    // ESTADÍSTICAS DEL MAGE
    public int MageVidaTotal;               // Vida máxima del personaje
    public int MageVidaActual;              // Vida actual del personaje
    public int MageAtaqueActual;            // Ataque actual del personaje
    public int MageAtaqueMax;               // Ataque máximo del personaje
    public int MageDefensaActual;           // Defensa actual del personaje
    public int MageDefensaMax;              // Defensa máxima del personaje
    public int MageDefensaActualPercentage; // Porcentaje de defensa actual del personaje

    // ESTADÍSTICAS DEL BOSS
    public int BossVidaTotal;               // Vida máxima del personaje
    public int BossVidaActual;              // Vida actual del personaje
    public int BossAtaqueActual;            // Ataque actual del personaje
    public int BossAtaqueMax;               // Ataque máximo del personaje
    public int BossDefensaActual;           // Defensa actual del personaje
    public int BossDefensaMax;              // Defensa máxima del personaje
    public int BossDefensaActualPercentage; // Porcentaje de defensa actual del personaje

    private void Awake() //carga los datos guardados
    {
        Debug.Log("Entrando en Awake");
        if (instance == null)
        {
            Debug.Log("Awake: No destruyo nada.");
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            if (!EscPressed)
            {
                if (instance != this)
                {
                    Debug.Log("Awake: Destruyo el GameObject: " + gameObject);
                    Destroy(gameObject);
                }   
            }
        }

        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            Debug.Log("Awake: La escena es Main Menu");
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Positions = new GameObject[PositionsX.Length]; // Crea el array de prefabs CombatPosition en base al tamaño del array de Coordenadas

        SetPositions(PositionsX.Length);              // Crea la posiciones en donde irá cada sala
        SetRooms(5);                                  // Coloca cada sala en su posición dentro del mapa

        Boss = false;
        EscPressed = false;

        // Establece los atributos y valores generales
        Money = 200; // modificar luego !!!!!!
        AtaqueMax = 50;
        DefensaMax = 10;

        // Establece los atributos del Knight
        KnightVidaTotal = 75;
        KnightVidaActual = KnightVidaTotal;
        KnightAtaqueActual = 30;
        KnightAtaqueMax = 50;
        KnightDefensaActual = 5;
        KnightDefensaMax = 10;
        KnightDefensaActualPercentage = KnightDefensaActual * 5;

        // Establece los atributos del Healer
        HealerVidaTotal = 100;
        HealerVidaActual = 100;
        HealerAtaqueActual = 5;
        HealerAtaqueMax = 50;
        HealerDefensaActual = 5;
        HealerDefensaMax = 10;
        HealerDefensaActualPercentage = HealerDefensaActual * 5;

        // Establece los atributos del Slime
        SlimeVidaTotal = 150;
        SlimeVidaActual = 150;
        SlimeAtaqueActual = 20;
        SlimeAtaqueMax = 50;
        SlimeDefensaActual = 2;
        SlimeDefensaMax = 10;
        SlimeDefensaActualPercentage = SlimeDefensaActual * 5;

        // Establece los atributos del Mage
        MageVidaTotal = 100;
        MageVidaActual = 100;
        MageAtaqueActual = 10;
        MageAtaqueMax = 50;
        MageDefensaActual = 7;
        MageDefensaMax = 10;
        MageDefensaActualPercentage = MageDefensaActual * 5;

        // Establece los atributos del Boss
        BossVidaTotal = 200;
        BossVidaActual = 200;
        BossAtaqueActual = 30;
        BossAtaqueMax = 50;
        BossDefensaActual = 5;
        BossDefensaMax = 10;
        BossDefensaActualPercentage = BossDefensaActual * 5;
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

        //Scene MainScene = UnityEngine.SceneManagement.Scene.name;
        //    SceneManager.Main();
        //bool GuardarEscena = false;
        //if (!GuardarEscena)
        //{
        //    MainScene = SceneManager.GetActiveScene();
        //    GuardarEscena = true;
        //}

        //string ActualScene = SceneManager.GetActiveScene().ToString();
        //Scene ActualScene = SceneManager.GetActiveScene();
        // string escenaactual = SceneManager.GetActiveScene().ToString();
        //if (/*SceneManager.GetActiveScene().ToString()*/ escenaactual == "Main")

        string CurrentSceneName = SceneManager.GetActiveScene().name;
        if (CurrentSceneName == "Main") // comprueba si la escena actual es la del mapa
        {
            RoomCanvas.SetActive(true); //se activan y muestran los botones de las salas al no estar en la escena del mapa
        }
        else
        {
            RoomCanvas.SetActive(false); //se desactivan y muestran los botones de las salas al no estar en la escena del mapa
        }
      
    }

    public void ContinuarPausa()
    {
        EscPressed = false;
        canvas.SetActive(!canvas.activeSelf);
    }
    private void SetPositions(int numPositions)
    {
        for (int i = 0; i < numPositions; i++) // Bucle for desde 0 al número de posiciones que debe haber para ir colocándolas
        {
            GameObject clon = Instantiate(RoomsPosition);                           // Crea un clon del prefab CombatPosition
            clon.transform.position = new Vector3(PositionsX[i], PositionsY[i], -1); // Coloca el clon en la primera posición que hay en los arrays de Coordenadas
            Positions[i] = clon;                                                     // Añade el clon al array de prefabs CombatPosition
        }
    }
    private void SetRooms(int salas)
    {
        GameObject position, position_aux;
        int RoomType;                                         // Almacena el tipo de sala                                // Coordenadas de una posición
        int[] RoomsAmount = { 3, 1, };

        position_aux = Positions[2];

        if (position_aux.GetComponent<RoomsPosition>().Occupied == false)
        {
            position = Positions[2];
            GameObject clon = Instantiate(PrefabJefe);             // Crea un clon del prefab jefe
            clon.transform.parent = _canvas;
            clon.transform.position = Camera.main.WorldToScreenPoint(position.transform.position);   //cambia de posición de mundo a posición de cámara          
            // Coloca el clon en la posición escogida
            position_aux.GetComponent<RoomsPosition>().Occupied = true;
            position.GetComponent<RoomsPosition>().Occupied = true; // Cambia esa posición a "ocupada"
        }

        for (int i = 0; i < salas - 1; i++) // Bucle for desde 0 hasta el número de salas que hay en el nivel
        {
            do
            {
                RoomType = UnityEngine.Random.Range(0, 2); // Elige aleatoriamente el tipo de sala para asignar
            } while (RoomsAmount[RoomType] == 0);

            do // Bucle While para controlar que la posición elegida aleatoriamente no esté ocupada, si lo está elige otra posición
            {
                position = Positions[Random.Range(0, Positions.Length)]; // Selecciona una posición al azar del array de prefabs CombatPosition

            } while (IsPositionOccupied(position)); // Si esa posición está ocupada (Occupied == true) vuelve a seleccionar otra, si no lo está, continua



            if (RoomType == 0)                                           // Si la sala es combate
            {

                GameObject clon = Instantiate(PrefabCombate);             // Crea un clon del prefab combate
                clon.transform.parent = _canvas;
                clon.transform.position = Camera.main.WorldToScreenPoint(position.transform.position);                  // Coloca el clon en la posición escogida aleatoriamente
                position.GetComponent<RoomsPosition>().Occupied = true; // Cambia esa posición a "ocupada"
                RoomsAmount[0] -= 1;
            }
            else /*(RoomType == 1)*/                                     // Si la sala es tienda
            {

                GameObject clon = Instantiate(PrefabTienda);             // Crea un clon del prefab tienda
                clon.transform.parent = _canvas;
                clon.transform.position = Camera.main.WorldToScreenPoint(position.transform.position);                  // Coloca el clon en la posición escogida aleatoriamente 
                position.GetComponent<RoomsPosition>().Occupied = true; // Cambia esa posición a "ocupada"
                RoomsAmount[1] -= 1;
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
        }
    }

    bool IsPositionOccupied(GameObject position)
    {
        if (position.GetComponent<RoomsPosition>().Occupied == true) // Si la variable "Occupied" es "true" es que está ocupada
            return true;                                             // Devuelve "true" por que la posición está ocupada

        return false;                                                // Devuelve "false" si "Occupied" es "false" y por tanto está libre
    }

    void SalirPausa()
    {
        EscPressed = false;
        canvas.SetActive(!canvas.activeSelf);
        SceneManager.LoadScene("MainMenu"); //abre la escena
    }

    ////private static Background instance = null;
    ////public static Background Instance { get { return instance; } } //creo el Singletons
}





