using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public Slot[] slots;
    public int itemCount = 0;
    public int SelectedSlotId = 0;

    public void newItem(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (item.isSound) SoundManager.Instance.PlaySound("Search");
            if (slots[i].hasItem == false)
            {
                slots[i].AddItem(item);
                itemCount++;
                break;
                //아이템이 들어오면 가장먼저 나오는 빈공간에 아이템 추가.
            }
        }
    }

    public void DeleteItem(int ID)
    {
        foreach(Slot slot in slots)
        {
            if (slot.itemID == 0) continue;
            if (slot.itemID == ID)
            {
                itemCount--;
                slot.RemoveItem();
            }
        }
    }
    //들어온 ID에 해당하는 아이템 제거

    public void SelectedItem(int postion, int id)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].BackImage.color = Color.gray;
        }

        if (postion != -1)
        {
            SelectedSlotId = id;
            slots[postion].BackImage.color = Color.white;
        }
        else
        {
            SelectedSlotId = 0;
        }
        //해당하는 postion의 아이템 활성화
        //-1로 들러올경우 전부다 비활성화
    }

    public bool ItemCheck(int ID)
    {
        foreach (Slot slot in slots)
        {
            if (slot.itemID == ID)
            {
                return true;
            }
        }
        return false;
    }
}
