using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider _Slider;      // Slider que representa la vida del personaje
    public Gradient _Gradient;  // Para controlar que a determinados porcentajes cambia el color de la Healthbar
    public Image Life;          // Barra de vida
    public TMP_Text _TextoLife; // Texto asociado

    /****************************************************************************************
     * Función: SetHealth                                                                   *
     * Uso: Controla la vida que tiene el personaje en ese momento                          *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void SetHealth(int health)
    {
        _Slider.value = health;                                   // Establece la vida actual del personaje
        Life.color = _Gradient.Evaluate(_Slider.normalizedValue); // Establece el color de la vida dependiendo de su porcentaje
    }

    /****************************************************************************************
     * Función: SetMaxHealth                                                                *
     * Uso: Establece la vida máxima de la Healthbar                                        *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void SetMaxHealth(int maxHealth)
    {
        _Slider.maxValue = maxHealth;        // Establece la vida máxima de la Healthbar
        _Slider.value = maxHealth;           // Establece la vida actual del personaje (al principio la vida está a tope)
        Life.color = _Gradient.Evaluate(1f); // Establece que al principio, el color de la Healthbar será el máximo
    }

    /****************************************************************************************
     * Función: ChangeText                                                                  *
     * Uso: Cambia el contenido del texto                                                   *
     * Variables entrada: text - texto a introducir                                         *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void ChangeText(string text)
    {
        _TextoLife.text = text;
    }
}
