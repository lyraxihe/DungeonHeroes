using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerKnight : MonoBehaviour
{
    public GameObject _CombatBackground; // Combate

    // ESTADÍSTICAS DEL PERSONAJE
    public int VidaTotal;              // Vida máxima del personaje
    public int VidaActual;             // Vida actual del personaje
    public int AtaqueActual;           // Ataque actual del personaje
    public int AtaqueMax;              // Ataque máximo del personaje
    public int DefensaActual;          // Defensa actual del personaje
    public int DefensaMax;             // Defensa máxima del personaje

    public bool Invencible;                // Bool para activar la invulnerabilidad al usar su habilidad especial
    public int PlayerKnightInvencibleCont; // Contador para saber cuantos turnos faltan para dejar de ser invencible       

    public GameObject PrefabHealthbar; // Prefab Healthbar
    public GameObject ClonHealthbar;  // Clon del prefab Healthbar

    public bool HabilidadMage; // Booleano para controlar si la habilidad del Mage está activa sobre este personaje

    // Start is called before the first frame update
    void Start()
    {
        // Establece los atributos del personaje
        VidaTotal = VariablesGlobales.instance.KnightVidaTotal;
        VidaActual = VariablesGlobales.instance.KnightVidaActual;
        AtaqueActual = VariablesGlobales.instance.KnightAtaqueActual;
        AtaqueMax = VariablesGlobales.instance.KnightAtaqueMax;
        DefensaActual = VariablesGlobales.instance.KnightDefensaActual;
        DefensaMax = VariablesGlobales.instance.KnightDefensaMax;

        Invencible = false;
        PlayerKnightInvencibleCont = 0;

        CreateHealthbar();                    // Crea la barra de vida del personaje
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthbarPosition();                                                          // Cada frame actualiza la posición de la barra de vida por si el personaje se mueve
        ClonHealthbar.GetComponent<Healthbar>().SetHealth(VidaActual);                      // Actualiza constantemente la Healthbar con la vida actual del personaje
        ClonHealthbar.GetComponent<Healthbar>().ChangeText(VidaActual + " / " + VidaTotal); // Actualiza el texto de la Healthbar a "VidaActual / VidaTotal"
        ResetInvencible();                                                                  // Controla los turnos de invulnerabilidad del Knight

        ControlAtributos();                                                                 // Controla cada frame que los valores de los atributos sean correctos
        ControlMageAbility();                                                               // Controla la duración de la habilidad del Mage
    }

    /****************************************************************************************
     * Función: CreateHealthbar                                                             *
     * Uso: Crea la barra de vida del personaje                                             *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void CreateHealthbar()
    {
        ClonHealthbar = Instantiate(PrefabHealthbar);                                                                                             // Instancia el prefab Healthbar en el clon del prefab Healthbar
        ClonHealthbar.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z); // Lo coloca justo debajo del sprite del enemigo
        ClonHealthbar.GetComponent<Healthbar>().SetMaxHealth(VidaTotal);                                                                          // Inicializa la Healthbar con la vida máxima del personaje
        ClonHealthbar.GetComponent<Healthbar>().ChangeText(VidaActual + " / " + VidaTotal);                                                       // Cambia el texto de la Healthbar a "VidaActual / VidaTotal"
    }

    /****************************************************************************************
     * Función: UpdateHealthbarPosition                                                     *
     * Uso: Actualiza la posición de la barra de vida del personaje                         *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void UpdateHealthbarPosition()
    {
        ClonHealthbar.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z); // Coloca la barra de vida debajo del personaje
    }

    /****************************************************************************************
     * Función: DefensePercentage                                                           *
     * Uso: Establece el porcentage de defensa del enemigo en función de su defensa actual  *
     * Variables entrada: Ninguno                                                           *
     * Return: int - Porcentaje de defensa del enemigo                                      *
     ****************************************************************************************/
    public int DefensePercentage()
    {
        if (DefensaActual == 1)
            return 5;
        else if (DefensaActual == 2)
            return 10;
        else if (DefensaActual == 3)
            return 15;
        else if (DefensaActual == 4)
            return 20;
        else if (DefensaActual == 5)
            return 25;
        else if (DefensaActual == 6)
            return 30;
        else if (DefensaActual == 7)
            return 35;
        else if (DefensaActual == 8)
            return 40;
        else if (DefensaActual == 9)
            return 45;
        else
            return 50;
    }

    /****************************************************************************************
     * Función: ResetInvencible                                                             *
     * Uso: Controla que al pasar los 3 turnos, el Kinght deje de ser invencible            *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void ResetInvencible()
    {
        if(PlayerKnightInvencibleCont == 3)
        {
            PlayerKnightInvencibleCont = 0;
            Invencible = false;
        }
    }

    /****************************************************************************************
     * Función: ControlAtributes                                                            *
     * Uso: Controla que si algún atributo del personaje es negativo, pasa a ser 0. Y si    *
     *      algún atributo es mayor que su máximo, pasa a ser su máximo                     *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void ControlAtributos()
    {
        if (VidaActual < 0)
            VidaActual = 0;
        if (VidaActual > VidaTotal)
            VidaActual = VidaTotal;

        if (AtaqueActual < 0)
            AtaqueActual = 0;
        if (AtaqueActual > AtaqueMax)
            AtaqueActual = AtaqueMax;

        if (DefensaActual < 0)
            DefensaActual = 0;
        if (DefensaActual > DefensaMax)
            DefensaActual = DefensaMax;
    }

    /****************************************************************************************
     * Función: ControlMageAbility                                                          *
     * Uso: Controla el tiempo que dura la habilidad del Mage                               *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void ControlMageAbility()
    {
        if (HabilidadMage == true)
        {
            if (_CombatBackground.GetComponent<CombatBackground>().ContHabilidadMage >= 3) // Si ya han pasado dos o más turnos
            {
                HabilidadMage = false;                                                     // Inhabilita la habilidad del Slime
                DefensaActual -= 2;                                                         // Devuelve la defensa perdida
                _CombatBackground.GetComponent<CombatBackground>().ContHabilidadMage = 0;  // Reinicia el contador

                // Indica al Mage del Jugador que ya puede usar de nuevo su habilidad
                for (int i = 0; i < _CombatBackground.GetComponent<CombatBackground>().Aliados.Length; i++)
                {
                    if (_CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 4)
                        _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().UsedAbility = false;
                }
            }
        }
    }
}
