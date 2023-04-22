using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public void CombatRoom()
    {
        SceneManager.LoadScene("Combat"); //abre la escena
        VariablesGlobales.instance.Boss = false; // Indica que el combate será normal
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
    }
    public void ChestRoom()
    {
        SceneManager.LoadScene("Chest"); //abre la escena
    }
}
