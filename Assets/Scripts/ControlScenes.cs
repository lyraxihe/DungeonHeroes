using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Button CombatButton;
    public Button BossButton;
   
    public void CombatRoom()
    {
        
        SceneManager.LoadScene("Combat"); //abre la escena
        VariablesGlobales.instance.Boss = false; // Indica que el combate será normal
        CombatButton.interactable = false;
    }

    public void Map()
    {
        SceneManager.LoadScene("Main"); //abre la escena
    }
    public void ShopRoom()
    {
        SceneManager.LoadScene("Shop"); //abre la escena
        
    }
    public void BossRoom()
    {
        SceneManager.LoadScene("Combat"); //abre la escena
        VariablesGlobales.instance.Boss = true; // Indica que el combate será contra el boss
        BossButton.interactable = false;
    }
   
}
