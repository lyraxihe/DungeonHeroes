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
    private GameObject ClonStartBattleButton;   // Clon del botón de ¡Comenzar Batalla!
    private GameObject ClonTextoTurno;          // Clon del texto "Turno de Batalla"

    // POSICIONES
    private float[] PositionsX = {0, -5.5f, -1.5f, 1.5f, 5.5f, -2.5f, 2.5f, 0, -3.5f, 3.5f}; // Array de posiciones Coordenadas X del prefab CombatPosition
    private float[] PositionsY = {3.5f, 1.5f, 1.5f, 1.5f, 1.5f, 0, 0, -1.5f, -3.5f, -3.5f};  // Array de posiciones Coordenadas Y del prefab CombatPosition
    public GameObject[] Positions;                                                           // Array de prefabs CombatPosition

    // PERSONAJES JUGADOR
    private float[] AliadoColocarX = {9, 9, 9, 9};     // Array de coordenadas X de la interfaz para colocar los personajes del jugador
    private float[] AliadoColocarY = {3, 1, -1, -3};   // Array de coordenadas Y de la interfaz para colocar los personajes del jugador

    // ATRIBUTOS VARIOS
    public bool StartBattle;                                               // Booleano que controla cuando el botón de ¡Comenzar Batalla! es pulsado
    public string TurnoBatalla;                                            // Nombre del turno del Jugador o el Enemigo
    private bool BoolTurnoBatalla;                                         // Booleano que controla que el texto "Turno de Batalla" se cree una sóla vez
    private bool[] AliadosPositionStatus = { false, false, false, false }; // Array de booleanos para ver que todos los aliados han sido colocados en el mapa de combate
    private bool CambiarTurno = true;
    public bool EnemigoParaAtacar = false;
    public int ContHabilidadSlime;                                         // Contador para saber cuántos turnos faltan para que termine la habilidad del Slime
    public int ContHabilidadMage;                                          // Contador para saber cuántos turnos faltan para que termine la habilidad del Mage
    public GameObject[] CharacterInterface;                                     // Array de info del Personaje del Jugador

    // Start is called before the first frame update
    void Start()
    {
        Positions = new GameObject[PositionsX.Length]; // Crea el array de prefabs CombatPosition en base al tamaño del array de Coordenadas
        Aliados = new GameObject[4];                   // Crea el array de personajes del jugador con tamaño 4 
        StartBattle = false;                           // El combate se incializa primero en fase de planificación
        TurnoBatalla = "Jugador";                      // Establece que el primer turno de la batalla será para el jugador
        BoolTurnoBatalla = false;                      // De momento se puede crear el Texto "Turno de Batalla"
        CharacterInterface = new GameObject[10];            // Incializa el array de info del Personaje del JUgador

        ContHabilidadSlime = 0;

        SetPositions(PositionsX.Length);               // Coloca las posiciones donde irán los personajes en pantalla
        SetArrayPositions();                           // Almacena en cada posición el array de posiciones
        SetPositionsToMove();                          // Establece las posiciones a las que se puede mover cada posición
        SetEnemies(2, 4);                              // Coloca los enemigos en las posiciones del combate
        
        // Bucle para almacenar el array de enemigos en cada posición
        for (int i = 0; i < Positions.Length; i++)
            Positions[i].GetComponent<CombatPosition>().Enemies = Enemies;

        SetAliadosColocar();                           // Crea la interfaz para colocar los personajes del jugador
        CreateStartBattleButton();                     // Crea el botón de ¡Comenzar Batalla!
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
     * Función: SetPositions                                                                *
     * Uso: Coloca las posiciones (CombatPositions) donde irán los personajes en el combate *
     * Variables entrada:                                                                   *
     *      - numPositions: Número de posiciones que habrá en el combate                    *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void SetPositions(int numPositions)
    {
        for(int i = 0; i < numPositions; i++) // Bucle for desde 0 al número de posiciones que debe haber para ir colocándolas
        {
            GameObject clon = Instantiate(CombatPosition);                             // Crea un clon del prefab CombatPosition
            clon.transform.position = new Vector3(PositionsX[i], PositionsY[i], 2);    // Coloca el clon en la primera posición que hay en los arrays de Coordenadas
            clon.GetComponent<CombatPosition>()._CombatBackground = _CombatBackground; // Almacena el combate
            clon.GetComponent<CombatPosition>()._Position = clon;                      // Almacena la posición en si misma par aluego tener accesos
            Positions[i] = clon;                                                       // Añade el clon al array de prefabs CombatPosition
        }
    }

    /****************************************************************************************
     * Función: SetPositionsToMove                                                          *
     * Uso: Establece las posiciones a las que se puede mover cada posición                 *
     * Variables entrada: Nada.                                                             *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void SetPositionsToMove()
    {
        for(int i = 0; i < Positions.Length; i++) // Recorre el array de posiciones
        {
            // Se estblecen las posiciones a las que se puede mover el personaje desde cada posición

            if (i == 0)      // Posición 1
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(1, 2, 3, 4);
            else if (i == 1) // Posición 2
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(0, 2, 5, 8);
            else if (i == 2) // Posición 3
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(0, 1, 3, 5);
            else if (i == 3) // Posición 4
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(0, 2, 4, 6);
            else if (i == 4) // Posición 5
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(0, 3, 6, 9);
            else if (i == 5) // Posición 6
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(1, 2, 7, 8);
            else if (i == 6) // Posición 7
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(3, 4, 7, 9);
            else if (i == 7) // Posición 8
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(5, 6, 8, 9);
            else if (i == 8) // Posición 9
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(1, 5, 7, 9);
            else             // Posición 10
                Positions[i].GetComponent<CombatPosition>().SetPositionsToMove(4, 6, 7, 8);
        }
    }

    /****************************************************************************************
     * Función: SetEnemies                                                                  *
     * Uso: Coloca los enemigos de manera aleatoria en las posiciones que hay en el combate *
     * Variables entrada:                                                                   *
     *      - minEnemies: Número mínimo de enemigos que habrá en el combate                 *
     *      - maxEnemies: Número máximo de enemigos que habrá en el combate                 *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void SetEnemies(int minEnemies, int maxEnemies)
    {
        GameObject position;                                       // Posición donde se colocará al enemigo
        int numEnemies = Random.Range(minEnemies, maxEnemies + 1); // Establece aleatoriamente el número de enemigos que habrá en el combate con un mínimo y un máximo
        int enemyType;                                             // Almacena el tipo de enemigo que se quiere colocar
        Vector3 positionVector;                                    // Coordenadas de una posición
        int i;

        Enemies = new GameObject[numEnemies];                      // Crea el array de enemigos con tamaño en base al número de enemigos creados aleatoriamente

        for(i = 0; i < numEnemies; i++) // Bucle for desde 0 hasta el número de enemigos que habrá en el combate
        {
            enemyType = Random.Range(1, 5); // Elige aleatoriamente el tipo de enemigo a colocar
            
            do // Bucle While para controlar que la posición elegida aleatoriamente no esté ocupada, si lo está elige otra posición
            {
                position = Positions[Random.Range(0, Positions.Length)]; // Selecciona una posición al azar del array de prefabs CombatPosition

            } while(IsPositionOccupied(position)); // Si esa posición está ocupada (Occupied == true) vuelve a seleccionar otra, si no lo está, continua

            // Almacena las coordenadas de la posición seleccionada sumándole a "Y" +0.5 por motivos estéticos
            positionVector = new Vector3(position.transform.position.x, position.transform.position.y + 0.5f, (position.transform.position.z - 1));

            if(enemyType == 1)                                              // Si el enemigo elegido es un Caballero
            {
                GameObject clon = Instantiate(PrefabEnemyKnight);           // Crea un clon del prefab Knight
                clon.transform.position = positionVector;                   // Coloca el clon en la posición escogida aleatoriamente
                position.GetComponent<CombatPosition>().Occupied = true;    // Cambia esa posición a "ocupada"
                position.GetComponent<CombatPosition>().CharacterType = 1;  // Indica que la posición está ocupada por un enemigo
                position.GetComponent<CombatPosition>().Character = clon;   // Almacena el personaje en la posición en la que se ubicará
                clon.GetComponent<EnemyKnight>().EnemyPosition = position;  // Almacena la posición del enemigo
                clon.GetComponent<EnemyKnight>().Positions = Positions;     // Almacena el array de posiciones del combate
                clon.GetComponent<GeneralEnemy>().Index = enemyType;        // Almacena el tipo de enemigo que es
                clon.GetComponent<GeneralEnemy>().EnemyPosition = position; // Alamcena la posición del enemigo
                clon.GetComponent<GeneralEnemy>().Enemy = clon;             // Almacena el enemigo
                clon.GetComponent<GeneralEnemy>()._CombatBackground = _CombatBackground; // Almacena el combate
                clon.GetComponent<EnemyKnight>()._CombatBackground = _CombatBackground;  // Almacena el combate
                Enemies[i] = clon;                                          // Mete el clon en el array de enemigos
            }
            else if (enemyType == 2)                                        // Si el enemigo elegido es un Healer
            {
                GameObject clon = Instantiate(PrefabEnemyHealer);           // Crea un clon del prefab Healer
                clon.transform.position = positionVector;                   // Coloca el clon en la posición escogida aleatoriamente 
                position.GetComponent<CombatPosition>().Occupied = true;    // Cambia esa posición a "ocupada"
                position.GetComponent<CombatPosition>().CharacterType = 1;  // Indica que la posición está ocupada por un enemigo
                position.GetComponent<CombatPosition>().Character = clon;   // Almacena el personaje en la posición en la que se ubicará
                clon.GetComponent<EnemyHealer>().EnemyPosition = position;  // Almacena la posición del enemigo
                clon.GetComponent<EnemyHealer>().Positions = Positions;     // Almacena el array de posiciones del combate
                clon.GetComponent<GeneralEnemy>().Index = enemyType;        // Almacena el tipo de enemigo que es
                clon.GetComponent<GeneralEnemy>().EnemyPosition = position; // Alamcena la posición del enemigo
                clon.GetComponent<GeneralEnemy>().Enemy = clon;             // Almacena el enemigo
                clon.GetComponent<GeneralEnemy>()._CombatBackground = _CombatBackground; // Almacena el combate
                clon.GetComponent<EnemyHealer>()._CombatBackground = _CombatBackground;  // Almacena el combate
                Enemies[i] = clon;                                          // Mete el clon en el array de enemigos
            }
            else if (enemyType == 3)                                        // Si el enemigo elegido es un Slime
            {
                GameObject clon = Instantiate(PrefabEnemySlime);            // Crea un clon del prefab Slime
                clon.transform.position = positionVector;                   // Coloca el clon en la posición escogida aleatoriamente 
                position.GetComponent<CombatPosition>().Occupied = true;    // Cambia esa posición a "ocupada"
                position.GetComponent<CombatPosition>().CharacterType = 1;  // Indica que la posición está ocupada por un enemigo
                position.GetComponent<CombatPosition>().Character = clon;   // Almacena el personaje en la posición en la que se ubicará
                clon.GetComponent<EnemySlime>().EnemyPosition = position;   // Almacena la posición del enemigo
                clon.GetComponent<EnemySlime>().Positions = Positions;      // Almacena el array de posiciones del combate
                clon.GetComponent<GeneralEnemy>().Index = enemyType;        // Almacena el tipo de enemigo que es
                clon.GetComponent<GeneralEnemy>().EnemyPosition = position; // Alamcena la posición del enemigo
                clon.GetComponent<GeneralEnemy>().Enemy = clon;             // Almacena el enemigo
                clon.GetComponent<GeneralEnemy>()._CombatBackground = _CombatBackground; // Almacena el combate
                clon.GetComponent<EnemySlime>()._CombatBackground = _CombatBackground;   // Almacena el combate
                Enemies[i] = clon;                                          // Mete el clon en el array de enemigos
            }
            else                                                            // Si el enemigo elegido es un Mage
            {
                GameObject clon = Instantiate(PrefabEnemyMage);             // Crea un clon del prefab Mage
                clon.transform.position = positionVector;                   // Coloca el clon en la posición escogida aleatoriamente 
                position.GetComponent<CombatPosition>().Occupied = true;    // Cambia esa posición a "ocupada"
                position.GetComponent<CombatPosition>().CharacterType = 1;  // Indica que la posición está ocupada por un enemigo
                position.GetComponent<CombatPosition>().Character = clon;   // Almacena el personaje en la posición en la que se ubicará
                clon.GetComponent<EnemyMage>().EnemyPosition = position;    // Almacena la posición del enemigo
                clon.GetComponent<EnemyMage>().Positions = Positions;       // Almacena el array de posiciones del combate
                clon.GetComponent<GeneralEnemy>().Index = enemyType;        // Almacena el tipo de enemigo que es
                clon.GetComponent<GeneralEnemy>().EnemyPosition = position; // Alamcena la posición del enemigo
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
     * Función: IsPositionOccupied                                                          *
     * Uso: Comprueba si el CombatPosition pasado como argumento está ocupado o no          *
     * Variables entrada:                                                                   *
     *      - position: prefab CombatPosition a comprobar                                   *
     * Return:                                                                              *
     *      - True: Si la posición está ocupada                                             *
     *      - False: Si la posición está libre                                              *
     ****************************************************************************************/
    private bool IsPositionOccupied(GameObject position)
    {
        if(position.GetComponent<CombatPosition>().Occupied == true) // Si la variable "Occupied" es "true" es que está ocupada
            return true;                                             // Devuelve "true" por que la posición está ocupada
        
        return false;                                                // Devuelve "false" si "Occupied" es "false" y por tanto está libre
    }

    /****************************************************************************************
     * Función: SetEnemiesInterface                                                         *
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
                aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posición de la interfaz
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
                aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posición de la interfaz
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
                aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posición de la interfaz
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
                aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posición de la interfaz
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
     * Función: CreateStartBattleButton                                                     *
     * Uso: Coloca el botón de ¡Comenzar Batalla! abajo                                     *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void CreateStartBattleButton()
    {
        ClonStartBattleButton = Instantiate(PrefabStartBattleButton);                 // Crea un clon del prefab del botón StartBattleButton
        ClonStartBattleButton.GetComponent<RectTransform>().position = new Vector3(0, -5.5f, 0); // Lo coloca abajo de la interfaz
    }

    /****************************************************************************************
     * Función: ChangeTurn                                                                  *
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
                        if (Aliados[i].GetComponent<PlayerKnight>().Invencible)                   // Si está en modo invencible
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
     * Función: UpdateTurn                                                                  *
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
     * Función: BattlePhase                                                                 *
     * Uso: Realiza cambios en CombateBackground después de haber pulsado el botón de       *
     * ¡Comenzar Batalla!                                                                   *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void BattlePhase()
    {
        if (StartBattle) // Si StarBattle es true (se ha pulsado el botón de ¡Comenzar Batalla!)
        {
            if (!BoolTurnoBatalla)
            {
                Destroy(ClonStartBattleButton);                                    // Elimina el botón de ¡Comenzar Batalla!

                for (int i = 0; i < Aliados.Length; i++)                           // Recorre el array de los personajes del jugador
                {
                    Aliados[i].GetComponent<DragDrop>().enabled = false;           // Desactiva el script que permite arrastrarlos durante la fase de preparación del combate
                }

                ClonTextoTurno = Instantiate(PrefabTextoTurno);                    // Crea el Texto del Turno
                ClonTextoTurno.transform.position = new Vector3(0, 6, 2);          // Lo coloca en la interfaz

                BoolTurnoBatalla = true;                                           // Activa la variable para controlar que todo esto se ejecute una sola vez
            }
        }
    }

    /****************************************************************************************
     * Función: StatusAliadosPosition                                                       *
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
     * Función: SetAliadosPositionStatus                                                    *
     * Uso: Devuelve el array que comprueba que todos los aliados están colocados en el     *
     * mapa de combate                                                                      *
     * Variables entrada: Ninguno                                                           *
     * Return: AliadosPositionStatus - Array de booleanos                                   *
     ****************************************************************************************/
    public bool[] SetAliadosPositionStatus()
    {
        return AliadosPositionStatus;
    }

    /****************************************************************************************
     * Función: UpdateOtherScripts                                                          *
     * Uso: - Comunica a los scripts de los personajes del Jugador que el combate ha        *
     *        comenzado                                                                     *
     *      - Le pasa el botón de ¡Comenzar Batalla! el estado de si todos los personajes   *
     *        del Jugador están colocados en el mapa de combate                             *
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
     * Función: SetArrayPositions                                                           *
     * Uso: Almacena en cada posición del array el array de posiciones                      *
     * Variables entrada: Ninguno                                                           *
     * Return:                                                                              *
     ****************************************************************************************/
    public void SetArrayPositions()
    {
        for (int i = 0; i < Positions.Length; i++)                             // Recorre el array de posiciones
            Positions[i].GetComponent<CombatPosition>().Positions = Positions; // Para cada posición almacena el array de posiciones
    }

    /****************************************************************************************
     * Función: EnemyTurn y EnemyTurnWait                                                   *
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
        int position; // Posición en rango del enemigo
        int a;

        do
        {
            do
            {
                a = Random.Range(0, Enemies.Length);
            } while (Enemies[a] == null);

            enemySelected = Enemies[a]; // Selecciona un enemigo al azar con el que hacer la acción

            for (int i = 0; i < 4; i++)                                                                                                // Recorre el array de posiciones en rango de la del enemigo
            {
                position = enemySelected.GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().PositionsToMove[i]; // Comprueba la primera posicicón en rango del enemigo
                if (Positions[position].GetComponent<CombatPosition>().CharacterType == 2)                                             // Si la posición seleccionada está ocupada por un personaje del Jugador
                    cont++;                                                                                                            // Aumenta el contador
            }

        } while (cont == 0); // Repite hasta que un enemigo con opción en rango de un personaje del Jugador sea seleccionado

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
     * Función: StatusCharacters                                                            *
     * Uso: Comprueba cada frame que los personajes tienen vida, si está por debajo de 0    *
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
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                        Destroy(Enemies[i].GetComponent<EnemyKnight>().ClonHealthbar);                                        // Destruye su Healthbar asociada
                        Destroy(Enemies[i]);                                          // Lo destruye
                    }
                }
                else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 2)          // Si es un Healer
                {
                    if (Enemies[i].GetComponent<EnemyHealer>().VidaActual <= 0)       // Si tiene 0 o menos de vida actual
                    {
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                        Destroy(Enemies[i].GetComponent<EnemyHealer>().ClonHealthbar);                                        // Destruye su Healthbar asociada
                        Destroy(Enemies[i]);                                          // Lo destruye
                    }
                }
                else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 3)          // Si es un Slime
                {
                    if (Enemies[i].GetComponent<EnemySlime>().VidaActual <= 0)        // Si tiene 0 o menos de vida actual
                    {
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                        Destroy(Enemies[i].GetComponent<EnemySlime>().ClonHealthbar);                                        // Destruye su Healthbar asociada
                        Destroy(Enemies[i]);                                          // Lo destruye
                    }
                }
                else                                                                  // Si es un Mage
                {
                    if (Enemies[i].GetComponent<EnemyMage>().VidaActual <= 0)         // Si tiene 0 o menos de vida actual
                    {
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                        Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
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
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                        Destroy(Aliados[i].GetComponent<PlayerKnight>().ClonHealthbar);                                              // Destruye su Healthbar asociada
                        Destroy(Aliados[i]);                                          // Lo destruye
                    }
                }
                else if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 2) // Si es un Healer
                {
                    if (Aliados[i].GetComponent<PlayerHealer>().VidaActual <= 0)      // Si tiene 0 o menos de vida actual
                    {
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                        Destroy(Aliados[i].GetComponent<PlayerHealer>().ClonHealthbar);                                              // Destruye su Healthbar asociada
                        Destroy(Aliados[i]);                                          // Lo destruye
                    }
                }
                else if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 3) // Si es un Slime
                {
                    if (Aliados[i].GetComponent<PlayerSlime>().VidaActual <= 0)       // Si tiene 0 o menos de vida actual
                    {
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                        Destroy(Aliados[i].GetComponent<PlayerSlime>().ClonHealthbar);                                              // Destruye su Healthbar asociada
                        Destroy(Aliados[i]);                                          // Lo destruye
                    }
                }
                else                                                                  // Si es un Mage
                {
                    if (Aliados[i].GetComponent<PlayerMage>().VidaActual <= 0)        // Si tiene 0 o menos de vida actual
                    {
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                        Aliados[i].GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                        Destroy(Aliados[i].GetComponent<PlayerMage>().ClonHealthbar);                                              // Destruye su Healthbar asociada
                        Destroy(Aliados[i]);                                          // Lo destruye
                    }
                }
            }
        }
    }
}
