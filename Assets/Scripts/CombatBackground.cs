using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatBackground : MonoBehaviour
{
    public GameObject _CombatBackground;        // Combate
    
    // PREFABS
    public GameObject CombatPosition;           // Prefab CombatPosition
    public GameObject PrefabEnemyKnight;        // Prefab Personaje 1 - Caballero Enemigo
    public GameObject PrefabPlayerKnight;       // Prefab Personaje 1 - Caballero del Jugador
    public GameObject PrefabEnemyHealer;        // Prefab Personaje 2 - Healer Enemigo
    public GameObject PrefabPlayerHealer;       // Prefab Personaje 2 - Healer del Jugador
    public GameObject PrefabEnemySlime;         // Prefab Personaje 3 - Slime Enemigo
    public GameObject PrefabPlayerSlime;        // Prefab Personaje 3 - Slime del Jugador
    public GameObject PrefabEnemyMage;          // Prefab Personaje 4 - Mago Enemigo
    public GameObject PrefabPlayerMage;         // Prefab Personaje 4 - Mago del Jugador
    public GameObject PrefabStartBattleButton;  // Prefab StartBattleButton
    public GameObject PrefabTextoTurno;         // Prefab Texto del turno

    // CLONES
    private GameObject[] Enemies;               // Array de clones de los enemigos
    public GameObject[] Aliados;               // Array de clones de los personajes del jugador
    private GameObject ClonStartBattleButton;   // Clon del bot�n de �Comenzar Batalla!
    private GameObject ClonTextoTurno;          // Clon del texto "Turno de Batalla"

    // POSICIONES
    private float[] PositionsX = {0, -5.5f, -1.5f, 1.5f, 5.5f, -2.5f, 2.5f, 0, -3.5f, 3.5f}; // Array de posiciones Coordenadas X del prefab CombatPosition
    private float[] PositionsY = {3.5f, 1.5f, 1.5f, 1.5f, 1.5f, 0, 0, -1.5f, -3.5f, -3.5f};  // Array de posiciones Coordenadas Y del prefab CombatPosition
    public GameObject[] Positions;                                                           // Array de prefabs CombatPosition

    // PERSONAJES JUGADOR
    private float[] AliadoColocarX = {9, 9, 9, 9};     // Array de coordenadas X de la interfaz para colocar los personajes del jugador
    private float[] AliadoColocarY = {3, 1, -1, -3};   // Array de coordenadas Y de la interfaz para colocar los personajes del jugador

    // ATRIBUTOS VARIOS
    public bool StartBattle;                                               // Booleano que controla cuando el bot�n de �Comenzar Batalla! es pulsado
    public string TurnoBatalla;                                            // Nombre del turno del Jugador o el Enemigo
    private bool BoolTurnoBatalla;                                         // Booleano que controla que el texto "Turno de Batalla" se cree una s�la vez
    private bool[] AliadosPositionStatus = { false, false, false, false }; // Array de booleanos para ver que todos los aliados han sido colocados en el mapa de combate
    private bool CambiarTurno = true;
    public bool EnemigoParaAtacar = false;
    public int ContHabilidadSlime;                                         // Contador para saber cu�ntos turnos faltan para que termine la habilidad del Slime
    public int ContHabilidadMage;                                          // Contador para saber cu�ntos turnos faltan para que termine la habilidad del Mage
    public GameObject[] CharacterInterface;                                     // Array de info del Personaje del Jugador

    // Start is called before the first frame update
    void Start()
    {
        Positions = new GameObject[PositionsX.Length]; // Crea el array de prefabs CombatPosition en base al tama�o del array de Coordenadas
        Aliados = new GameObject[4];                   // Crea el array de personajes del jugador con tama�o 4 
        StartBattle = false;                           // El combate se incializa primero en fase de planificaci�n
        TurnoBatalla = "Jugador";                      // Establece que el primer turno de la batalla ser� para el jugador
        BoolTurnoBatalla = false;                      // De momento se puede crear el Texto "Turno de Batalla"
        CharacterInterface = new GameObject[10];            // Incializa el array de info del Personaje del JUgador

        ContHabilidadSlime = 0;

        SetPositions(PositionsX.Length);               // Coloca las posiciones donde ir�n los personajes en pantalla
        SetArrayPositions();                           // Almacena en cada posici�n el array de posiciones
        SetPositionsToMove();                          // Establece las posiciones a las que se puede mover cada posici�n
        SetEnemies(2, 4);                              // Coloca los enemigos en las posiciones del combate
        
        // Bucle para almacenar el array de enemigos en cada posici�n
        for (int i = 0; i < Positions.Length; i++)
            Positions[i].GetComponent<CombatPosition>().Enemies = Enemies;

        SetAliadosColocar();                           // Crea la interfaz para colocar los personajes del jugador
        CreateStartBattleButton();                     // Crea el bot�n de �Comenzar Batalla!
    }

    // Update is called once per frame
    void Update()
    {
        if(!BoolTurnoBatalla)
            StartBattle = ClonStartBattleButton.GetComponent<StartBattleButton>().GetStartBattleStatus();

        BattlePhase();                                 // Actualiza el estado del Combate
        UpdateTurn();                                  // Actualiza el turno del Combate
        StatusAliadosPosition();                       // Comprueba que todos los personajes del Jugador han sido colocados en el mapa de combate
        UpdateOtherScripts();                          // Actualiza constantemente otros scripts con variables
        
        if(StartBattle)
            StatusCharacters();

        if (TurnoBatalla == "Enemigo")
            EnemyTurn();                               // Realiza las acciones de la IA cuando es el turno del enemigo
    }

    /****************************************************************************************
     * Funci�n: SetPositions                                                                *
     * Uso: Coloca las posiciones (CombatPositions) donde ir�n los personajes en el combate *
     * Variables entrada:                                                                   *
     *      - numPositions: N�mero de posiciones que habr� en el combate                    *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void SetPositions(int numPositions)
    {
        for(int i = 0; i < numPositions; i++) // Bucle for desde 0 al n�mero de posiciones que debe haber para ir coloc�ndolas
        {
            GameObject clon = Instantiate(CombatPosition);                             // Crea un clon del prefab CombatPosition
            clon.transform.position = new Vector3(PositionsX[i], PositionsY[i], 2);    // Coloca el clon en la primera posici�n que hay en los arrays de Coordenadas
            clon.GetComponent<CombatPosition>()._CombatBackground = _CombatBackground; // Almacena el combate
            clon.GetComponent<CombatPosition>()._Position = clon;                      // Almacena la posici�n en si misma par aluego tener accesos
            Positions[i] = clon;                                                       // A�ade el clon al array de prefabs CombatPosition
        }
    }

    /****************************************************************************************
     * Funci�n: SetPositionsToMove                                                          *
     * Uso: Establece las posiciones a las que se puede mover cada posici�n                 *
     * Variables entrada: Nada.                                                             *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void SetPositionsToMove()
    {
        for(int i = 0; i < Positions.Length; i++) // Recorre el array de posiciones
        {
            // Se estblecen las posiciones a las que se puede mover el personaje desde cada posici�n

            if (i == 0)      // Posici�n 1
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(1, 2, 3, 4);
            else if (i == 1) // Posici�n 2
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(0, 2, 5, 8);
            else if (i == 2) // Posici�n 3
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(0, 1, 3, 5);
            else if (i == 3) // Posici�n 4
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(0, 2, 4, 6);
            else if (i == 4) // Posici�n 5
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(0, 3, 6, 9);
            else if (i == 5) // Posici�n 6
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(1, 2, 7, 8);
            else if (i == 6) // Posici�n 7
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(3, 4, 7, 9);
            else if (i == 7) // Posici�n 8
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(5, 6, 8, 9);
            else if (i == 8) // Posici�n 9
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(1, 5, 7, 9);
            else             // Posici�n 10
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(4, 6, 7, 8);
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
    private void SetEnemies(int minEnemies, int maxEnemies)
    {
        GameObject position;                                       // Posici�n donde se colocar� al enemigo
        int numEnemies = Random.Range(minEnemies, maxEnemies + 1); // Establece aleatoriamente el n�mero de enemigos que habr� en el combate con un m�nimo y un m�ximo
        int enemyType;                                             // Almacena el tipo de enemigo que se quiere colocar
        Vector3 positionVector;                                    // Coordenadas de una posici�n
        int i;

        Enemies = new GameObject[numEnemies];                      // Crea el array de enemigos con tama�o en base al n�mero de enemigos creados aleatoriamente

        for(i = 0; i < numEnemies; i++) // Bucle for desde 0 hasta el n�mero de enemigos que habr� en el combate
        {
            enemyType = Random.Range(1, 5); // Elige aleatoriamente el tipo de enemigo a colocar
            
            do // Bucle While para controlar que la posici�n elegida aleatoriamente no est� ocupada, si lo est� elige otra posici�n
            {
                position = Positions[Random.Range(0, Positions.Length)]; // Selecciona una posici�n al azar del array de prefabs CombatPosition

            } while(IsPositionOccupied(position)); // Si esa posici�n est� ocupada (Occupied == true) vuelve a seleccionar otra, si no lo est�, continua

            // Almacena las coordenadas de la posici�n seleccionada sum�ndole a "Y" +0.5 por motivos est�ticos
            positionVector = new Vector3(position.transform.position.x, position.transform.position.y + 0.5f, (position.transform.position.z - 1));

            if(enemyType == 1)                                              // Si el enemigo elegido es un Caballero
            {
                GameObject clon = Instantiate(PrefabEnemyKnight);           // Crea un clon del prefab Knight
                clon.transform.position = positionVector;                   // Coloca el clon en la posici�n escogida aleatoriamente
                position.GetComponent<CombatPosition>().Occupied = true;    // Cambia esa posici�n a "ocupada"
                position.GetComponent<CombatPosition>().CharacterType = 1;  // Indica que la posici�n est� ocupada por un enemigo
                position.GetComponent<CombatPosition>().Character = clon;   // Almacena el personaje en la posici�n en la que se ubicar�
                clon.GetComponent<EnemyKnight>().EnemyPosition = position;  // Almacena la posici�n del enemigo
                clon.GetComponent<EnemyKnight>().Positions = Positions;     // Almacena el array de posiciones del combate
                clon.GetComponent<GeneralEnemy>().Index = enemyType;        // Almacena el tipo de enemigo que es
                clon.GetComponent<GeneralEnemy>().EnemyPosition = position; // Alamcena la posici�n del enemigo
                clon.GetComponent<GeneralEnemy>().Enemy = clon;             // Almacena el enemigo
                clon.GetComponent<GeneralEnemy>()._CombatBackground = _CombatBackground; // Almacena el combate
                clon.GetComponent<EnemyKnight>()._CombatBackground = _CombatBackground;  // Almacena el combate
                Enemies[i] = clon;                                          // Mete el clon en el array de enemigos
            }
            else if (enemyType == 2)                                        // Si el enemigo elegido es un Healer
            {
                GameObject clon = Instantiate(PrefabEnemyHealer);           // Crea un clon del prefab Healer
                clon.transform.position = positionVector;                   // Coloca el clon en la posici�n escogida aleatoriamente 
                position.GetComponent<CombatPosition>().Occupied = true;    // Cambia esa posici�n a "ocupada"
                position.GetComponent<CombatPosition>().CharacterType = 1;  // Indica que la posici�n est� ocupada por un enemigo
                position.GetComponent<CombatPosition>().Character = clon;   // Almacena el personaje en la posici�n en la que se ubicar�
                clon.GetComponent<EnemyHealer>().EnemyPosition = position;  // Almacena la posici�n del enemigo
                clon.GetComponent<EnemyHealer>().Positions = Positions;     // Almacena el array de posiciones del combate
                clon.GetComponent<GeneralEnemy>().Index = enemyType;        // Almacena el tipo de enemigo que es
                clon.GetComponent<GeneralEnemy>().EnemyPosition = position; // Alamcena la posici�n del enemigo
                clon.GetComponent<GeneralEnemy>().Enemy = clon;             // Almacena el enemigo
                clon.GetComponent<GeneralEnemy>()._CombatBackground = _CombatBackground; // Almacena el combate
                clon.GetComponent<EnemyHealer>()._CombatBackground = _CombatBackground;  // Almacena el combate
                Enemies[i] = clon;                                          // Mete el clon en el array de enemigos
            }
            else if (enemyType == 3)                                        // Si el enemigo elegido es un Slime
            {
                GameObject clon = Instantiate(PrefabEnemySlime);            // Crea un clon del prefab Slime
                clon.transform.position = positionVector;                   // Coloca el clon en la posici�n escogida aleatoriamente 
                position.GetComponent<CombatPosition>().Occupied = true;    // Cambia esa posici�n a "ocupada"
                position.GetComponent<CombatPosition>().CharacterType = 1;  // Indica que la posici�n est� ocupada por un enemigo
                position.GetComponent<CombatPosition>().Character = clon;   // Almacena el personaje en la posici�n en la que se ubicar�
                clon.GetComponent<EnemySlime>().EnemyPosition = position;   // Almacena la posici�n del enemigo
                clon.GetComponent<EnemySlime>().Positions = Positions;      // Almacena el array de posiciones del combate
                clon.GetComponent<GeneralEnemy>().Index = enemyType;        // Almacena el tipo de enemigo que es
                clon.GetComponent<GeneralEnemy>().EnemyPosition = position; // Alamcena la posici�n del enemigo
                clon.GetComponent<GeneralEnemy>().Enemy = clon;             // Almacena el enemigo
                clon.GetComponent<GeneralEnemy>()._CombatBackground = _CombatBackground; // Almacena el combate
                clon.GetComponent<EnemySlime>()._CombatBackground = _CombatBackground;   // Almacena el combate
                Enemies[i] = clon;                                          // Mete el clon en el array de enemigos
            }
            else                                                            // Si el enemigo elegido es un Mage
            {
                GameObject clon = Instantiate(PrefabEnemyMage);             // Crea un clon del prefab Mage
                clon.transform.position = positionVector;                   // Coloca el clon en la posici�n escogida aleatoriamente 
                position.GetComponent<CombatPosition>().Occupied = true;    // Cambia esa posici�n a "ocupada"
                position.GetComponent<CombatPosition>().CharacterType = 1;  // Indica que la posici�n est� ocupada por un enemigo
                position.GetComponent<CombatPosition>().Character = clon;   // Almacena el personaje en la posici�n en la que se ubicar�
                clon.GetComponent<EnemyMage>().EnemyPosition = position;    // Almacena la posici�n del enemigo
                clon.GetComponent<EnemyMage>().Positions = Positions;       // Almacena el array de posiciones del combate
                clon.GetComponent<GeneralEnemy>().Index = enemyType;        // Almacena el tipo de enemigo que es
                clon.GetComponent<GeneralEnemy>().EnemyPosition = position; // Alamcena la posici�n del enemigo
                clon.GetComponent<GeneralEnemy>().Enemy = clon;             // Almacena el enemigo
                clon.GetComponent<GeneralEnemy>()._CombatBackground = _CombatBackground; // Almacena el combate
                clon.GetComponent<EnemyMage>()._CombatBackground = _CombatBackground;    // Almacena el combate
                Enemies[i] = clon;                                          // Mete el clon en el array de enemigos
            }
        }

        // Bucle para almacenar en cada enemigo el array de enemigos
        for (int j = 0; j < Enemies.Length; j++)
            Enemies[j].GetComponent<GeneralEnemy>().Enemies = Enemies;
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
        if(position.GetComponent<CombatPosition>().Occupied == true) // Si la variable "Occupied" es "true" es que est� ocupada
            return true;                                             // Devuelve "true" por que la posici�n est� ocupada
        
        return false;                                                // Devuelve "false" si "Occupied" es "false" y por tanto est� libre
    }

    /****************************************************************************************
     * Funci�n: SetEnemiesInterface                                                         *
     * Uso: Coloca la interfaz de los enemigos a la derecha                                 *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void SetAliadosColocar()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i == 0)                                                                                   // Si el personaje es un Knight
            {
                GameObject aliadoColocar = Instantiate(PrefabPlayerKnight);                               // Crea un clon del Knight
                aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posici�n de la interfaz
                aliadoColocar.GetComponent<DragDrop>().GetPositions(Positions);                           // Pasa el array de posiciones de combate al Aliado que acaba de colocar
                aliadoColocar.GetComponent<GeneralPlayer>().Positions = Positions;                        // Almacena en el personaje el array de posiciones de combate
                aliadoColocar.GetComponent<GeneralPlayer>().Character = aliadoColocar;                    // Almacena el personaje creado
                aliadoColocar.GetComponent<GeneralPlayer>().CharacterType = 1;                            // Almacena el tipo de enemigo
                aliadoColocar.GetComponent<GeneralPlayer>().Enemies = Enemies;                            // Almacena el array de enemigos del combate
                aliadoColocar.GetComponent<GeneralPlayer>()._CombatBackground = _CombatBackground;        // Almacena el combate
                aliadoColocar.GetComponent<PlayerKnight>()._CombatBackground = _CombatBackground;        // Almacena el combate
                Aliados[i] = aliadoColocar;                                                               // Mete el clon en el array de personajes del jugador
            }
            else if (i == 1)                                                                              // Si el enemigo es un Healer
            {
                GameObject aliadoColocar = Instantiate(PrefabPlayerHealer);                               // Crea un clon del Healer
                aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posici�n de la interfaz
                aliadoColocar.GetComponent<DragDrop>().GetPositions(Positions);                           // Pasa el array de posiciones de combate al Aliado que acaba de colocar
                aliadoColocar.GetComponent<GeneralPlayer>().Positions = Positions;                        // Almacena en el personaje el array de posiciones de combate
                aliadoColocar.GetComponent<GeneralPlayer>().Character = aliadoColocar;                    // Almacena el personaje creado
                aliadoColocar.GetComponent<GeneralPlayer>().CharacterType = 2;                            // Almacena el tipo de enemigo
                aliadoColocar.GetComponent<GeneralPlayer>().Enemies = Enemies;                            // Almacena el array de enemigos del combate
                aliadoColocar.GetComponent<GeneralPlayer>()._CombatBackground = _CombatBackground;        // Almacena el combate
                aliadoColocar.GetComponent<PlayerHealer>()._CombatBackground = _CombatBackground;        // Almacena el combate
                Aliados[i] = aliadoColocar;                                                               // Mete el clon en el array de personajes del jugador
            }
            else if (i == 2)                                                                              // Si el enemigo es un Slime
            {
                GameObject aliadoColocar = Instantiate(PrefabPlayerSlime);                                // Crea un clon del Slime
                aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posici�n de la interfaz
                aliadoColocar.GetComponent<DragDrop>().GetPositions(Positions);                           // Pasa el array de posiciones de combate al Aliado que acaba de colocar
                aliadoColocar.GetComponent<GeneralPlayer>().Positions = Positions;                        // Almacena en el personaje el array de posiciones de combate
                aliadoColocar.GetComponent<GeneralPlayer>().Character = aliadoColocar;                    // Almacena el personaje creado
                aliadoColocar.GetComponent<GeneralPlayer>().CharacterType = 3;                            // Almacena el tipo de enemigo
                aliadoColocar.GetComponent<GeneralPlayer>().Enemies = Enemies;                            // Almacena el array de enemigos del combate
                aliadoColocar.GetComponent<GeneralPlayer>()._CombatBackground = _CombatBackground;        // Almacena el combate
                aliadoColocar.GetComponent<PlayerSlime>()._CombatBackground = _CombatBackground;        // Almacena el combate
                Aliados[i] = aliadoColocar;                                                               // Mete el clon en el array de personajes del jugador
            }
            else                                                                                          // Si el enemigo es un Mage
            {
                GameObject aliadoColocar = Instantiate(PrefabPlayerMage);                                 // Crea un clon del Mage
                aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posici�n de la interfaz
                aliadoColocar.GetComponent<DragDrop>().GetPositions(Positions);                           // Pasa el array de posiciones de combate al Aliado que acaba de colocar
                aliadoColocar.GetComponent<GeneralPlayer>().Positions = Positions;                        // Almacena en el personaje el array de posiciones de combate
                aliadoColocar.GetComponent<GeneralPlayer>().Character = aliadoColocar;                    // Almacena el personaje creado
                aliadoColocar.GetComponent<GeneralPlayer>().CharacterType = 4;                            // Almacena el tipo de enemigo
                aliadoColocar.GetComponent<GeneralPlayer>().Enemies = Enemies;                            // Almacena el array de enemigos del combate
                aliadoColocar.GetComponent<GeneralPlayer>()._CombatBackground = _CombatBackground;        // Almacena el combate
                aliadoColocar.GetComponent<PlayerMage>()._CombatBackground = _CombatBackground;        // Almacena el combate
                Aliados[i] = aliadoColocar;                                                               // Mete el clon en el array de personajes del jugador
            }
        }
    }

    /****************************************************************************************
     * Funci�n: CreateStartBattleButton                                                     *
     * Uso: Coloca el bot�n de �Comenzar Batalla! abajo                                     *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void CreateStartBattleButton()
    {
        ClonStartBattleButton = Instantiate(PrefabStartBattleButton);                 // Crea un clon del prefab del bot�n StartBattleButton
        ClonStartBattleButton.GetComponent<RectTransform>().position = new Vector3(0, -5.5f, 0); // Lo coloca abajo de la interfaz
    }

    /****************************************************************************************
     * Funci�n: ChangeTurn                                                                  *
     * Uso: Cambia el turno durante la batalla entre el jugador y la IA                     *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void ChangeTurn()
    {
        if (TurnoBatalla == "Jugador") // Si turno actual era del Jugador
        {
            CambiarTurno = true;                                                // CambiarTurno a true
            for (int i = 0; i < Aliados.Length; i++)                            // Recorre el array de personajes del Jugador
                if (Aliados[i] != null)
                    Aliados[i].GetComponent<GeneralPlayer>().ClickOnce = false; // Para que puedan volver a crear la interfaz de movimientos
            TurnoBatalla = "Enemigo";                                           // El turno pasa a ser del Enemigo
        }
        else                                                                    // Si turno actual era del Enemigo
        {
            TurnoBatalla = "Jugador";                                           // El turno pasa a ser del Jugador
            
            // Actualiza el contador de invulnerabilidad del Knight del Jugador
            for(int i = 0; i < Aliados.Length; i++)                                               // Recorre el array de personajes del Jugador
                if (Aliados[i] != null)
                    if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 1)              // Si es el Knight
                        if (Aliados[i].GetComponent<PlayerKnight>().Invencible)                   // Si est� en modo invencible
                            Aliados[i].GetComponent<PlayerKnight>().PlayerKnightInvencibleCont++; // Aumenta el contador

            // Actualiza el contador de la habilidad del Slime del Jugador
            for(int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i] != null)
                {
                    if (Enemies[i].GetComponent<GeneralEnemy>().Index == 1)
                    {
                        if (Enemies[i].GetComponent<EnemyKnight>().HabilidadSlime == true)
                            ContHabilidadSlime++;
                    }
                    else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 2)
                    {
                        if (Enemies[i].GetComponent<EnemyHealer>().HabilidadSlime == true)
                            ContHabilidadSlime++;
                    }
                    else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 3)
                    {
                        if (Enemies[i].GetComponent<EnemySlime>().HabilidadSlime == true)
                            ContHabilidadSlime++;
                    }
                    else
                    {
                        if (Enemies[i].GetComponent<EnemyMage>().HabilidadSlime == true)
                            ContHabilidadSlime++;
                    }
                }
            }

            // Actualiza el contador de la habilidad del Mage del Jugador
            for (int i = 0; i < Aliados.Length; i++)
            {
                if (Aliados[i] != null)
                {
                    if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 1)
                    {
                        if (Aliados[i].GetComponent<PlayerKnight>().HabilidadMage == true)
                            ContHabilidadMage++;
                    }
                    else if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 2)
                    {
                        if (Aliados[i].GetComponent<PlayerHealer>().HabilidadMage == true)
                            ContHabilidadMage++;
                    }
                    else if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 3)
                    {
                        if (Aliados[i].GetComponent<PlayerSlime>().HabilidadMage == true)
                            ContHabilidadMage++;
                    }
                    else
                    {
                        if (Aliados[i].GetComponent<PlayerMage>().HabilidadMage == true)
                            ContHabilidadMage++;
                    }
                }
            }
        }
    }

    /****************************************************************************************
     * Funci�n: UpdateTurn                                                                  *
     * Uso: Actualiza las cosas relacionadas con el turno de la batalla                     *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void UpdateTurn()
    {
        if (BoolTurnoBatalla) // Si el texto "Texto de Batalla" ha sido creado
        {
            if (TurnoBatalla == "Jugador") // Si es el turno del jugador
                ClonTextoTurno.GetComponent<TextoTurno>().ChangeText("Turno del Jugador"); // Cambia el texto a "Turno del Jugador"
            else                           // Si es el turno del Enemigo
                ClonTextoTurno.GetComponent<TextoTurno>().ChangeText("Turno del Enemigo"); // Cambia el texto a "Turno del Enemigo"
        }
    }

    /****************************************************************************************
     * Funci�n: BattlePhase                                                                 *
     * Uso: Realiza cambios en CombateBackground despu�s de haber pulsado el bot�n de       *
     * �Comenzar Batalla!                                                                   *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void BattlePhase()
    {
        if (StartBattle) // Si StarBattle es true (se ha pulsado el bot�n de �Comenzar Batalla!)
        {
            if (!BoolTurnoBatalla)
            {
                Destroy(ClonStartBattleButton);                                    // Elimina el bot�n de �Comenzar Batalla!

                for (int i = 0; i < Aliados.Length; i++)                           // Recorre el array de los personajes del jugador
                {
                    Aliados[i].GetComponent<DragDrop>().enabled = false;           // Desactiva el script que permite arrastrarlos durante la fase de preparaci�n del combate
                }

                ClonTextoTurno = Instantiate(PrefabTextoTurno);                    // Crea el Texto del Turno
                ClonTextoTurno.transform.position = new Vector3(0, 6, 2);          // Lo coloca en la interfaz

                BoolTurnoBatalla = true;                                           // Activa la variable para controlar que todo esto se ejecute una sola vez
            }
        }
    }

    /****************************************************************************************
     * Funci�n: StatusAliadosPosition                                                       *
     * Uso: Comprueba que todos los personajes del Jugador han sido colocados en el mapa    *
     * de combate                                                                           *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void StatusAliadosPosition()
    {
        for (int i = 0; i < Aliados.Length; i++)
        {
            if (Aliados[i] != null)
            {
                if (Aliados[i].GetComponent<DragDrop>().CurrentPosition != Aliados[i].GetComponent<DragDrop>().OriginalPosition)
                    AliadosPositionStatus[i] = true;
            }
        }
    }

    /****************************************************************************************
     * Funci�n: SetAliadosPositionStatus                                                    *
     * Uso: Devuelve el array que comprueba que todos los aliados est�n colocados en el     *
     * mapa de combate                                                                      *
     * Variables entrada: Ninguno                                                           *
     * Return: AliadosPositionStatus - Array de booleanos                                   *
     ****************************************************************************************/
    public bool[] SetAliadosPositionStatus()
    {
        return AliadosPositionStatus;
    }

    /****************************************************************************************
     * Funci�n: UpdateOtherScripts                                                          *
     * Uso: - Comunica a los scripts de los personajes del Jugador que el combate ha        *
     *        comenzado                                                                     *
     *      - Le pasa el bot�n de �Comenzar Batalla! el estado de si todos los personajes   *
     *        del Jugador est�n colocados en el mapa de combate                             *
     * Variables entrada: Ninguno                                                           *
     * Return:                                                                              *
     ****************************************************************************************/
    public void UpdateOtherScripts()
    {
        int i;
        
        for(i = 0; i < Aliados.Length; i++)
        {
            if (Aliados[i] != null)
                Aliados[i].GetComponent<GeneralPlayer>().SetStartBattle(StartBattle);
        }

        if (!BoolTurnoBatalla)
        {
            for (i = 0; i < AliadosPositionStatus.Length; i++)
                ClonStartBattleButton.GetComponent<StartBattleButton>().SetAliadosPositionStatus(AliadosPositionStatus);
        }
    }

    /****************************************************************************************
     * Funci�n: SetArrayPositions                                                           *
     * Uso: Almacena en cada posici�n del array el array de posiciones                      *
     * Variables entrada: Ninguno                                                           *
     * Return:                                                                              *
     ****************************************************************************************/
    public void SetArrayPositions()
    {
        for (int i = 0; i < Positions.Length; i++)                             // Recorre el array de posiciones
            Positions[i].GetComponent<CombatPosition>().Positions = Positions; // Para cada posici�n almacena el array de posiciones
    }

    /****************************************************************************************
     * Funci�n: EnemyTurn y EnemyTurnWait                                                   *
     * Uso: Realiza los movimientos de la IA cuando es el turno del Enemigo                 *
     * Variables entrada: Ninguno                                                           *
     * Return:                                                                              *
     ****************************************************************************************/
    public void EnemyTurn()
    {
        StartCoroutine(EnemyTurnWait());    // Rutina que espera 3 segundos y cambia de turno
    }

    IEnumerator EnemyTurnWait()
    {
        yield return new WaitForSeconds(3); // Espera 3 segundos

        GameObject enemySelected;
        int cont = 0; // Contador
        int position; // Posici�n en rango del enemigo
        int a;

        do
        {
            do
            {
                a = Random.Range(0, Enemies.Length);
            } while (Enemies[a] == null);

            enemySelected = Enemies[a]; // Selecciona un enemigo al azar con el que hacer la acci�n

            for (int i = 0; i < 4; i++)                                                                                                // Recorre el array de posiciones en rango de la del enemigo
            {
                position = enemySelected.GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().PositionsToMove[i]; // Comprueba la primera posicic�n en rango del enemigo
                if (Positions[position].GetComponent<CombatPosition>().CharacterType == 2)                                             // Si la posici�n seleccionada est� ocupada por un personaje del Jugador
                    cont++;                                                                                                            // Aumenta el contador
            }

        } while (cont == 0); // Repite hasta que un enemigo con opci�n en rango de un personaje del Jugador sea seleccionado

        if (EnemigoParaAtacar)
        {
            if (enemySelected.GetComponent<GeneralEnemy>().Index == 1)      // Si el enemigo seleccionado es un Knight
            {
                enemySelected.GetComponent<EnemyKnight>().EnemyAtack();
                EnemigoParaAtacar = false;
            }
            else if (enemySelected.GetComponent<GeneralEnemy>().Index == 2) // Si el enemigo seleccionado es un Healer
            {
                enemySelected.GetComponent<EnemyHealer>().EnemyAtack();
                EnemigoParaAtacar = false;
            }
            else if (enemySelected.GetComponent<GeneralEnemy>().Index == 3) // Si el enemigo seleccionado es un Slime
            {
                enemySelected.GetComponent<EnemySlime>().EnemyAtack();
                EnemigoParaAtacar = false;
            }
            else                                                            // Si el enemigo seleccionado es un Mage
            {
                enemySelected.GetComponent<EnemyMage>().EnemyAtack();
                EnemigoParaAtacar = false;
            }
        }

        if (CambiarTurno)                   // Si CambiarTurno es true
        {
            ChangeTurn();                   // Cambia de turno
            CambiarTurno = false;           // Pone CmabiarTurno a false
        }
    }

    /****************************************************************************************
     * Funci�n: StatusCharacters                                                            *
     * Uso: Comprueba cada frame que los personajes tienen vida, si est� por debajo de 0    *
     *      los destruye                                                                    *
     * Variables entrada: Ninguno                                                           *
     * Return:                                                                              *
     ****************************************************************************************/
    public void StatusCharacters()
    {
        int i;
        
        for(i = 0; i < Enemies.Length; i++)                                       // Recorre el array de enemigos del combate
        {
            if (Enemies[i] != null)
            {
                if (Enemies[i].GetComponent<GeneralEnemy>().Index == 1)               // Si es un Knight
                {
                    if (Enemies[i].GetComponent<EnemyKnight>().VidaActual <= 0)       // Si tiene 0 o menos de vida actual
                    {
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posici�n pasa a estar vac�a
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posici�n pasa a estar vac�a
                        Destroy(Enemies[i].GetComponent<EnemyKnight>().ClonHealthbar);                                        // Destruye su Healthbar asociada
                        Destroy(Enemies[i]);                                          // Lo destruye
                    }
                }
                else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 2)          // Si es un Healer
                {
                    if (Enemies[i].GetComponent<EnemyHealer>().VidaActual <= 0)       // Si tiene 0 o menos de vida actual
                    {
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posici�n pasa a estar vac�a
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posici�n pasa a estar vac�a
                        Destroy(Enemies[i].GetComponent<EnemyHealer>().ClonHealthbar);                                        // Destruye su Healthbar asociada
                        Destroy(Enemies[i]);                                          // Lo destruye
                    }
                }
                else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 3)          // Si es un Slime
                {
                    if (Enemies[i].GetComponent<EnemySlime>().VidaActual <= 0)        // Si tiene 0 o menos de vida actual
                    {
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posici�n pasa a estar vac�a
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posici�n pasa a estar vac�a
                        Destroy(Enemies[i].GetComponent<EnemySlime>().ClonHealthbar);                                        // Destruye su Healthbar asociada
                        Destroy(Enemies[i]);                                          // Lo destruye
                    }
                }
                else                                                                  // Si es un Mage
                {
                    if (Enemies[i].GetComponent<EnemyMage>().VidaActual <= 0)         // Si tiene 0 o menos de vida actual
                    {
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posici�n pasa a estar vac�a
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posici�n pasa a estar vac�a
                        Destroy(Enemies[i].GetComponent<EnemyMage>().ClonHealthbar);                                        // Destruye su Healthbar asociada
                        Destroy(Enemies[i]);                                          // Lo destruye
                    }
                }
            }
        }

        for (i = 0; i < Aliados.Length; i++)                                      // Recorre el array de aliados
        {
            if (Aliados[i] != null)
            {
                if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 1)      // Si es un Knight
                {
                    if (Aliados[i].GetComponent<PlayerKnight>().VidaActual <= 0)      // Si tiene 0 o menos de vida actual
                    {
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posici�n pasa a estar vac�a
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posici�n pasa a estar vac�a
                        Destroy(Aliados[i].GetComponent<PlayerKnight>().ClonHealthbar);                                              // Destruye su Healthbar asociada
                        Destroy(Aliados[i]);                                          // Lo destruye
                    }
                }
                else if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 2) // Si es un Healer
                {
                    if (Aliados[i].GetComponent<PlayerHealer>().VidaActual <= 0)      // Si tiene 0 o menos de vida actual
                    {
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posici�n pasa a estar vac�a
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posici�n pasa a estar vac�a
                        Destroy(Aliados[i].GetComponent<PlayerHealer>().ClonHealthbar);                                              // Destruye su Healthbar asociada
                        Destroy(Aliados[i]);                                          // Lo destruye
                    }
                }
                else if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 3) // Si es un Slime
                {
                    if (Aliados[i].GetComponent<PlayerSlime>().VidaActual <= 0)       // Si tiene 0 o menos de vida actual
                    {
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posici�n pasa a estar vac�a
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posici�n pasa a estar vac�a
                        Destroy(Aliados[i].GetComponent<PlayerSlime>().ClonHealthbar);                                              // Destruye su Healthbar asociada
                        Destroy(Aliados[i]);                                          // Lo destruye
                    }
                }
                else                                                                  // Si es un Mage
                {
                    if (Aliados[i].GetComponent<PlayerMage>().VidaActual <= 0)        // Si tiene 0 o menos de vida actual
                    {
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posici�n pasa a estar vac�a
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posici�n pasa a estar vac�a
                        Destroy(Aliados[i].GetComponent<PlayerMage>().ClonHealthbar);                                              // Destruye su Healthbar asociada
                        Destroy(Aliados[i]);                                          // Lo destruye
                    }
                }
            }
        }
    }
}
