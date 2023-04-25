using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPosition : MonoBehaviour
{
    public GameObject _Position;                   // Esta posición
    public GameObject Character;                   // Personaje actual en esta posición
    public GameObject CharacterToMove;             // Personaje que va a ser movido a esta posición
    public GameObject _CombatBackground;           // Combate
    public GameObject[] Positions;                 // Array de posiciones
    public GameObject[] Enemies;                   // Array de enemigos

    public int CharacterType = 0;                      // Tipo de personaje que lo está ocupando (0 - vacío, 1 - enemigo, 2 - personaje del jugador)
    
    public int[] PositionsToMove = { 0, 0, 0, 0 }; // Array de enteros que indica el index de las posiciones a las que se puede mover
    public bool Occupied = false;                  // Variable que indica si la posición está libre (false) u ocupada (true)
    public bool SelectedToMove = false;            // Booleano que indica si la posición es seleccionable para que el personaje se mueva a ella
    public bool Down = false;                      // Blooleano que indica cuando puede empezar a reducir de tamaño la posición (Estética)
    public bool Vibrate = false;                   // Booleano que controla si la posición seleccionable para que el jugador se mueva puede vibrar o no (Estética)
    public Vector2 MinTam;                        // Tamaño máximo que puede llegar a tener la posición
    private Vector2 MaxTam;                        // Tamaño mínimo que puede llegar a tener la posición

    // Start is called before the first frame update
    void Start()
    {
        MinTam = new Vector2(transform.localScale.x, transform.localScale.y);         // Establece el tamaño máximo que puede llegar a tener la posición
        MaxTam = new Vector2(transform.localScale.x * 2, transform.localScale.y * 2); // Establece el tamaño mínimo que puede llegar a tener la posición
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedToMove && CharacterToMove.GetComponent<GeneralPlayer>().Moviendo)                                                  // Si el personaje puede moverse a esta posición
        {
            if(Vibrate)                                                      // Si vibrar es true
                ScaleUpPositionBecauseSelected();                            // Empieza la animación para indicar que el personaje se puede mover a esta posición
            
            //OnMouseDown();                                                   // Cuando se hace click en la posición, mueve al personaje
        }                   
    }

    /****************************************************************************************
     * Función: SetPositionsToMove                                                          *
     * Uso: Almacena las posiciones conectadas a esta                                       *
     * Variables entrada: - pos1 Posición conectada 1                                       *
     *                    - pos1 Posición conectada 2                                       *
     *                    - pos1 Posición conectada 3                                       *
     *                    - pos1 Posición conectada 4                                       *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void SetPositionsToMove(int pos1, int pos2, int pos3, int pos4)
    {
        PositionsToMove[0] = pos1;
        PositionsToMove[1] = pos2;
        PositionsToMove[2] = pos3;
        PositionsToMove[3] = pos4;
    }

    /****************************************************************************************
     * Función: ScalePositionBecauseSelected                                                *
     * Uso: Modifica el tamaño de la posición para que vibre al poderse mover el personaje  *
     *      a la misma                                                                      *
     * Variables entrada: Nada                                                              *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void ScaleUpPositionBecauseSelected()
    {
        float velocidad = 1.5f;                                                                            // Velocidad a la que variará el tamaño de la posición

        if (Vector2.Distance(transform.localScale, MaxTam) < 0.2f)                                         // Si la posición ha alcanzado el tamño máximo, indica que puede empezar a reducirse
            Down = true;                                                                                   // Indica que puede empezar a reducir el tamaño

        if (Vector2.Distance(transform.localScale, MinTam) < 0.2f)                                         // Si la posición ha alcanzado el tamaño mínimo, indica que puede empezar a aumentar
            Down = false;                                                                                  // INdica que puede empezar a aumentar el tamaño

        if(!Down)                                                                                          // Si puede aumentar de tamaño
            transform.localScale = Vector2.Lerp(transform.localScale, MaxTam, velocidad * Time.deltaTime); // Aumenta el tamaño progresivamente hasta el tamaño máximo
        else                                                                                               // Si puede reducir de tamaño
            transform.localScale = Vector2.Lerp(transform.localScale, MinTam, velocidad * Time.deltaTime); // Reduce el tamaño progresivamentehasta el tamaño mínimo
    }

    /****************************************************************************************
     * Función: OnMouseOver                                                                 *
     * Uso: Al hacer click en la posición realiza una serie de acciones                     *
     * Variables entrada: Nada                                                              *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void OnMouseDown()
    {
        int i;

        for (i = 0; i < Positions.Length; i++)                                   // Recorre el array de posiciones
        {
            if (Positions[i].transform.position != transform.position)              // Para el resto de posiciones que no son esta
            {
                Positions[i].GetComponent<CombatPosition>().SelectedToMove = false; // Inhabilita que el personaje pueda moverse a ellas
                Positions[i].transform.localScale = MinTam;                         // Dichas posiciones modificadas vuelven a su tamaño original después de vibrar
            }
        }

        if (SelectedToMove && CharacterToMove.GetComponent<GeneralPlayer>().Moviendo)                                                         // Para la posición que el Jugador ha elegido para moverse
        {
            Vibrate = false;                                                                                                                            // El resto de posiciones dejan de vibrar
            transform.localScale = MinTam;                                                                                                              // La posición vuelve a su tamaño original
            CharacterToMove.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);                                        // Mueve el personaje a esta posición
            CharacterToMove.transform.localScale = new Vector2(CharacterToMove.transform.localScale.x / 2, CharacterToMove.transform.localScale.y / 2); // Vuelve el personaje a su tamaño original
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = false;                            // Indica que la posición pasa a estar vacía
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Character = null;                            // Indica que ya no hay un personaje asociado a esa posición
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 0;                           // Indica que la ya no hay ningún personaje en esa posición
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition = _Position;                                                                // Almacena y actualiza la posición actual del personaje
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = true;                             // Indica que la posición pasa a estar ocupada
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Character = CharacterToMove;                 // Alamcena el personaje que acaba de ocupar esa posición
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 2;                           // Indica que en a nueva posición hay un aliado
            SelectedToMove = false;                                                                                                                     // Hace que esta acción sólo se pueda realizar una vez
            CharacterToMove.GetComponent<GeneralPlayer>().Moviendo = false;                                                                             // Indica que deja de moverse
            CharacterToMove.GetComponent<GeneralPlayer>().DestroyCharacterInfo();                                                                       // Destruye la interfaz de información del personaje
            _CombatBackground.GetComponent<CombatBackground>().ChangeTurn();                                                                            // Tras la acción del movimiento, cambia el turno de la partida

            for (i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i] != null)
                {
                    if (!VariablesGlobales.instance.Boss)
                        Enemies[i].GetComponent<GeneralEnemy>().Atacar = true;
                    else
                        Enemies[i].GetComponent<Boss>().Atacar = true;
                }
            }

            _CombatBackground.GetComponent<CombatBackground>().EnemigoParaAtacar = true;
        }
    }
}
