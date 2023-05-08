using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability2ButtonAction : MonoBehaviour
{
    public GameObject _CombatBackground; // Combate
    public GameObject[] Positions;       // Array de posiciones del combate
    public GameObject[] Enemies;         // Array de enemigos del combate
    public GameObject[] Aliados;         // Array de aliados del combate
    public GameObject CharacterPosition; // Posición actual del personaje
    public GameObject Character;         // Personaje asociado

    public GameObject UIHabilidadKnight;
    public GameObject UIHabilidadHealer;
    public GameObject UIHabilidadSlime;
    public GameObject UIHabilidadMage;

    public GameObject UIEstadisticasPersonaje;

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
        Character.GetComponent<GeneralPlayer>().Habilidad2 = true;                              // Indica que el personaje está usando si habilidad
        
        Character.GetComponent<GeneralPlayer>().Moviendo = false;                               // Indica que el personaje deja de moverse
        for (int j = 0; j < _CombatBackground.GetComponent<CombatBackground>().Positions.Length; j++)
            _CombatBackground.GetComponent<CombatBackground>().Positions[j].GetComponent<CombatPosition>().transform.localScale = _CombatBackground.GetComponent<CombatBackground>().Positions[j].GetComponent<CombatPosition>().MinTam;
        Character.GetComponent<GeneralPlayer>().Atacando = false;                               // Indica que el personaje deja de atacar
        for (int k = 0; k < _CombatBackground.GetComponent<CombatBackground>().Enemies.Length; k++)
        {
            if (_CombatBackground.GetComponent<CombatBackground>().Enemies[k] != null)
            {
                if (!VariablesGlobales.instance.Boss)
                {
                    Enemies[k].GetComponent<GeneralEnemy>().Action = 0;
                    Enemies[k].GetComponent<GeneralEnemy>().SelectedToAttack = false;                                                  // Indica que el enemigo puede ser atacado
                    Enemies[k].GetComponent<GeneralEnemy>().Vibrate = false;
                    Enemies[k].GetComponent<GeneralEnemy>().PlayerAttacking = null;
                    _CombatBackground.GetComponent<CombatBackground>().Enemies[k].GetComponent<GeneralEnemy>().transform.localScale = _CombatBackground.GetComponent<CombatBackground>().Enemies[k].GetComponent<GeneralEnemy>().MinTam;
                }
                else
                {
                    Enemies[k].GetComponent<Boss>().Action = 0;
                    Enemies[k].GetComponent<Boss>().SelectedToAttack = true;                                                  // Indica que el enemigo puede ser atacado
                    Enemies[k].GetComponent<Boss>().Vibrate = true;
                    Enemies[k].GetComponent<Boss>().PlayerAttacking = null;
                    _CombatBackground.GetComponent<CombatBackground>().Enemies[k].GetComponent<Boss>().transform.localScale = _CombatBackground.GetComponent<CombatBackground>().Enemies[k].GetComponent<Boss>().MinTam;
                }
            }
        }

        if (Character.GetComponent<GeneralPlayer>().CharacterType == 1)     // Si el personaje es un Knight
        {
            if (Character.GetComponent<PlayerKnight>().Invencible != true)
            {
                Character.GetComponent<PlayerKnight>().Invencible = true;              // Activa la invulnerabilidad durante 3 turnos
                Character.GetComponent<PlayerKnight>().PlayerKnightInvencibleCont = 0; // Inicia el contador

                Character.transform.localScale = Character.GetComponent<GeneralPlayer>().MinTam;                         // Devuelve al personaje a su tamaño original
                Character.GetComponent<GeneralPlayer>().DestroyCharacterInfo();                                          // Destruye la interfaz de información del personaje

                for (int i = 0; i < Enemies.Length; i++)
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

                Character.GetComponent<GeneralPlayer>()._CombatBackground.GetComponent<CombatBackground>().ChangeTurn(); // Tras la acción, cambia el turno de la partida
                Character.GetComponent<GeneralPlayer>().Habilidad2 = false;

                UIHabilidadKnight.SetActive(false);
                UIEstadisticasPersonaje.SetActive(false);
            }
        }
        else if (Character.GetComponent<GeneralPlayer>().CharacterType == 2)                                                         // Si el personaje es un Healer
        {
            if (VariablesGlobales.instance.Boss)
                Enemies[0].GetComponent<Boss>().SelectedToAttack = false;

            // TEXTO EXPLICACIÓN
            /************************************************************************************************************************/
            _CombatBackground.GetComponent<CombatBackground>().ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeText("Selecciona un heroe aliado al que curar");
            /************************************************************************************************************************/

            if (Character.GetComponent<PlayerHealer>().UsedAbility != true)                                                          // Si puede usar su habilidad
            {
                for (int i = 0; i < Aliados.Length; i++)                                                                             // Bucle para recorrer los aliados del combate
                {
                    if (Aliados[i] != null)
                    {
                        Aliados[i].GetComponent<GeneralPlayer>().Action = 1;                                                             // Indica que la acción es la habilidad del Slime
                        Aliados[i].GetComponent<GeneralPlayer>().Selected = true;                                                        // Indica que el personaje puede ser atacado
                        Aliados[i].GetComponent<GeneralPlayer>().Vibrate = true;                                                         // Indica que el personaje puede vibrar
                        Aliados[i].GetComponent<GeneralPlayer>().PlayerUsingAbility = Character.GetComponent<GeneralPlayer>().Character; // Indica el personaje del Jugador que le ha seleccionado
                    }
                }
            }
        }
        else if(Character.GetComponent<GeneralPlayer>().CharacterType == 3) // Si el personaje es un Slime
        {
            if (Character.GetComponent<PlayerSlime>().UsedAbility != true)
            {
                // TEXTO EXPLICACIÓN
                /************************************************************************************************************************/
                _CombatBackground.GetComponent<CombatBackground>().ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeText("Selecciona un enemigo al que bajarle la defensa");
                /************************************************************************************************************************/

                for (int i = 0; i < Enemies.Length; i++)                    // Blucle que hace seleccionables a todos los enemigos del combate
                {
                    if (Enemies[i] != null)
                    {
                        if (!VariablesGlobales.instance.Boss)
                        {
                            Enemies[i].GetComponent<GeneralEnemy>().Action = 2;                                                              // Indica que la acción es la habilidad del Slime
                            Enemies[i].GetComponent<GeneralEnemy>().SelectedToAttack = true;                                                 // Indica que el enemigo puede ser atacado
                            Enemies[i].GetComponent<GeneralEnemy>().Vibrate = true;                                                          // Indica que el enemigo puede vibrar
                            Enemies[i].GetComponent<GeneralEnemy>().PlayerAttacking = Character.GetComponent<GeneralPlayer>().Character;     // Indica el personaje del Jugador que le puede atacar
                        }
                        else
                        {
                            Enemies[i].GetComponent<Boss>().Action = 2;                                                              // Indica que la acción es la habilidad del Slime
                            Enemies[i].GetComponent<Boss>().SelectedToAttack = true;                                                 // Indica que el enemigo puede ser atacado
                            Enemies[i].GetComponent<Boss>().Vibrate = true;                                                          // Indica que el enemigo puede vibrar
                            Enemies[i].GetComponent<Boss>().PlayerAttacking = Character.GetComponent<GeneralPlayer>().Character;     // Indica el personaje del Jugador que le puede atacar
                        }
                    }
                }
            }
        }
        else                                                                                                                         // Si el personaje es un Mage
        {
            if (Character.GetComponent<PlayerMage>().UsedAbility != true)                                                          // Si puede usar su habilidad
            {
                // TEXTO EXPLICACIÓN
                /************************************************************************************************************************/
                _CombatBackground.GetComponent<CombatBackground>().ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeText("Selecciona un heroe aliado al que aumentarle la defensa");
                /************************************************************************************************************************/

                for (int i = 0; i < Aliados.Length; i++)                                                                             // Bucle para recorrer los aliados del combate
                {
                    if (Aliados[i] != null)
                    {
                        Aliados[i].GetComponent<GeneralPlayer>().Action = 2;                                                             // Indica que la acción es la habilidad del Slime
                        Aliados[i].GetComponent<GeneralPlayer>().Selected = true;                                                        // Indica que el personaje puede ser atacado
                        Aliados[i].GetComponent<GeneralPlayer>().Vibrate = true;                                                         // Indica que el personaje puede vibrar
                        Aliados[i].GetComponent<GeneralPlayer>().PlayerUsingAbility = Character.GetComponent<GeneralPlayer>().Character; // Indica el personaje del Jugador que le ha seleccionado
                    }
                }
            }
        }
    }

    public void OnMouseEnter()
    {
        if(Character.GetComponent<GeneralPlayer>().CharacterType == 1)
            UIHabilidadKnight.SetActive(true);
        else if (Character.GetComponent<GeneralPlayer>().CharacterType == 2)
            UIHabilidadHealer.SetActive(true);
        else if (Character.GetComponent<GeneralPlayer>().CharacterType == 3)
            UIHabilidadSlime.SetActive(true);
        else
            UIHabilidadMage.SetActive(true);
    }

    public void OnMouseExit()
    {
        UIHabilidadKnight.SetActive(false);
        UIHabilidadHealer.SetActive(false);
        UIHabilidadSlime.SetActive(false);
        UIHabilidadMage.SetActive(false);
    }
}
