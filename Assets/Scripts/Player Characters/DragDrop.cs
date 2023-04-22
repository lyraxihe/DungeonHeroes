using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragDrop : MonoBehaviour
{
    public GameObject _CombatBackground;
    
    private GameObject[] Positions;         // Array de posiciones del combate
    private GameObject PreviousPosition;    // Posición anterior en la que se hayaba el personaje
    private GameObject PreviousPositionAux; // Auziliar de la posición anterior en la que se hayaba el personaje

    public GameObject CharacterPosition;    // Posición actual del personaje

    private bool dragging = false;          // Booleano para controlar si se está arrastrando

    private Vector2 Offset;                 // Offset para colocar correctamente el personaje al arrastrarlo
    public Vector2 OriginalPosition;        // Coordenadas de la posición original del personaje
    private Vector2 PreviousPositionVector; // Coordenadas de la posición anterior en la que se hayaba el personaje
    public Vector2 CurrentPosition;         // Coordenadas de la posición actual del personaje

    void Start()
    {
        OriginalPosition = transform.position; // Establece la posición original del personaje
        CurrentPosition = OriginalPosition;    // Establece la posición actual como la posición original del personaje
    }

    void Update()
    {
        if (!dragging) // Si no está arrastrando al personaje no hace nada
            return;

        transform.position = GetMousePos() - Offset; // Si lo está arrastrando coloca el sprite justo donde está en ratón
    }

    /****************************************************************************************
     * Función: OnMouseDown                                                                 *
     * Uso: Cuando se pulsa el click izquierdo, indica que se está arrastrando, establece   *
     *      el offset y el vector de la posición previa del personaje                       *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void OnMouseDown()
    {
        dragging = true;                                      // Indica que el personaje se está arrastrando

        Offset = GetMousePos() - (Vector2)transform.position; // Establece el offset
        PreviousPositionVector = transform.position;          // Establece la posción previa donde se hayaba el personaje
    }

    /****************************************************************************************
     * Función: GetMousePos                                                                 *
     * Uso: Obtiene la posición del ratón                                                   *
     * Variables entrada: Ninguno                                                           *
     * Return: Vector2 de la posición del ratón                                             *
     ****************************************************************************************/
    private Vector2 GetMousePos()
    {
        return (Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    /****************************************************************************************
     * Función: OnMouseUp                                                                   *
     * Uso: Cuando se deja de pulsar el click izquierdo, indica que se ha dejado de         *
     *      arrastrar y coloca el personaje en la posición válida más cercana, si no la hay *
     *      devuelve el personaje a la posición previa                                      *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void OnMouseUp()
    {
        bool colocado = false; // Booleano que indica si el personaje se ha podido colocar en una nueva posición válida
        
        for(int i = 0; i < Positions.Length; i++) // Bucle for que recorre el array de posiciones del combate
        {
            if (Vector2.Distance(transform.position, Positions[i].transform.position) < 1 && Positions[i].GetComponent<CombatPosition>().Occupied == false)           // Si hay una posición válida cerca y no está ocupada
            {
                transform.position = new Vector3(Positions[i].transform.position.x, Positions[i].transform.position.y + 0.5f, Positions[i].transform.position.z - 1); // Coloca el personaje en dicha posición
                Positions[i].GetComponent<CombatPosition>().Occupied = true;                                                                                          // Indica que dicha posición pasa a estar ocupada
                Positions[i].GetComponent<CombatPosition>().CharacterType = 2;                                                                                        // Indica que la posición está ocupada por un personaje del Jugador
                Positions[i].GetComponent<CombatPosition>().Character = GetComponent<GeneralPlayer>().Character;                                                      // Almacena el personaje en la posición en la que se ubicará
                PreviousPositionAux = Positions[i];                                                                                                                   // Establece la posción auxiliar para luego poder usarla
                CurrentPosition = new Vector2(Positions[i].transform.position.x, Positions[i].transform.position.y + 0.5f);                                           // Establece la posición actual del personaje como la de la posición en la que ha sido colocado
                CharacterPosition = Positions[i];                                                                                                                     // Almacena la posición actual del personaje
                colocado = true;                                                                                                                                      // Indica que el personaje ha sido colocado con éxito

                Positions[i].GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterPosition = Positions[i];

                if (PreviousPositionVector != OriginalPosition)                        // Si la posición previa del personaje era una posición del combate
                    PreviousPosition.GetComponent<CombatPosition>().Occupied = false; // Después de mover el personaje a su nueva posición indica que la posición previa deja de estar ocupada
            }
        }

        PreviousPosition = PreviousPositionAux; // Utiliza el auziliar para establecer la posición previa en la que se hayaba el personaje antes de ser movido

        if (!colocado)                                   // Si el personaje no ha podido colocarse en una nueva posición válida
            transform.position = PreviousPositionVector; // Devuelve al personaje a su posición previa

        dragging = false; // Indica que se ha dejado de arrastrar al personaje
    }

    /****************************************************************************************
     * Función: GetPositions                                                                *
     * Uso: Obtiene el array de posiciones del combate                                      *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void GetPositions(GameObject[] positions)
    {
        Positions = positions; // Guarda el array de posiciones en la variable local
    }
}
