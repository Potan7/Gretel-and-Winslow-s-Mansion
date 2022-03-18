using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabPasswordPuzzle : MonoBehaviour
{
    bool[] check = new bool[6];

    public void CheckStatus(int ID, int position)
    {
        switch (ID)
        {
            case 0:
                check[ID] = position == 11 ? true : false;
                break;

            case 1:
                check[ID] = position == 0 ? true : false;
                break;

            case 2:
                check[ID] = position == 1 ? true : false;
                break;

            case 3:
                check[ID] = position == 17 ? true : false;
                break;

            case 4:
                check[ID] = position == 0 ? true : false;
                break;

            case 5:
                check[ID] = position == 19 ? true : false;
                break;
        }

        for (int i = 0; i < check.Length; i++)
        {
            if (!check[i]) return;
        }
        SoundManager.Instance.PlaySound("Quest1");
        FindObjectOfType<ButtonFunction>().DownButton();
        GameObject.Find("DoorProtection").SetActive(false);
    }
}
