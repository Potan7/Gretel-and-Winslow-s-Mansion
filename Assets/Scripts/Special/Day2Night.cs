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
    // ó������ ������ ��ũ��Ʈ�� �����ߴ��� Ȯ��

    public int failCount = 0;

    private void Start()
    {
        display = FindObjectOfType<displayManager>();
        button = FindObjectOfType<ButtonFunction>();

        fade = FindObjectOfType<ButtonFunction>().GetFade();

        fade.gameObject.SetActive(true);

        isFirst = true;

        if (GameManager.Instance.Progress < 7)
            //���� ��ħ �۾��� ���� �ʰ� �̸� �۾��ϴ°Ŷ� �ӽ÷� �ص�
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
        // ���� �ӱ��ܰ� ���� ��ȭ & ������������ ���� �Ұ����ϰ� ��ó��

        SoundManager.Instance.PlayBGM(-1);
        yield return StartCoroutine(Fade(0.01f, false, day));

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(Fade(-0.01f, false, day));
        //��¥ ���̵� ���� �ڷ�ƾ

        yield return new WaitForSeconds(1f);

        day.gameObject.SetActive(false);
        //��¥ ��Ȱ��ȭ
        
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
        // �ϴ� Day1 Night�� ������ ������� ����
        // ��, �������� ���ϸ� �󸶵��� callable��
        
        // ó���� ������ �ʾҴٰ� -> �ٽ� ���ؼ� ���ô� ����� ��� ����
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
            
            // �������Ÿ��� ���� �ֱ� �ʿ�
            ChatManager.Instance.StartDialogue(dialogue[4]);
            yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

            key.GetItem();
            //�̳� ����� ��ü
            
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
