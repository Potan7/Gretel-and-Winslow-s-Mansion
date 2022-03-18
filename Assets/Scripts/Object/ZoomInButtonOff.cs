using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomInButtonOff : MonoBehaviour
{
    public Button button;
    private displayManager display;
    public Day3Night day3Night;

    void Start()
    {
        display = FindObjectOfType<displayManager>();
    }
    
    void Update()
    {
        if (day3Night.isFirst)
        {
            button.gameObject.SetActive(display.state == displayManager.State.normal
                                        && !day3Night.choseSleep);
        }
    }
}
