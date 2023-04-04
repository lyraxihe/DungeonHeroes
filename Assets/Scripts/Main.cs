using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject CombatBackground;

    // Start is called before the first frame update
    void Start()
    {
        GameObject clon = Instantiate(CombatBackground); // Crea el fondo que tendrá el Combate
        clon.transform.position = new Vector3(0, 0, 0); // Coloca el fondo en (0, 0, 0)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
