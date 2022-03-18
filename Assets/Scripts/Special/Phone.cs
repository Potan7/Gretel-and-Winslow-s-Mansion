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
                ChatManager.Instance.SearchChat("평범한 전화기다");
                break;

            case 1:
                SoundManager.Instance.PlayRepeatSound();
                SoundManager.Instance.PlaySound("Phone_Open");
                ChatManager.Instance.StartDialogue(dialogue);
                GetComponent<BoxCollider2D>().enabled = false;
                GameObject.Find("Lobby").transform.GetChild(2).transform.Find("DoorPhone").gameObject.SetActive(false);
                //두번째 클릭시 대화 진행
                break;
        }
    }
}
