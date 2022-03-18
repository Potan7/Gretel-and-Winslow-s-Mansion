using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public Dialogue dialogue;

    void Interact()
    {
        switch (GameManager.Instance.Progress)
        {
            case 0:
                ChatManager.Instance.SearchChat("����� ��ȭ���");
                break;

            case 1:
                SoundManager.Instance.PlayRepeatSound();
                SoundManager.Instance.PlaySound("Phone_Open");
                ChatManager.Instance.StartDialogue(dialogue);
                GetComponent<BoxCollider2D>().enabled = false;
                GameObject.Find("Lobby").transform.GetChild(2).transform.Find("DoorPhone").gameObject.SetActive(false);
                //�ι�° Ŭ���� ��ȭ ����
                break;
        }
    }
}
