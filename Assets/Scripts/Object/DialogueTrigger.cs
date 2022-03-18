using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public bool IsInteract = true;
    public bool isDisable = false;

    void Interact()
    {
        if (IsInteract)
            ActiveDialogue();
        //�ش� ĳ���͸� Ŭ���ϸ� ��ȭ�� ����.
    }

    void ActiveDialogue()
    {
        ChatManager.Instance.StartDialogue(dialogue);
        if (isDisable) gameObject.SetActive(false);
    }
}
