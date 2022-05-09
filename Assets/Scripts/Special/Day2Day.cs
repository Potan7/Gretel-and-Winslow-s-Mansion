using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day2Day : MonoBehaviour
{
    AfterDialogue afterDialogue;
    displayManager display;
    ButtonFunction button;
    Inventory inventory;

    public Dialogue[] dialogues;
    public Dialogue[] dialogues2;
    
    Image fade;
    public Image day;

    public GameObject beforeRoom;
    public GameObject CheckToDoList;

    public Text[] buttonText;

    public bool isPoision = false;
    
    private void Start()
    {
        afterDialogue = ChatManager.Instance.gameObject.GetComponent<AfterDialogue>(); 

        display = FindObjectOfType<displayManager>();
        button = FindObjectOfType<ButtonFunction>();
        inventory = FindObjectOfType<Inventory>();

        display.beforeRoom = beforeRoom;
        fade = button.GetFade();

        FindObjectOfType<MapManager>().TeleportStop = true;

        StartCoroutine(StartDay2());
        fade.gameObject.SetActive(true);
    }

    IEnumerator StartDay2()
    {
        SoundManager.Instance.PlayBGM(-1);
        inventory.DeleteItem(15);
        // 아침이 되었으니 꽃 삭제

        button.DontDownButton = true;
        
        yield return afterDialogue.StartCoroutine(afterDialogue.Fade(0.01f, day, true));

        if (GameManager.Instance.Progress < 4)
        //여기서 바로 시작했을때 용
        {
            display.RoomLoad("GuestRoom");
            GameManager.Instance.SetProgress(4);
            transform.Find("Bush").GetComponent<Item>().GetItem();
            transform.Find("Bird").GetComponent<Item>().GetItem();
        }

        button.SetAllbuttons(true);

        button.buttons[2].gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        afterDialogue.StartCoroutine(afterDialogue.Fade(-0.01f, fade));
        yield return afterDialogue.StartCoroutine(afterDialogue.Fade(-0.01f, day, true));

        fade.gameObject.SetActive(false);
        day.gameObject.SetActive(false);
        //Fade In / Out

        yield return new WaitForSeconds(1f);
        
        SoundManager.Instance.PlayBGM(6);

        bool isNight = GameManager.Instance.Nighttime == 0;

        if (isNight)
        {
            ChatManager.Instance.StartDialogue(dialogues[0]);
        }
        else
        {
            ChatManager.Instance.StartDialogue(dialogues[1]);
        }
        //전날 밤 행동에 따라 대화창이 나뉨

        yield return new WaitWhile(() => CheckToDoList.activeSelf == false);
        yield return new WaitWhile(() => CheckToDoList.activeSelf == true);
        //할일 목록 체크

        yield return new WaitForSeconds(1f);

        if (isNight)
        {
            ChatManager.Instance.StartDialogue(dialogues[2]);
        }
        else
        {
            ChatManager.Instance.StartDialogue(dialogues[3]);
        }
        //전날 밤 행동에 따라 대화창이 나뉨
        
        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        inventory.DeleteItem(11);
        inventory.DeleteItem(10);
        fade.gameObject.SetActive(true);
        yield return afterDialogue.StartCoroutine(afterDialogue.Fade(0.01f, fade));

        display.RoomLoad("Kitchen");

        button.SetEachButtons(false, false, false);

        yield return new WaitForSeconds(0.5f);
        yield return afterDialogue.StartCoroutine(afterDialogue.Fade(-0.008f, fade));
        GameManager.Instance.NextProgress();

        StartCoroutine(WaitingClear());
    }

    IEnumerator WaitingClear()
    {
        yield return new WaitWhile(() => GameManager.Instance.Progress < 7);
        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);
        yield return new WaitForSeconds(1f);

        FindObjectOfType<PhoneManager>().Ring();

        FindObjectOfType<TodoManager>().ToDoCheck(0);

        Dialogue dia = new Dialogue();
        dia.sentences = new string[1];
        dia.sentences[0] = "어라 종이컵 전화기가 울리고 있어 :0";
        ChatManager.Instance.StartDialogue(dia);

        Interact.StopInteract = true;
    }

    public void SetButton(bool OnOff)
    {
        buttonText[0].transform.parent.gameObject.SetActive(OnOff);
        buttonText[1].transform.parent.gameObject.SetActive(OnOff);
        Interact.StopInteract = OnOff;
    }

    public void Choice1()
    {
        if (GameManager.Instance.Progress < 7)
        {
            ChatManager.Instance.StartDialogue(dialogues2[0]);
            SetButton(false);
            GameManager.Instance.NextProgress();
            buttonText[0].text = "길버트를 따라간다.";
            buttonText[1].text = "전화의 지시를 따른다.";
        }
        else if (GameManager.Instance.Progress == 7)
        {
            ChatManager.Instance.StartDialogue(dialogues2[2]);
            SetButton(false);
        }
    }

    public void Choice2()
    {
        if (GameManager.Instance.Progress < 7)
        {
            ChatManager.Instance.StartDialogue(dialogues2[1]);
            SetButton(false);
            GameManager.Instance.NextProgress();
            buttonText[0].text = "길버트를 따라간다.";
            buttonText[1].text = "전화의 지시를 따른다.";
            GameManager.Instance.TakeOrder++;
            isPoision = true;
        }
        else if (GameManager.Instance.Progress == 7)
        {
            ChatManager.Instance.StartDialogue(dialogues2[3]);
            SetButton(false);
        }
    }

    public void QuitButton()
    {
        ChatManager.Instance.StartDialogue(dialogues2[4]);
    }
}
