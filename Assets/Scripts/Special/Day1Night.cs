using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day1Night : MonoBehaviour
{
    displayManager display;
    ButtonFunction button;


    public Dialogue dialogue;
    public Dialogue ChoiceDialogue;

    Image fade;
    public Image day, Nightmare;
    public Button[] choice;
    public GameObject Hall;
    public GameObject[] detects;
    /// <summary>
    /// 0 - �� ����
    /// 1 - Ȧ ���� (Ȧ ������ ���ʿ����� �ȳ�����)
    /// </summary>

    private void Start()
    {
        display = FindObjectOfType<displayManager>();
        button = FindObjectOfType<ButtonFunction>();
        
        if (GameManager.Instance.Progress != 4)
        {
            display.RoomLoad("GuestRoomNight");
            GameManager.Instance.SetProgress(4);
            transform.Find("Bush").GetComponent<Item>().GetItem();
            transform.Find("Bird").GetComponent<Item>().GetItem();
        }

        StartCoroutine("BeforeSearch");

        
        // display.beforenum = -1;
    }

    IEnumerator BeforeSearch()
    {
        // button.SetAllbuttons(false);

        SoundManager.Instance.PlayBGM(-1);
        
        yield return StartCoroutine(Fade(0.01f, false, day));
        fade = FindObjectOfType<ButtonFunction>().GetFade();

        yield return new WaitForSeconds(1f);
        
        yield return StartCoroutine(Fade(-0.01f, false, day));
        //��¥ ���̵� ���� �ڷ�ƾ

        yield return new WaitForSeconds(1f);

        day.gameObject.SetActive(false);
        //��¥ ��Ȱ��ȭ

        SoundManager.Instance.PlayBGM(0);
        
        Nightmare.gameObject.SetActive(true);

        yield return new WaitForSeconds(17f);
        SoundManager.Instance.PlaySound("������_����ȭ��", true);
        yield return new WaitForSeconds(1.5f);

        Nightmare.gameObject.SetActive(false);
       
        button.SetAllbuttons(false);
        fade.gameObject.SetActive(false);

        ChatManager.Instance.StartDialogue(dialogue);

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        ChoiceOnOff(true);
    }

    public void ChoseSleep()
    {
        ChoiceOnOff(false);
        StartCoroutine(Sleep());
        Interact.StopInteract = false;
    }

    IEnumerator Sleep()
    {
        fade.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0.01f, true, fade));
        yield return new WaitForSeconds(1f);

        GameManager.Instance.NextScene("GuestRoom");
    }

    public void ChoseSearch()
    {
        ChoiceOnOff(false);
        ChatManager.Instance.StartDialogue(ChoiceDialogue);
        display.beforeRoom = Hall;
        StartCoroutine(DetectFlower());
        Interact.StopInteract = false;
    }

    IEnumerator Detect()
    {
        while (true)
        {
            if (detects[1].activeSelf == true)
            {
                button.SetAllbuttons(false);
            }
            yield return null;
        }
    }

    IEnumerator DetectFlower()
    {
        yield return new WaitWhile(() => detects[0].activeSelf == true);

        button.SetDownButton(true);
        StartCoroutine(Detect());
    }

    void ChoiceOnOff(bool OnOff)
    {
        for (int i = 0; i < choice.Length; i++)
        {
            choice[i].gameObject.SetActive(OnOff);
        }
    }
    
    IEnumerator Fade(float plus, bool isBlack, Image image)
        //���̵� In/Out �ڷ�ƾ
        //plus�� ���� ���� In/Out ����
        //���� ����� ������, Day 1 �޼����� �Ͼ���̶� ���� ����
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
