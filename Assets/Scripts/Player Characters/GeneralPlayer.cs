using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralPlayer : MonoBehaviour
{
    public GameObject _CombatBackground;               // Combate

    public GameObject Character;                       // Personaje
    public int CharacterType;                          // Indica el tipo de personaje

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

    public Sprite ImagenHabilidadKnight;
    public Sprite ImagenHabilidadHealer;
    public Sprite ImagenHabilidadSlime;
    public Sprite ImagenHabilidadMage;

    public GameObject EstadisticaVida;
    public GameObject EstadisticaAtaque;
    public GameObject EstadisticaDefensa;

    public GameObject[] Enemies;                       // Array de enemigos del combate
    public GameObject[] Aliados;                       // Array de aliados del combate

    public GameObject PrefabCharacterBattleInfoBorder; // Prefab Character Battle Info Broder
    public GameObject PrefabCharacterBattleInfo;       // Prefab Character Battle Info
    public GameObject PrefabCharacterImage;            // Prefab Character Image
    public GameObject PrefabMove;                      // Prefab Button Move
    public GameObject PrefabAbility1;                  // Prefab Button Ability 1
    public GameObject PrefabAbility2;                  // Prefab Button Ability 2
    public GameObject PrefabTexto;                     // Prefab del texto de Mover, habilidad 1 y habilidad 2

    private GameObject ClonCharacterBattleInfoBorder;  // Clon del prefab Character Battle Info Broder
    private GameObject ClonCharacterBattleInfo;        // Clon del prefab Character Battle Info
    private GameObject ClonCharacterImage;             // Clon del Character Image
    private GameObject ClonMove;                       // Clon del Button Move
    private GameObject ClonTextoMover;                 // Clon del prefab Texto para Mover
    private GameObject ClonAbility1;                   // Clon del Button Ability 1
    private GameObject ClonTextoAbility1;              // Clon del prefab Texto para la Habilidad 1
    private GameObject ClonAbility2;                   // Clon del Button Ability 2
    private GameObject ClonTextoAbility2;              // Clon del prefab Texto para la Habilidad 2

    public GameObject CharacterPosition; // Posici�n actual en la que se encuentra el personaje
    public GameObject[] Positions;       // Array de posiciones

    public bool _StartBattle; // Booleano que indica si se ha iniciado el Combate en CombatBackGround

    public Vector2 MinTam; // Tama�o m�ximo que puede llegar a tener el enemigo
    public Vector2 MaxTam; // Tama�o m�nimo que puede llegar a tener el enemigo

    public bool ClickOnce = false; // Booleano para controlar que s�lo genere la interfaz de movimientos una vez

    public bool Selected = false; // Booleano que indica si un personaje del Jugador lo ha seleccionado para su habilidad
    public bool Down = false;     // Blooleano que indica cuando puede empezar a reducir de tama�o el personaje (Est�tica)
    public bool Vibrate = false;  // Booleano que controla si el personaje seleccionable puede vibrar o no (Est�tica)

    public int Action;                    // 0 - ninguna acci�n, 1 - Habilidad Healer, 2 - Habilidad Mage
    public GameObject PlayerUsingAbility; // Tipo de personaje del Jugador usando su habilidad

    //ACCIONES QUE EST� REALIZANDO EL PERSONAJE
    public bool Moviendo;   // Indica si el Jugador ha seleccionado que el personaje se mueva
    public bool Atacando;   // Indica si el Jugador ha seleccionado que el personaje ataque normal
    public bool Habilidad2; // Indica si el Jugador ha seleccionado que el personaje use su habilidad especial

    // Start is called before the first frame update
    void Start()
    {
        MinTam = new Vector2(transform.localScale.x, transform.localScale.y);         // Establece el tama�o m�ximo que puede llegar a tener la posici�n
        MaxTam = new Vector2(transform.localScale.x * 2, transform.localScale.y * 2); // Establece el tama�o m�nimo que puede llegar a tener la posici�n

        Aliados = _CombatBackground.GetComponent<CombatBackground>().Aliados;

        Moviendo = false;
        Atacando = false;
        Habilidad2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<DragDrop>().enabled == true)                       // Si el script DragDrop est� activo
            CharacterPosition = GetComponent<DragDrop>().CharacterPosition; // Almacena la posici�n actual del personaje

        if (Selected && PlayerUsingAbility.GetComponent<GeneralPlayer>().Habilidad2)                                                       // Si el enemigo puede ser atacado
        {
            if (Vibrate)                                                    // Si vibrar es true
                ScaleUpPositionBecauseSelected();                           // Empieza la animaci�n para indicar que el enemjigo puede ser atacado

            //OnMouseOver();                                                  // Cuando se hace click en el enemigo, este es atacado
        }
    }

    /****************************************************************************************
     * Funci�n: OnMouseDown                                                                 *
     * Uso: Controla lo que le pasa al personaje cuando se clica en �l durante el combate   *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    private void OnMouseDown()
    {
        GameObject[] arrayInfo = _CombatBackground.GetComponent<CombatBackground>().CharacterInterface;
        int i;

        if (_CombatBackground.GetComponent<CombatBackground>().Victoria == false && _CombatBackground.GetComponent<CombatBackground>().Derrota == false)
        {
            if (Action == 0)
            {
                if (_StartBattle && !ClickOnce)                                                        // Si el combate est� iniciado
                {
                    // TEXTO EXPLICACI�N
                    /************************************************************************************************************************/
                    _CombatBackground.GetComponent<CombatBackground>().ClonTextoExplicacion.GetComponent<TextoTurno>().ChangeText("Selecciona que accion quieres que realice tu heroe");
                    /************************************************************************************************************************/

                    // Elimina la interfaz del personaje anterior si selecciona otro nuevo
                    for (i = 0; i < arrayInfo.Length; i++)                                                         // Recorre el array de la interfaz del personaje
                    {
                        if (arrayInfo[i] != null)
                            if (i == 0)                                                                                // La primera posici�n corresponde al personaje de la interfaz
                            {
                                arrayInfo[i].transform.localScale = arrayInfo[i].GetComponent<GeneralPlayer>().MinTam; // Lo devuelve a su tama�o original
                                arrayInfo[i].GetComponent<GeneralPlayer>().ClickOnce = false;                          // Permite que pueda volver a ser seleccionado
                            }
                            else
                                Destroy(arrayInfo[i]);                                                                 // Elimina el elemento de la interfaz del personaje
                    }

                    for (i = 0; i < Enemies.Length; i++)
                    {
                        if (Enemies[i] != null)
                        {
                            if (!VariablesGlobales.instance.Boss)
                            {
                                Enemies[i].GetComponent<GeneralEnemy>().SelectedToAttack = false;
                                Enemies[i].transform.localScale = Enemies[i].GetComponent<GeneralEnemy>().MinTam;
                            }
                            else
                            {
                                Enemies[i].GetComponent<Boss>().SelectedToAttack = false;
                                Enemies[i].transform.localScale = Enemies[i].GetComponent<Boss>().MinTam;
                            }
                        }
                    }

                    for (i = 0; i < Positions.Length; i++)
                    {
                        Positions[i].GetComponent<CombatPosition>().SelectedToMove = false;
                        Positions[i].transform.localScale = Positions[i].GetComponent<CombatPosition>().MinTam;
                    }


                    transform.localScale = MaxTam;                              // Aumenta el tama�o del spriteclonHealthbar.GetComponent<RectTransform>().position = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);

                    // Crea la interfaz del personaje actual

                    _CombatBackground.GetComponent<CombatBackground>().CharacterInterface[0] = Character;   // Almacena el personaje en el array de info del Personaje del Jugador

                    UIEstadisticasPersonaje.SetActive(true);
                    if(CharacterType == 1)
                    {
                        // Estad�stica Vida
                        if (Character.GetComponent<PlayerKnight>().VidaActual <= ((20 * Character.GetComponent<PlayerKnight>().VidaTotal) / 100))
                        {
                            TextoVida.text = "<b><color=red>" + Character.GetComponent<PlayerKnight>().VidaActual + "</color> / " + Character.GetComponent<PlayerKnight>().VidaTotal + "</b>";
                            VidaImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerKnight>().VidaActual <= ((50 * Character.GetComponent<PlayerKnight>().VidaTotal) / 100))
                        {
                            TextoVida.text = "<b><color=yellow>" + Character.GetComponent<PlayerKnight>().VidaActual + "</color> / " + Character.GetComponent<PlayerKnight>().VidaTotal + "</b>";
                            VidaImagen.color = Color.yellow;
                        }
                        else
                        {
                            TextoVida.text = "<b>" + Character.GetComponent<PlayerKnight>().VidaActual + " / " + Character.GetComponent<PlayerKnight>().VidaTotal + "</b>";
                            VidaImagen.color = Color.white;
                        }

                        // Estad�stica Ataque
                        if (Character.GetComponent<PlayerKnight>().AtaqueActual < Character.GetComponent<PlayerKnight>().AtaqueOriginal)
                        {
                            TextoAtaque.text = "<b><color=red>" + Character.GetComponent<PlayerKnight>().AtaqueActual + "</color> / " + Character.GetComponent<PlayerKnight>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerKnight>().AtaqueActual > Character.GetComponent<PlayerKnight>().AtaqueOriginal)
                        {
                            TextoAtaque.text = "<b><color=green>" + Character.GetComponent<PlayerKnight>().AtaqueActual + "</color> / " + Character.GetComponent<PlayerKnight>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.green;
                        }
                        else
                        {
                            TextoAtaque.text = "<b>" + Character.GetComponent<PlayerKnight>().AtaqueActual + " / " + Character.GetComponent<PlayerKnight>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.white;
                        }

                        // Estad�stica Defensa
                        if (Character.GetComponent<PlayerKnight>().DefensaActual < Character.GetComponent<PlayerKnight>().DefensaOriginal)
                        {
                            TextoDefensa.text = "<b><color=red>" + Character.GetComponent<PlayerKnight>().DefensePercentage() + "%</color> / 50%</b>";
                            DefensaImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerKnight>().DefensaActual > Character.GetComponent<PlayerKnight>().DefensaOriginal)
                        {
                            TextoDefensa.text = "<b><color=green>" + Character.GetComponent<PlayerKnight>().DefensePercentage() + "%</color> / 50%</b>";
                            DefensaImagen.color = Color.green;
                        }
                        else
                        {
                            TextoDefensa.text = "<b>" + Character.GetComponent<PlayerKnight>().DefensePercentage() + "% / 50%</b>";
                            DefensaImagen.color = Color.white;
                        }
                    }
                    else if (CharacterType == 2)
                    {
                        // Estad�stica Vida
                        if (Character.GetComponent<PlayerHealer>().VidaActual <= ((20 * Character.GetComponent<PlayerHealer>().VidaTotal) / 100))
                        {
                            TextoVida.text = "<b><color=red>" + Character.GetComponent<PlayerHealer>().VidaActual + "</color> / " + Character.GetComponent<PlayerHealer>().VidaTotal + "</b>";
                            VidaImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerHealer>().VidaActual <= ((50 * Character.GetComponent<PlayerHealer>().VidaTotal) / 100))
                        {
                            TextoVida.text = "<b><color=yellow>" + Character.GetComponent<PlayerHealer>().VidaActual + "</color> / " + Character.GetComponent<PlayerHealer>().VidaTotal + "</b>";
                            VidaImagen.color = Color.yellow;
                        }
                        else
                        {
                            TextoVida.text = "<b>" + Character.GetComponent<PlayerHealer>().VidaActual + " / " + Character.GetComponent<PlayerHealer>().VidaTotal + "</b>";
                            VidaImagen.color = Color.white;
                        }

                        // Estad�stica Ataque
                        if (Character.GetComponent<PlayerHealer>().AtaqueActual < Character.GetComponent<PlayerHealer>().AtaqueOriginal)
                        {
                            TextoAtaque.text = "<b><color=red>" + Character.GetComponent<PlayerHealer>().AtaqueActual + "</color> / " + Character.GetComponent<PlayerHealer>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerHealer>().AtaqueActual > Character.GetComponent<PlayerHealer>().AtaqueOriginal)
                        {
                            TextoAtaque.text = "<b><color=green>" + Character.GetComponent<PlayerHealer>().AtaqueActual + "</color> / " + Character.GetComponent<PlayerHealer>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.green;
                        }
                        else
                        {
                            TextoAtaque.text = "<b>" + Character.GetComponent<PlayerHealer>().AtaqueActual + " / " + Character.GetComponent<PlayerHealer>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.white;
                        }

                        // Estad�stica Defensa
                        if (Character.GetComponent<PlayerHealer>().DefensaActual < Character.GetComponent<PlayerHealer>().DefensaOriginal)
                        {
                            TextoDefensa.text = "<b><color=red>" + Character.GetComponent<PlayerHealer>().DefensePercentage() + "%</color> / 50%</b>";
                            DefensaImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerHealer>().DefensaActual > Character.GetComponent<PlayerHealer>().DefensaOriginal)
                        {
                            TextoDefensa.text = "<b><color=green>" + Character.GetComponent<PlayerHealer>().DefensePercentage() + "%</color> / 50%</b>";
                            DefensaImagen.color = Color.green;
                        }
                        else
                        {
                            TextoDefensa.text = "<b>" + Character.GetComponent<PlayerHealer>().DefensePercentage() + "% / 50%</b>";
                            DefensaImagen.color = Color.white;
                        }
                    }
                    else if (CharacterType == 3)
                    {
                        // Estad�stica Vida
                        if (Character.GetComponent<PlayerSlime>().VidaActual <= ((20 * Character.GetComponent<PlayerSlime>().VidaTotal) / 100))
                        {
                            TextoVida.text = "<b><color=red>" + Character.GetComponent<PlayerSlime>().VidaActual + "</color> / " + Character.GetComponent<PlayerSlime>().VidaTotal + "</b>";
                            VidaImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerSlime>().VidaActual <= ((50 * Character.GetComponent<PlayerSlime>().VidaTotal) / 100))
                        {
                            TextoVida.text = "<b><color=yellow>" + Character.GetComponent<PlayerSlime>().VidaActual + "</color> / " + Character.GetComponent<PlayerSlime>().VidaTotal + "</b>";
                            VidaImagen.color = Color.yellow;
                        }
                        else
                        {
                            TextoVida.text = "<b>" + Character.GetComponent<PlayerSlime>().VidaActual + " / " + Character.GetComponent<PlayerSlime>().VidaTotal + "</b>";
                            VidaImagen.color = Color.white;
                        }

                        // Estad�stica Ataque
                        if (Character.GetComponent<PlayerSlime>().AtaqueActual < Character.GetComponent<PlayerSlime>().AtaqueOriginal)
                        {
                            TextoAtaque.text = "<b><color=red>" + Character.GetComponent<PlayerSlime>().AtaqueActual + "</color> / " + Character.GetComponent<PlayerSlime>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerSlime>().AtaqueActual > Character.GetComponent<PlayerSlime>().AtaqueOriginal)
                        {
                            TextoAtaque.text = "<b><color=green>" + Character.GetComponent<PlayerSlime>().AtaqueActual + "</color> / " + Character.GetComponent<PlayerSlime>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.green;
                        }
                        else
                        {
                            TextoAtaque.text = "<b>" + Character.GetComponent<PlayerSlime>().AtaqueActual + " / " + Character.GetComponent<PlayerSlime>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.white;
                        }

                        // Estad�stica Defensa
                        if (Character.GetComponent<PlayerSlime>().DefensaActual < Character.GetComponent<PlayerSlime>().DefensaOriginal)
                        {
                            TextoDefensa.text = "<b><color=red>" + Character.GetComponent<PlayerSlime>().DefensePercentage() + "%</color> / 50%</b>";
                            DefensaImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerSlime>().DefensaActual > Character.GetComponent<PlayerSlime>().DefensaOriginal)
                        {
                            TextoDefensa.text = "<b><color=green>" + Character.GetComponent<PlayerSlime>().DefensePercentage() + "%</color> / 50%</b>";
                            DefensaImagen.color = Color.green;
                        }
                        else
                        {
                            TextoDefensa.text = "<b>" + Character.GetComponent<PlayerSlime>().DefensePercentage() + "% / 50%</b>";
                            DefensaImagen.color = Color.white;
                        }
                    }
                    else
                    {
                        // Estad�stica Vida
                        if (Character.GetComponent<PlayerMage>().VidaActual <= ((20 * Character.GetComponent<PlayerMage>().VidaTotal) / 100))
                        {
                            TextoVida.text = "<b><color=red>" + Character.GetComponent<PlayerMage>().VidaActual + "</color> / " + Character.GetComponent<PlayerMage>().VidaTotal + "</b>";
                            VidaImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerMage>().VidaActual <= ((50 * Character.GetComponent<PlayerMage>().VidaTotal) / 100))
                        {
                            TextoVida.text = "<b><color=yellow>" + Character.GetComponent<PlayerMage>().VidaActual + "</color> / " + Character.GetComponent<PlayerMage>().VidaTotal + "</b>";
                            VidaImagen.color = Color.yellow;
                        }
                        else
                        {
                            TextoVida.text = "<b>" + Character.GetComponent<PlayerMage>().VidaActual + " / " + Character.GetComponent<PlayerMage>().VidaTotal + "</b>";
                            VidaImagen.color = Color.white;
                        }

                        // Estad�stica Ataque
                        if (Character.GetComponent<PlayerMage>().AtaqueActual < Character.GetComponent<PlayerMage>().AtaqueOriginal)
                        {
                            TextoAtaque.text = "<b><color=red>" + Character.GetComponent<PlayerMage>().AtaqueActual + "</color> / " + Character.GetComponent<PlayerMage>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerMage>().AtaqueActual > Character.GetComponent<PlayerMage>().AtaqueOriginal)
                        {
                            TextoAtaque.text = "<b><color=green>" + Character.GetComponent<PlayerMage>().AtaqueActual + "</color> / " + Character.GetComponent<PlayerMage>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.green;
                        }
                        else
                        {
                            TextoAtaque.text = "<b>" + Character.GetComponent<PlayerMage>().AtaqueActual + " / " + Character.GetComponent<PlayerMage>().AtaqueMax + "</b>";
                            AtaqueImagen.color = Color.white;
                        }

                        // Estad�stica Defensa
                        if (Character.GetComponent<PlayerMage>().DefensaActual < Character.GetComponent<PlayerMage>().DefensaOriginal)
                        {
                            TextoDefensa.text = "<b><color=red>" + Character.GetComponent<PlayerMage>().DefensePercentage() + "%</color> / 50%</b>";
                            DefensaImagen.color = Color.red;
                        }
                        else if (Character.GetComponent<PlayerMage>().DefensaActual > Character.GetComponent<PlayerMage>().DefensaOriginal)
                        {
                            TextoDefensa.text = "<b><color=green>" + Character.GetComponent<PlayerMage>().DefensePercentage() + "%</color> / 50%</b>";
                            DefensaImagen.color = Color.green;
                        }
                        else
                        {
                            TextoDefensa.text = "<b>" + Character.GetComponent<PlayerMage>().DefensePercentage() + "% / 50%</b>";
                            DefensaImagen.color = Color.white;
                        }
                    }

                    EstadisticaVida.SetActive(true);
                    EstadisticaAtaque.SetActive(true);
                    EstadisticaDefensa.SetActive(true);

                    ClonCharacterBattleInfoBorder = Instantiate(PrefabCharacterBattleInfoBorder);      // Crea el borde de la interfaz de movimientos durante el turno del jugador
                    ClonCharacterBattleInfoBorder.transform.position = new Vector3(-9.5f, 0.05f, 2);   // Lo posiciona
                    ClonCharacterBattleInfoBorder.transform.localScale = new Vector3(5.5f, 12, 1);     // Lo redimensiona
                    _CombatBackground.GetComponent<CombatBackground>().CharacterInterface[1] = ClonCharacterBattleInfoBorder; // Lo alamcena en el array de info del Personaje del Jugador 

                    ClonCharacterBattleInfo = Instantiate(PrefabCharacterBattleInfo);                  // Crea la caja de la interfaz de movimientos durante el turno del jugador
                    ClonCharacterBattleInfo.transform.position = new Vector3(-9.5f, 0.05f, 1);         // Lo posiciona
                    ClonCharacterBattleInfo.transform.localScale = new Vector3(5.2f, 11.5f, 1);        // Lo redimensiona
                    _CombatBackground.GetComponent<CombatBackground>().CharacterInterface[2] = ClonCharacterBattleInfo; // Lo alamcena en el array de info del Personaje del Jugador

                    ClonCharacterImage = Instantiate(PrefabCharacterImage);                            // Crea la imagen del peronsaje seleccionado para realizar una acci�n
                    ClonCharacterImage.transform.position = new Vector3(-9.5f, 4, 0);                  // Lo posiciona
                    _CombatBackground.GetComponent<CombatBackground>().CharacterInterface[3] = ClonCharacterImage; // Lo alamcena en el array de info del Personaje del Jugador

                    ClonMove = Instantiate(PrefabMove);                                                // Crea el bot�n de movimiento del personaje
                    ClonMove.transform.position = new Vector3(-11.5f, 0, 0);                           // Lo posiciona
                    ClonMove.GetComponent<MoveButton>().CharacterPosition = CharacterPosition;         // Almacena en el bot�n la posici�n actual del personaje
                    ClonMove.GetComponent<MoveButton>().Positions = Positions;                         // Almacena en el bot�n el array de posiciones del combate
                    ClonMove.GetComponent<MoveButton>().Character = Character;                         // Almacena el personaje
                    ClonMove.GetComponent<MoveButton>().UIMover = UIMover;
                    ClonMove.GetComponent<MoveButton>()._CombatBackground = _CombatBackground;         // Almacena el combate
                    _CombatBackground.GetComponent<CombatBackground>().CharacterInterface[4] = ClonMove; // Lo alamcena en el array de info del Personaje del Jugador

                    ClonTextoMover = Instantiate(PrefabTexto);                                         // Crea el Texto Mover
                    ClonTextoMover.transform.position = new Vector3(-10.3f, 0, 0);                     // Lo coloca en la interfaz
                    ClonTextoMover.GetComponent<TextoTurno>().ChangeText("Mover");                     // Cambia el texto a "Mover"
                    ClonTextoMover.GetComponent<TextoTurno>().ChangeFontSize(0.6f);                    // Cambio el tama�o de la fuente del texto
                    ClonTextoMover.GetComponent<TextoTurno>().ChangeColor(1, 1, 1);                    // Cambia el color del texto a blanco
                    _CombatBackground.GetComponent<CombatBackground>().CharacterInterface[5] = ClonTextoMover; // Lo alamcena en el array de info del Personaje del Jugador

                    ClonAbility1 = Instantiate(PrefabAbility1);                                        // Crea el bot�n de la habilidad 1 del personaje
                    ClonAbility1.transform.position = new Vector3(-11.5f, -2, 0);                      // Lo posiciona
                    ClonAbility1.GetComponent<Ability1Button>().CharacterPosition = CharacterPosition; // Almacena en el bot�n la posici�n actual del personaje
                    ClonAbility1.GetComponent<Ability1Button>().Enemies = Enemies;                     // Almacena el array de enemigos 
                    ClonAbility1.GetComponent<Ability1Button>().Positions = Positions;                 // Almacena en el bot�n el array de posiciones del combate
                    ClonAbility1.GetComponent<Ability1Button>().Character = Character;                 // Almacena el personaje
                    ClonAbility1.GetComponent<Ability1Button>().UIAtacarConRango = UIAtacarConRango;
                    ClonAbility1.GetComponent<Ability1Button>().UIAtacarSinRango = UIAtacarSinRango;
                    ClonAbility1.GetComponent<Ability1Button>()._CombatBackground = _CombatBackground; // Almacena el combate
                    _CombatBackground.GetComponent<CombatBackground>().CharacterInterface[6] = ClonAbility1; // Lo alamcena en el array de info del Personaje del Jugador

                    ClonTextoAbility1 = Instantiate(PrefabTexto);                                      // Crea el Texto Ability 1
                    if (CharacterType == 2)                                                            // Si es un Healer
                        ClonTextoAbility1.transform.position = new Vector3(-9.3f, -2, 0);              // Lo coloca en la interfaz
                    else
                        ClonTextoAbility1.transform.position = new Vector3(-9.2f, -2, 0);              // Lo coloca en la interfaz

                    if (CharacterType == 1)       // Si es un Knight
                        ClonTextoAbility1.GetComponent<TextoTurno>().ChangeText("Atacar (ATK: " + Character.GetComponent<PlayerKnight>().AtaqueActual + "p)"); // Cambia el texto a "Atacar + AtaqueActual"
                    else if (CharacterType == 2) // Si es un Healer
                        ClonTextoAbility1.GetComponent<TextoTurno>().ChangeText("Atacar (ATK: " + Character.GetComponent<PlayerHealer>().AtaqueActual + "p)"); // Cambia el texto a "Atacar + AtaqueActual"
                    else if (CharacterType == 3) // Si es un Slime
                        ClonTextoAbility1.GetComponent<TextoTurno>().ChangeText("Atacar (ATK: " + Character.GetComponent<PlayerSlime>().AtaqueActual + "p)");  // Cambia el texto a "Atacar + AtaqueActual"
                    else                         // Si es un Mage
                        ClonTextoAbility1.GetComponent<TextoTurno>().ChangeText("Atacar (ATK: " + Character.GetComponent<PlayerMage>().AtaqueActual + "p)");   // Cambia el texto a "Atacar + AtaqueActual"

                    ClonTextoAbility1.GetComponent<TextoTurno>().ChangeFontSize(0.6f);                 // Cambio el tama�o de la fuente del texto
                    ClonTextoAbility1.GetComponent<TextoTurno>().ChangeColor(1, 1, 1);                 // Cambia el color del texto a blanco
                    _CombatBackground.GetComponent<CombatBackground>().CharacterInterface[7] = ClonTextoAbility1; // Lo alamcena en el array de info del Personaje del Jugador

                    ClonAbility2 = Instantiate(PrefabAbility2);                                        // Crea el bot�n de la habilidad 2 del personaje
                    ClonAbility2.transform.position = new Vector3(-11.5f, -4, 0);                      // Lo posiciona
                    ClonAbility2.GetComponent<Ability2Button>().CharacterPosition = CharacterPosition; // Almacena en el bot�n la posici�n actual del personaje
                    ClonAbility2.GetComponent<Ability2Button>().Enemies = Enemies;                     // Almacena el array de enemigos 
                    ClonAbility2.GetComponent<Ability2Button>().Aliados = _CombatBackground.GetComponent<CombatBackground>().Aliados; // Almacena el array de aliados
                    ClonAbility2.GetComponent<Ability2Button>().Positions = Positions;                 // Almacena en el bot�n el array de posiciones del combate
                    ClonAbility2.GetComponent<Ability2Button>().Character = Character;                 // Almacena el personaje
                    ClonAbility2.GetComponent<Ability2Button>().UIHabilidadKnight = UIHabilidadKnight;
                    ClonAbility2.GetComponent<Ability2Button>().UIHabilidadHealer = UIHabilidadHealer;
                    ClonAbility2.GetComponent<Ability2Button>().UIHabilidadSlime = UIHabilidadSlime;
                    ClonAbility2.GetComponent<Ability2Button>().UIHabilidadMage = UIHabilidadMage;
                    ClonAbility2.GetComponent<Ability2Button>().UIEstadisticasPersonaje = UIEstadisticasPersonaje;
                    ClonAbility2.GetComponent<Ability2Button>()._CombatBackground = _CombatBackground; // Almacena el combate

                    if (CharacterType == 1)                                                                                                                   // Si es un Knight
                    {
                        ClonAbility2.GetComponent<Ability2Button>()._Button.GetComponent<Image>().sprite = ImagenHabilidadKnight;
                        if (Character.GetComponent<PlayerKnight>().Invencible == true)                                                                        // Est� en modo invencible
                        {
                            ClonAbility2.GetComponent<Ability2Button>()._Button.GetComponent<Image>().color = new Color(0.5882353f, 0.5882353f, 0.5882353f);  // Desactiva el bot�n de "Habilidad 2"
                            ClonAbility2.GetComponent<Ability2Button>()._Button.GetComponent<Button>().transition = Selectable.Transition.None;
                        }
                    }
                    else if (CharacterType == 2)
                    {
                        ClonAbility2.GetComponent<Ability2Button>()._Button.GetComponent<Image>().sprite = ImagenHabilidadHealer;
                    }
                    else if (CharacterType == 3)                                                                                                              // Si es un Slime
                    {
                        ClonAbility2.GetComponent<Ability2Button>()._Button.GetComponent<Image>().sprite = ImagenHabilidadSlime;
                        if (Character.GetComponent<PlayerSlime>().UsedAbility == true)                                                                        // Ha usado su habilidad
                        {
                            ClonAbility2.GetComponent<Ability2Button>()._Button.GetComponent<Image>().color = new Color(0.5882353f, 0.5882353f, 0.5882353f);  // Desactiva el bot�n de "Habilidad 2"
                            ClonAbility2.GetComponent<Ability2Button>()._Button.GetComponent<Button>().transition = Selectable.Transition.None;
                        }
                    }
                    else if (CharacterType == 4)                                                                                                              // Si es un Slime
                    {
                        ClonAbility2.GetComponent<Ability2Button>()._Button.GetComponent<Image>().sprite = ImagenHabilidadMage;
                        if (Character.GetComponent<PlayerMage>().UsedAbility == true)                                                                         // Ha usado su habilidad
                        {
                            ClonAbility2.GetComponent<Ability2Button>()._Button.GetComponent<Image>().color = new Color(0.5882353f, 0.5882353f, 0.5882353f);  // Desactiva el bot�n de "Habilidad 2"
                            ClonAbility2.GetComponent<Ability2Button>()._Button.GetComponent<Button>().transition = Selectable.Transition.None;
                        }
                    }

                    _CombatBackground.GetComponent<CombatBackground>().CharacterInterface[8] = ClonAbility2; // Lo alamcena en el array de info del Personaje del Jugador

                    ClonTextoAbility2 = Instantiate(PrefabTexto);                                         // Crea el Texto Ability 2

                    if (CharacterType == 1)                                                               // Si es el Knight
                    {
                        if (Character.GetComponent<PlayerKnight>().Invencible == true)                    // Si el Knight est� en modo invencible
                        {
                            ClonTextoAbility2.transform.position = new Vector3(-9.35f, -4, 0);            // Lo coloca en la interfaz
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeColor(0.5882353f, 0.5882353f, 0.5882353f);
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeText("Invencible (" + (3 - Character.GetComponent<PlayerKnight>().PlayerKnightInvencibleCont) + " t.)"); // Cambia el texto
                        }
                        else                                                                              // Si no
                        {
                            ClonTextoAbility2.transform.position = new Vector3(-9.35f, -4, 0);            // Lo coloca en la interfaz
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeColor(1, 1, 1);
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeText("Invencible (3 t.)"); // Cambia el texto
                        }
                    }
                    else if (CharacterType == 2)                                                         // Si es el Healer
                    {
                        ClonTextoAbility2.transform.position = new Vector3(-9.45f, -4, 0);               // Lo coloca en la interfaz
                        ClonTextoAbility2.GetComponent<TextoTurno>().ChangeColor(1, 1, 1);
                        ClonTextoAbility2.GetComponent<TextoTurno>().ChangeText("Curar (+30 hp)");       // Cambia el texto
                    }
                    else if (CharacterType == 3)                                                         // Si es el Slime
                    {
                        if (Character.GetComponent<PlayerSlime>().UsedAbility == true)                   // Si el Slime ha usado su habilidad
                        {
                            ClonTextoAbility2.transform.position = new Vector3(-9.45f, -4, 0);           // Lo coloca en la interfaz
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeColor(0.5882353f, 0.5882353f, 0.5882353f);
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeText("Red. Def. (" + (3 - _CombatBackground.GetComponent<CombatBackground>().ContHabilidadSlime) + " t.)"); // Cambia el texto
                        }
                        else                                                                             // Si no
                        {
                            ClonTextoAbility2.transform.position = new Vector3(-9.45f, -4, 0);           // Lo coloca en la interfaz
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeColor(1, 1, 1);
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeText("Red. Def. (3 t.)"); // Cambia el texto
                        }
                    }
                    else
                    {
                        if (Character.GetComponent<PlayerMage>().UsedAbility == true)                     // Si el Slime ha usado su habilidad
                        {
                            ClonTextoAbility2.transform.position = new Vector3(-9.45f, -4, 0);            // Lo coloca en la interfaz
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeColor(0.5882353f, 0.5882353f, 0.5882353f);
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeText("Aum. Def. (" + (3 - _CombatBackground.GetComponent<CombatBackground>().ContHabilidadMage) + " t.)"); // Cambia el texto
                        }
                        else
                        {
                            ClonTextoAbility2.transform.position = new Vector3(-9.45f, -4, 0);            // Lo coloca en la interfaz
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeColor(1, 1, 1);
                            ClonTextoAbility2.GetComponent<TextoTurno>().ChangeText("Aum. Def. (3 t.)");  // Cambia el texto
                        }
                    }

                    ClonTextoAbility2.GetComponent<TextoTurno>().ChangeFontSize(0.6f);                 // Cambio el tama�o de la fuente del texto
                    //ClonTextoAbility2.GetComponent<TextoTurno>().ChangeColor(1, 1, 1);                 // Cambia el color del texto a blanco
                    _CombatBackground.GetComponent<CombatBackground>().CharacterInterface[9] = ClonTextoAbility2; // Lo alamcena en el array de info del Personaje del Jugador

                    ClickOnce = true;                                                                  // Booleano para controlar que la interfaz s�lo se cree una vez al clicar en el personaje
                }
            }
            else
            {
                for (i = 0; i < Aliados.Length; i++)                                                           // Recorre el array de aliados
                {
                    if (Aliados[i] != null)
                    {
                        if (Aliados[i].GetComponent<GeneralPlayer>().Character != Character)                   // Para el resto de aliados que no son este
                        {
                            Aliados[i].GetComponent<GeneralPlayer>().Selected = false;                         // Inhabilita que el personaje pueda seleccionarlos
                            Aliados[i].transform.localScale = Aliados[i].GetComponent<GeneralPlayer>().MinTam; // Dichos personajes modificados vuelven a su tama�o original despu�s de vibrar
                        }
                    }
                }

                if (Selected && PlayerUsingAbility.GetComponent<GeneralPlayer>().Habilidad2)                                                                         // Para el enemigo que el Jugador ha elegido para atacar
                {
                    Vibrate = false;                                                                                                                            // El resto de posiciones dejan de vibrar
                    transform.localScale = MinTam;                                                                                                              // La posici�n vuelve a su tama�o original

                    PlayerUsingAbility.GetComponent<Animator>().SetTrigger("ataque");

                    if (Action == 1)                                                                  // Si la acci�n es la habilidad del Healer
                    {
                        if (CharacterType == 1)
                        {
                            Character.GetComponent<PlayerKnight>().VidaActual += 30;
                        }
                        else if (CharacterType == 2)
                        {
                            Character.GetComponent<PlayerHealer>().VidaActual += 30;
                        }
                        else if (CharacterType == 3)
                        {
                            Character.GetComponent<PlayerSlime>().VidaActual += 30;
                        }
                        else
                        {
                            Character.GetComponent<PlayerMage>().VidaActual += 30;
                        }
                    }
                    else if (Action == 2)                                                             // Si la acci�n es la habilidad del Mage
                    {
                        if (CharacterType == 1) // Si el personaje elegido es un Knight
                        {
                            Character.GetComponent<PlayerKnight>().DefensaActual += 2;
                            Character.GetComponent<PlayerKnight>().HabilidadMage = true;
                            _CombatBackground.GetComponent<CombatBackground>().ContHabilidadMage = 0;
                        }
                        else if (CharacterType == 2) // Si el enemigo elegido es un Healer
                        {
                            Character.GetComponent<PlayerHealer>().DefensaActual += 2;
                            Character.GetComponent<PlayerHealer>().HabilidadMage = true;
                            _CombatBackground.GetComponent<CombatBackground>().ContHabilidadMage = 0;
                        }
                        else if (CharacterType == 3) // Si el enemigo elegido es un Slime
                        {
                            Character.GetComponent<PlayerSlime>().DefensaActual += 2;
                            Character.GetComponent<PlayerSlime>().HabilidadMage = true;
                            _CombatBackground.GetComponent<CombatBackground>().ContHabilidadMage = 0;
                        }
                        else                 // Si el enemigo elegido es un Mage
                        {
                            Character.GetComponent<PlayerMage>().DefensaActual += 2;
                            Character.GetComponent<PlayerMage>().HabilidadMage = true;
                            _CombatBackground.GetComponent<CombatBackground>().ContHabilidadMage = 0;
                        }

                        PlayerUsingAbility.GetComponent<PlayerMage>().UsedAbility = true;
                    }

                    PlayerUsingAbility.GetComponent<GeneralPlayer>().Habilidad2 = false;

                    Selected = false;                                                                    // Hace que esta acci�n s�lo se pueda realizar una vez
                    PlayerUsingAbility.transform.localScale = PlayerUsingAbility.GetComponent<GeneralPlayer>().MinTam; // Devuelve al atacante a su tama�o original
                    transform.localScale = MinTam;                                                               // Devuelve al enemigo a su tama�o original

                    PlayerUsingAbility.GetComponent<GeneralPlayer>().DestroyCharacterInfo();         // Destruye la interfaz de informaci�n del personaje
                    _CombatBackground.GetComponent<CombatBackground>().ChangeTurn();              // Tras la acci�n del movimiento, cambia el turno de la partida

                    for (i = 0; i < Enemies.Length; i++)
                    {
                        if (Enemies[i] != null)
                        {
                            if (!VariablesGlobales.instance.Boss)
                                Enemies[i].GetComponent<GeneralEnemy>().Atacar = true;
                            else
                                Enemies[i].GetComponent<Boss>().Atacar = true;

                        }
                    }

                    for (i = 0; i < Aliados.Length; i++)
                    {
                        if (Aliados[i] != null)
                            Aliados[i].GetComponent<GeneralPlayer>().Action = 0;                       // Indica que ya no se realiza ninguna acci�n
                    }

                    PlayerUsingAbility.GetComponent<GeneralPlayer>().Action = 0;                       // Indica que ya no se realiza ninguna acci�n
                    _CombatBackground.GetComponent<CombatBackground>().EnemigoParaAtacar = true;

                    UIEstadisticasPersonaje.SetActive(false);
                }
            }
        }
    }

    /****************************************************************************************
     * Funci�n: DestroyCharacterInfo                                                        *
     * Uso: Destruye la interfaz de la info del personaje seleccionado                      *
     * Variables entrada: Nada                                                              *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void DestroyCharacterInfo()
    {
        Destroy(ClonAbility2);                  // Destruye el bot�n de la Habilidad 2
        Destroy(ClonTextoAbility2);             // Destruye el texto de Habilidad 2
        Destroy(ClonAbility1);                  // Destruye el bot�n de la Habilidad 1
        Destroy(ClonTextoAbility1);             // Destruye el texto de Habilidad 1
        Destroy(ClonMove);                      // Destruye el bot�n de Mover Personaje
        Destroy(ClonTextoMover);                // Destruye el texto de Mover Personaje
        Destroy(ClonCharacterImage);            // Destruye la imagen del personaje
        Destroy(ClonCharacterBattleInfo);       // Destruye la caja de interfaz de movimientos
        Destroy(ClonCharacterBattleInfoBorder); // Destruye el borde de la caja de interfaz de movimientos
    }

    /****************************************************************************************
     * Funci�n: SetStartBattle                                                              *
     * Uso: Obtiene el booleano de COmbatBackground que indica si el combate ha sido        *
     * iniciado                                                                             *
     * Variables entrada: startBattle - booleano que indica si el combate ha sido iniciado  *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void SetStartBattle(bool startBattle)
    {
        _StartBattle = startBattle;
    }

    /****************************************************************************************
     * Funci�n: MoveCharacter                                                               *
     * Uso: Cambai la ubicaci�n del personaje                                               *                                                                             *
     * Variables entrada: ubicacion - Nueva posici�n del personaje                          *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void MoveCharacter(Vector2 ubicacion)
    {
        transform.position = ubicacion;
    }

    /****************************************************************************************
     * Funci�n: ScalePositionBecauseSelected                                                *
     * Uso: Modifica el tama�o del enemigo para que vibre al poder ser atacado              *
     * Variables entrada: Nada                                                              *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void ScaleUpPositionBecauseSelected()
    {
        float velocidad = 1.5f;                                                                            // Velocidad a la que variar� el tama�o de la posici�n

        if (Vector2.Distance(transform.localScale, MaxTam) < 0.2f)                                         // Si la posici�n ha alcanzado el tam�o m�ximo, indica que puede empezar a reducirse
            Down = true;                                                                                   // Indica que puede empezar a reducir el tama�o

        if (Vector2.Distance(transform.localScale, MinTam) < 0.2f)                                         // Si la posici�n ha alcanzado el tama�o m�nimo, indica que puede empezar a aumentar
            Down = false;                                                                                  // INdica que puede empezar a aumentar el tama�o

        if (!Down)                                                                                          // Si puede aumentar de tama�o
            transform.localScale = Vector2.Lerp(transform.localScale, MaxTam, velocidad * Time.deltaTime);  // Aumenta el tama�o progresivamente hasta el tama�o m�ximo
        else                                                                                                // Si puede reducir de tama�o
            transform.localScale = Vector2.Lerp(transform.localScale, MinTam, velocidad * Time.deltaTime);  // Reduce el tama�o progresivamentehasta el tama�o m�nimo
    }
}
