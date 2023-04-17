using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class MainCombat : MonoBehaviour
{
    public GameObject _Control;
    
    public GameObject CombatBackground; // Prefab del Combate

    public TMP_Text PausaText;
    public Button ContinuarButton;
    public TMP_Text ContinuarButtonText;
    public Button SalirButton;
    public TMP_Text SalirButtonText;

    // Start is called before the first frame update
    void Start()
    {
        GameObject clon = Instantiate(CombatBackground);                // Crea el fondo que tendrá el Combate
        clon.transform.position = new Vector3(0, 0, 3);                 // Coloca el fondo en (0, 0, 0)
        clon.GetComponent<CombatBackground>()._CombatBackground = clon; // Almacena el combate
        clon.GetComponent<CombatBackground>()._Control = _Control;
        clon.GetComponent<CombatBackground>().PausaText = PausaText;
        clon.GetComponent<CombatBackground>().ContinuarButton = ContinuarButton;
        clon.GetComponent<CombatBackground>().ContinuarButtonText = ContinuarButtonText;
        clon.GetComponent<CombatBackground>().SalirButton = SalirButton;
        clon.GetComponent<CombatBackground>().SalirButtonText = SalirButtonText;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
