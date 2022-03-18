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
        //해당 캐릭터를 클릭하면 대화문 실행.
    }

    void ActiveDialogue()
    {
        ChatManager.Instance.StartDialogue(dialogue);
        if (isDisable) gameObject.SetActive(false);
    }
}
