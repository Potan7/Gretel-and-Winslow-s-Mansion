using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day3Night : MonoBehaviour
{
    displayManager display;
    ButtonFunction button;

    public Dialogue[] dialogues;

    public Image day;
    Image fade;
    public Button[] buttons;
    public GameObject Hall;

    public bool isFirst = false, choseSleep = false;

    private void Start()
    {
        Interact.StopInteract = true;
        display = FindObjectOfType<displayManager>();
        button = FindObjectOfType<ButtonFunction>();

        if (GameManager.Instance.Progress == 0)
        {
            display.RoomLoad("GuestRoomNight");
        }
        GameManager.Instance.SetProgress(9);

        fade = button.GetFade();

        StartCoroutine(BeforeSearch());
    }

    IEnumerator BeforeSearch()
    {
        fade.gameObject.SetActive(true);
        SoundManager.Instance.PlayBGM(-1);
        button.SetAllbuttons(false);
        SoundManager.Instance.PlayBGM(2);

        yield return StartCoroutine(Fade(0.01f, false, day));

        yield return new WaitForSeconds(1f);

        StartCoroutine(Fade(-0.01f, true, fade));
        yield return StartCoroutine(Fade(-0.01f, false, day));
        //날짜 페이드 띄우는 코루틴

        day.gameObject.SetActive(false);
        fade.gameObject.SetActive(false);
        //날짜 비활성화

        yield return new WaitForSeconds(1f);

        SoundManager.Instance.PlayBGM(3);
        ChatManager.Instance.StartDialogue(dialogues[0]);

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        buttons[0].gameObject.SetActive(true);
        buttons[1].gameObject.SetActive(true);
    }

    public void ChoiceSleep()
    {
        if (!isFirst)
        {
            buttons[0].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
        }
        else
        {
            choseSleep = true;
        }
        StartCoroutine(Sleep());
    }

    IEnumerator Sleep()
    {
        ChatManager.Instance.StartDialogue(dialogues[isFirst ? 3 : 1]);
        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);
        fade.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0.01f, true, fade));
        yield return new WaitForSeconds(1f);

        GameManager.Instance.NextProgress(3);

        GameManager.Instance.NextScene("Lobby");
    }
    public void ChoiceSearch()
    {
        buttons[0].gameObject.SetActive(false);
        buttons[1].gameObject.SetActive(false);
        StartCoroutine(ChoiceSearchDialogue());
    }

    IEnumerator ChoiceSearchDialogue()
    {
        ChatManager.Instance.StartDialogue(dialogues[2]);

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);
        
        button.SetDownButton(true);
        display.beforeRoom = Hall;
        Interact.StopInteract = false;
        buttons[2].gameObject.SetActive(true);
        isFirst = true;
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
