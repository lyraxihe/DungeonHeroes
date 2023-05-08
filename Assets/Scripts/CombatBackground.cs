using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatBackground : MonoBehaviour
{
    public GameObject _Control;

    public GameObject _CombatBackground;        // Combate
    public RectTransform Canvas;                // Canva

    public GameObject UIEstadisticasPersonaje;
    public TMP_Text TextoVida;
    public TMP_Text TextoAtaque;
    public TMP_Text TextoDefensa;
    public Image VidaImagen;
    public Image AtaqueImagen;
    public Image DefensaImagen;

    public GameObject UIMover;
    public GameObject UIAtacarConRango;
    public GameObject UIAtacarSinRango;
    public GameObject UIHabilidadKnight;
    public GameObject UIHabilidadHealer;
    public GameObject UIHabilidadSlime;
    public GameObject UIHabilidadMage;

    public GameObject EstadisticaVida;
    public GameObject EstadisticaAtaque;
    public GameObject EstadisticaDefensa;

    //UI ENEMIGOS
    public GameObject UIEnemigo1;
    public TMP_Text VidaEnemigo1;
    public TMP_Text AtaqueEnemigo1;
    public TMP_Text DefensaEnemigo1;
    public GameObject UIEnemigo2;
    public TMP_Text VidaEnemigo2;
    public TMP_Text AtaqueEnemigo2;
    public TMP_Text DefensaEnemigo2;
    public GameObject UIEnemigo3;
    public TMP_Text VidaEnemigo3;
    public TMP_Text AtaqueEnemigo3;
    public TMP_Text DefensaEnemigo3;
    public GameObject UIEnemigo4;
    public TMP_Text VidaEnemigo4;
    public TMP_Text AtaqueEnemigo4;
    public TMP_Text DefensaEnemigo4;

    public GameObject BorderUIEnemigo;
    public GameObject ContainerUIEnemigo;
    public GameObject UIEnemigoKnight;
    public GameObject UIEnemigoHealer;
    public GameObject UIEnemigoSlime;
    public GameObject UIEnemigoMage;
    public GameObject UIEnemigoBoss;
    public GameObject UIEnemigoCorazon;
    public GameObject UIEnemigoEspada;
    public GameObject UIEnemigoEscudo;

    public GameObject UIEstadisticaVidaEnemy1;
    public GameObject UIEstadisticaAtaqueEnemy1;
    public GameObject UIEstadisticaDefensaEnemy1;
    public GameObject UIEstadisticaVidaEnemy2;
    public GameObject UIEstadisticaAtaqueEnemy2;
    public GameObject UIEstadisticaDefensaEnemy2;
    public GameObject UIEstadisticaVidaEnemy3;
    public GameObject UIEstadisticaAtaqueEnemy3;
    public GameObject UIEstadisticaDefensaEnemy3;
    public GameObject UIEstadisticaVidaEnemy4;
    public GameObject UIEstadisticaAtaqueEnemy4;
    public GameObject UIEstadisticaDefensaEnemy4;

    // PREFABS
    public GameObject CombatPosition;              // Prefab CombatPosition
    public GameObject CombatLine;                  // Prefab CombatLine
    public GameObject PrefabBoss;                  // Prefab Boss
    public GameObject PrefabEnemyKnight;           // Prefab Personaje 1 - Caballero Enemigo
    public GameObject PrefabPlayerKnight;          // Prefab Personaje 1 - Caballero del Jugador
    public GameObject PrefabEnemyHealer;           // Prefab Personaje 2 - Healer Enemigo
    public GameObject PrefabPlayerHealer;          // Prefab Personaje 2 - Healer del Jugador
    public GameObject PrefabEnemySlime;            // Prefab Personaje 3 - Slime Enemigo
    public GameObject PrefabPlayerSlime;           // Prefab Personaje 3 - Slime del Jugador
    public GameObject PrefabEnemyMage;             // Prefab Personaje 4 - Mago Enemigo
    public GameObject PrefabPlayerMage;            // Prefab Personaje 4 - Mago del Jugador
    public GameObject PrefabStartBattleButton;     // Prefab StartBattleButton
    public GameObject PrefabTextoTurno;            // Prefab Texto del turno
    public GameObject PrefabVictoriaDerrota;       // Prefab del cartel de Victoria o Derrota
    public GameObject PrefabVictoriaDerrotaBorder; // Prefab del borde del cartel de Victoria o Derrota
    public GameObject PrefabButtonVictoriaDerrota; // Prefab del botón de Vitoria o Derrota

    // CLONES
    public GameObject[] Enemies;                 // Array de clones de los enemigos
    public  GameObject[] Aliados;                 // Array de clones de los personajes del jugador
    private GameObject ClonStartBattleButton;     // Clon del botón de ¡Comenzar Batalla!
    private GameObject ClonTextoTurno;            // Clon del texto "Turno de Batalla"
    private GameObject ClonVictoriaDerrota;       // Clon del cartel de Victoria o Derrota
    private GameObject ClonVictoriaDerrotaBorder; // Clon del borde del cartel de Victoria o Derrota
    private GameObject ClonTextoVictoriaDerrota;    // Clon del texto de Victoria o Derrota
    private GameObject ClonButtonVictoriaDerrota; // Clon del botón de Vitoria o Derrota
    private GameObject ClonTextoRecompensa;       // Clon del texto de Victoria "Recompensa"
    private GameObject ClonTextoDerrotado;        // Clon del texto de Derrota "Has sido derrotado"
    public GameObject ClonTextoExplicacion;       // Clon del texto que explica las acciones del jugador

    // POSICIONES
    private float[] PositionsX = {0, -5.5f, -1.5f, 1.5f, 5.5f, -2.5f, 2.5f, 0, -3.5f, 3.5f}; // Array de posiciones Coordenadas X del prefab CombatPosition
    private float[] PositionsY = {3.5f, 1.5f, 1.5f, 1.5f, 1.5f, 0, 0, -1.5f, -3.5f, -3.5f};  // Array de posiciones Coordenadas Y del prefab CombatPosition
    public GameObject[] Positions;                                                           // Array de prefabs CombatPosition

    // CAMINOS
    private float[] LinesX = { -3.5f, -2.8f, 2.8f, 3.5f, 0, 0, -2, 2, -1.3f, 1.3f, -0.8f, 0.8f, -4, 4, -3, 3, 1.7f, -1.7f, 4.5f, -4.5f};
    private float[] LinesY = {1.5f, 2.5f, 2.5f, 1.5f, -3.5f, 1.5f, 0.8f, 0.8f, -0.7f, -0.7f, 2.5f, 2.5f, 0.8f, 0.8f, -1.7f, -1.7f, -2.5f, -2.5f, -1, -1};
    private float[] LinesScaleX = {4f, 5.8f, 5.8f, 4, 7, 3, 1.8f, 1.8f, 3, 3, 2.4f, 2.4f, 3.2f, 3.2f, 3.6f, 3.6f, 4, 4, 5.6f, 5.6f};
    private float LinesScaleY = 0.2f;
    private float[] LinesRotationZ = {0, 20, -20, 0, 0, 0, 56, -56, -30, 30, 53, -53, -28, 28, 74, -74, -30, 30, 67, -67};
    private GameObject[] Lines;                   // Array de caminos de las posiciones

    // PERSONAJES JUGADOR
    private float[] AliadoColocarX = {9, 9, 9, 9};     // Array de coordenadas X de la interfaz para colocar los personajes del jugador
    private float[] AliadoColocarY = {3, 1, -1, -3};   // Array de coordenadas Y de la interfaz para colocar los personajes del jugador

    // ATRIBUTOS VARIOS
    public bool StartBattle;                                               // Booleano que controla cuando el botón de ¡Comenzar Batalla! es pulsado
    public string TurnoBatalla;                                            // Nombre del turno del Jugador o el Enemigo
    private bool BoolTurnoBatalla;                                         // Booleano que controla que el texto "Turno de Batalla" se cree una sóla vez
    private bool[] AliadosPositionStatus = new bool[4]; // Array de booleanos para ver que todos los aliados han sido colocados en el mapa de combate
    private bool CambiarTurno = true;
    public bool EnemigoParaAtacar = false;
    public int ContHabilidadSlime;                                         // Contador para saber cuántos turnos faltan para que termine la habilidad del Slime
    public int ContHabilidadMage;                                          // Contador para saber cuántos turnos faltan para que termine la habilidad del Mage
    public GameObject[] CharacterInterface;                                // Array de info del Personaje del Jugador
    public int ContEnemigos;                                               // Contador de los enemigos en combate
    public int ContAliados;                                                // Contador de los enemigos en combate
    public bool Victoria;                                                  // Booleano que controla si hay condición de victoria
    public bool Derrota;                                                   // Booleano que controla si hay condición de derrota
    public bool VictoriaDerrotaCreado;                                     // Booleano que controla que la interfaz de Victoria o Derrota se cree una vez
    public int NumEnemigos;

    //// MENÚ PAUSA
    //public GameObject ClonMenuPausaBorder;
    //public GameObject ClonMenuPausaContainer;
    //public GameObject ClonContinuarButton;
    //public GameObject ClonSalirButton;

    //public TMP_Text PausaText;
    //public Button ContinuarButton;
    //public TMP_Text ContinuarButtonText;
    //public Button SalirButton;
    //public TMP_Text SalirButtonText;

    //public bool EscPressed;

    // Start is called before the first frame update
    void Start()
    {
        // MENU PAUSA
        //ClonMenuPausaBorder = Instantiate(PrefabVictoriaDerrotaBorder);
        //ClonMenuPausaBorder.transform.position = new Vector3(0, 0, -1);
        //ClonMenuPausaBorder.transform.localScale = new Vector3(9, 10, 1);
        //ClonMenuPausaBorder.GetComponent<SpriteRenderer>().enabled = false;

        //ClonMenuPausaContainer = Instantiate(PrefabVictoriaDerrota);
        //ClonMenuPausaContainer.transform.position = new Vector3(0, 0, -2);
        //ClonMenuPausaContainer.transform.localScale = new Vector3(8.5f, 9.5f, 1);
        //ClonMenuPausaContainer.GetComponent<SpriteRenderer>().enabled = false;

        // RESTO
        Positions = new GameObject[PositionsX.Length];                      // Crea el array de prefabs CombatPosition en base al tamaño del array de Coordenadas
        Lines = new GameObject[LinesX.Length];                              // Crea el array de prefabs CombatLine en base al tamaño del array de Coordenadas
        Aliados = new GameObject[4];                                        // Crea el array de personajes del jugador con tamaño 4 
        StartBattle = false;                                                // El combate se incializa primero en fase de planificación
        TurnoBatalla = "Jugador";                                           // Establece que el primer turno de la batalla será para el jugador
        BoolTurnoBatalla = false;                                           // De momento se puede crear el Texto "Turno de Batalla"
        CharacterInterface = new GameObject[10];                            // Incializa el array de info del Personaje del Jugador

        ContHabilidadSlime = 0;

        ContEnemigos = 0;
        ContAliados = 0;
        Victoria = false;
        Derrota = false;
        VictoriaDerrotaCreado = false;
        //EscPressed = false;

        // TEXTO EXPLICATIVO
        /******************************************************************************************************/
        ClonTextoExplicacion = Instantiate(PrefabTextoTurno);
        ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeText("Arrastra a tus heroes al mapa de combate");
        ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeFontSize(0.60f);
        ClonTextoExplicacion.transform.position = new Vector3(8, 5, 2);
        /******************************************************************************************************/

        SetPositions(PositionsX.Length);               // Coloca las posiciones donde irán los personajes en pantalla
        SetLines();                                    // Coloca las líneas del combate
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
        // CONTROL
        //_Control.GetComponent<ControlPausa>()._CombatBackground = _CombatBackground;

        //_Control.GetComponent<ControlPausa>().ClonMenuPausaBorder = ClonMenuPausaBorder;
        //_Control.GetComponent<ControlPausa>().ClonMenuPausaContainer = ClonMenuPausaContainer;

        //_Control.GetComponent<ControlPausa>().PausaText = PausaText;
        //_Control.GetComponent<ControlPausa>().ContinuarButton = ContinuarButton;
        //_Control.GetComponent<ControlPausa>().ContinuarButtonText = ContinuarButtonText;
        //_Control.GetComponent<ControlPausa>().SalirButton = SalirButton;
        //_Control.GetComponent<ControlPausa>().SalirButtonText = SalirButtonText;

        // MENU DE PAUSA
        /***********************************/
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (!EscPressed)
        //    {
        //        MenuPausa();
        //    }    
        //    else
        //    {
        //        _CombatBackground.GetComponent<CombatBackground>().EscPressed = false;

        //        ClonMenuPausaBorder.GetComponent<SpriteRenderer>().enabled = false;

        //        ClonMenuPausaContainer.GetComponent<SpriteRenderer>().enabled = false;

        //        PausaText.enabled = false;
        //        ContinuarButton.image.enabled = false;
        //        ContinuarButtonText.enabled = false;
        //        SalirButton.image.enabled = false;
        //        SalirButtonText.enabled = false;
        //    }
        //}
        /***********************************/

        if (!BoolTurnoBatalla)
            StartBattle = ClonStartBattleButton.GetComponent<StartBattleButton>().GetStartBattleStatus();

        BattlePhase();                                 // Actualiza el estado del Combate
        UpdateTurn();                                  // Actualiza el turno del Combate
        StatusAliadosPosition();                       // Comprueba que todos los personajes del Jugador han sido colocados en el mapa de combate
        UpdateOtherScripts();                          // Actualiza constantemente otros scripts con variables

        if (StartBattle)
            StatusCharacters();

        if (TurnoBatalla == "Enemigo")
            EnemyTurn();                               // Realiza las acciones de la IA cuando es el turno del enemigo

        StatusPartida();                               // Comprueba si hay condición de victoria o de derrota
        InterfazVictoriaDerrota();                     // Crea la interfaz de Victoria o derrota
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
            clon.GetComponent<CombatPosition>()._Position = clon;                      // Almacena la posición en si misma para luego tener accesos
            clon.GetComponent<CombatPosition>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
            Positions[i] = clon;                                                       // Añade el clon al array de prefabs CombatPosition
        }
    }

    /****************************************************************************************
     * Función: SetLines                                                                    *
     * Uso: Coloca los caminos que unen las posiciones                                      *
     * Variables entrada:                                                                   *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void SetLines()
    {
        for(int i = 0; i < Lines.Length; i++)
        {
            GameObject clon = Instantiate(CombatLine);                                 // Crea un clon del prefab CombatLine
            clon.transform.position = new Vector3(LinesX[i], LinesY[i], 2);            // Coloca el clon en la primera posición que hay en los arrays de Coordenadas
            clon.transform.localScale = new Vector3(LinesScaleX[i], LinesScaleY, 1);   // Configura su tamaño
            clon.transform.eulerAngles = new Vector3(0, 0, LinesRotationZ[i]);         // Lo rota
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

        if (!VariablesGlobales.instance.Boss) // Si es un combate normal
        {
            Enemies = new GameObject[numEnemies];                      // Crea el array de enemigos con tamaño en base al número de enemigos creados aleatoriamente
            ContEnemigos = numEnemies;                                 // Almacena el número de enemigos en combate

            for (i = 0; i < numEnemies; i++) // Bucle for desde 0 hasta el número de enemigos que habrá en el combate
            {
                enemyType = Random.Range(1, 5); // Elige aleatoriamente el tipo de enemigo a colocar

                do // Bucle While para controlar que la posición elegida aleatoriamente no esté ocupada, si lo está elige otra posición
                {
                    position = Positions[Random.Range(0, Positions.Length)]; // Selecciona una posición al azar del array de prefabs CombatPosition

                } while (IsPositionOccupied(position)); // Si esa posición está ocupada (Occupied == true) vuelve a seleccionar otra, si no lo está, continua

                // Almacena las coordenadas de la posición seleccionada sumándole a "Y" +0.5 por motivos estéticos
                positionVector = new Vector3(position.transform.position.x, position.transform.position.y + 0.5f, (position.transform.position.z - 1));

                if (enemyType == 1)                                              // Si el enemigo elegido es un Caballero
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
                    clon.GetComponent<GeneralEnemy>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
                    clon.GetComponent<GeneralEnemy>()._CombatBackground = _CombatBackground; // Almacena el combate
                    clon.GetComponent<EnemyKnight>()._CombatBackground = _CombatBackground;  // Almacena el combate
                    Enemies[i] = clon;                                          // Mete el clon en el array de enemigos

                    //UI ENEMIGO
                    if( i == 0)
                    {
                        VidaEnemigo1.text = VariablesGlobales.instance.KnightVidaActual + " / " + VariablesGlobales.instance.KnightVidaTotal;
                        AtaqueEnemigo1.text = VariablesGlobales.instance.KnightAtaqueActual + " / " + VariablesGlobales.instance.KnightAtaqueMax;
                        DefensaEnemigo1.text = VariablesGlobales.instance.KnightDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyKnight>().UIEnemigo = UIEnemigo1;
                        clon.GetComponent<EnemyKnight>().VidaEnemigo = VidaEnemigo1;
                        clon.GetComponent<EnemyKnight>().AtaqueEnemigo = AtaqueEnemigo1;
                        clon.GetComponent<EnemyKnight>().DefensaEnemigo = DefensaEnemigo1;
                    }
                    else if (i == 1)
                    {
                        VidaEnemigo2.text = VariablesGlobales.instance.KnightVidaActual + " / " + VariablesGlobales.instance.KnightVidaTotal;
                        AtaqueEnemigo2.text = VariablesGlobales.instance.KnightAtaqueActual + " / " + VariablesGlobales.instance.KnightAtaqueMax;
                        DefensaEnemigo2.text = VariablesGlobales.instance.KnightDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyKnight>().UIEnemigo = UIEnemigo2;
                        clon.GetComponent<EnemyKnight>().VidaEnemigo = VidaEnemigo2;
                        clon.GetComponent<EnemyKnight>().AtaqueEnemigo = AtaqueEnemigo2;
                        clon.GetComponent<EnemyKnight>().DefensaEnemigo = DefensaEnemigo2;
                    }
                    else if (i == 2)
                    {
                        VidaEnemigo3.text = VariablesGlobales.instance.KnightVidaActual + " / " + VariablesGlobales.instance.KnightVidaTotal;
                        AtaqueEnemigo3.text = VariablesGlobales.instance.KnightAtaqueActual + " / " + VariablesGlobales.instance.KnightAtaqueMax;
                        DefensaEnemigo3.text = VariablesGlobales.instance.KnightDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyKnight>().UIEnemigo = UIEnemigo3;
                        clon.GetComponent<EnemyKnight>().VidaEnemigo = VidaEnemigo3;
                        clon.GetComponent<EnemyKnight>().AtaqueEnemigo = AtaqueEnemigo3;
                        clon.GetComponent<EnemyKnight>().DefensaEnemigo = DefensaEnemigo3;
                    }
                    else
                    {
                        VidaEnemigo4.text = VariablesGlobales.instance.KnightVidaActual + " / " + VariablesGlobales.instance.KnightVidaTotal;
                        AtaqueEnemigo4.text = VariablesGlobales.instance.KnightAtaqueActual + " / " + VariablesGlobales.instance.KnightAtaqueMax;
                        DefensaEnemigo4.text = VariablesGlobales.instance.KnightDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyKnight>().UIEnemigo = UIEnemigo4;
                        clon.GetComponent<EnemyKnight>().VidaEnemigo = VidaEnemigo4;
                        clon.GetComponent<EnemyKnight>().AtaqueEnemigo = AtaqueEnemigo4;
                        clon.GetComponent<EnemyKnight>().DefensaEnemigo = DefensaEnemigo4;
                    }
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
                    clon.GetComponent<GeneralEnemy>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
                    clon.GetComponent<GeneralEnemy>()._CombatBackground = _CombatBackground; // Almacena el combate
                    clon.GetComponent<EnemyHealer>()._CombatBackground = _CombatBackground;  // Almacena el combate
                    Enemies[i] = clon;                                          // Mete el clon en el array de enemigos

                    //UI ENEMIGO
                    if (i == 0)
                    {
                        VidaEnemigo1.text = VariablesGlobales.instance.HealerVidaActual + " / " + VariablesGlobales.instance.HealerVidaTotal;
                        AtaqueEnemigo1.text = VariablesGlobales.instance.HealerAtaqueActual + " / " + VariablesGlobales.instance.HealerAtaqueMax;
                        DefensaEnemigo1.text = VariablesGlobales.instance.HealerDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyHealer>().UIEnemigo = UIEnemigo1;
                        clon.GetComponent<EnemyHealer>().VidaEnemigo = VidaEnemigo1;
                        clon.GetComponent<EnemyHealer>().AtaqueEnemigo = AtaqueEnemigo1;
                        clon.GetComponent<EnemyHealer>().DefensaEnemigo = DefensaEnemigo1;
                    }
                    else if (i == 1)
                    {
                        VidaEnemigo2.text = VariablesGlobales.instance.HealerVidaActual + " / " + VariablesGlobales.instance.HealerVidaTotal;
                        AtaqueEnemigo2.text = VariablesGlobales.instance.HealerAtaqueActual + " / " + VariablesGlobales.instance.HealerAtaqueMax;
                        DefensaEnemigo2.text = VariablesGlobales.instance.HealerDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyHealer>().UIEnemigo = UIEnemigo2;
                        clon.GetComponent<EnemyHealer>().VidaEnemigo = VidaEnemigo2;
                        clon.GetComponent<EnemyHealer>().AtaqueEnemigo = AtaqueEnemigo2;
                        clon.GetComponent<EnemyHealer>().DefensaEnemigo = DefensaEnemigo2;
                    }
                    else if (i == 2)
                    {
                        VidaEnemigo3.text = VariablesGlobales.instance.HealerVidaActual + " / " + VariablesGlobales.instance.HealerVidaTotal;
                        AtaqueEnemigo3.text = VariablesGlobales.instance.HealerAtaqueActual + " / " + VariablesGlobales.instance.HealerAtaqueMax;
                        DefensaEnemigo3.text = VariablesGlobales.instance.HealerDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyHealer>().UIEnemigo = UIEnemigo3;
                        clon.GetComponent<EnemyHealer>().VidaEnemigo = VidaEnemigo3;
                        clon.GetComponent<EnemyHealer>().AtaqueEnemigo = AtaqueEnemigo3;
                        clon.GetComponent<EnemyHealer>().DefensaEnemigo = DefensaEnemigo3;
                    }
                    else
                    {
                        VidaEnemigo4.text = VariablesGlobales.instance.HealerVidaActual + " / " + VariablesGlobales.instance.HealerVidaTotal;
                        AtaqueEnemigo4.text = VariablesGlobales.instance.HealerAtaqueActual + " / " + VariablesGlobales.instance.HealerAtaqueMax;
                        DefensaEnemigo4.text = VariablesGlobales.instance.HealerDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyHealer>().UIEnemigo = UIEnemigo4;
                        clon.GetComponent<EnemyHealer>().VidaEnemigo = VidaEnemigo4;
                        clon.GetComponent<EnemyHealer>().AtaqueEnemigo = AtaqueEnemigo4;
                        clon.GetComponent<EnemyHealer>().DefensaEnemigo = DefensaEnemigo4;
                    }
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
                    clon.GetComponent<GeneralEnemy>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
                    clon.GetComponent<GeneralEnemy>()._CombatBackground = _CombatBackground; // Almacena el combate
                    clon.GetComponent<EnemySlime>()._CombatBackground = _CombatBackground;   // Almacena el combate
                    Enemies[i] = clon;                                          // Mete el clon en el array de enemigos

                    //UI ENEMIGO
                    if (i == 0)
                    {
                        VidaEnemigo1.text = VariablesGlobales.instance.SlimeVidaActual + " / " + VariablesGlobales.instance.SlimeVidaTotal;
                        AtaqueEnemigo1.text = VariablesGlobales.instance.SlimeAtaqueActual + " / " + VariablesGlobales.instance.SlimeAtaqueMax;
                        DefensaEnemigo1.text = VariablesGlobales.instance.SlimeDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemySlime>().UIEnemigo = UIEnemigo1;
                        clon.GetComponent<EnemySlime>().VidaEnemigo = VidaEnemigo1;
                        clon.GetComponent<EnemySlime>().AtaqueEnemigo = AtaqueEnemigo1;
                        clon.GetComponent<EnemySlime>().DefensaEnemigo = DefensaEnemigo1;
                    }
                    else if (i == 1)
                    {
                        VidaEnemigo2.text = VariablesGlobales.instance.SlimeVidaActual + " / " + VariablesGlobales.instance.SlimeVidaTotal;
                        AtaqueEnemigo2.text = VariablesGlobales.instance.SlimeAtaqueActual + " / " + VariablesGlobales.instance.SlimeAtaqueMax;
                        DefensaEnemigo2.text = VariablesGlobales.instance.SlimeDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemySlime>().UIEnemigo = UIEnemigo2;
                        clon.GetComponent<EnemySlime>().VidaEnemigo = VidaEnemigo2;
                        clon.GetComponent<EnemySlime>().AtaqueEnemigo = AtaqueEnemigo2;
                        clon.GetComponent<EnemySlime>().DefensaEnemigo = DefensaEnemigo2;
                    }
                    else if (i == 2)
                    {
                        VidaEnemigo3.text = VariablesGlobales.instance.SlimeVidaActual + " / " + VariablesGlobales.instance.SlimeVidaTotal;
                        AtaqueEnemigo3.text = VariablesGlobales.instance.SlimeAtaqueActual + " / " + VariablesGlobales.instance.SlimeAtaqueMax;
                        DefensaEnemigo3.text = VariablesGlobales.instance.SlimeDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemySlime>().UIEnemigo = UIEnemigo3;
                        clon.GetComponent<EnemySlime>().VidaEnemigo = VidaEnemigo3;
                        clon.GetComponent<EnemySlime>().AtaqueEnemigo = AtaqueEnemigo3;
                        clon.GetComponent<EnemySlime>().DefensaEnemigo = DefensaEnemigo3;
                    }
                    else
                    {
                        VidaEnemigo4.text = VariablesGlobales.instance.SlimeVidaActual + " / " + VariablesGlobales.instance.SlimeVidaTotal;
                        AtaqueEnemigo4.text = VariablesGlobales.instance.SlimeAtaqueActual + " / " + VariablesGlobales.instance.SlimeAtaqueMax;
                        DefensaEnemigo4.text = VariablesGlobales.instance.SlimeDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemySlime>().UIEnemigo = UIEnemigo4;
                        clon.GetComponent<EnemySlime>().VidaEnemigo = VidaEnemigo4;
                        clon.GetComponent<EnemySlime>().AtaqueEnemigo = AtaqueEnemigo4;
                        clon.GetComponent<EnemySlime>().DefensaEnemigo = DefensaEnemigo4;
                    }
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
                    clon.GetComponent<GeneralEnemy>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
                    clon.GetComponent<GeneralEnemy>()._CombatBackground = _CombatBackground; // Almacena el combate
                    clon.GetComponent<EnemyMage>()._CombatBackground = _CombatBackground;    // Almacena el combate
                    Enemies[i] = clon;                                          // Mete el clon en el array de enemigos

                    //UI ENEMIGO
                    if (i == 0)
                    {
                        VidaEnemigo1.text = VariablesGlobales.instance.MageVidaActual + " / " + VariablesGlobales.instance.MageVidaTotal;
                        AtaqueEnemigo1.text = VariablesGlobales.instance.MageAtaqueActual + " / " + VariablesGlobales.instance.MageAtaqueMax;
                        DefensaEnemigo1.text = VariablesGlobales.instance.MageDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyMage>().UIEnemigo = UIEnemigo1;
                        clon.GetComponent<EnemyMage>().VidaEnemigo = VidaEnemigo1;
                        clon.GetComponent<EnemyMage>().AtaqueEnemigo = AtaqueEnemigo1;
                        clon.GetComponent<EnemyMage>().DefensaEnemigo = DefensaEnemigo1;
                    }
                    else if (i == 1)
                    {
                        VidaEnemigo2.text = VariablesGlobales.instance.MageVidaActual + " / " + VariablesGlobales.instance.MageVidaTotal;
                        AtaqueEnemigo2.text = VariablesGlobales.instance.MageAtaqueActual + " / " + VariablesGlobales.instance.MageAtaqueMax;
                        DefensaEnemigo2.text = VariablesGlobales.instance.MageDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyMage>().UIEnemigo = UIEnemigo2;
                        clon.GetComponent<EnemyMage>().VidaEnemigo = VidaEnemigo2;
                        clon.GetComponent<EnemyMage>().AtaqueEnemigo = AtaqueEnemigo2;
                        clon.GetComponent<EnemyMage>().DefensaEnemigo = DefensaEnemigo2;
                    }
                    else if (i == 2)
                    {
                        VidaEnemigo3.text = VariablesGlobales.instance.MageVidaActual + " / " + VariablesGlobales.instance.MageVidaTotal;
                        AtaqueEnemigo3.text = VariablesGlobales.instance.MageAtaqueActual + " / " + VariablesGlobales.instance.MageAtaqueMax;
                        DefensaEnemigo3.text = VariablesGlobales.instance.MageDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyMage>().UIEnemigo = UIEnemigo3;
                        clon.GetComponent<EnemyMage>().VidaEnemigo = VidaEnemigo3;
                        clon.GetComponent<EnemyMage>().AtaqueEnemigo = AtaqueEnemigo3;
                        clon.GetComponent<EnemyMage>().DefensaEnemigo = DefensaEnemigo3;
                    }
                    else
                    {
                        VidaEnemigo4.text = VariablesGlobales.instance.MageVidaActual + " / " + VariablesGlobales.instance.MageVidaTotal;
                        AtaqueEnemigo4.text = VariablesGlobales.instance.MageAtaqueActual + " / " + VariablesGlobales.instance.MageAtaqueMax;
                        DefensaEnemigo4.text = VariablesGlobales.instance.MageDefensaActualPercentage + "% / 50%";

                        clon.GetComponent<EnemyMage>().UIEnemigo = UIEnemigo4;
                        clon.GetComponent<EnemyMage>().VidaEnemigo = VidaEnemigo4;
                        clon.GetComponent<EnemyMage>().AtaqueEnemigo = AtaqueEnemigo4;
                        clon.GetComponent<EnemyMage>().DefensaEnemigo = DefensaEnemigo4;
                    }
                }
            }

            // Bucle para almacenar en cada enemigo el array de enemigos
            for (int j = 0; j < Enemies.Length; j++)
                Enemies[j].GetComponent<GeneralEnemy>().Enemies = Enemies;

            NumEnemigos = numEnemies;
        }
        else // Si es el combate contra el boss
        {
            Enemies = new GameObject[1]; // Crea el array de enemigos con tamaño en base al número de enemigos creados aleatoriamente
            ContEnemigos = 1;

            do // Bucle While para controlar que la posición elegida aleatoriamente no esté ocupada, si lo está elige otra posición
            {
                position = Positions[Random.Range(0, Positions.Length)]; // Selecciona una posición al azar del array de prefabs CombatPosition

            } while (IsPositionOccupied(position)); // Si esa posición está ocupada (Occupied == true) vuelve a seleccionar otra, si no lo está, continua

            // Almacena las coordenadas de la posición seleccionada sumándole a "Y" +0.5 por motivos estéticos
            positionVector = new Vector3(position.transform.position.x, position.transform.position.y + 0.5f, (position.transform.position.z - 1));

            GameObject clon = Instantiate(PrefabBoss);                  // Crea un clon del prefab Boss
            clon.transform.position = positionVector;                   // Coloca el clon en la posición escogida aleatoriamente
            position.GetComponent<CombatPosition>().Occupied = true;    // Cambia esa posición a "ocupada"
            position.GetComponent<CombatPosition>().CharacterType = 1;  // Indica que la posición está ocupada por un enemigo
            position.GetComponent<CombatPosition>().Character = clon;   // Almacena el personaje en la posición en la que se ubicará
            clon.GetComponent<Boss>().EnemyPosition = position;  // Almacena la posición del enemigo
            clon.GetComponent<Boss>().Positions = Positions;     // Almacena el array de posiciones del combate
            clon.GetComponent<Boss>().Index = 5;        // Almacena el tipo de enemigo que es
            clon.GetComponent<Boss>().EnemyPosition = position; // Alamcena la posición del enemigo
            clon.GetComponent<Boss>().Enemy = clon;             // Almacena el enemigo
            clon.GetComponent<Boss>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
            clon.GetComponent<Boss>()._CombatBackground = _CombatBackground; // Almacena el combate
            clon.GetComponent<Boss>()._CombatBackground = _CombatBackground;  // Almacena el combate
            Enemies[0] = clon;

            //UI ENEMIGO
            VidaEnemigo1.text = clon.GetComponent<Boss>().VidaActual + " / " + clon.GetComponent<Boss>().VidaTotal;
            AtaqueEnemigo1.text = clon.GetComponent<Boss>().AtaqueActual + " / " + clon.GetComponent<Boss>().AtaqueMax;
            DefensaEnemigo1.text = clon.GetComponent<Boss>().DefensePercentage() + "% / 50%";

            clon.GetComponent<Boss>().UIEnemigo = UIEnemigo1;
            clon.GetComponent<Boss>().VidaEnemigo = VidaEnemigo1;
            clon.GetComponent<Boss>().AtaqueEnemigo = AtaqueEnemigo1;
            clon.GetComponent<Boss>().DefensaEnemigo = DefensaEnemigo1;

            NumEnemigos = 1;
        }
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
                if (VariablesGlobales.instance.KnightVivo)
                {
                    GameObject aliadoColocar = Instantiate(PrefabPlayerKnight);                               // Crea un clon del Knight
                    aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posición de la interfaz
                    aliadoColocar.GetComponent<DragDrop>().GetPositions(Positions);                           // Pasa el array de posiciones de combate al Aliado que acaba de colocar
                    aliadoColocar.GetComponent<GeneralPlayer>().Positions = Positions;                        // Almacena en el personaje el array de posiciones de combate
                    aliadoColocar.GetComponent<GeneralPlayer>().Character = aliadoColocar;                    // Almacena el personaje creado
                    aliadoColocar.GetComponent<GeneralPlayer>().CharacterType = 1;                            // Almacena el tipo de enemigo
                    aliadoColocar.GetComponent<GeneralPlayer>().Enemies = Enemies;                            // Almacena el array de enemigos del combate
                    aliadoColocar.GetComponent<GeneralPlayer>().UIMover = UIMover;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIAtacarConRango = UIAtacarConRango;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIAtacarSinRango = UIAtacarSinRango;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadKnight = UIHabilidadKnight;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadHealer = UIHabilidadHealer;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadSlime = UIHabilidadSlime;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadMage = UIHabilidadMage;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoVida = TextoVida;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoAtaque = TextoAtaque;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoDefensa = TextoDefensa;
                    aliadoColocar.GetComponent<GeneralPlayer>().VidaImagen = VidaImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().AtaqueImagen = AtaqueImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().DefensaImagen = DefensaImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaVida = EstadisticaVida;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaAtaque = EstadisticaAtaque;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaDefensa = EstadisticaDefensa;
                    aliadoColocar.GetComponent<GeneralPlayer>()._CombatBackground = _CombatBackground;        // Almacena el combate
                    aliadoColocar.GetComponent<DragDrop>()._CombatBackground = _CombatBackground;             // Almacena el combate
                    aliadoColocar.GetComponent<PlayerKnight>()._CombatBackground = _CombatBackground;         // Almacena el combate
                    Aliados[i] = aliadoColocar;                                                               // Mete el clon en el array de personajes del jugador

                    ContAliados++;                                                                                // Aumenta el contador de aliados en combate
                }
            }
            else if (i == 1)                                                                              // Si el enemigo es un Healer
            {
                if (VariablesGlobales.instance.HealerVivo)
                {
                    GameObject aliadoColocar = Instantiate(PrefabPlayerHealer);                               // Crea un clon del Healer
                    aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posición de la interfaz
                    aliadoColocar.GetComponent<DragDrop>().GetPositions(Positions);                           // Pasa el array de posiciones de combate al Aliado que acaba de colocar
                    aliadoColocar.GetComponent<GeneralPlayer>().Positions = Positions;                        // Almacena en el personaje el array de posiciones de combate
                    aliadoColocar.GetComponent<GeneralPlayer>().Character = aliadoColocar;                    // Almacena el personaje creado
                    aliadoColocar.GetComponent<GeneralPlayer>().CharacterType = 2;                            // Almacena el tipo de enemigo
                    aliadoColocar.GetComponent<GeneralPlayer>().Enemies = Enemies;                            // Almacena el array de enemigos del combate
                    aliadoColocar.GetComponent<GeneralPlayer>().UIMover = UIMover;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIAtacarConRango = UIAtacarConRango;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIAtacarSinRango = UIAtacarSinRango;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadKnight = UIHabilidadKnight;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadHealer = UIHabilidadHealer;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadSlime = UIHabilidadSlime;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadMage = UIHabilidadMage;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoVida = TextoVida;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoAtaque = TextoAtaque;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoDefensa = TextoDefensa;
                    aliadoColocar.GetComponent<GeneralPlayer>().VidaImagen = VidaImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().AtaqueImagen = AtaqueImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().DefensaImagen = DefensaImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaVida = EstadisticaVida;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaAtaque = EstadisticaAtaque;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaDefensa = EstadisticaDefensa;
                    aliadoColocar.GetComponent<GeneralPlayer>()._CombatBackground = _CombatBackground;        // Almacena el combate
                    aliadoColocar.GetComponent<DragDrop>()._CombatBackground = _CombatBackground;             // Almacena el combate
                    aliadoColocar.GetComponent<PlayerHealer>()._CombatBackground = _CombatBackground;         // Almacena el combate
                    Aliados[i] = aliadoColocar;                                                               // Mete el clon en el array de personajes del jugador

                    ContAliados++;                                                                                // Aumenta el contador de aliados en combate
                }
            }
            else if (i == 2)                                                                              // Si el enemigo es un Slime
            {
                if (VariablesGlobales.instance.SlimeVivo)
                {
                    GameObject aliadoColocar = Instantiate(PrefabPlayerSlime);                                // Crea un clon del Slime
                    aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posición de la interfaz
                    aliadoColocar.GetComponent<DragDrop>().GetPositions(Positions);                           // Pasa el array de posiciones de combate al Aliado que acaba de colocar
                    aliadoColocar.GetComponent<GeneralPlayer>().Positions = Positions;                        // Almacena en el personaje el array de posiciones de combate
                    aliadoColocar.GetComponent<GeneralPlayer>().Character = aliadoColocar;                    // Almacena el personaje creado
                    aliadoColocar.GetComponent<GeneralPlayer>().CharacterType = 3;                            // Almacena el tipo de enemigo
                    aliadoColocar.GetComponent<GeneralPlayer>().Enemies = Enemies;                            // Almacena el array de enemigos del combate
                    aliadoColocar.GetComponent<GeneralPlayer>().UIMover = UIMover;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIAtacarConRango = UIAtacarConRango;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIAtacarSinRango = UIAtacarSinRango;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadKnight = UIHabilidadKnight;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadHealer = UIHabilidadHealer;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadSlime = UIHabilidadSlime;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadMage = UIHabilidadMage;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoVida = TextoVida;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoAtaque = TextoAtaque;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoDefensa = TextoDefensa;
                    aliadoColocar.GetComponent<GeneralPlayer>().VidaImagen = VidaImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().AtaqueImagen = AtaqueImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().DefensaImagen = DefensaImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaVida = EstadisticaVida;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaAtaque = EstadisticaAtaque;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaDefensa = EstadisticaDefensa;
                    aliadoColocar.GetComponent<GeneralPlayer>()._CombatBackground = _CombatBackground;        // Almacena el combate
                    aliadoColocar.GetComponent<DragDrop>()._CombatBackground = _CombatBackground;             // Almacena el combate
                    aliadoColocar.GetComponent<PlayerSlime>()._CombatBackground = _CombatBackground;          // Almacena el combate
                    Aliados[i] = aliadoColocar;                                                               // Mete el clon en el array de personajes del jugador

                    ContAliados++;                                                                                // Aumenta el contador de aliados en combate
                }
            }
            else                                                                                          // Si el enemigo es un Mage
            {
                if (VariablesGlobales.instance.MageVivo)
                {
                    GameObject aliadoColocar = Instantiate(PrefabPlayerMage);                                 // Crea un clon del Mage
                    aliadoColocar.transform.position = new Vector3(AliadoColocarX[i], AliadoColocarY[i], -1); // Coloca el clon es la posición de la interfaz
                    aliadoColocar.GetComponent<DragDrop>().GetPositions(Positions);                           // Pasa el array de posiciones de combate al Aliado que acaba de colocar
                    aliadoColocar.GetComponent<GeneralPlayer>().Positions = Positions;                        // Almacena en el personaje el array de posiciones de combate
                    aliadoColocar.GetComponent<GeneralPlayer>().Character = aliadoColocar;                    // Almacena el personaje creado
                    aliadoColocar.GetComponent<GeneralPlayer>().CharacterType = 4;                            // Almacena el tipo de enemigo
                    aliadoColocar.GetComponent<GeneralPlayer>().Enemies = Enemies;                            // Almacena el array de enemigos del combate
                    aliadoColocar.GetComponent<GeneralPlayer>().UIMover = UIMover;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIAtacarConRango = UIAtacarConRango;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIAtacarSinRango = UIAtacarSinRango;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadKnight = UIHabilidadKnight;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadHealer = UIHabilidadHealer;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadSlime = UIHabilidadSlime;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIHabilidadMage = UIHabilidadMage;
                    aliadoColocar.GetComponent<GeneralPlayer>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoVida = TextoVida;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoAtaque = TextoAtaque;
                    aliadoColocar.GetComponent<GeneralPlayer>().TextoDefensa = TextoDefensa;
                    aliadoColocar.GetComponent<GeneralPlayer>().VidaImagen = VidaImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().AtaqueImagen = AtaqueImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().DefensaImagen = DefensaImagen;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaVida = EstadisticaVida;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaAtaque = EstadisticaAtaque;
                    aliadoColocar.GetComponent<GeneralPlayer>().EstadisticaDefensa = EstadisticaDefensa;
                    aliadoColocar.GetComponent<GeneralPlayer>()._CombatBackground = _CombatBackground;        // Almacena el combate
                    aliadoColocar.GetComponent<DragDrop>()._CombatBackground = _CombatBackground;             // Almacena el combate
                    aliadoColocar.GetComponent<PlayerMage>()._CombatBackground = _CombatBackground;           // Almacena el combate
                    Aliados[i] = aliadoColocar;                                                               // Mete el clon en el array de personajes del jugador

                    ContAliados++;                                                                                // Aumenta el contador de aliados en combate
                }
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

        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigo1 = UIEnemigo1;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigo2 = UIEnemigo2;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigo3 = UIEnemigo3;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigo4 = UIEnemigo4;
        ClonStartBattleButton.GetComponent<StartBattleButton>().NumEnemigos = NumEnemigos;

        ClonStartBattleButton.GetComponent<StartBattleButton>().BorderUIEnemigo = BorderUIEnemigo;
        ClonStartBattleButton.GetComponent<StartBattleButton>().ContainerUIEnemigo = ContainerUIEnemigo;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigoKnight = UIEnemigoKnight;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigoHealer = UIEnemigoHealer;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigoSlime = UIEnemigoSlime;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigoMage = UIEnemigoMage;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigoBoss = UIEnemigoBoss;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigoCorazon = UIEnemigoCorazon;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigoEspada = UIEnemigoEspada;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEnemigoEscudo = UIEnemigoEscudo;

        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaVidaEnemy1 = UIEstadisticaVidaEnemy1;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaAtaqueEnemy1 = UIEstadisticaAtaqueEnemy1;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaDefensaEnemy1 = UIEstadisticaDefensaEnemy1;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaVidaEnemy2 = UIEstadisticaVidaEnemy2;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaAtaqueEnemy2 = UIEstadisticaAtaqueEnemy2;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaDefensaEnemy2 = UIEstadisticaDefensaEnemy2;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaVidaEnemy3 = UIEstadisticaVidaEnemy3;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaAtaqueEnemy3 = UIEstadisticaAtaqueEnemy3;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaDefensaEnemy3 = UIEstadisticaDefensaEnemy3;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaVidaEnemy4 = UIEstadisticaVidaEnemy4;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaAtaqueEnemy4 = UIEstadisticaAtaqueEnemy4;
        ClonStartBattleButton.GetComponent<StartBattleButton>().UIEstadisticaDefensaEnemy4 = UIEstadisticaDefensaEnemy4;

        ClonStartBattleButton.GetComponent<StartBattleButton>().Enemies = Enemies;
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

            // TEXTO EXPLICACIÓN
            /************************************************************************************************************************/
            _CombatBackground.GetComponent<CombatBackground>().ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeText("");
            /************************************************************************************************************************/

            EstadisticaVida.SetActive(false);
            EstadisticaAtaque.SetActive(false);
            EstadisticaDefensa.SetActive(false);

            TurnoBatalla = "Enemigo";                                           // El turno pasa a ser del Enemigo
        }
        else                                                                    // Si turno actual era del Enemigo
        {
            // TEXTO EXPLICACIÓN
            /************************************************************************************************************************/
            _CombatBackground.GetComponent<CombatBackground>().ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeText("Haz click en alguno de tus heroes para realizar una accion");
            /************************************************************************************************************************/

            TurnoBatalla = "Jugador";                                           // El turno pasa a ser del Jugador
            
            // Actualiza el contador de invulnerabilidad del Knight del Jugador
            for(int i = 0; i < Aliados.Length; i++)                                               // Recorre el array de personajes del Jugador
                if (Aliados[i] != null)
                    if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 1)              // Si es el Knight
                        if (Aliados[i].GetComponent<PlayerKnight>().Invencible)                   // Si está en modo invencible
                            Aliados[i].GetComponent<PlayerKnight>().PlayerKnightInvencibleCont++; // Aumenta el contador

            // Actualiza el contador de la habilidad del Slime del Jugador
            //for(int i = 0; i < Enemies.Length; i++)
            //{
            //    if (Enemies[i] != null)
            //    {
            //        if (!VariablesGlobales.instance.Boss)
            //        {
            //            if (Enemies[i].GetComponent<GeneralEnemy>().Index == 1)
            //            {
            //                if (Enemies[i].GetComponent<EnemyKnight>().HabilidadSlime == true)
            //                    ContHabilidadSlime++;
            //            }
            //            else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 2)
            //            {
            //                if (Enemies[i].GetComponent<EnemyHealer>().HabilidadSlime == true)
            //                    ContHabilidadSlime++;
            //            }
            //            else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 3)
            //            {
            //                if (Enemies[i].GetComponent<EnemySlime>().HabilidadSlime == true)
            //                    ContHabilidadSlime++;
            //            }
            //            else
            //            {
            //                if (Enemies[i].GetComponent<EnemyMage>().HabilidadSlime == true)
            //                    ContHabilidadSlime++;
            //            }
            //        }
            //        else
            //        {
            //            if (Enemies[i].GetComponent<Boss>().HabilidadSlime == true)
            //                ContHabilidadSlime++;
            //        }
            //    }
            //}

            //// Actualiza el contador de la habilidad del Mage del Jugador
            //for (int i = 0; i < Aliados.Length; i++)
            //{
            //    if (Aliados[i] != null)
            //    {
            //        if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 1)
            //        {
            //            if (Aliados[i].GetComponent<PlayerKnight>().HabilidadMage == true)
            //                ContHabilidadMage++;
            //        }
            //        else if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 2)
            //        {
            //            if (Aliados[i].GetComponent<PlayerHealer>().HabilidadMage == true)
            //                ContHabilidadMage++;
            //        }
            //        else if (Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 3)
            //        {
            //            if (Aliados[i].GetComponent<PlayerSlime>().HabilidadMage == true)
            //                ContHabilidadMage++;
            //        }
            //        else
            //        {
            //            if (Aliados[i].GetComponent<PlayerMage>().HabilidadMage == true)
            //                ContHabilidadMage++;
            //        }
            //    }
            //}

            if (Aliados[2] != null)
            {
                if (Aliados[2].GetComponent<PlayerSlime>().UsedAbility == true)
                    ContHabilidadSlime ++;
            }
            else
            {
                for (int k = 0; k < Enemies.Length; k++)
                {
                    if (Enemies[k] != null)
                    {
                        if (!VariablesGlobales.instance.Boss)
                        {
                            if (Enemies[k].GetComponent<GeneralEnemy>().Index == 1)
                            {
                                if (Enemies[k].GetComponent<EnemyKnight>().HabilidadSlime == true)
                                    ContHabilidadSlime++;
                            }
                            else if (Enemies[k].GetComponent<GeneralEnemy>().Index == 2)
                            {
                                if (Enemies[k].GetComponent<EnemyHealer>().HabilidadSlime == true)
                                    ContHabilidadSlime++;
                            }
                            else if (Enemies[k].GetComponent<GeneralEnemy>().Index == 3)
                            {
                                if (Enemies[k].GetComponent<EnemySlime>().HabilidadSlime == true)
                                    ContHabilidadSlime++;
                            }
                            else
                            {
                                if (Enemies[k].GetComponent<EnemyMage>().HabilidadSlime == true)
                                    ContHabilidadSlime++;
                            }
                        }
                        else
                        {
                            if (Enemies[k].GetComponent<Boss>().HabilidadSlime == true)
                                ContHabilidadSlime++;
                        }
                    }
                }
            }

            if (Aliados[3] != null)
            {
                if (Aliados[3].GetComponent<PlayerMage>().UsedAbility == true)
                    ContHabilidadMage++;
            }
            else
            {
                for (int k = 0; k < Aliados.Length; k++)
                {
                    if (Aliados[k] != null)
                    {
                        if (Aliados[k].GetComponent<GeneralPlayer>().CharacterType == 1)
                        {
                            if (Aliados[k].GetComponent<PlayerKnight>().HabilidadMage == true)
                                ContHabilidadMage++;
                        }
                        else if (Aliados[k].GetComponent<GeneralPlayer>().CharacterType == 2)
                        {
                            if (Aliados[k].GetComponent<PlayerHealer>().HabilidadMage == true)
                                ContHabilidadMage++;
                        }
                        else if (Aliados[k].GetComponent<GeneralPlayer>().CharacterType == 3)
                        {
                            if (Aliados[k].GetComponent<PlayerSlime>().HabilidadMage == true)
                                ContHabilidadMage++;
                        }
                        else
                        {
                            if (Aliados[k].GetComponent<PlayerMage>().HabilidadMage == true)
                                ContHabilidadMage++;
                        }
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
                // TEXTO EXPLICACIÓN
                /************************************************************************************************************************/
                ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeText("Haz click en alguno de tus heroes para realizar una accion");
                /************************************************************************************************************************/

                Destroy(ClonStartBattleButton);                                    // Elimina el botón de ¡Comenzar Batalla!

                for (int i = 0; i < Aliados.Length; i++)                           // Recorre el array de los personajes del jugador
                {
                    if (Aliados[i] != null)
                        Aliados[i].GetComponent<DragDrop>().enabled = false;       // Desactiva el script que permite arrastrarlos durante la fase de preparación del combate
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

        if(!Victoria && !Derrota)
        {
            //do
            //{
            //    do
            //    {
            //        a = Random.Range(0, Enemies.Length);
            //    } while (Enemies[a] == null);

            //    enemySelected = Enemies[a]; // Selecciona un enemigo al azar con el que hacer la acción

            //    for (int i = 0; i < 4; i++)                                                                                                // Recorre el array de posiciones en rango de la del enemigo
            //    {
            //        if (!VariablesGlobales.instance.Boss)
            //            position = enemySelected.GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().PositionsToMove[i]; // Comprueba la primera posicicón en rango del enemigo
            //        else
            //            position = enemySelected.GetComponent<Boss>().EnemyPosition.GetComponent<CombatPosition>().PositionsToMove[i]; // Comprueba la primera posicicón en rango del enemigo

            //        if (Positions[position].GetComponent<CombatPosition>().CharacterType == 2)                                             // Si la posición seleccionada está ocupada por un personaje del Jugador
            //            cont++;                                                                                                            // Aumenta el contador
            //    }

            //} while (cont == 0); // Repite hasta que un enemigo con opción en rango de un personaje del Jugador sea seleccionado

            do
            {
                a = Random.Range(0, Enemies.Length);
            } while (Enemies[a] == null);

            enemySelected = Enemies[a]; // Selecciona un enemigo al azar con el que hacer la acción

            for (int i = 0; i < 4; i++)                                                                                                // Recorre el array de posiciones en rango del enemigo
            {
                if (!VariablesGlobales.instance.Boss)
                    position = enemySelected.GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().PositionsToMove[i]; // Comprueba la primera posicicón en rango del enemigo
                else
                    position = enemySelected.GetComponent<Boss>().EnemyPosition.GetComponent<CombatPosition>().PositionsToMove[i]; // Comprueba la primera posicicón en rango del enemigo

                if (Positions[position].GetComponent<CombatPosition>().CharacterType == 2)                                             // Si la posición seleccionada está ocupada por un personaje del Jugador
                    cont++;                                                                                                            // Aumenta el contador
            }

            if (EnemigoParaAtacar)
            {
                if (!VariablesGlobales.instance.Boss)
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
                else
                {
                    enemySelected.GetComponent<Boss>().EnemyAtack();
                    EnemigoParaAtacar = false;
                }
            }

            if (CambiarTurno)                   // Si CambiarTurno es true
            {
                ChangeTurn();                   // Cambia de turno
                CambiarTurno = false;           // Pone CmabiarTurno a false
            }
        }
        else
        {
            TurnoBatalla = "Enemigo"; // Cambia de turno
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


        for (i = 0; i < Enemies.Length; i++)                                       // Recorre el array de enemigos del combate
        {
            if (!VariablesGlobales.instance.Boss)
            {
                if (Enemies[i] != null)
                {
                    if (Enemies[i].GetComponent<GeneralEnemy>().Index == 1)               // Si es un Knight
                    {
                        if (Enemies[i].GetComponent<EnemyKnight>().VidaActual <= 0)       // Si tiene 0 o menos de vida actual
                        {
                            Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                            Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                            Enemies[i].GetComponent<EnemyKnight>().UIEnemigo.SetActive(false);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo);
                            Destroy(Enemies[i].GetComponent<EnemyKnight>().ClonHealthbar);                                          // Destruye su Healthbar asociada
                            Destroy(Enemies[i]);                                          // Lo destruye
                            ContEnemigos--;                                               // Reduce el´contador de enemigos en Combate
                        }
                    }
                    else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 2)          // Si es un Healer
                    {
                        if (Enemies[i].GetComponent<EnemyHealer>().VidaActual <= 0)       // Si tiene 0 o menos de vida actual
                        {
                            Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                            Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                            Enemies[i].GetComponent<EnemyHealer>().UIEnemigo.SetActive(false);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo);
                            Destroy(Enemies[i].GetComponent<EnemyHealer>().ClonHealthbar);                                        // Destruye su Healthbar asociada
                            Destroy(Enemies[i]);                                          // Lo destruye
                            ContEnemigos--;                                               // Reduce el´contador de enemigos en Combate
                        }
                    }
                    else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 3)          // Si es un Slime
                    {
                        if (Enemies[i].GetComponent<EnemySlime>().VidaActual <= 0)        // Si tiene 0 o menos de vida actual
                        {
                            Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                            Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                            Enemies[i].GetComponent<EnemySlime>().UIEnemigo.SetActive(false);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo);
                            Destroy(Enemies[i].GetComponent<EnemySlime>().ClonHealthbar);                                        // Destruye su Healthbar asociada
                            Destroy(Enemies[i]);                                          // Lo destruye
                            ContEnemigos--;                                               // Reduce el´contador de enemigos en Combate
                        }
                    }
                    else                                                                  // Si es un Mage
                    {
                        if (Enemies[i].GetComponent<EnemyMage>().VidaActual <= 0)         // Si tiene 0 o menos de vida actual
                        {
                            Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                            Enemies[i].GetComponent<GeneralEnemy>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                            Enemies[i].GetComponent<EnemyMage>().UIEnemigo.SetActive(false);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada);
                            Destroy(Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo);
                            Destroy(Enemies[i].GetComponent<EnemyMage>().ClonHealthbar);                                        // Destruye su Healthbar asociada
                            Destroy(Enemies[i]);                                          // Lo destruye
                            ContEnemigos--;                                               // Reduce el´contador de enemigos en Combate
                        }
                    }
                }
            }
            else
            {
                if (Enemies[i] != null)
                {
                    if (Enemies[i].GetComponent<Boss>().VidaActual <= 0)         // Si tiene 0 o menos de vida actual
                    {
                        Enemies[i].GetComponent<Boss>().EnemyPosition.GetComponent<CombatPosition>().CharacterType = 0; // Establece que su posición pasa a estar vacía
                        Enemies[i].GetComponent<Boss>().EnemyPosition.GetComponent<CombatPosition>().Occupied = false;  // Establece que su posición pasa a estar vacía
                        Enemies[i].GetComponent<Boss>().UIEnemigo.SetActive(false);
                        Destroy(Enemies[i].GetComponent<Boss>().UIEnemigoImagen);
                        Destroy(Enemies[i].GetComponent<Boss>().UIEnemigoCorazon);
                        Destroy(Enemies[i].GetComponent<Boss>().UIEnemigoEspada);
                        Destroy(Enemies[i].GetComponent<Boss>().UIEnemigoEscudo);
                        Destroy(Enemies[i].GetComponent<Boss>().ClonHealthbar);                                        // Destruye su Healthbar asociada
                        Destroy(Enemies[i]);                                          // Lo destruye
                        ContEnemigos--;                                               // Reduce el´contador de enemigos en Combate
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
                        ContAliados--;                                                // Reduce el´contador de personajes del Jugador en Combate
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
                        ContAliados--;                                                // Reduce el´contador de personajes del Jugador en Combate
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
                        ContAliados--;                                                // Reduce el´contador de personajes del Jugador en Combate
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
                        ContAliados--;                                                // Reduce el´contador de personajes del Jugador en Combate
                    }
                }
            }
        }
    }

    /****************************************************************************************
     * Función: StatusPartida                                                               *
     * Uso: Comprueba si se cumple la condición de victoria o derrota                       *
     * Variables entrada: Ninguno                                                           *
     * Return:                                                                              *
     ****************************************************************************************/
    public void StatusPartida()
    {
        if (ContAliados <= 0)
            Derrota = true;

        if (ContEnemigos <= 0)
            Victoria = true;
    }

    /****************************************************************************************
     * Función: InterfazVictoriaDerrota                                                     *
     * Uso: Crea la interfaz de Vicotira o derrota si se cumple alguna de las condiciones   *
     * Variables entrada: Ninguno                                                           *
     * Return:                                                                              *
     ****************************************************************************************/
    public void InterfazVictoriaDerrota()
    {
        int money;
        
        if (Victoria)
        {
            if (!VictoriaDerrotaCreado)
            {
                if (!VariablesGlobales.instance.Boss)
                {
                    do
                    {
                        money = Random.Range(200, 251);
                    } while (money % 10 != 0);

                    ClonVictoriaDerrotaBorder = Instantiate(PrefabVictoriaDerrotaBorder);
                    ClonVictoriaDerrotaBorder.transform.position = new Vector3(0, 0, 0);
                    ClonVictoriaDerrotaBorder.transform.localScale = new Vector2(10, 5);

                    ClonVictoriaDerrota = Instantiate(PrefabVictoriaDerrota);
                    ClonVictoriaDerrota.transform.position = new Vector3(0, 0, -1);
                    ClonVictoriaDerrota.transform.localScale = new Vector2(9.5f, 4.5f);

                    ClonTextoVictoriaDerrota = Instantiate(PrefabTextoTurno);                          // Crea el texto Victoria
                    ClonTextoVictoriaDerrota.transform.position = new Vector3(0, 1.5f, -1);            // Lo coloca en la interfaz
                    ClonTextoVictoriaDerrota.GetComponent<TextoTurno>().ChangeText("Victoria!");       // Cambia el texto a "Mover"
                    ClonTextoVictoriaDerrota.GetComponent<TextoTurno>().ChangeFontSize(1);             // Cambio el tamaño de la fuente del texto
                    ClonTextoVictoriaDerrota.GetComponent<TextoTurno>().ChangeColor(0, 1, 0);          // Cambia el color del texto a verde

                    ClonTextoRecompensa = Instantiate(PrefabTextoTurno);                                       // Crea el texto Recompensa
                    ClonTextoRecompensa.transform.position = new Vector3(0, 0.3f, -1);                         // Lo coloca en la interfaz
                    ClonTextoRecompensa.GetComponent<TextoTurno>().ChangeText("Recompensa: " + money +" oro"); // Cambia el texto a "Mover"
                    ClonTextoRecompensa.GetComponent<TextoTurno>().ChangeFontSize(0.6f);                       // Cambio el tamaño de la fuente del texto
                    ClonTextoRecompensa.GetComponent<TextoTurno>().ChangeColor(1, 1, 1);                       // Cambia el color del texto a blanco

                    ClonButtonVictoriaDerrota = Instantiate(PrefabButtonVictoriaDerrota);
                    ClonButtonVictoriaDerrota.GetComponent<VictoriaDerrotaButton>().Money = money;
                    ClonButtonVictoriaDerrota.GetComponent<VictoriaDerrotaButton>()._Combatbackground = _CombatBackground; // Almacena el combate

                    VictoriaDerrotaCreado = true;
                }
                else
                {
                    do
                    {
                        money = Random.Range(400, 501);
                    } while (money % 10 != 0);

                    ClonVictoriaDerrotaBorder = Instantiate(PrefabVictoriaDerrotaBorder);
                    ClonVictoriaDerrotaBorder.transform.position = new Vector3(0, 0, 0);
                    ClonVictoriaDerrotaBorder.transform.localScale = new Vector2(10, 5);

                    ClonVictoriaDerrota = Instantiate(PrefabVictoriaDerrota);
                    ClonVictoriaDerrota.transform.position = new Vector3(0, 0, -1);
                    ClonVictoriaDerrota.transform.localScale = new Vector2(9.5f, 4.5f);

                    ClonTextoVictoriaDerrota = Instantiate(PrefabTextoTurno);                          // Crea el texto Victoria
                    ClonTextoVictoriaDerrota.transform.position = new Vector3(0, 1.5f, -1);            // Lo coloca en la interfaz
                    ClonTextoVictoriaDerrota.GetComponent<TextoTurno>().ChangeText("Victoria!");       // Cambia el texto a "Mover"
                    ClonTextoVictoriaDerrota.GetComponent<TextoTurno>().ChangeFontSize(1);             // Cambio el tamaño de la fuente del texto
                    ClonTextoVictoriaDerrota.GetComponent<TextoTurno>().ChangeColor(0, 1, 0);          // Cambia el color del texto a verde

                    ClonTextoRecompensa = Instantiate(PrefabTextoTurno);                              // Crea el texto Recompensa
                    ClonTextoRecompensa.transform.position = new Vector3(0, 0.3f, -1);                // Lo coloca en la interfaz
                    ClonTextoRecompensa.GetComponent<TextoTurno>().ChangeText("Has derrotado al jefe de la zona.\nRecompensa: " + money + " oro"); // Cambia el texto a "Mover"
                    ClonTextoRecompensa.GetComponent<TextoTurno>().ChangeFontSize(0.6f);              // Cambio el tamaño de la fuente del texto
                    ClonTextoRecompensa.GetComponent<TextoTurno>().ChangeColor(1, 1, 1);              // Cambia el color del texto a blanco

                    ClonButtonVictoriaDerrota = Instantiate(PrefabButtonVictoriaDerrota);
                    ClonButtonVictoriaDerrota.GetComponent<VictoriaDerrotaButton>()._Button.GetComponent<VictoriaDerrotaButtonAction>().Texto.text = "Continuar";
                    ClonButtonVictoriaDerrota.GetComponent<VictoriaDerrotaButton>().Money = money;
                    ClonButtonVictoriaDerrota.GetComponent<VictoriaDerrotaButton>()._Combatbackground = _CombatBackground; // Almacena el combate

                    VictoriaDerrotaCreado = true;
                }
            }
        }

        if (Derrota)
        {
            if (!VictoriaDerrotaCreado)
            {
                ClonVictoriaDerrotaBorder = Instantiate(PrefabVictoriaDerrotaBorder);
                ClonVictoriaDerrotaBorder.transform.position = new Vector3(0, 0, 0);
                ClonVictoriaDerrotaBorder.transform.localScale = new Vector2(10, 5);

                ClonVictoriaDerrota = Instantiate(PrefabVictoriaDerrota);
                ClonVictoriaDerrota.transform.position = new Vector3(0, 0, -1);
                ClonVictoriaDerrota.transform.localScale = new Vector2(9.5f, 4.5f);

                ClonTextoVictoriaDerrota = Instantiate(PrefabTextoTurno);                           // Crea el texto Victoria
                ClonTextoVictoriaDerrota.transform.position = new Vector3(0, 1, -1);                // Lo coloca en la interfaz
                ClonTextoVictoriaDerrota.GetComponent<TextoTurno>().ChangeText("Derrota!");         // Cambia el texto a "Mover"
                ClonTextoVictoriaDerrota.GetComponent<TextoTurno>().ChangeFontSize(1);              // Cambio el tamaño de la fuente del texto
                ClonTextoVictoriaDerrota.GetComponent<TextoTurno>().ChangeColor(1, 0, 0);           // Cambia el color del texto a rojo

                ClonTextoDerrotado = Instantiate(PrefabTextoTurno);                              // Crea el texto Derrotado
                ClonTextoDerrotado.transform.position = new Vector3(0, -0.2f, -1);                   // Lo coloca en la interfaz
                ClonTextoDerrotado.GetComponent<TextoTurno>().ChangeText("Has sido derrotado."); // Cambia el texto a "Mover"
                ClonTextoDerrotado.GetComponent<TextoTurno>().ChangeFontSize(0.6f);              // Cambio el tamaño de la fuente del texto
                ClonTextoDerrotado.GetComponent<TextoTurno>().ChangeColor(1, 1, 1);              // Cambia el color del texto a blanco

                ClonButtonVictoriaDerrota = Instantiate(PrefabButtonVictoriaDerrota);
                ClonButtonVictoriaDerrota.GetComponent<VictoriaDerrotaButton>()._Button.GetComponent<VictoriaDerrotaButtonAction>().Texto.text = "Volver al Menu Principal";
                ClonButtonVictoriaDerrota.GetComponent<VictoriaDerrotaButton>()._Combatbackground = _CombatBackground; // Almacena el combate

                VictoriaDerrotaCreado = true;
            }
        }
    }

    //public void MenuPausa()
    //{
    //    EscPressed = true;

    //    ClonMenuPausaBorder.GetComponent<SpriteRenderer>().enabled = true;

    //    ClonMenuPausaContainer.GetComponent<SpriteRenderer>().enabled = true;

    //    PausaText.enabled = true;
    //    ContinuarButton.image.enabled = true;
    //    ContinuarButtonText.enabled = true;
    //    SalirButton.image.enabled = true;
    //    SalirButtonText.enabled = true;
    //}
}
