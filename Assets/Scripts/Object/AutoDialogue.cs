using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    GameObject parent;

    private void Start()
    {
        parent = transform.parent.gameObject;
    }
    private void Update()
    {
        if (parent.activeSelf == true)
        {
            ChatManager.Instance.StartDialogue(dialogue);
            this.gameObject.SetActive(false);
        }
    }
}
