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
                //로비 길버트 비활성화 및 페이드 아웃
                mapManager.TeleportStop = false;
                //텔레포트 허용
                SoundManager.Instance.PlaySound("Door_Open");
                //문 여는 소리
                GameObject.Find("Lobby").transform.GetChild(2).transform.Find("LobbyDoor(disable)").gameObject.SetActive(false);
                //복도로 나가는 문 활성화

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
                //주방 사다리 획득
                GameObject.Find("Ladder").SetActive(false);
                break;
            
            case 5:
                //Day2Day 종이컵 전화기
                FindObjectOfType<Day2Day>().SetButton(true);
                break;

            case 6:
                //주방 길버트 따라가기
                FindObjectOfType<Inventory>().DeleteItem(25);
                StartCoroutine(GoGreenHouse());
                break;

            case 7:
                //주방 전화지시 따르기
                GameObject.Find("wall1").transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("wall1").transform.Find("Blender").transform.GetChild(0).GetChild(1).GetComponent<UseItem>().work = 3;
                break;

            case 8:
                //주방 레시피북
                GameManager.Instance.TakeOrder++;
                StartCoroutine(GoGreenHouse());
                break;
            
            case 9:
                // 낙서 지우기 시작
                GameManager.Instance.NextProgress();
                // Debug.Log(GameManager.Instance.Progress);
                break;
           
            case 10:
                // 윈슬로 위로 끝
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

    #region 페이드
    //-----------------------------------------------------------------------------Fade 두개

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
    //페이드 In/Out 코루틴
    //plus의 값에 따라 In/Out 결정
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

    //-----------------------------------------------------------------------------1회용
    IEnumerator StartCall()
    {
        yield return new WaitForSeconds(0.5f);

        SoundManager.Instance.PlayRepeatSound("Phone_Ring");

        ChatManager.Instance.SearchChat("벽난로 옆에 있는 전화기에서 소리가 들린다");
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
