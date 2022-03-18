using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class BlueBird : MonoBehaviour
{
    public AudioClip rustling;
    private displayManager display;

    public Dialogue dialogue;
    public Dialogue dialogue2;

    public GameObject cutscene_bluebird;
    public GameObject cutscene_toyphone;

    public Item blue_bird, toy_phone;
    private Inventory inven;

    public string inven_full;
    
    public ChatManager chatManager;

    void Start()
    {
        cutscene_bluebird.SetActive(false);
        cutscene_toyphone.SetActive(false);
        
        display = FindObjectOfType<displayManager>();
        chatManager = FindObjectOfType<ChatManager>();
        inven = GameObject.Find("InventoryManager").GetComponent<Inventory>();
    }

    void Interact()
    {
        StartCoroutine("BirdDialogue");
    }

    IEnumerator BirdDialogue()
    {
        cutscene_bluebird.SetActive(true);
        chatManager.StartDialogue(dialogue);
        
        yield return new WaitWhile(() => chatManager.chatState == ChatManager.ChatState.Chat);

        // yield return new WaitForSeconds(1.0f);
        SoundManager.Instance.PlaySound("Search"); // 후에 부스럭 거리는 소리로 변경
        
        cutscene_bluebird.SetActive(false);
        cutscene_toyphone.SetActive(true);
        chatManager.StartDialogue(dialogue2);
        
        yield return new WaitWhile(() => chatManager.chatState == ChatManager.ChatState.Chat);
        
        cutscene_bluebird.SetActive(false);
        cutscene_toyphone.SetActive(false);
        
        GetItems();
    }

    void GetItems()
    {
        if (inven.itemCount < inven.slots.Length) // 아이템 2개
        {
            this.gameObject.SetActive(false);
            inven.newItem(blue_bird);
            //inven.newItem(toy_phone);
            FindObjectOfType<ButtonFunction>().buttons[3].gameObject.SetActive(true);
        }

        else
        {
            GameObject.Find("ChatManger").GetComponent<ChatManager>().SearchChat(inven_full);
        }
    }
}
