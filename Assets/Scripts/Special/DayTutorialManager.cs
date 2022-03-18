using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTutorialManager : MonoBehaviour
{
    ChatManager chatManager;
    ButtonFunction button;
    MapManager map;
    displayManager display;

    public BoxCollider2D table;

    public GameObject[] TutorialObject;
    /// ���
    /// 0 - todo
    /// 1 - map
    /// 2 - fade
    /// 3 - day
    /// 4 - table
    /// </summary>
    /// 
    public GameObject[] QuestObject;
    /// <summary>
    /// ���
    /// 0 - TodoList
    /// 1 - Map
    /// 2 - changeview
    /// </summary>

    public Dialogue[] dialogues;

    private void Start()
    {
        StartCoroutine("Tutorial");
        button = FindObjectOfType<ButtonFunction>();
        map = FindObjectOfType<MapManager>();
        display = FindObjectOfType<displayManager>();
        chatManager = ChatManager.Instance;

        TutorialObject[2] = button.GetFade().gameObject;
        QuestObject[1] = button.transform.Find("Map").gameObject;

        map.TeleportStop = true;
        button.DontDownButton = true;
    }

    IEnumerator Tutorial()
    {
        yield return StartCoroutine(Fade(0.01f, false, TutorialObject[3]));

        yield return new WaitForSeconds(1f);
        

        StartCoroutine(Fade(-0.01f, false, TutorialObject[3]));
        yield return StartCoroutine(Fade(-0.01f, true, TutorialObject[2]));
        //Day1 ���̵� ���� �ڷ�ƾ

        SoundManager.Instance.PlayBGM(6);
        yield return new WaitForSeconds(1f);

        TutorialObject[2].SetActive(false);
        TutorialObject[3].SetActive(false);
        //���̵� ��Ȱ��ȭ

        chatManager.StartDialogue(dialogues[0]);

        yield return new WaitWhile(() => chatManager.chatState == ChatManager.ChatState.Chat);

        TutorialObject[0].SetActive(true);
        //1�� Ʃ�丮�� (ToDo)

        yield return new WaitWhile(() => QuestObject[0].activeSelf == false);
        TutorialObject[0].SetActive(false);
        yield return new WaitWhile(() => QuestObject[0].activeSelf == true);

        chatManager.StartDialogue(dialogues[1]);

        yield return new WaitWhile(() => chatManager.chatState == ChatManager.ChatState.Chat);

        TutorialObject[1].SetActive(true);
        //2�� Ʃ�丮�� (Map)

        yield return new WaitWhile(() => QuestObject[1].activeSelf == false);
        TutorialObject[1].SetActive(false);
        yield return new WaitWhile(() => QuestObject[1].activeSelf == true);

        chatManager.StartDialogue(dialogues[2]);

        yield return new WaitWhile(() => chatManager.chatState == ChatManager.ChatState.Chat);

        table.enabled = true;
        TutorialObject[4].SetActive(true);

        yield return new WaitWhile(() => display.state == displayManager.State.normal);
        TutorialObject[4].SetActive(false);

        yield return new WaitWhile(() => chatManager.chatState != ChatManager.ChatState.Chat);
        yield return new WaitWhile(() => chatManager.chatState == ChatManager.ChatState.Chat);
        //3�� Ʃ�丮�� (Item)

        EndTutorial();
    }

    void EndTutorial()
    {
        button.DontDownButton = false;
        button.SetDownButton(false);
        GameManager.Instance.NextScene("Lobby");
    }

    IEnumerator Fade(float plus, bool isBlack, GameObject fade)
        //���̵� In/Out �ڷ�ƾ
        //plus�� ���� ���� In/Out ����
        //���� ����� ������, Day 1 �޼����� �Ͼ���̶� ���� ����
    {
        Image image = fade.GetComponent<Image>();
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

    public void UseCard()
    {
        chatManager.StartDialogue(dialogues[3]);
    }
}
