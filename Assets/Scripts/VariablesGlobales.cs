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
    public GameObject canvasCreditos;
    //public RectTransform _canvas;
    public UnityEngine.Transform _canvas;

    //// MEN� PAUSA
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

    // ESTAD�STICAS DEL KNIGHT
    public int KnightVidaTotal;              // Vida m�xima del personaje
    public int KnightVidaActual;             // Vida actual del personaje
    public int KnightAtaqueActual;           // Ataque actual del personaje
    public int KnightAtaqueMax;              // Ataque m�ximo del personaje
    public int KnightDefensaActual;          // Defensa actual del personaje
    public int KnightDefensaMax;             // Defensa m�xima del personaje

    // ESTAD�STICAS DEL HEALER
    public int HealerVidaTotal;              // Vida m�xima del personaje
    public int HealerVidaActual;             // Vida actual del personaje
    public int HealerAtaqueActual;           // Ataque actual del personaje
    public int HealerAtaqueMax;              // Ataque m�ximo del personaje
    public int HealerDefensaActual;          // Defensa actual del personaje
    public int HealerDefensaMax;             // Defensa m�xima del personaje

    // ESTAD�STICAS DEL SLIME
    public int SlimeVidaTotal;              // Vida m�xima del personaje
    public int SlimeVidaActual;             // Vida actual del personaje
    public int SlimeAtaqueActual;           // Ataque actual del personaje
    public int SlimeAtaqueMax;              // Ataque m�ximo del personaje
    public int SlimeDefensaActual;          // Defensa actual del personaje
    public int SlimeDefensaMax;             // Defensa m�xima del personaje

    // ESTAD�STICAS DEL MAGE
    public int MageVidaTotal;              // Vida m�xima del personaje
    public int MageVidaActual;             // Vida actual del personaje
    public int MageAtaqueActual;           // Ataque actual del personaje
    public int MageAtaqueMax;              // Ataque m�ximo del personaje
    public int MageDefensaActual;          // Defensa actual del personaje
    public int MageDefensaMax;             // Defensa m�xima del personaje

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
        Positions = new GameObject[PositionsX.Length]; // Crea el array de prefabs CombatPosition en base al tama�o del array de Coordenadas

        SetPositions(PositionsX.Length);              // Crea la posiciones en donde ir� cada sala
        SetRooms(5);                                  // Coloca cada sala en su posici�n dentro del mapa

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
    private void SetPositions(int numPositions)
    {
        for (int i = 0; i < numPositions; i++) // Bucle for desde 0 al n�mero de posiciones que debe haber para ir coloc�ndolas
        {
            GameObject clon = Instantiate(RoomsPosition);                           // Crea un clon del prefab CombatPosition
            clon.transform.position = new Vector3(PositionsX[i], PositionsY[i], -1); // Coloca el clon en la primera posici�n que hay en los arrays de Coordenadas
            Positions[i] = clon;                                                     // A�ade el clon al array de prefabs CombatPosition
        }
    }
    private void SetRooms(int salas)
    {
        GameObject position, position_aux;
        int RoomType;                                         // Almacena el tipo de sala                                // Coordenadas de una posici�n
        int[] RoomsAmount = { 3, 1, };

        position_aux = Positions[2];

        if (position_aux.GetComponent<RoomsPosition>().Occupied == false)
        {
            position = Positions[2];
            GameObject clon = Instantiate(PrefabJefe);             // Crea un clon del prefab jefe
            clon.transform.parent = _canvas;
            clon.transform.position = Camera.main.WorldToScreenPoint(position.transform.position);   //cambia de posici�n de mundo a posici�n de c�mara          
            // Coloca el clon en la posici�n escogida
            position_aux.GetComponent<RoomsPosition>().Occupied = true;
            position.GetComponent<RoomsPosition>().Occupied = true; // Cambia esa posici�n a "ocupada"
        }

        for (int i = 0; i < salas - 1; i++) // Bucle for desde 0 hasta el n�mero de salas que hay en el nivel
        {
            do
            {
                RoomType = Random.Range(0, 2); // Elige aleatoriamente el tipo de sala para asignar
            } while (RoomsAmount[RoomType] == 0);

            do // Bucle While para controlar que la posici�n elegida aleatoriamente no est� ocupada, si lo est� elige otra posici�n
            {
                position = Positions[Random.Range(0, Positions.Length)]; // Selecciona una posici�n al azar del array de prefabs CombatPosition

            } while (IsPositionOccupied(position)); // Si esa posici�n est� ocupada (Occupied == true) vuelve a seleccionar otra, si no lo est�, continua



            if (RoomType == 0)                                           // Si la sala es combate
            {

                GameObject clon = Instantiate(PrefabCombate);             // Crea un clon del prefab combate
                clon.transform.parent = _canvas;
                clon.transform.position = Camera.main.WorldToScreenPoint(position.transform.position);                  // Coloca el clon en la posici�n escogida aleatoriamente
                position.GetComponent<RoomsPosition>().Occupied = true; // Cambia esa posici�n a "ocupada"
                RoomsAmount[0] -= 1;
            }
            else /*(RoomType == 1)*/                                     // Si la sala es tienda
            {

                GameObject clon = Instantiate(PrefabTienda);             // Crea un clon del prefab tienda
                clon.transform.parent = _canvas;
                clon.transform.position = Camera.main.WorldToScreenPoint(position.transform.position);                  // Coloca el clon en la posici�n escogida aleatoriamente 
                position.GetComponent<RoomsPosition>().Occupied = true; // Cambia esa posici�n a "ocupada"
                RoomsAmount[1] -= 1;
            }
            bool IsPositionOccupied(GameObject position)
            {
                if (position.GetComponent<RoomsPosition>().Occupied == true) // Si la variable "Occupied" es "true" es que est� ocupada
                    return true;                                             // Devuelve "true" por que la posici�n est� ocupada

                return false;                                                // Devuelve "false" si "Occupied" es "false" y por tanto est� libre
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
            void SalirPausa()
            {
                EscPressed = false;
                canvas.SetActive(!canvas.activeSelf);
                SceneManager.LoadScene("MainMenu"); //abre la escena
            }

            ////private static Background instance = null;
            ////public static Background Instance { get { return instance; } } //creo el Singletons
        }
    }
}

   

   

