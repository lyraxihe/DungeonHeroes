using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleButtonAction : MonoBehaviour
{
    public bool[] AliadosPositionStatus; // Array de booleanos para comprobar que todos los personajes del Jugador están colocados en el mapa de combate
    public bool Activated = false;       // Booleano que controla si el botón ha sido activado cuando todos los personajes del Jugador estaban colocados en el mapa de combate
    public GameObject[] Enemies;

    //UI ENEMIGOS
    public GameObject UIEnemigo1;
    public GameObject UIEnemigo2;
    public GameObject UIEnemigo3;
    public GameObject UIEnemigo4;
    public int NumEnemigos;

    public GameObject BorderUIEnemigo;
    public GameObject ContainerUIEnemigo;
    public GameObject UIEnemigoKnight;
    public GameObject UIEnemigoHealer;
    public GameObject UIEnemigoSlime;
    public GameObject UIEnemigoMage;
    public GameObject UIEnemigoBoss;
    public GameObject UIEnemigoCorazon;
    public GameObject UIEnemigoEspada;
    public GameObject UIEnemigoEscudo;

    public GameObject UIEstadisticaVidaEnemy1;
    public GameObject UIEstadisticaAtaqueEnemy1;
    public GameObject UIEstadisticaDefensaEnemy1;
    public GameObject UIEstadisticaVidaEnemy2;
    public GameObject UIEstadisticaAtaqueEnemy2;
    public GameObject UIEstadisticaDefensaEnemy2;
    public GameObject UIEstadisticaVidaEnemy3;
    public GameObject UIEstadisticaAtaqueEnemy3;
    public GameObject UIEstadisticaDefensaEnemy3;
    public GameObject UIEstadisticaVidaEnemy4;
    public GameObject UIEstadisticaAtaqueEnemy4;
    public GameObject UIEstadisticaDefensaEnemy4;

    /****************************************************************************************
     * Función: OnClicked                                                                   *
     * Uso: "Activa" el botón si este es clicado                                            *
     * Variables entrada: Ninguno                                                           *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void OnClicked()
    {
        if (CheckAliadosPosition()) // Si al hacer click todos los personajes del Jugador están colocados en el mapa de combate
        {
            Activated = true;       // Activa el botón
            for(int i = 0; i < NumEnemigos; i++)
            {
                if(i == 0)
                    UIEnemigo1.SetActive(true);
                else if (i == 1)
                    UIEnemigo2.SetActive(true);
                else if (i == 2)
                    UIEnemigo3.SetActive(true);
                else
                    UIEnemigo4.SetActive(true);
            }

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    GameObject clon = Instantiate(BorderUIEnemigo);
                    clon.transform.position = new Vector3(10, 1.5f, 2);

                    clon = Instantiate(ContainerUIEnemigo);
                    clon.transform.position = new Vector3(10, 1.5f, 1);
                }
                else if (i == 1)
                {
                    GameObject clon = Instantiate(BorderUIEnemigo);
                    clon.transform.position = new Vector3(10, -0.5f, 2);

                    clon = Instantiate(ContainerUIEnemigo);
                    clon.transform.position = new Vector3(10, -0.5f, 1);
                }
                else if (i == 2)
                {
                    GameObject clon = Instantiate(BorderUIEnemigo);
                    clon.transform.position = new Vector3(10, -2.5f, 2);

                    clon = Instantiate(ContainerUIEnemigo);
                    clon.transform.position = new Vector3(10, -2.5f, 1);
                }
                else
                {
                    GameObject clon = Instantiate(BorderUIEnemigo);
                    clon.transform.position = new Vector3(10, -4.5f, 2);

                    clon = Instantiate(ContainerUIEnemigo);
                    clon.transform.position = new Vector3(10, -4.5f, 1);
                }
            }

            for (int i = 0; i < Enemies.Length; i++)
            {
                if (i == 0)
                {
                    if (!VariablesGlobales.instance.Boss)
                    {
                        if (Enemies[i].GetComponent<GeneralEnemy>().Index == 1)
                        {
                            GameObject clon = Instantiate(UIEnemigoKnight);
                            clon.transform.position = new Vector3(9, 1.5f, 0);
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                            clon = Instantiate(UIEnemigoCorazon);
                            clon.transform.position = new Vector3(10, 2, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                            clon = Instantiate(UIEnemigoEspada);
                            clon.transform.position = new Vector3(10, 1.5f, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                            clon = Instantiate(UIEnemigoEscudo);
                            clon.transform.position = new Vector3(10, 1, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                        }
                        else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 2)
                        {
                            GameObject clon = Instantiate(UIEnemigoHealer);
                            clon.transform.position = new Vector3(9, 1.5f, 0);
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                            clon = Instantiate(UIEnemigoCorazon);
                            clon.transform.position = new Vector3(10, 2, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                            clon = Instantiate(UIEnemigoEspada);
                            clon.transform.position = new Vector3(10, 1.5f, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                            clon = Instantiate(UIEnemigoEscudo);
                            clon.transform.position = new Vector3(10, 1, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                        }
                        else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 3)
                        {
                            GameObject clon = Instantiate(UIEnemigoSlime);
                            clon.transform.position = new Vector3(9, 1.5f, 0);
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                            clon = Instantiate(UIEnemigoCorazon);
                            clon.transform.position = new Vector3(10, 2, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                            clon = Instantiate(UIEnemigoEspada);
                            clon.transform.position = new Vector3(10, 1.5f, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                            clon = Instantiate(UIEnemigoEscudo);
                            clon.transform.position = new Vector3(10, 1, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                        }
                        else
                        {
                            GameObject clon = Instantiate(UIEnemigoMage);
                            clon.transform.position = new Vector3(9, 1.5f, 0);
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                            clon = Instantiate(UIEnemigoCorazon);
                            clon.transform.position = new Vector3(10, 2, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                            clon = Instantiate(UIEnemigoEspada);
                            clon.transform.position = new Vector3(10, 1.5f, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                            clon = Instantiate(UIEnemigoEscudo);
                            clon.transform.position = new Vector3(10, 1, 0);
                            clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy1;
                            Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                        }
                    }
                    else
                    {
                        GameObject clon = Instantiate(UIEnemigoBoss);
                        clon.transform.position = new Vector3(9, 1.5f, 0);
                        Enemies[i].GetComponent<Boss>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, 2, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy1;
                        Enemies[i].GetComponent<Boss>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, 1.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy1;
                        Enemies[i].GetComponent<Boss>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, 1, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy1;
                        Enemies[i].GetComponent<Boss>().UIEnemigoEscudo = clon;
                    }
                }
                else if (i == 1)
                {
                    if (Enemies[i].GetComponent<GeneralEnemy>().Index == 1)
                    {
                        GameObject clon = Instantiate(UIEnemigoKnight);
                        clon.transform.position = new Vector3(9, -0.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, 0, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -0.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -1, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                    else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 2)
                    {
                        GameObject clon = Instantiate(UIEnemigoHealer);
                        clon.transform.position = new Vector3(9, -0.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, 0, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -0.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -1, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                    else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 3)
                    {
                        GameObject clon = Instantiate(UIEnemigoSlime);
                        clon.transform.position = new Vector3(9, -0.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, 0, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -0.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -1, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                    else
                    {
                        GameObject clon = Instantiate(UIEnemigoMage);
                        clon.transform.position = new Vector3(9, -0.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, 0, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -0.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -1, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy2;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                }
                else if (i == 2)
                {
                    if (Enemies[i].GetComponent<GeneralEnemy>().Index == 1)
                    {
                        GameObject clon = Instantiate(UIEnemigoKnight);
                        clon.transform.position = new Vector3(9, -2.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, -2, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -2.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -3, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                    else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 2)
                    {
                        GameObject clon = Instantiate(UIEnemigoHealer);
                        clon.transform.position = new Vector3(9, -2.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, -2, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -2.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -3, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                    else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 3)
                    {
                        GameObject clon = Instantiate(UIEnemigoSlime);
                        clon.transform.position = new Vector3(9, -2.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, -2, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -2.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -3, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                    else
                    {
                        GameObject clon = Instantiate(UIEnemigoMage);
                        clon.transform.position = new Vector3(9, -2.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, -2, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -2.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -3, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy3;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                }
                else
                {
                    if (Enemies[i].GetComponent<GeneralEnemy>().Index == 1)
                    {
                        GameObject clon = Instantiate(UIEnemigoKnight);
                        clon.transform.position = new Vector3(9, -4.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, -4, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -4.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -5, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                    else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 2)
                    {
                        GameObject clon = Instantiate(UIEnemigoHealer);
                        clon.transform.position = new Vector3(9, -4.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, -4, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -4.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -5, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                    else if (Enemies[i].GetComponent<GeneralEnemy>().Index == 3)
                    {
                        GameObject clon = Instantiate(UIEnemigoSlime);
                        clon.transform.position = new Vector3(9, -4.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, -4, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -4.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -5, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                    else
                    {
                        GameObject clon = Instantiate(UIEnemigoMage);
                        clon.transform.position = new Vector3(9, -4.5f, 0);
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoImagen = clon;

                        clon = Instantiate(UIEnemigoCorazon);
                        clon.transform.position = new Vector3(10, -4, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaVidaEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoCorazon = clon;

                        clon = Instantiate(UIEnemigoEspada);
                        clon.transform.position = new Vector3(10, -4.5f, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaAtaqueEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEspada = clon;

                        clon = Instantiate(UIEnemigoEscudo);
                        clon.transform.position = new Vector3(10, -5, 0);
                        clon.GetComponent<EnemyUI>().UI = UIEstadisticaDefensaEnemy4;
                        Enemies[i].GetComponent<GeneralEnemy>().UIEnemigoEscudo = clon;
                    }
                }
            }
        }
        else                        // Si al hacer click no todos los personajes del Jugador están colocados en el mapa de combate
            Activated = false;      // Sigue sin activar el botón, sin esto si diésemos al botón sin estar todos colocados no se activaría,
                                    // pero al colocar al último de los personajes se activaría sólo sin tener que volver a dar a l botón
    }

    /****************************************************************************************
     * Función: SetAliadosPositionStatus                                                    *
     * Uso: Obtiene el array de booleanos que indica si todos los personajes del Jugador    *
     *      están colocados en el mapa de combate                                           *
     * Variables entrada: aliadoPositionStatus - Array de booleanos que indica si todos los *
     *                                           personajes del Jugador están colocados en  *
     *                                           el mapa de batalla                         *
     * Return: Nada                                                                         *
     ****************************************************************************************/
    public void SetAliadosPositionStatus(bool[] aliadoPositionStatus)
    {
        AliadosPositionStatus = aliadoPositionStatus; // Guarda en la variable local el array de booleanos
    }

    /****************************************************************************************
     * Función: CheckAliadosPosition                                                        *
     * Uso: Comprueba que todos los personajes del Jugador están colocados en el mapa de    *
     *      combate                                                                         *
     * Variables entrada: Nada                                                              *
     * Return: True - Si todos los personajes están colocados en el mapa de combate         *
     *         False - Si no todos los personajes están colocados en el mapa de combate     *
     ****************************************************************************************/
    private bool CheckAliadosPosition()
    {
        int contador = 0;                                      // Inicializa el contador a 0

        for (int i = 0; i < AliadosPositionStatus.Length; i++) // Recorre el array de booleanos
        {
            if (AliadosPositionStatus[i] == true)              // Si el personaje del Jugador está colocado en el mapa de combate
                contador++;                                    // +1 al contador
        }

        if (contador == VariablesGlobales.instance.NumPersonajes) // Si los 4 personajes están colocados en el mapa de combate
            return true;                                          // Devuelve true
        else                                                      // Si no están los 4 colocados
            return false;                                         // Devuelve false
    }
}
