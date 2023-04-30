using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ability2Button : MonoBehaviour
{
    public GameObject _CombatBackground; // Combate
    public GameObject[] Positions;       // Array de posiciones del combate
    public GameObject[] Enemies;         // Array de enemigos del combate
    public GameObject[] Aliados;         // Array de aliados del combate
    public GameObject CharacterPosition; // Posición actual en la que está el personaje
    public Button _Button;               // Botón asociado al Canva "MoveButton"
    public GameObject Character;         // Personaje asociado

    public GameObject UIHabilidadKnight;
    public GameObject UIHabilidadHealer;
    public GameObject UIHabilidadSlime;
    public GameObject UIHabilidadMage;

    public GameObject UIEstadisticasPersonaje;

    // Start is called before the first frame update
    private void Start()
    {
        _Button.GetComponent<Ability2ButtonAction>().CharacterPosition = CharacterPosition; // Almacena en el botón la posición actual del personaje
        _Button.GetComponent<Ability2ButtonAction>().Enemies = Enemies;                     // Almacena el array de enemigos del combate
        _Button.GetComponent<Ability2ButtonAction>().Aliados = Aliados;                     // Almacena el array de aliados del combate
        _Button.GetComponent<Ability2ButtonAction>().Positions = Positions;                 // Almacena en el botón el array de posiciones del combate
        _Button.GetComponent<Ability2ButtonAction>().Character = Character;                 // Almacena el personaje
        _Button.GetComponent<Ability2ButtonAction>()._CombatBackground= _CombatBackground;  // Almacena el combate
        _Button.GetComponent<Ability2ButtonAction>().UIHabilidadKnight = UIHabilidadKnight;
        _Button.GetComponent<Ability2ButtonAction>().UIHabilidadHealer = UIHabilidadHealer;
        _Button.GetComponent<Ability2ButtonAction>().UIHabilidadSlime = UIHabilidadSlime;
        _Button.GetComponent<Ability2ButtonAction>().UIHabilidadMage = UIHabilidadMage;
        _Button.GetComponent<Ability2ButtonAction>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
    }
}
