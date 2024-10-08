using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMage : MonoBehaviour
{
    public GameObject _CombatBackground; // Combate

    // ESTAD�STICAS DEL PERSONAJE
    public int VidaTotal;              // Vida m�xima del personaje
    public int VidaActual;             // Vida actual del personaje
    public int AtaqueActual;           // Ataque actual del personaje
    public int AtaqueMax;              // Ataque m�ximo del personaje
    public int DefensaActual;          // Defensa actual del personaje
    public int DefensaMax;             // Defensa m�xima del personaje

    public int AtaqueOriginal;
    public int DefensaOriginal;

    public GameObject PrefabHealthbar; // Prefab Healthbar
    public GameObject ClonHealthbar;   // Clon del prefab Healthbar

    public bool UsedAbility;           // Booleano para controlar si ha usado su habilidad o no

    public bool HabilidadMage; // Booleano para controlar si la habilidad del Mage est� activa sobre este personaje

    // Start is called before the first frame update
    void Start()
    {
        // Establece los atributos del personaje
        VidaTotal = VariablesGlobales.instance.MageVidaTotal;
        VidaActual = VariablesGlobales.instance.MageVidaActual;
        AtaqueActual = VariablesGlobales.instance.MageAtaqueActual;
        AtaqueMax = VariablesGlobales.instance.MageAtaqueMax;
        DefensaActual = VariablesGlobales.instance.MageDefensaActual;
        DefensaMax = VariablesGlobales.instance.MageDefensaMax;

        AtaqueOriginal = AtaqueActual;
        DefensaOriginal= DefensaActual;

        CreateHealthbar();                    // Crea la barra de vida del personaje
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthbarPosition();                                                          // Cada frame actualiza la posici�n de la barra de vida por si el personaje se mueve
        ClonHealthbar.GetComponent<Healthbar>().SetHealth(VidaActual);                      // Actualiza constantemente la Healthbar con la vida actual del personaje
        ClonHealthbar.GetComponent<Healthbar>().ChangeText(VidaActual + " / " + VidaTotal); // Actualiza el texto de la Healthbar a "VidaActual / VidaTotal"

        ControlAtributos();                                                                 // Controla cada frame que los valores de los atributos sean correctos
        ControlMageAbility();                                                               // Controla la duraci�n de la habilidad del Mage
        ControlMageAbility2();
    }

    /****************************************************************************************
     * Funci�n: CreateHealthbar                                                             *
     * Uso: Crea la barra de vida del personaje                                             *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void CreateHealthbar()
    {
        ClonHealthbar = Instantiate(PrefabHealthbar);                                                                                             // Instancia el prefab Healthbar en el clon del prefab Healthbar
        ClonHealthbar.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z); // Lo coloca justo debajo del sprite del enemigo
        ClonHealthbar.GetComponent<Healthbar>().SetMaxHealth(VidaTotal);                                                                          // Actualiza constantemente la Healthbar con la vida actual del personaje                                                                          // Inicializa la Healthbar con la vida m�xima del personaje
        ClonHealthbar.GetComponent<Healthbar>().ChangeText(VidaActual + " / " + VidaTotal);                                                       // Cambia el texto de la Healthbar a "VidaActual / VidaTotal"
    }

    /****************************************************************************************
     * Funci�n: UpdateHealthbarPosition                                                     *
     * Uso: Actualiza la posici�n de la barra de vida del personaje                         *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void UpdateHealthbarPosition()
    {
        ClonHealthbar.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z); // Coloca la barra de vida debajo del personaje
    }

    /****************************************************************************************
     * Funci�n: DefensePercentage                                                           *
     * Uso: Establece el porcentage de defensa del enemigo en funci�n de su defensa actual  *
     * Variables entrada: Ninguno                                                           *
     * Return: int - Porcentaje de defensa del enemigo                                      *
     ****************************************************************************************/
    public int DefensePercentage()
    {
        return DefensaActual * 5;
    }

    /****************************************************************************************
     * Funci�n: ControlAtributes                                                            *
     * Uso: Controla que si alg�n atributo del personaje es negativo, pasa a ser 0. Y si    *
     *      alg�n atributo es mayor que su m�ximo, pasa a ser su m�ximo                     *
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

        if (HabilidadMage == false)
        {
            if (DefensaActual < 0)
                DefensaActual = 0;
            if (DefensaActual > DefensaMax)
                DefensaActual = DefensaMax;
        }
    }

    /****************************************************************************************
     * Funci�n: ControlMageAbility                                                          *
     * Uso: Controla el tiempo que dura la habilidad del Mage                               *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void ControlMageAbility()
    {
        if (HabilidadMage == true)
        {
            if (_CombatBackground.GetComponent<CombatBackground>().ContHabilidadMage >= 3) // Si ya han pasado dos o m�s turnos
            {
                HabilidadMage = false;                                                     // Inhabilita la habilidad del Slime
                DefensaActual -= 2;                                                         // Devuelve la defensa perdida
                _CombatBackground.GetComponent<CombatBackground>().ContHabilidadMage = 0;  // Reinicia el contador

                // Indica al Mage del Jugador que ya puede usar de nuevo su habilidad
                for (int i = 0; i < _CombatBackground.GetComponent<CombatBackground>().Aliados.Length; i++)
                {
                    if (_CombatBackground.GetComponent<CombatBackground>().Aliados[i] != null)
                    {
                        if (_CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<GeneralPlayer>().CharacterType == 4)
                            _CombatBackground.GetComponent<CombatBackground>().Aliados[i].GetComponent<PlayerMage>().UsedAbility = false;
                    }
                }
            }
        }
    }

    private void ControlMageAbility2()
    {
        if (UsedAbility == true)
        {
            if (_CombatBackground.GetComponent<CombatBackground>().ContHabilidadMage >= 3) // Si ya han pasado dos o m�s turnos
            {
                UsedAbility = false;                                                     // Inhabilita la habilidad del Slime
                _CombatBackground.GetComponent<CombatBackground>().ContHabilidadMage = 0;  // Reinicia el contador
            }
        }
    }
}
