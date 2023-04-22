using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragDrop : MonoBehaviour
{
    public GameObject _CombatBackground;
    
    private GameObject[] Positions;         // Array de posiciones del combate
    private GameObject PreviousPosition;    // Posici�n anterior en la que se hayaba el personaje
    private GameObject PreviousPositionAux; // Auziliar de la posici�n anterior en la que se hayaba el personaje

    public GameObject CharacterPosition;    // Posici�n actual del personaje

    private bool dragging = false;          // Booleano para controlar si se est� arrastrando

    private Vector2 Offset;                 // Offset para colocar correctamente el personaje al arrastrarlo
    public Vector2 OriginalPosition;        // Coordenadas de la posici�n original del personaje
    private Vector2 PreviousPositionVector; // Coordenadas de la posici�n anterior en la que se hayaba el personaje
    public Vector2 CurrentPosition;         // Coordenadas de la posici�n actual del personaje

    void Start()
    {
        OriginalPosition = transform.position; // Establece la posici�n original del personaje
        CurrentPosition = OriginalPosition;    // Establece la posici�n actual como la posici�n original del personaje
    }

    void Update()
    {
        if (!dragging) // Si no est� arrastrando al personaje no hace nada
            return;

        transform.position = GetMousePos() - Offset; // Si lo est� arrastrando coloca el sprite justo donde est� en rat�n
    }

    /****************************************************************************************
     * Funci�n: OnMouseDown                                                                 *
     * Uso: Cuando se pulsa el click izquierdo, indica que se est� arrastrando, establece   *
     *      el offset y el vector de la posici�n previa del personaje                       *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void OnMouseDown()
    {
        dragging = true;                                      // Indica que el personaje se est� arrastrando

        Offset = GetMousePos() - (Vector2)transform.position; // Establece el offset
        PreviousPositionVector = transform.position;          // Establece la posci�n previa donde se hayaba el personaje
    }

    /****************************************************************************************
     * Funci�n: GetMousePos                                                                 *
     * Uso: Obtiene la posici�n del rat�n                                                   *
     * Variables entrada: Ninguno                                                           *
     * Return: Vector2 de la posici�n del rat�n                                             *
     ****************************************************************************************/
    private Vector2 GetMousePos()
    {
        return (Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    /****************************************************************************************
     * Funci�n: OnMouseUp                                                                   *
     * Uso: Cuando se deja de pulsar el click izquierdo, indica que se ha dejado de         *
     *      arrastrar y coloca el personaje en la posici�n v�lida m�s cercana, si no la hay *
     *      devuelve el personaje a la posici�n previa                                      *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void OnMouseUp()
    {
        bool colocado = false; // Booleano que indica si el personaje se ha podido colocar en una nueva posici�n v�lida
        
        for(int i = 0; i < Positions.Length; i++) // Bucle for que recorre el array de posiciones del combate
        {
            if (Vector2.Distance(transform.position, Positions[i].transform.position) < 1 && Positions[i].GetComponent<CombatPosition>().Occupied == false)           // Si hay una posici�n v�lida cerca y no est� ocupada
            {
                transform.position = new Vector3(Positions[i].transform.position.x, Positions[i].transform.position.y + 0.5f, Positions[i].transform.position.z - 1); // Coloca el personaje en dicha posici�n
                Positions[i].GetComponent<CombatPosition>().Occupied = true;                                                                                          // Indica que dicha posici�n pasa a estar ocupada
                Positions[i].GetComponent<CombatPosition>().CharacterType = 2;                                                                                        // Indica que la posici�n est� ocupada por un personaje del Jugador
                Positions[i].GetComponent<CombatPosition>().Character = GetComponent<GeneralPlayer>().Character;                                                      // Almacena el personaje en la posici�n en la que se ubicar�
                PreviousPositionAux = Positions[i];                                                                                                                   // Establece la posci�n auxiliar para luego poder usarla
                CurrentPosition = new Vector2(Positions[i].transform.position.x, Positions[i].transform.position.y + 0.5f);                                           // Establece la posici�n actual del personaje como la de la posici�n en la que ha sido colocado
                CharacterPosition = Positions[i];                                                                                                                     // Almacena la posici�n actual del personaje
                colocado = true;                                                                                                                                      // Indica que el personaje ha sido colocado con �xito

                Positions[i].GetComponent<CombatPosition>().Character.GetComponent<GeneralPlayer>().CharacterPosition = Positions[i];

                if (PreviousPositionVector != OriginalPosition)                        // Si la posici�n previa del personaje era una posici�n del combate
                    PreviousPosition.GetComponent<CombatPosition>().Occupied = false; // Despu�s de mover el personaje a su nueva posici�n indica que la posici�n previa deja de estar ocupada
            }
        }

        PreviousPosition = PreviousPositionAux; // Utiliza el auziliar para establecer la posici�n previa en la que se hayaba el personaje antes de ser movido

        if (!colocado)                                   // Si el personaje no ha podido colocarse en una nueva posici�n v�lida
            transform.position = PreviousPositionVector; // Devuelve al personaje a su posici�n previa

        dragging = false; // Indica que se ha dejado de arrastrar al personaje
    }

    /****************************************************************************************
     * Funci�n: GetPositions                                                                *
     * Uso: Obtiene el array de posiciones del combate                                      *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void GetPositions(GameObject[] positions)
    {
        Positions = positions; // Guarda el array de posiciones en la variable local
    }
}
