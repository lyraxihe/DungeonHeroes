using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextoTurno : MonoBehaviour
{
    public TMP_Text _TextoTurno; // Texto asociado
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /****************************************************************************************
     * Funci�n: ChangeText                                                                  *
     * Uso: Cambia el contenido del texto                                                   *
     * Variables entrada: text - texto a introducir                                         *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void ChangeText(string text)
    {
        _TextoTurno.text = text;
    }

    /****************************************************************************************
     * Funci�n: ChangeColor                                                                 *
     * Uso: Cambia el color del texto                                                       *
     * Variables entrada: red - valor del color rojo                                        *
     *                    green - valor del color verde                                     *
     *                    blue - valor del color azul                                       *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void ChangeColor (float red, float green, float blue)
    {
        _TextoTurno.color = new Color(red, green, blue);
    }

    /****************************************************************************************
     * Funci�n: ChangeFontSize                                                              *
     * Uso: Cambia el tama�o del texto                                                      *
     * Variables entrada: size - nuevo tama�o de la fuente                                  *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void ChangeFontSize(float size)
    {
        _TextoTurno.fontSize = size;
    }
}
