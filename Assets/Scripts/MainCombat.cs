using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class MainCombat : MonoBehaviour
{
    public GameObject _Control;
    
    public GameObject CombatBackground; // Prefab del Combate

    public TMP_Text PausaText;
    public Button ContinuarButton;
    public TMP_Text ContinuarButtonText;
    public Button SalirButton;
    public TMP_Text SalirButtonText;

    public GameObject UIEstadisticasPersonaje;
    public TMP_Text TextoVida;
    public TMP_Text TextoAtaque;
    public TMP_Text TextoDefensa;
    public Image VidaImagen;
    public Image AtaqueImagen;
    public Image DefensaImagen;

    public GameObject UIMover;
    public GameObject UIAtacarConRango;
    public GameObject UIAtacarSinRango;
    public GameObject UIHabilidadKnight;
    public GameObject UIHabilidadHealer;
    public GameObject UIHabilidadSlime;
    public GameObject UIHabilidadMage;

    public GameObject EstadisticaVida;
    public GameObject EstadisticaAtaque;
    public GameObject EstadisticaDefensa;

    //UI ENEMIGOS
    public GameObject UIEnemigo1;
    public TMP_Text VidaEnemigo1;
    public TMP_Text AtaqueEnemigo1;
    public TMP_Text DefensaEnemigo1;
    public GameObject UIEnemigo2;
    public TMP_Text VidaEnemigo2;
    public TMP_Text AtaqueEnemigo2;
    public TMP_Text DefensaEnemigo2;
    public GameObject UIEnemigo3;
    public TMP_Text VidaEnemigo3;
    public TMP_Text AtaqueEnemigo3;
    public TMP_Text DefensaEnemigo3;
    public GameObject UIEnemigo4;
    public TMP_Text VidaEnemigo4;
    public TMP_Text AtaqueEnemigo4;
    public TMP_Text DefensaEnemigo4;

    public GameObject UIEstadisticaVidaEnemy1;
    public GameObject UIEstadisticaAtaqueEnemy1;
    public GameObject UIEstadisticaDefensaEnemy1;
    public GameObject UIEstadisticaVidaEnemy2;
    public GameObject UIEstadisticaAtaqueEnemy2;
    public GameObject UIEstadisticaDefensaEnemy2;
    public GameObject UIEstadisticaVidaEnemy3;
    public GameObject UIEstadisticaAtaqueEnemy3;
    public GameObject UIEstadisticaDefensaEnemy3;
    public GameObject UIEstadisticaVidaEnemy4;
    public GameObject UIEstadisticaAtaqueEnemy4;
    public GameObject UIEstadisticaDefensaEnemy4;

    // Start is called before the first frame update
    void Start()
    {
        GameObject clon = Instantiate(CombatBackground);                // Crea el fondo que tendrá el Combate
        clon.transform.position = new Vector3(0, 0, 3);                 // Coloca el fondo en (0, 0, 0)
        clon.GetComponent<CombatBackground>()._CombatBackground = clon; // Almacena el combate
        clon.GetComponent<CombatBackground>().UIMover = UIMover;
        clon.GetComponent<CombatBackground>().UIAtacarConRango = UIAtacarConRango;
        clon.GetComponent<CombatBackground>().UIAtacarSinRango = UIAtacarSinRango;
        clon.GetComponent<CombatBackground>().UIHabilidadKnight = UIHabilidadKnight;
        clon.GetComponent<CombatBackground>().UIHabilidadHealer = UIHabilidadHealer;
        clon.GetComponent<CombatBackground>().UIHabilidadSlime = UIHabilidadSlime;
        clon.GetComponent<CombatBackground>().UIHabilidadMage = UIHabilidadMage;
        clon.GetComponent<CombatBackground>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
        clon.GetComponent<CombatBackground>().TextoVida = TextoVida;
        clon.GetComponent<CombatBackground>().TextoAtaque = TextoAtaque;
        clon.GetComponent<CombatBackground>().TextoDefensa = TextoDefensa;
        clon.GetComponent<CombatBackground>().VidaImagen = VidaImagen;
        clon.GetComponent<CombatBackground>().AtaqueImagen = AtaqueImagen;
        clon.GetComponent<CombatBackground>().DefensaImagen = DefensaImagen;

        clon.GetComponent<CombatBackground>().EstadisticaVida = EstadisticaVida;
        clon.GetComponent<CombatBackground>().EstadisticaAtaque = EstadisticaAtaque;
        clon.GetComponent<CombatBackground>().EstadisticaDefensa = EstadisticaDefensa;

        //UI ENEMIGOS
        clon.GetComponent<CombatBackground>().UIEnemigo1 = UIEnemigo1;
        clon.GetComponent<CombatBackground>().VidaEnemigo1 = VidaEnemigo1;
        clon.GetComponent<CombatBackground>().AtaqueEnemigo1 = AtaqueEnemigo1;
        clon.GetComponent<CombatBackground>().DefensaEnemigo1 = DefensaEnemigo1;
        clon.GetComponent<CombatBackground>().UIEnemigo2 = UIEnemigo2;
        clon.GetComponent<CombatBackground>().VidaEnemigo2 = VidaEnemigo2;
        clon.GetComponent<CombatBackground>().AtaqueEnemigo2 = AtaqueEnemigo2;
        clon.GetComponent<CombatBackground>().DefensaEnemigo2 = DefensaEnemigo2;
        clon.GetComponent<CombatBackground>().UIEnemigo3 = UIEnemigo3;
        clon.GetComponent<CombatBackground>().VidaEnemigo3 = VidaEnemigo3;
        clon.GetComponent<CombatBackground>().AtaqueEnemigo3 = AtaqueEnemigo3;
        clon.GetComponent<CombatBackground>().DefensaEnemigo3 = DefensaEnemigo3;
        clon.GetComponent<CombatBackground>().UIEnemigo4 = UIEnemigo4;
        clon.GetComponent<CombatBackground>().VidaEnemigo4 = VidaEnemigo4;
        clon.GetComponent<CombatBackground>().AtaqueEnemigo4 = AtaqueEnemigo4;
        clon.GetComponent<CombatBackground>().DefensaEnemigo4 = DefensaEnemigo4;

        clon.GetComponent<CombatBackground>().UIEstadisticaVidaEnemy1 = UIEstadisticaVidaEnemy1;
        clon.GetComponent<CombatBackground>().UIEstadisticaAtaqueEnemy1 = UIEstadisticaAtaqueEnemy1;
        clon.GetComponent<CombatBackground>().UIEstadisticaDefensaEnemy1 = UIEstadisticaDefensaEnemy1;
        clon.GetComponent<CombatBackground>().UIEstadisticaVidaEnemy2 = UIEstadisticaVidaEnemy2;
        clon.GetComponent<CombatBackground>().UIEstadisticaAtaqueEnemy2 = UIEstadisticaAtaqueEnemy2;
        clon.GetComponent<CombatBackground>().UIEstadisticaDefensaEnemy2 = UIEstadisticaDefensaEnemy2;
        clon.GetComponent<CombatBackground>().UIEstadisticaVidaEnemy3 = UIEstadisticaVidaEnemy3;
        clon.GetComponent<CombatBackground>().UIEstadisticaAtaqueEnemy3 = UIEstadisticaAtaqueEnemy3;
        clon.GetComponent<CombatBackground>().UIEstadisticaDefensaEnemy3 = UIEstadisticaDefensaEnemy3;
        clon.GetComponent<CombatBackground>().UIEstadisticaVidaEnemy4 = UIEstadisticaVidaEnemy4;
        clon.GetComponent<CombatBackground>().UIEstadisticaAtaqueEnemy4 = UIEstadisticaAtaqueEnemy4;
        clon.GetComponent<CombatBackground>().UIEstadisticaDefensaEnemy4 = UIEstadisticaDefensaEnemy4;

    //clon.GetComponent<CombatBackground>()._Control = _Control;
    //clon.GetComponent<CombatBackground>().PausaText = PausaText;
    //clon.GetComponent<CombatBackground>().ContinuarButton = ContinuarButton;
    //clon.GetComponent<CombatBackground>().ContinuarButtonText = ContinuarButtonText;
    //clon.GetComponent<CombatBackground>().SalirButton = SalirButton;
    //clon.GetComponent<CombatBackground>().SalirButtonText = SalirButtonText;
}

    // Update is called once per frame
    void Update()
    {

    }
}
