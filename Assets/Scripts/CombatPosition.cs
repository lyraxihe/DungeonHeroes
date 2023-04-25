using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPosition : MonoBehaviour
{
    public GameObject _Position;                   // Esta posici�n
    public GameObject Character;                   // Personaje actual en esta posici�n
    public GameObject CharacterToMove;             // Personaje que va a ser movido a esta posici�n
    public GameObject _CombatBackground;           // Combate
    public GameObject[] Positions;                 // Array de posiciones
    public GameObject[] Enemies;                   // Array de enemigos

    public int CharacterType = 0;                      // Tipo de personaje que lo est� ocupando (0 - vac�o, 1 - enemigo, 2 - personaje del jugador)
    
    public int[] PositionsToMove = { 0, 0, 0, 0 }; // Array de enteros que indica el index de las posiciones a las que se puede mover
    public bool Occupied = false;                  // Variable que indica si la posici�n est� libre (false) u ocupada (true)
    public bool SelectedToMove = false;            // Booleano que indica si la posici�n es seleccionable para que el personaje se mueva a ella
    public bool Down = false;                      // Blooleano que indica cuando puede empezar a reducir de tama�o la posici�n (Est�tica)
    public bool Vibrate = false;                   // Booleano que controla si la posici�n seleccionable para que el jugador se mueva puede vibrar o no (Est�tica)
    public Vector2 MinTam;                        // Tama�o m�ximo que puede llegar a tener la posici�n
    private Vector2 MaxTam;                        // Tama�o m�nimo que puede llegar a tener la posici�n

    // Start is called before the first frame update
    void Start()
    {
        MinTam = new Vector2(transform.localScale.x, transform.localScale.y);         // Establece el tama�o m�ximo que puede llegar a tener la posici�n
        MaxTam = new Vector2(transform.localScale.x * 2, transform.localScale.y * 2); // Establece el tama�o m�nimo que puede llegar a tener la posici�n
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectedToMove && CharacterToMove.GetComponent<GeneralPlayer>().Moviendo)                                                  // Si el personaje puede moverse a esta posici�n
        {
            if(Vibrate)                                                      // Si vibrar es true
                ScaleUpPositionBecauseSelected();                            // Empieza la animaci�n para indicar que el personaje se puede mover a esta posici�n
            
            //OnMouseDown();                                                   // Cuando se hace click en la posici�n, mueve al personaje
        }                   
    }

    /****************************************************************************************
     * Funci�n: SetPositionsToMove                                                          *
     * Uso: Almacena las posiciones conectadas a esta                                       *
     * Variables entrada: - pos1 Posici�n conectada 1                                       *
     *                    - pos1 Posici�n conectada 2                                       *
     *                    - pos1 Posici�n conectada 3                                       *
     *                    - pos1 Posici�n conectada 4                                       *
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
     * Funci�n: ScalePositionBecauseSelected                                                *
     * Uso: Modifica el tama�o de la posici�n para que vibre al poderse mover el personaje  *
     *      a la misma                                                                      *
     * Variables entrada: Nada                                                              *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void ScaleUpPositionBecauseSelected()
    {
        float velocidad = 1.5f;                                                                            // Velocidad a la que variar� el tama�o de la posici�n

        if (Vector2.Distance(transform.localScale, MaxTam) < 0.2f)                                         // Si la posici�n ha alcanzado el tam�o m�ximo, indica que puede empezar a reducirse
            Down = true;                                                                                   // Indica que puede empezar a reducir el tama�o

        if (Vector2.Distance(transform.localScale, MinTam) < 0.2f)                                         // Si la posici�n ha alcanzado el tama�o m�nimo, indica que puede empezar a aumentar
            Down = false;                                                                                  // INdica que puede empezar a aumentar el tama�o

        if(!Down)                                                                                          // Si puede aumentar de tama�o
            transform.localScale = Vector2.Lerp(transform.localScale, MaxTam, velocidad * Time.deltaTime); // Aumenta el tama�o progresivamente hasta el tama�o m�ximo
        else                                                                                               // Si puede reducir de tama�o
            transform.localScale = Vector2.Lerp(transform.localScale, MinTam, velocidad * Time.deltaTime); // Reduce el tama�o progresivamentehasta el tama�o m�nimo
    }

    /****************************************************************************************
     * Funci�n: OnMouseOver                                                                 *
     * Uso: Al hacer click en la posici�n realiza una serie de acciones                     *
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
                Positions[i].transform.localScale = MinTam;                         // Dichas posiciones modificadas vuelven a su tama�o original despu�s de vibrar
            }
        }

        if (SelectedToMove && CharacterToMove.GetComponent<GeneralPlayer>().Moviendo)                                                         // Para la posici�n que el Jugador ha elegido para moverse
        {
            Vibrate = false;                                                                                                                            // El resto de posiciones dejan de vibrar
            transform.localScale = MinTam;                                                                                                              // La posici�n vuelve a su tama�o original
            CharacterToMove.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);                                        // Mueve el personaje a esta posici�n
            CharacterToMove.transform.localScale = new Vector2(CharacterToMove.transform.localScale.x / 2, CharacterToMove.transform.localScale.y / 2); // Vuelve el personaje a su tama�o original
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = false;                            // Indica que la posici�n pasa a estar vac�a
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Character = null;                            // Indica que ya no hay un personaje asociado a esa posici�n
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 0;                           // Indica que la ya no hay ning�n personaje en esa posici�n
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition = _Position;                                                                // Almacena y actualiza la posici�n actual del personaje
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Occupied = true;                             // Indica que la posici�n pasa a estar ocupada
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().Character = CharacterToMove;                 // Alamcena el personaje que acaba de ocupar esa posici�n
            CharacterToMove.GetComponent<GeneralPlayer>().CharacterPosition.GetComponent<CombatPosition>().CharacterType = 2;                           // Indica que en a nueva posici�n hay un aliado
            SelectedToMove = false;                                                                                                                     // Hace que esta acci�n s�lo se pueda realizar una vez
            CharacterToMove.GetComponent<GeneralPlayer>().Moviendo = false;                                                                             // Indica que deja de moverse
            CharacterToMove.GetComponent<GeneralPlayer>().DestroyCharacterInfo();                                                                       // Destruye la interfaz de informaci�n del personaje
            _CombatBackground.GetComponent<CombatBackground>().ChangeTurn();                                                                            // Tras la acci�n del movimiento, cambia el turno de la partida

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
