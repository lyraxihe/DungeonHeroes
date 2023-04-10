using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability1Button : MonoBehaviour
{
    public GameObject _CombatBackground; // Combate
    public GameObject[] Positions;       // Array de posiciones del combate
    public GameObject[] Enemies;         // Array de enemigos del combate
    public GameObject CharacterPosition; // Posición actual en la que está el personaje
    public Button _Button;               // Botón asociado al Canva "MoveButton"
    public GameObject Character;         // Personaje asociado

    // Start is called before the first frame update
    private void Start()
    {
        _Button.GetComponent<Ability1ButtonAction>().CharacterPosition = CharacterPosition; // Almacena en el botón la posición actual del personaje
        _Button.GetComponent<Ability1ButtonAction>().Enemies = Enemies;                     // Almacena el array de enemigos del combate
        _Button.GetComponent<Ability1ButtonAction>().Positions = Positions;                 // Almacena en el botón el array de posiciones del combate
        _Button.GetComponent<Ability1ButtonAction>().Character = Character;                 // Almacena el personaje
        _Button.GetComponent<Ability1ButtonAction>()._CombatBackground= _CombatBackground;  // Almacena el combate
    }
}
