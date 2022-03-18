using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Inventory inventory;

    public Image BackImage;
    public Image image;
    public bool hasItem = false;
    public int itemID = 0;
    public int Position;
    public Item.ItemType ItemType;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void AddItem(Item item)
    {
        image.color = new Color(255, 255, 255, 1f);
        //슬롯의 하얀 바탕을 제거하기 위해 평소에는 투명하게 했다가
        //아이템이 들어오면 투명도 제거.
        image.sprite = item.itemImage;
        itemID = item.itemID;
        ItemType = item.itemtype;
        hasItem = true;
        //아이템 저장.
    }

    public void RemoveItem()
    {
        image.color = new Color(255, 255, 255, 0);
        itemID = 0;
        image.sprite = null;
        hasItem = false;
        //아이템 제거.
    }

    public void ItemSelect()
    {
        if (itemID == 0) return;
        else if (ItemType == Item.ItemType.Useable)
        {
            inventory.SelectedItem(Position, itemID);
        }
        //아이템 선택
    }
}
