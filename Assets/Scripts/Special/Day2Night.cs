using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day2Night : MonoBehaviour
{
    displayManager display;
    ButtonFunction button;
    public Item key;

    public Dialogue[] dialogue;
    public Dialogue PhoneDialouge;

    Image fade;
    public Image day;
    public Button[] choice;
    public GameObject Hall, Milk, Flower;

    public bool isFirst;
    // 처음으로 선택지 스크립트에 진입했는지 확인

    public int failCount = 0;

    private void Start()
    {
        display = FindObjectOfType<displayManager>();
        button = FindObjectOfType<ButtonFunction>();

        fade = FindObjectOfType<ButtonFunction>().GetFade();

        fade.gameObject.SetActive(true);

        isFirst = true;

        if (GameManager.Instance.Progress < 7)
            //전날 아침 작업을 하지 않고 미리 작업하는거라 임시로 해둠
        {
            display.RoomLoad("GuestRoomNight");
            GameManager.Instance.SetProgress(7);
        }

        StartCoroutine("BeforeSearch");

        // display.beforenum = -1;
    }

    IEnumerator BeforeSearch()
    {
        Milk.SetActive(false);
        Flower.SetActive(false);
        // 우유 머그잔과 꽃이 대화 & 선택지에서는 선택 불가능하게 전처리

        SoundManager.Instance.PlayBGM(-1);
        yield return StartCoroutine(Fade(0.01f, false, day));

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(Fade(-0.01f, false, day));
        //날짜 페이드 띄우는 코루틴

        yield return new WaitForSeconds(1f);

        day.gameObject.SetActive(false);
        //날짜 비활성화
        
        SoundManager.Instance.PlayBGM(1);

        button.SetAllbuttons(false);
        fade.gameObject.SetActive(false);

        ChatManager.Instance.StartDialogue(dialogue[0]);

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        ChoiceOnOff(true);
    }

    public void YesMilk()
    {
        ChoiceOnOff(false);
        StartCoroutine(MilkSleep());
    }

    public void NotMilk()
    {
        ChoiceOnOff(false);
        StartCoroutine(NotSleep());
    }
    
    IEnumerator MilkSleep()
    {
        // 일단 Day1 Night과 동일한 방식으로 구성
        // 단, 우유잔을 탭하면 얼마든지 callable함
        
        // 처음에 마시지 않았다가 -> 다시 탭해서 마시는 경우의 대사 수정
        ChatManager.Instance.StartDialogue(dialogue[isFirst ? 1 : 3]);
        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);
        StartCoroutine(NextScene());
    }

    public IEnumerator NextScene()
    {
        fade.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0.01f, true, fade));
        yield return new WaitForSeconds(1f);
        button.DontDownButton = false;
        GameManager.Instance.NextProgress();
        GameManager.Instance.NextScene("GuestRoom");
    }

    IEnumerator NotSleep()
    {
        ChatManager.Instance.StartDialogue(dialogue[2]);

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        if (isFirst)
        {
            Flower.SetActive(true);
            
            // 따르릉거리는 연출 넣기 필요
            ChatManager.Instance.StartDialogue(dialogue[4]);
            yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

            key.GetItem();
            //이끼 열쇠로 교체
            
            isFirst = false;
            display.beforeRoom = Hall;
            button.DontDownButton = true;
        }

        Milk.SetActive(true);
    }
    
    void ChoiceOnOff(bool OnOff)
    {
        for (int i = 0; i < choice.Length; i++)
        {
            choice[i].gameObject.SetActive(OnOff);
        }
    }

    IEnumerator Fade(float plus, bool isBlack, Image image)
    //페이드 In/Out 코루틴
    //plus의 값에 따라 In/Out 결정
    //검은 배경은 검은색, Day 1 메세지는 하얀색이라 따로 결정
    {
        float fadeCount;
        if (plus > 0)
        {
            fadeCount = 0.0f;
            while (fadeCount < 1.0f)
            {
                fadeCount += plus;
                yield return new WaitForSeconds(0.01f);
                if (isBlack)
                    image.color = new Color(0, 0, 0, fadeCount);
                else
                    image.color = new Color(255, 255, 255, fadeCount);

            }
        }
        else
        {
            fadeCount = 1.0f;
            while (fadeCount > 0)
            {
                fadeCount += plus;
                yield return new WaitForSeconds(0.01f);
                if (isBlack)
                    image.color = new Color(0, 0, 0, fadeCount);
                else
                    image.color = new Color(255, 255, 255, fadeCount);
            }
        }
    }
}
