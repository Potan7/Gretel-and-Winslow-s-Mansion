using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchExitButton : MonoBehaviour
    // �ֹ� �̼� �Ϸ� �� ������ ��ư
{
    GameObject text;

    private void Awake()
    {
        text = transform.GetChild(0).gameObject;
    }

    private void OnMouseEnter()
    {
        text.SetActive(true);
    }

    private void OnMouseExit()
    {
        text.SetActive(false);
    }

    private void OnMouseUp()
    {
        if (text.activeSelf == true)
            FindObjectOfType<Day2Day>().QuitButton();
    }
}
