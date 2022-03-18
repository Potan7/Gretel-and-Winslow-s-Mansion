using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public int[] checkItemID;
    public bool isdisable = true;

    public int work = 0;

    ItemManager itemManager;

    private void Start()
    {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }

    void ItemUse(int ID)
    {
        for (int i = 0; i < checkItemID.Length; i++)
            if (checkItemID[i] == ID)
            {
                if (i == work)
                {
                    itemManager.itemUsed(checkItemID[work], this);
                    //한 오브젝트에 여러가지 아이템을 쓰는 경우
                    //배열을 통해 여러가지 아이템 ID를 확인할 수 있음.
                    //work를 통해 순서대로 사용
                    if (isdisable)
                        this.gameObject.SetActive(false);
                }
                else
                {
                    itemManager.WrongItem(checkItemID[i], this);
                    //해당 아이템을 사용하는 것은 맞으나 순서가 틀릴 경우 다른 함수 실행.
                }
            }
    }
}
