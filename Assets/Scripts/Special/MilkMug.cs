using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MilkMug : MonoBehaviour
{
    public Button[] choice;
    public GameObject Flower;
    
    ButtonFunction button;

    void Start()
    {
        button = FindObjectOfType<ButtonFunction>();
    }

    void Interact()
    {
        // button.SetAllbuttons(false);
        
        ChoiceOnOff(true);
    }
    
    void ChoiceOnOff(bool OnOff)
    {
        for (int i = 0; i < choice.Length; i++)
        {
            choice[i].gameObject.SetActive(OnOff);
        }
    }
}
