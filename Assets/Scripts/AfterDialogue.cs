using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterDialogue : MonoBehaviour
{
    public MapManager mapManager;
    displayManager display;
    ButtonFunction button;
    Image fade;

    private void Start()
    {
        display = FindObjectOfType<displayManager>();
        button = FindObjectOfType<ButtonFunction>();
        fade = button.GetFade();
    }

    public void DialougeFunction(int ID)
    {
        switch (ID)
        {
            case 1:
                GameManager.Instance.NextProgress();

                GameObject gameObject = GameObject.Find("LobbyGilbert");
                gameObject.tag = "Untagged";
                StartCoroutine(Fade(-0.01f, gameObject));
                //�κ� ���Ʈ ��Ȱ��ȭ �� ���̵� �ƿ�
                mapManager.TeleportStop = false;
                //�ڷ���Ʈ ���
                SoundManager.Instance.PlaySound("Door_Open");
                //�� ���� �Ҹ�
                GameObject.Find("Lobby").transform.GetChild(2).transform.Find("LobbyDoor(disable)").gameObject.SetActive(false);
                //������ ������ �� Ȱ��ȭ

                StartCoroutine(StartCall());

                break;

            case 2:
                ChatManager.Instance.SearchOff();
                SoundManager.Instance.PlayRepeatSound();
                StartCoroutine(GoRestaurant());
                break;

            case 3:
                GameManager.Instance.NextProgress();
                break;
            
            case 4:
                //�ֹ� ��ٸ� ȹ��
                GameObject.Find("Ladder").SetActive(false);
                break;
            
            case 5:
                //Day2Day ������ ��ȭ��
                FindObjectOfType<Day2Day>().SetButton(true);
                break;

            case 6:
                //�ֹ� ���Ʈ ���󰡱�
                FindObjectOfType<Inventory>().DeleteItem(25);
                StartCoroutine(GoGreenHouse());
                break;

            case 7:
                //�ֹ� ��ȭ���� ������
                GameObject.Find("wall1").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("wall1").transform.Find("Blender").transform.GetChild(0).GetChild(1).GetComponent<UseItem>().work = 3;
                break;

            case 8:
                //�ֹ� �����Ǻ�
                GameManager.Instance.TakeOrder++;
                StartCoroutine(GoGreenHouse());
                break;
            
            case 9:
                // ���� ����� ����
                GameManager.Instance.NextProgress();
                // Debug.Log(GameManager.Instance.Progress);
                break;
           
            case 10:
                // ������ ���� ��
                SoundManager.Instance.PlayBGM(-1);
                FindObjectOfType<Inventory>().DeleteItem(29);
                FindObjectOfType<Inventory>().DeleteItem(30);
                
                GameManager.Instance.NextProgress();
                StartCoroutine(GoDay3Night());
                break;

            case 11:
                Day2Night day2;
                day2 = FindObjectOfType<Day2Night>();
                GameManager.Instance.Nighttime++;
                day2.StartCoroutine(day2.NextScene());
                break;

            case 12:
                button.SetEachButtons(false, false, false);
                StartCoroutine(SecretRoom());
                break;

            case 13:
                StartCoroutine(Day3NightEnd());
                break;

            case 14:
                FindObjectOfType<Inventory>().ResetItem();
                StartCoroutine(GoGreenHouse());
                break;
        }
    }

    #region ���̵�
    //-----------------------------------------------------------------------------Fade �ΰ�

    public IEnumerator Fade(float plus, Image fade, bool isWhite = false)
    {
        int _color = 0;
        if (isWhite)
        {
            _color = 255;
        }

        float fadeCount;
        if (plus > 0)
        {
            fadeCount = 0.0f;
            fade.gameObject.SetActive(true);
            while (fadeCount < 1.0f)
            {
                fadeCount += plus;
                yield return new WaitForSeconds(0.01f);
                fade.color = new Color(_color, _color, _color, fadeCount);
            }
        }
        else
        {
            fadeCount = 1.0f;
            while (fadeCount > 0)
            {
                fadeCount += plus;
                yield return new WaitForSeconds(0.01f);
                fade.color = new Color(_color, _color, _color, fadeCount);

            }
            fade.gameObject.SetActive(false);
        }
    }

    IEnumerator Fade(float plus, GameObject fade)
    //���̵� In/Out �ڷ�ƾ
    //plus�� ���� ���� In/Out ����
    {
        SpriteRenderer spriteRenderer = fade.GetComponent<SpriteRenderer>();

        float fadeCount;
        if (plus > 0)
        {
            fadeCount = 0.0f;
            while (fadeCount < 1.0f)
            {
                fadeCount += plus;
                yield return new WaitForSeconds(0.01f);
                spriteRenderer.color = new Color(255, 255, 255, fadeCount);
            }
        }
        else
        {
            fadeCount = 1.0f;
            while (fadeCount > 0)
            {
                fadeCount += plus;
                yield return new WaitForSeconds(0.01f);
                spriteRenderer.color = new Color(255, 255, 255, fadeCount);

            }
            fade.SetActive(false);
        }   
    }
    #endregion

    //-----------------------------------------------------------------------------1ȸ��
    IEnumerator StartCall()
    {
        yield return new WaitForSeconds(0.5f);

        SoundManager.Instance.PlayRepeatSound("Phone_Ring");

        ChatManager.Instance.SearchChat("������ ���� �ִ� ��ȭ�⿡�� �Ҹ��� �鸰��");
    }

    IEnumerator GoRestaurant()
    {
        mapManager.TeleportStop = true;
        yield return StartCoroutine(Fade(0.01f, fade));

        display.RoomLoad("Restaurant");
        button.SetAllbuttons(false);
        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(Fade(-0.01f, fade));

        yield return new WaitForSeconds(0.5f);
        GameObject.Find("DinnerScript").SendMessage("ActiveDialogue");

        yield return new WaitWhile(() => GameManager.Instance.Progress < 4);
        
        yield return StartCoroutine(Fade(0.005f, fade));

        Interact.StopInteract = true;

        GameManager.Instance.NextScene("GuestRoomNight");
    }

    IEnumerator GoGreenHouse()
    {
        GameObject.Find("wall1").transform.GetChild(0).gameObject.SetActive(false);

        Interact.StopInteract = true;
        yield return StartCoroutine(Fade(0.01f, fade));

        display.RoomLoad("Greenhouse");
        button.SetEachButtons(false, false, false);
        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(Fade(-0.01f, fade));

        DialogueTrigger[] dialogues = new DialogueTrigger[4];
        for (int i = 0; i < 4; i++)
        {
            dialogues[i] = GameObject.Find("wall1").transform.GetChild(i).GetComponent<DialogueTrigger>();
        }
        dialogues[0].SendMessage("Interact");

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        dialogues[FindObjectOfType<Day2Day>().isPoision ? 2 : 1].SendMessage("Interact");

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        dialogues[3].SendMessage("Interact");

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        yield return StartCoroutine(Fade(0.01f, fade));
        Interact.StopInteract = false;

        GameManager.Instance.NextScene("GuestRoomNight");
    }
    
    IEnumerator GoDay3Night()
    {
        yield return StartCoroutine(Fade(0.01f, fade));
        Interact.StopInteract = false;

        GameManager.Instance.NextScene("GuestRoomNight");
    }

    IEnumerator SecretRoom()
    {
        yield return new WaitWhile(() => GameManager.Instance.Progress < 12);

        button.buttons[3].gameObject.SetActive(true);
        FindObjectOfType<PhoneManager>().Ring();
    }

    IEnumerator Day3NightEnd()
    {
        yield return StartCoroutine(Fade(0.01f, fade));
        GameManager.Instance.Nighttime++;
        GameManager.Instance.gameData.condition1++;
        GameManager.Instance.NextScene("Lobby");
    }
}
