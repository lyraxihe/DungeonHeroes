using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoriaDerrotaButton : MonoBehaviour
{
    public Button _Button;               // Botón asociado al Canva "MoveButton"
    public GameObject _Combatbackground; // Combate
    public int Money;

    public void Start()
    {
        _Button.GetComponent<VictoriaDerrotaButtonAction>()._CombatBackground= _Combatbackground;
        _Button.GetComponent<VictoriaDerrotaButtonAction>().Money = Money;
    }
}
