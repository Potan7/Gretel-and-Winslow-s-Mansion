using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottlePuzzle : MonoBehaviour
{
    int labelCount = 0;
    bool[] check = new bool[4];

    public BottlePuzzleNameTag[] bottles;
    public BottlePuzzleNameTag[] tags;
    public GameObject[] Bottles;

    Inventory inven;

    private void Start()
    {
        inven = FindObjectOfType<Inventory>();
    }

    public void Addlabel(int Postion, bool Check)
    {
        labelCount++;
        check[Postion] = Check;

        if (labelCount == 4)
        {
            for (int i = 0; i < check.Length; i++)
            {
                if (!check[i])
                {
                    Invoke("LabelFail", 0.3f);
                    return;
                }
            }

            if (inven.itemCount == inven.slots.Length)
            {
                ChatManager.Instance.SearchChat("인벤토리에 빈 공간이 없다.");
                Invoke("LabelFail", 0.3f);
                return;
            }

            Complete();
        }
    }

    void Complete()
    {
        for (int i = 0; i < Bottles.Length; i++)
        {
            Bottles[i].SetActive(false);
        }
        gameObject.SendMessage("GetItem");
        SoundManager.Instance.PlaySound();
        SoundManager.Instance.PlaySound("Quest1");
        transform.parent.parent.gameObject.SetActive(false);
        FindObjectOfType<ButtonFunction>().DownButton();
        gameObject.SetActive(false);
    }

    void LabelFail()
    {
        for (int i = 0; i < bottles.Length; i++)
        {
            bottles[i].image.color = new Color(255, 255, 255, 0);
        }
        for (int i = 0; i < tags.Length; i++)
        {
            tags[i].gameObject.SetActive(true);
        }
        labelCount = 0;
    }
}
