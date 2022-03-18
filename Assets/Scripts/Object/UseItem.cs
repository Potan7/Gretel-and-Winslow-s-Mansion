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
                    //�� ������Ʈ�� �������� �������� ���� ���
                    //�迭�� ���� �������� ������ ID�� Ȯ���� �� ����.
                    //work�� ���� ������� ���
                    if (isdisable)
                        this.gameObject.SetActive(false);
                }
                else
                {
                    itemManager.WrongItem(checkItemID[i], this);
                    //�ش� �������� ����ϴ� ���� ������ ������ Ʋ�� ��� �ٸ� �Լ� ����.
                }
            }
    }
}
