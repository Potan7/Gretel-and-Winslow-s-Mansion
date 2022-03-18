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
                //�������� ������ ������� ������ ������� ������ �߰�.
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
    //���� ID�� �ش��ϴ� ������ ����

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
        //�ش��ϴ� postion�� ������ Ȱ��ȭ
        //-1�� �鷯�ð�� ���δ� ��Ȱ��ȭ
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
