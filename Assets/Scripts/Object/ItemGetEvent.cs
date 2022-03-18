using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetEvent : Item
{
    public GameObject EventObject;

    void Interact()
    {
        ItemEvent();
    }

    void ItemEvent()
    {
        if (itemID == 30)
        {
            EventObject.SetActive(true);
        }

        GetItem();
    }
}
