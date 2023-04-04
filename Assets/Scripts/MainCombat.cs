using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class MainCombat : MonoBehaviour
{
    public GameObject CombatBackground; // Prefab del Combate

    // Start is called before the first frame update
    void Start()
    {
        GameObject clon = Instantiate(CombatBackground);                // Crea el fondo que tendrá el Combate
        clon.transform.position = new Vector3(0, 0, 3);                 // Coloca el fondo en (0, 0, 0)
        clon.GetComponent<CombatBackground>()._CombatBackground = clon; // Almacena el combate
    }

    // Update is called once per frame
    void Update()
    {

    }
}
