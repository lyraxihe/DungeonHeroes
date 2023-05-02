using System;
using System.Collections;
using System.Collections.Generic;
//using TMPro.EditorUtilities;
using UnityEngine;

public class Ability1ButtonAction : MonoBehaviour
{
    public GameObject _CombatBackground; // Combate
    public GameObject[] Positions;       // Array de posiciones del combate
    public GameObject[] Enemies;         // Array de enemigos del combate
    public GameObject CharacterPosition; // Posición actual del personaje
    public GameObject Character;         // Personaje asociado

    public GameObject UIAtacarConRango;
    public GameObject UIAtacarSinRango;

    public int[] PositionsToAttack;        // Array de posiciones conectadas a la posición actual del personaje

    // Start is called before the first frame update
    private void Start()
    {
        PositionsToAttack = CharacterPosition.GetComponent<CombatPosition>().PositionsToMove; // Alamcena el array de posiciones conectadas a la posición actual del personaje
    }

    /****************************************************************************************
     * Función: OnClicked                                                                   *
     * Uso: Indica los enemigos a los que un personaje se puede atacar al pulasar el botón  *
     * Variables entrada: Nada                                                              *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void OnClicked()
    {
        // TEXTO EXPLICACIÓN
        /************************************************************************************************************************/
        _CombatBackground.GetComponent<CombatBackground>().ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeText("Selecciona un enemigo al que atacar");
        /************************************************************************************************************************/

        Character.GetComponent<GeneralPlayer>().Atacando = true;
        
        Character.GetComponent<GeneralPlayer>().Moviendo = false;                                 // Indica que el personaje deja de moverse
        for (int j = 0; j < _CombatBackground.GetComponent<CombatBackground>().Positions.Length; j++)
            _CombatBackground.GetComponent<CombatBackground>().Positions[j].GetComponent<CombatPosition>().transform.localScale = _CombatBackground.GetComponent<CombatBackground>().Positions[j].GetComponent<CombatPosition>().MinTam;
        Character.GetComponent<GeneralPlayer>().Habilidad2 = false;                               // Indica que el personaje deja de usar su habilidad
        if (Character.GetComponent<GeneralPlayer>().CharacterType == 3)
        {
            for (int k = 0; k < _CombatBackground.GetComponent<CombatBackground>().Enemies.Length; k++)
            {
                if (!VariablesGlobales.instance.Boss)
                {
                    if (Enemies[k] != null)
                    {
                        Enemies[k].GetComponent<GeneralEnemy>().Action = 0;
                        Enemies[k].GetComponent<GeneralEnemy>().SelectedToAttack = false;                                                  // Indica que el enemigo puede ser atacado
                        Enemies[k].GetComponent<GeneralEnemy>().Vibrate = false;
                        Enemies[k].GetComponent<GeneralEnemy>().PlayerAttacking = null;
                        _CombatBackground.GetComponent<CombatBackground>().Enemies[k].GetComponent<GeneralEnemy>().transform.localScale = _CombatBackground.GetComponent<CombatBackground>().Enemies[k].GetComponent<GeneralEnemy>().MinTam;
                    }
                }
                else
                {
                    if (Enemies[k] != null)
                    {
                        Enemies[k].GetComponent<Boss>().Action = 0;
                        Enemies[k].GetComponent<Boss>().SelectedToAttack = true;                                                  // Indica que el enemigo puede ser atacado
                        Enemies[k].GetComponent<Boss>().Vibrate = true;
                        Enemies[k].GetComponent<Boss>().PlayerAttacking = null;
                        _CombatBackground.GetComponent<CombatBackground>().Enemies[k].GetComponent<Boss>().transform.localScale = _CombatBackground.GetComponent<CombatBackground>().Enemies[k].GetComponent<Boss>().MinTam;
                    }
                }
            }
        }
        else
        {
            for (int k = 0; k < _CombatBackground.GetComponent<CombatBackground>().Aliados.Length; k++)
            {
                if (_CombatBackground.GetComponent<CombatBackground>().Aliados[k] != null)
                {
                    if (_CombatBackground.GetComponent<CombatBackground>().Aliados[k] != Character)
                    {
                        _CombatBackground.GetComponent<CombatBackground>().Aliados[k].GetComponent<GeneralPlayer>().Action = 0;
                        _CombatBackground.GetComponent<CombatBackground>().Aliados[k].GetComponent<GeneralPlayer>().transform.localScale = _CombatBackground.GetComponent<CombatBackground>().Aliados[k].GetComponent<GeneralPlayer>().MinTam;
                    }
                }
            }
        }

