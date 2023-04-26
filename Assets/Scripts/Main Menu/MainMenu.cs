using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuBackground;
    public GameObject ClonMenuBackground;

    public TMP_Text Titulo;

    public Button EmpezarPartidaButton;
    public TMP_Text EmpezarPartidaText;
    public Button CreditosButton;
    public TMP_Text CreditosText;
    public Button ControlesButton;
    public TMP_Text ControlesText;
    public Button SalirButton;
    public TMP_Text SalirText;
    public TMP_Text Version;
    public TMP_Text Autor;
    
    public Button VolverButton;
    public TMP_Text VolverText;
    public TMP_Text Diseñadores;
    public TMP_Text DiseñadoresNombres;
    public TMP_Text Artistas;
    public TMP_Text ArtistasNombres;
    public TMP_Text Programadores;
    public TMP_Text ProgramadoresNombres;

    public GameObject Escape;
    public TMP_Text EscapeText;
    public GameObject Mouse;
    public TMP_Text MouseText;

    // Start is called before the first frame update
    void Start()
    {
        ClonMenuBackground = Instantiate(MenuBackground);  // Crea el fondo que tendrá el Menú
        ClonMenuBackground.transform.position = new Vector3(0, 0, 0); // Coloca el fondo en (0, 0, 0)
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    public void Creditos()
    {
        Titulo.text = "Creditos";

        // Deshabilitar
        EmpezarPartidaButton.image.enabled = false;
        EmpezarPartidaText.enabled = false;
        CreditosButton.image.enabled = false;
        CreditosText.enabled = false;
        ControlesButton.image.enabled = false;
        ControlesText.enabled = false;
        SalirButton.image.enabled = false;
        SalirText.enabled = false;
        Version.enabled = false;
        Autor.enabled = false;

        // Habilitar
        VolverButton.image.enabled = true;
        VolverText.enabled = true;
        Diseñadores.enabled = true;
        DiseñadoresNombres.enabled = true;
        Artistas.enabled = true;
        ArtistasNombres.enabled = true;
        Programadores.enabled = true;
        ProgramadoresNombres.enabled = true;
    }

    public void Volver()
    {
        Titulo.text = "Dungeons Heroes";

        // Deshabilitar
        VolverButton.image.enabled = false;
        VolverText.enabled = false;
        Diseñadores.enabled = false;
        DiseñadoresNombres.enabled = false;
        Artistas.enabled = false;
        ArtistasNombres.enabled = false;
        Programadores.enabled = false;
        ProgramadoresNombres.enabled = false;
        Escape.GetComponent<SpriteRenderer>().enabled = false;
        EscapeText.enabled = false;
        Mouse.GetComponent<SpriteRenderer>().enabled = false;
        MouseText.enabled = false;

        // Habilitar
        EmpezarPartidaButton.image.enabled = true;
        EmpezarPartidaText.enabled = true;
        CreditosButton.image.enabled = true;
        CreditosText.enabled = true;
        ControlesButton.image.enabled = true;
        ControlesText.enabled = true;
        SalirButton.image.enabled = true;
        SalirText.enabled = true;
        Version.enabled = true;
        Autor.enabled = true;
    }

    public void Controles()
    {
        Titulo.text = "Controles";

        // Deshabilitar
        EmpezarPartidaButton.image.enabled = false;
        EmpezarPartidaText.enabled = false;
        CreditosButton.image.enabled = false;
        CreditosText.enabled = false;
        ControlesButton.image.enabled = false;
        ControlesText.enabled = false;
        SalirButton.image.enabled = false;
        SalirText.enabled = false;
        Version.enabled = false;
        Autor.enabled = false;

        // Habilitar
        VolverButton.image.enabled = true;
        VolverText.enabled = true;
        Escape.GetComponent<SpriteRenderer>().enabled = true;
        EscapeText.enabled = true;
        Mouse.GetComponent<SpriteRenderer>().enabled = true;
        MouseText.enabled = true;
    }

    public void EmpezarPartida()
    {
        SceneManager.LoadScene("Main"); //abre la escena
    }
}
