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
        SceneManager.LoadScene("Boss"); //abre la escena
    }
    public void ChestRoom()
    {
        SceneManager.LoadScene("Chest"); //abre la escena
    }
}
