using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchInfo : MonoBehaviour
{
    public string Message;
    public string Message2;
    //��� �޼���

    void Interact()
    {
        ChatManager.Instance.SearchChat(Message, Message2);
        //������� Ȯ�� �޼��� ����
    }
}
