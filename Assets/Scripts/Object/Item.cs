using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite itemImage;
    Inventory inven;

    public bool ItemEnable = true;
    public bool PowerOff = false;
    public bool isSound = true;
    public string Name = "";

    public int itemID;
    //������ ��뿡 ���� ID
    public enum ItemType
    {
        Useable,
        KeyItem
    }
    //�������� ����
    public ItemType itemtype;

    private void Awake()
    {
        inven = FindObjectOfType<Inventory>();
    }

    void Interact()
    {
        if (ItemEnable)
            GetItem();
        //ItemEnable�� ���������� �ֿ������.
        //���� ���Ű��� �������� ��� ���� �� �ִ� �����ۿ�
    }

    public void GetItem()
    {
        if (inven.itemCount < inven.slots.Length)
        {
            if (Name != "")
            {
                ChatManager.Instance.SearchChat(Name + " ��(��) �����.");
            }

            this.gameObject.SetActive(false);
            if (PowerOff)
            {
                ItemEnable = false;
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            inven.newItem(this);
        }
        //Ŭ���� �ش� ������Ʈ�� ��Ȱ��ȭ�ϰ� �κ��丮�� �ڽ��� �߰���.
        //�κ��� ������� �۵�X
    }
}
