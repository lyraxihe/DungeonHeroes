using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    public GameObject UI;

    public void OnMouseEnter()
    {
        UI.SetActive(true);
    }

    public void OnMouseExit()
    {
        UI.SetActive(false);
    }
}
