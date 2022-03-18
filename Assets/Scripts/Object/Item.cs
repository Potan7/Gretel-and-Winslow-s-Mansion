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
    //아이템 사용에 쓰일 ID
    public enum ItemType
    {
        Useable,
        KeyItem
    }
    //아이템의 유형
    public ItemType itemtype;

    private void Awake()
    {
        inven = FindObjectOfType<Inventory>();
    }

    void Interact()
    {
        if (ItemEnable)
            GetItem();
        //ItemEnable이 꺼져있으면 주울수없음.
        //불의 열매같은 아이템을 써야 얻을 수 있는 아이템용
    }

    public void GetItem()
    {
        if (inven.itemCount < inven.slots.Length)
        {
            if (Name != "")
            {
                ChatManager.Instance.SearchChat(Name + " 을(를) 얻었다.");
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
        //클릭시 해당 오브젝트를 비활성화하고 인벤토리에 자신을 추가함.
        //인벤이 꽉찰경우 작동X
    }
}
