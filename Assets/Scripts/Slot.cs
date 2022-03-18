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
        //������ �Ͼ� ������ �����ϱ� ���� ��ҿ��� �����ϰ� �ߴٰ�
        //�������� ������ ���� ����.
        image.sprite = item.itemImage;
        itemID = item.itemID;
        ItemType = item.itemtype;
        hasItem = true;
        //������ ����.
    }

    public void RemoveItem()
    {
        image.color = new Color(255, 255, 255, 0);
        itemID = 0;
        image.sprite = null;
        hasItem = false;
        //������ ����.
    }

    public void ItemSelect()
    {
        if (itemID == 0) return;
        else if (ItemType == Item.ItemType.Useable)
        {
            inventory.SelectedItem(Position, itemID);
        }
        //������ ����
    }
}
