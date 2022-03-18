using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day4 : MonoBehaviour
{
    AfterDialogue afterDialogue;
    displayManager display;
    ButtonFunction button;
    Inventory inventory;

    Image fade;
    public Image day;
    public Button[] choice;

    public Dialogue[] BeforeEnding;
    //public Dialogue[] Endings;

    public EndingManager ending;

    public Animator chatAnim;
    public Text chatText;

    void Start()
    {
        afterDialogue = ChatManager.Instance.gameObject.GetComponent<AfterDialogue>();

        display = FindObjectOfType<displayManager>();
        button = FindObjectOfType<ButtonFunction>();
        inventory = FindObjectOfType<Inventory>();

        if (GameManager.Instance.Progress < 12)
            //여기서 바로 시작했을때 용
        {
            display.RoomLoad("GuestRoom");
            GameManager.Instance.SetProgress(12);
        }
        display.RoomLoad("Lobby");
        fade = button.GetFade();

        fade.gameObject.SetActive(true);
        StartCoroutine(StartDay4());
    }

    IEnumerator StartDay4()
    {
        SoundManager.Instance.PlayBGM(-1);
        button.SetAllbuttons(false);

        inventory.DeleteItem(15);

        yield return afterDialogue.StartCoroutine(afterDialogue.Fade(0.01f, day, true));

        yield return new WaitForSeconds(1f);

        afterDialogue.StartCoroutine(afterDialogue.Fade(-0.01f, fade));
        yield return afterDialogue.StartCoroutine(afterDialogue.Fade(-0.01f, day, true));

        fade.gameObject.SetActive(false);
        day.gameObject.SetActive(false);
        //Fade In / Out

        SoundManager.Instance.PlayBGM(6);
        yield return new WaitForSeconds(1f);
        
        ChatManager.Instance.StartDialogue(BeforeEnding[0]);

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);
        
        SoundManager.Instance.PlayBGM(-1);
        SoundManager.Instance.PlaySound("Day4 문쾅쾅");
        yield return new WaitForSeconds(0.7f);
        SoundManager.Instance.PlaySound("Day4 문쾅쾅");
        yield return new WaitForSeconds(0.7f);
        
        ChatManager.Instance.StartDialogue(BeforeEnding[1]);

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);


        // 노멀, 진엔딩
        if (GameManager.Instance.TakeOrder >= 2 && GameManager.Instance.Nighttime >= 2)
        {
            choice[1].gameObject.SetActive(true);
        }
        
        // 배드 엔딩
        choice[0].gameObject.SetActive(true);

        ChatManager.Instance.ChatText = chatText;
        ChatManager.Instance.Chatanimator = chatAnim;
    }

    public void OpenDoor()
    {
        choice[0].gameObject.SetActive(false);
        choice[1].gameObject.SetActive(false);

        if (GameManager.Instance.gameData.condition1 < 2)
        //노말 엔딩 1 - 문 열고 나가는 조건 만족 but Day3 조건 지키지 못함
        {
            SoundManager.Instance.PlayBGM(4);
            ending.NormalEnding1();
        }
        else if (GameManager.Instance.TakeOrder < 3 && GameManager.Instance.Nighttime < 3)
        //노말 엔딩 2 - Day3 조건 만족했으나 Day1, 2 조건 만족 X
        {
            SoundManager.Instance.PlayBGM(4);
            ending.NormalEnding2();
        }
        else
        //진엔딩 - 모든 조건 완료
        {
            SoundManager.Instance.PlayBGM(7);
            ending.TrueEnding();
        }
    }

    public void CloseDoor()
    {
        choice[0].gameObject.SetActive(false);
        choice[1].gameObject.SetActive(false);
        StartCoroutine(Close());
    }

    IEnumerator Close()
        //배드엔딩
    {
        SoundManager.Instance.PlayBGM(4);
        
        yield return afterDialogue.StartCoroutine(afterDialogue.Fade(0.004f, fade));

        yield return new WaitForSeconds(1f);

        Dialogue dia = new Dialogue() { sentences = new string[1] };
        dia.sentences[0] = "잘 생각했단다, 아가... :2";

        ChatManager.Instance.StartDialogue(dia);

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        ending.BadEnding();
    }
}