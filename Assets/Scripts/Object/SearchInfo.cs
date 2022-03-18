using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchInfo : MonoBehaviour
{
    public string Message;
    public string Message2;
    //띄울 메세지

    void Interact()
    {
        ChatManager.Instance.SearchChat(Message, Message2);
        //누를경우 확인 메세지 띄우기
    }
}
