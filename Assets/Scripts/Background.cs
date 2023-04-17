using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public GameObject CombatPosition;   // Prefab CombatPosition
    public GameObject PrefabCombate;     // Prefab sala 1 - combate
    public GameObject PrefabTienda;     // Prefab sala 2 - tienda
    public GameObject PrefabCofre;      // Prefab sala 3 - cofre
    public GameObject PrefabJefe; // Prefab sala del medio - jefe
    public RectTransform canvas;


    // - - - - - - - - - - - - - - - - - - lyrita intento - - - - - - - - - - - - - - - - - - - - - - - - - - - //
    public int Money; //usar este al terminar la batalla para que le de al jugador (sumarle 100 como ejemplo)
    public string MoneyName = "Money";
    public int HpKnight;
    public string HpName = "HpKnight";
    // - - - - - - - - - - - - - - - - - - lyrita intento - - - - - - - - - - - - - - - - - - - - - - - - - - - //


    private float[] PositionsX = { 0, -3.5f, 0, 3.5f, 0 }; // Array de posiciones Coordenadas X del prefab CombatPosition
    private float[] PositionsY = { 3.5f, 0, 0, 0, -3.5f };  // Array de posiciones Coordenadas Y del prefab CombatPosition
    private GameObject[] Positions;                                                          // Array de prefabs CombatPosition

    // Start is called before the first frame update
    void Start()
    {
        Positions = new GameObject[PositionsX.Length]; // Crea el array de prefabs CombatPosition en base al tama�o del array de Coordenadas

        SetPositions(PositionsX.Length);              // Coloca las posiciones donde ir�n los personajes en pantalla
        SetRooms(5);                             // Coloca los enemigos en las posiciones del combate
        //RefreshUI();

    }

    // Update is called once per frame
    void Update()
    {
        // SALIR DEL JUEGO
        /***********************************/
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    Application.Quit();
        /***********************************/
    }

    // - - - - - - - - - - - - - - - - - - lyrita intento - - - - - - - - - - - - - - - - - - - - - - - - - - - //
    private void Awake() //carga los datos guardados
    {
        LoadData();
        if (Instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        SaveData();
    }
    private void RefreshUI()
    {
       
        //userInterface.RefreshMoney(Money);
        //userInterface.RefreshHpKnight(HpKnight);
    }

    private void SaveData()
    {

        PlayerPrefs.SetInt(MoneyName, Money); //(primero el string y luego el int)
        PlayerPrefs.SetInt(HpName, HpKnight);

    }

    private void LoadData()
    {
        Money = PlayerPrefs.GetInt(MoneyName, Money);
        HpKnight = PlayerPrefs.GetInt(HpName, HpKnight);

    }
    // - - - - - - - - - - - - - - - - - - lyrita intento - - - - - - - - - - - - - - - - - - - - - - - - - - - //

    private void SetPositions(int numPositions)
    {
        for (int i = 0; i < numPositions; i++) // Bucle for desde 0 al n�mero de posiciones que debe haber para ir coloc�ndolas
        {
            GameObject clon = Instantiate(CombatPosition);                           // Crea un clon del prefab CombatPosition
            clon.transform.position = new Vector3(PositionsX[i], PositionsY[i], -1); // Coloca el clon en la primera posici�n que hay en los arrays de Coordenadas
            Positions[i] = clon;                                                     // A�ade el clon al array de prefabs CombatPosition
        }
    }

    /****************************************************************************************
     * Funci�n: SetEnemies                                                                  *
     * Uso: Coloca los enemigos de manera aleatoria en las posiciones que hay en el combate *
     * Variables entrada:                                                                   *
     *      - minEnemies: N�mero m�nimo de enemigos que habr� en el combate                 *
     *      - maxEnemies: N�mero m�ximo de enemigos que habr� en el combate                 *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void SetRooms(int salas)
    {
        GameObject position, position_aux;
        int RoomType;                                         // Almacena el tipo de sala                                // Coordenadas de una posici�n
        int[] RoomsAmount = { 2, 1, 1 };

        position_aux = Positions[2];

        if (position_aux.GetComponent<RoomsPosition>().Occupied == false)
        {
            position = Positions[2];
            GameObject clon = Instantiate(PrefabJefe);             // Crea un clon del prefab jefe
            clon.transform.parent = canvas;
            clon.transform.position = Camera.main.WorldToScreenPoint(position.transform.position);   //cambia de posici�n de mundo a posici�n de c�mara          
            // Coloca el clon en la posici�n escogida
            position_aux.GetComponent<RoomsPosition>().Occupied = true;
            position.GetComponent<RoomsPosition>().Occupied = true; // Cambia esa posici�n a "ocupada"
        }

        for (int i = 0; i < salas - 1; i++) // Bucle for desde 0 hasta el n�mero de salas que hay en el nivel
        {
            do
            {
                RoomType = Random.Range(0, 3); // Elige aleatoriamente el tipo de sala para asignar
            } while (RoomsAmount[RoomType] == 0);

            do // Bucle While para controlar que la posici�n elegida aleatoriamente no est� ocupada, si lo est� elige otra posici�n
            {
                position = Positions[Random.Range(0, Positions.Length)]; // Selecciona una posici�n al azar del array de prefabs CombatPosition

            } while (IsPositionOccupied(position)); // Si esa posici�n est� ocupada (Occupied == true) vuelve a seleccionar otra, si no lo est�, continua



            if (RoomType == 0)                                           // Si la sala es combate
            {

                GameObject clon = Instantiate(PrefabCombate);             // Crea un clon del prefab combate
                clon.transform.parent = canvas;
                clon.transform.position = Camera.main.WorldToScreenPoint(position.transform.position);                  // Coloca el clon en la posici�n escogida aleatoriamente
                position.GetComponent<RoomsPosition>().Occupied = true; // Cambia esa posici�n a "ocupada"
                RoomsAmount[0] -= 1;
            }
            else if (RoomType == 1)                                     // Si la sala es tienda
            {

                GameObject clon = Instantiate(PrefabTienda);             // Crea un clon del prefab tienda
                clon.transform.parent = canvas;
                clon.transform.position = Camera.main.WorldToScreenPoint(position.transform.position);                  // Coloca el clon en la posici�n escogida aleatoriamente 
                position.GetComponent<RoomsPosition>().Occupied = true; // Cambia esa posici�n a "ocupada"
                RoomsAmount[1] -= 1;
            }
            else                                     // Si el enemigo elegido es un Slime
            {
                GameObject clon = Instantiate(PrefabCofre);              // Crea un clon del prefab cofre
                clon.transform.parent = canvas;
                clon.transform.position = Camera.main.WorldToScreenPoint(position.transform.position);                   // Coloca el clon en la posici�n escogida aleatoriamente 
                position.GetComponent<RoomsPosition>().Occupied = true; // Cambia esa posici�n a "ocupada"
                RoomsAmount[2] -= 1;
            }

        }
    }

    /****************************************************************************************
     * Funci�n: IsPositionOccupied                                                          *
     * Uso: Comprueba si el CombatPosition pasado como argumento est� ocupado o no          *
     * Variables entrada:                                                                   *
     *      - position: prefab CombatPosition a comprobar                                   *
     * Return:                                                                              *
     *      - True: Si la posici�n est� ocupada                                             *
     *      - False: Si la posici�n est� libre                                              *
     ****************************************************************************************/
    private bool IsPositionOccupied(GameObject position)
    {
        if (position.GetComponent<RoomsPosition>().Occupied == true) // Si la variable "Occupied" es "true" es que est� ocupada
            return true;                                             // Devuelve "true" por que la posici�n est� ocupada

        return false;                                                // Devuelve "false" si "Occupied" es "false" y por tanto est� libre
    }

    private static Background instance = null;
    public static Background Instance { get { return instance; } } //creo el Singletons

    
}
