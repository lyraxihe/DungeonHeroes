using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private static Control instance = null;
    public static Control Instance { get { return instance; } } //creo el Singletons

    public int Money;
    public int HpKnight;

    private void Awake()
    {
        if (Instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad( this.gameObject );
    }

    public void Update()
    {
        // SALIR DEL JUEGO
        /***********************************/
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        /***********************************/
    }
}
