using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class CantidadDinero : MonoBehaviour
{
    public TMP_Text MoneyText;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = "" + VariablesGlobales.instance.Money;
    }
}
