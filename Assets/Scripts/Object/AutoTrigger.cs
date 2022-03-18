using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTrigger : MonoBehaviour
{
    GameObject parent;

    public int ID;

    private void Start()
    {
        parent = transform.parent.gameObject;
    }
    private void Update()
    {
        if (parent.activeSelf == true)
        {
            switch (ID)
            {
                case 1:
                    ButtonFunction button = FindObjectOfType<ButtonFunction>();
                    button.DontDownButton = true;
                    button.SetAllbuttons(false);
                    break;
            }
            this.gameObject.SetActive(false);
        }
    }
}
