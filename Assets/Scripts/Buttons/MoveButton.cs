using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour
{
    public GameObject _CombatBackground; // Combate
    public GameObject[] Positions;       // Array de posiciones del combate
    public GameObject CharacterPosition; // Posici�n actual en la que est� el personaje
    public Button _Button;               // Bot�n asociado al Canva "MoveButton"
    public GameObject Character;         // Personaje asociado

    public GameObject UIMover;

    // Start is called before the first frame update
    private void Start()
    {
        _Button.GetComponent<MoveButtonAction>().CharacterPosition = CharacterPosition; // Almacena en el bot�n la posici�n actual del personaje
        _Button.GetComponent<MoveButtonAction>().Positions = Positions;                 // Almacena en el bot�n el array de posiciones del combate
        _Button.GetComponent<MoveButtonAction>().Character = Character;                 // Almacena el personaje
        _Button.GetComponent<MoveButtonAction>().UIMover = UIMover;
        _Button.GetComponent<MoveButtonAction>()._CombatBackground = _CombatBackground; // Almacena el combate
    }
}
