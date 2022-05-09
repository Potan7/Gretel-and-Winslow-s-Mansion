using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day3Day : MonoBehaviour
{
    AfterDialogue afterDialogue;
    displayManager display;
    ButtonFunction button;
    Inventory inventory;

    public Dialogue dialogue;

    Image fade;
    public Image day;

    public GameObject beforeRoom;
    public GameObject CheckToDoList;

    public int EraseTry = 0;
    // û�ҵ����� ���� ���� �õ�
    public GameObject CaseDisableObject;
    // ũ���Ľ� ���� ���� ������Ʈ

    public int hide = 0, erased = 0;
    // ���� ����� ������ ����

    public Dialogue[] EraseCalls;
    public Dialogue OK, NextDialogue;

    public GameObject Door2;

    public GameObject[] Draws;
    // ������
    TodoManager todo;
    
    
    void Start()
    {
        afterDialogue = ChatManager.Instance.gameObject.GetComponent<AfterDialogue>();
        todo = FindObjectOfType<TodoManager>();
        display = FindObjectOfType<displayManager>();
        button = FindObjectOfType<ButtonFunction>();
        inventory = FindObjectOfType<Inventory>();

        /*if (GameManager.Instance.Progress < 7)
            //���⼭ �ٷ� ���������� ��
        {
            display.RoomLoad("GuestRoom");
            GameManager.Instance.SetProgress(7);
        }*/
        display.RoomLoad("GuestRoom");
        
        // ���Ŀ� ��ġ�� ���μ��� ������ ���� 7�� �����Ͽ� �缳��
        GameManager.Instance.SetProgress(7);

        FindObjectOfType<MapManager>().TeleportStop = false;

        display.beforeRoom = beforeRoom;
        fade = button.GetFade();
        fade.gameObject.SetActive(true);
        StartCoroutine(StartDay3());
    }
    
    IEnumerator StartDay3()
    {
        SoundManager.Instance.PlayBGM(-1);
        inventory.DeleteItem(15);
        // ��ħ�� �Ǿ����� �� ����
        
        yield return afterDialogue.StartCoroutine(afterDialogue.Fade(0.01f, day, true));

        button.SetAllbuttons(true);

        // button.buttons[2].gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        SoundManager.Instance.PlaySound("���Ҹ�", true);

        afterDialogue.StartCoroutine(afterDialogue.Fade(-0.01f, fade));
        yield return afterDialogue.StartCoroutine(afterDialogue.Fade(-0.01f, day, true));

        fade.gameObject.SetActive(false);
        day.gameObject.SetActive(false);
        //Fade In / Out

        yield return new WaitForSeconds(1f);

        SoundManager.Instance.PlayBGM(2);
        ChatManager.Instance.StartDialogue(dialogue);
        
        // yield return new WaitWhile(() => CheckToDoList.activeSelf == false);
        // yield return new WaitWhile(() => CheckToDoList.activeSelf == true);
        //���� ��� üũ

        yield return new WaitForSeconds(1f);

        button.DontDownButton = false;

        // GameManager.Instance.NextProgress();

        //StartCoroutine(WaitingClear());
    }

    void EraseCall()
    {
        if (EraseTry < 2)
        {
            CaseDisableObject.SetActive(false);
        }

        if (EraseTry <= 4)
        {
            StartCoroutine(EraseCoroutine());
        }
    }

    IEnumerator EraseCoroutine()
    {
        ChatManager.Instance.StartDialogue(EraseCalls[EraseTry - 1]);
        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);
    }

    void CheckClear()
    {
        if (Draws[0].activeSelf == false && Draws[1].activeSelf == false)
        {
            todo.ToDoCheck(0);
        }

        if (Draws[2].activeSelf == false)
        {
            todo.ToDoCheck(1);
        }

        if (Draws[3].activeSelf == false)
        {
            todo.ToDoCheck(2);
        }

        if (Draws[4].activeSelf == false)
        {
            todo.ToDoCheck(3);
        }

        if (hide + erased == 5)
        {
            todo.ToDoCheck(4);
            StartCoroutine(Complete());
        }
    }

    IEnumerator Complete()
    {
        GameManager.Instance.NextProgress();
        
        if (erased <= 2)
        {
            GameManager.Instance.TakeOrder++;
            ChatManager.Instance.StartDialogue(OK);
            GameManager.Instance.gameData.condition1++;
            yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);
        }
        
        ChatManager.Instance.StartDialogue(NextDialogue);
        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);
        
        Door2.SetActive(false);
    }
}