        if (Character.GetComponent<GeneralPlayer>().CharacterType == 4) // Si el personaje seleccionado es el mago no habrá rango de ataque
        {
            for(int i = 0; i < Enemies.Length; i++)                    // Blucle para que, al no tener rango de ataque, todos los enemigos del combate sean seleccionados para ser atacados
            {
                if (Enemies[i] != null)
                {
                    if (!VariablesGlobales.instance.Boss)
                    {
                        Enemies[i].GetComponent<GeneralEnemy>().Action = 1;                                                               // Indica que la acción es la de atacar
                        Enemies[i].GetComponent<GeneralEnemy>().SelectedToAttack = true;                                                  // Indica que el enemigo puede ser atacado
                        Enemies[i].GetComponent<GeneralEnemy>().Vibrate = true;                                                           // Indica que el enemigo puede vibrar
                        Enemies[i].GetComponent<GeneralEnemy>().PlayerAttacking = Character.GetComponent<GeneralPlayer>().Character;      // Indica el personaje del Jugador que le puede atacar
                    }
                    else
                    {
                        Enemies[i].GetComponent<Boss>().Action = 1;                                                               // Indica que la acción es la de atacar
                        Enemies[i].GetComponent<Boss>().SelectedToAttack = true;                                                  // Indica que el enemigo puede ser atacado
                        Enemies[i].GetComponent<Boss>().Vibrate = true;                                                           // Indica que el enemigo puede vibrar
                        Enemies[i].GetComponent<Boss>().PlayerAttacking = Character.GetComponent<GeneralPlayer>().Character;      // Indica el personaje del Jugador que le puede atacar
                    }
                }
            }
        }
        else                                                           // Para el resto de personajes que tienen rango de ataque
        {
            for(int i = 0; i < PositionsToAttack.Length; i++)          // Recorre el array de posiciones conectadas a la posición del personaje seleccionado
            {
                if (Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().CharacterType == 1)
                {
                    if (!VariablesGlobales.instance.Boss)
                    {
                        Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<GeneralEnemy>().Action = 1;                                                          // Indica que la acción es la de atacar
                        Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<GeneralEnemy>().SelectedToAttack = true;                                             // Indica que el enemigo puede ser atacado
                        Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<GeneralEnemy>().Vibrate = true;                                                      // Indica que el enemigo puede vibrar
                        Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<GeneralEnemy>().PlayerAttacking = Character.GetComponent<GeneralPlayer>().Character; // Indica el personaje del Jugador que le puede atacar
                    }
                    else
                    {
                        Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<Boss>().Action = 1;                                                          // Indica que la acción es la de atacar
                        Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<Boss>().SelectedToAttack = true;                                             // Indica que el enemigo puede ser atacado
                        Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<Boss>().Vibrate = true;                                                      // Indica que el enemigo puede vibrar
                        Positions[PositionsToAttack[i]].GetComponent<CombatPosition>().Character.GetComponent<Boss>().PlayerAttacking = Character.GetComponent<GeneralPlayer>().Character; // Indica el personaje del Jugador que le puede atacar
                    }
                }
            }
        }
        Character.GetComponent<GeneralPlayer>().Action = 0;
    }

    public void OnMouseEnter()
    {
        if (Character.GetComponent<GeneralPlayer>().CharacterType != 4)
            UIAtacarConRango.SetActive(true);
        else
            UIAtacarSinRango.SetActive(true);
    }

    public void OnMouseExit()
    {
        UIAtacarConRango.SetActive(false);
        UIAtacarSinRango.SetActive(false);
    }
}
