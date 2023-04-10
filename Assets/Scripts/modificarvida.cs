using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class modificarvida : MonoBehaviour
{
    public Text Moneytxt; //creo un public text para poder modificar el valor en pantalla

    
    public int HpCost = 10; //declaro las variables del costo de corazones para curar
    public int HpHeal = 25; //declaro las variables de la cantidad de cura
    // Start is called before the first frame update
    void Start()
    {
        //RefreshUI();
        Moneytxt.text = Control.Instance.Money.ToString(); //llamo la clase de "control" para poder usar el valor de "money" y mostrarlo en pantalla
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void masvida() //suma vida y resta dinero
    {
        if (Money >= HpCost)
        {
            HpKnight += HpHeal;
            Money -= HpCost;
        }

    }


    //private void awake() //carga los datos guardados
    //{
    //    LoadData();
    //}

    //private void OnDestroy()
    //{
    //    SaveData();
    //}
    //private void RefreshUI()
    //{
    //    userInterface.RefreshMoney(Money);
    //    userInterface.RefreshHpKnight(HpKnight);
    //}

    //private void SaveData()
    //{

    //    PlayerPrefs.SetInt(MoneyName, Money); //(primero el string y luego el int)
    //    PlayerPrefs.SetInt(HpName, HpKnight);

    //}

    //private void LoadData()
    //{
    //    Money = PlayerPrefs.GetInt();
    //    HpKnight = PlayerPrefs.GetInt();

    //}
}
