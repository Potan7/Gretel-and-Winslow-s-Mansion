using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1Day : MonoBehaviour
{
    ButtonFunction button;
    void Start()
    {
        button = FindObjectOfType<ButtonFunction>();

        button.GetFade().gameObject.SetActive(false);
        button.SetAllbuttons(true);

        button.GetPhone().gameObject.SetActive(false);

        FindObjectOfType<MapManager>().TeleportStop = true;
    }
}
