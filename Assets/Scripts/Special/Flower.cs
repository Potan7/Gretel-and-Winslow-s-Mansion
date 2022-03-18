using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    ButtonFunction button;
    
    void Start()
    {
        button = FindObjectOfType<ButtonFunction>();
    }

    void Interact()
    {
        button.SetDownButton(true);
    }
}
