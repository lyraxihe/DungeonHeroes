using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class modificarvida : MonoBehaviour
{
    //public Text Moneytxt; //creo un public text para poder modificar el valor en pantalla

    //public GameObject _Control;
    
    //public int HpCost = 5; //declaro las variables del costo de corazones para curar
    //public int HpHeal = 25; //declaro las variables de la cantidad de cura
    // Start is called before the first frame update
    void Start()
    {
        //RefreshUI();
       // Moneytxt.text = "Money: 100";
       // Moneytxt.text = Control.Instance.Money.ToString(); //llamo la clase de "control" para poder usar el valor de "money" y mostrarlo en pantalla
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void masvida() //suma vida y resta dinero
    {
        //if (_Control.GetComponent<Control>().Money >= HpCost)
        //{
        //    _Control.GetComponent<Control>().HpKnight += HpHeal;
        //    _Control.GetComponent<Control>().Money -= HpCost;
        //}

    }


    //private void Awake() //carga los datos guardados
    //{
    //    LoadData();
    //}

    //private void OnDestroy()
    //{
    //    SaveData();
    //}
    //private void RefreshUI()
    //{
    //    //userInterface.RefreshMoney(GetComponent<Background>().Money);
    //    //userInterface.RefreshHpKnight(GetComponent<Background>().HpKnight);
    //}

    //private void SaveData()
    //{

    //    PlayerPrefs.SetInt(GetComponent<Background>().MoneyName, GetComponent<Background>().Money); //(primero el string y luego el int)
    //    PlayerPrefs.SetInt(GetComponent<Background>().HpName, GetComponent<Background>().HpKnight);

    //}

    //private void LoadData()
    //{
    //    GetComponent<Background>().Money = PlayerPrefs.GetInt(GetComponent<Background>().MoneyName, GetComponent<Background>().Money);
        
    //    GetComponent<Background>().HpKnight = PlayerPrefs.GetInt(GetComponent<Background>().HpName, GetComponent<Background>().HpKnight);

    //}
}
