using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Inventory inventory;
    public ButtonFunction button;

    Sprite[] sprites;
    //아이템 사용 함수에서 쓰일 스프라이트들

    bool IsItemConsume;

    private void Awake()
    {
        sprites = Resources.LoadAll<Sprite>("Sprites/ItemManager");
    }

    public void itemUsed(int ID, UseItem item)
    {
        IsItemConsume = true;
        switch (ID)
        {
            case 1:
                //초대장 사용
                GameObject.Find("TutorialManager").GetComponent<DayTutorialManager>().UseCard();
                break;

            case 2:
                //장작 사용
                ChatManager.Instance.SearchChat("장작을 벽난로에 넣었다");
                item.gameObject.GetComponent<SearchInfo>().Message = "장작에 불을 붙일 것이 필요하다";
                item.work = 1;
                break;

            case 3:
                //불의 열매 사용
                item.gameObject.SendMessage("ActiveDialogue");
                GameObject.Find("Lobby").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                FindObjectOfType<TodoManager>().ToDoCheck(0);
                item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                item.gameObject.GetComponent<SearchInfo>().Message = "잘 타고 있는 벽난로다";
                break;

            case 4:
                //햇빛 구슬 사용
                item.gameObject.GetComponent<Animator>().SetTrigger("Disapper");
                GameObject.Find("Case").GetComponent<SpriteRenderer>().sprite = sprites[0];
                StartCoroutine(ObjectDisable(item.gameObject, 0.7f));
                break;

            case 5:
                //물의 구슬 사용
                GameObject.Find("Flowerpot").transform.Find("FireFruit").gameObject.SetActive(true);
                break;

            case 6:
                //가위 사용
                if (item.gameObject.name == "FireFruit")
                {
                    IsItemConsume = false;
                    item.gameObject.GetComponent<Item>().GetItem();
                }
                else if (item.gameObject.name == "DollDummy")
                {
                    Dialogue dialogue = new Dialogue();
                    dialogue.sentences = new string[1];
                    dialogue.sentences[0] = "인형을 자르다가 가위가 망가졌어...:0";
                    ChatManager.Instance.StartDialogue(dialogue);
                    item.transform.parent.Find("Dummies").gameObject.SetActive(true);
                }
                break;

            case 7:
                //물이든 병 사용
                item.gameObject.transform.Find("Water").gameObject.SetActive(true);
                SoundManager.Instance.PlaySound("Statue_Open", true);
                break;

            case 8:
                //왕관 사용
                item.gameObject.transform.Find("QueensCrown").gameObject.SetActive(true);
                SoundManager.Instance.PlaySound("Statue_Open", true);
                break;

            case 9:
                //꿀단지 사용
                item.gameObject.GetComponent<Animator>().SetTrigger("UseHoney");
                break;
            
            case 10:
                //장미열쇠 사용
                // todo
                break;
            
            case 11:
                //파랑새 인형 사용
                // todo
                break;

            case 12:
                //인형 머리 사용
                ChatManager.Instance.SearchChat("잘 정리했다.");
                FindObjectOfType<DollDisplayPuzzle>().Trashbin();
                break;

            case 13:
                //인형 몸 사용
                ChatManager.Instance.SearchChat("잘 정리했다.");
                FindObjectOfType<DollDisplayPuzzle>().Trashbin();
                break;

            case 14:
                //인형 팔다리
                ChatManager.Instance.SearchChat("잘 정리했다.");
                FindObjectOfType<DollDisplayPuzzle>().Trashbin();
                break;

            case 16:
                //티팟 물넣기
                inventory.DeleteItem(ID);
                item.gameObject.transform.GetChild(0).gameObject.SendMessage("GetItem");
                ChatManager.Instance.SearchChat("물을 넣었다");
                break;

            case 20:
                //가루 블렌더 넣기
                ChatManager.Instance.SearchChat("마젠타 가루를 넣었다.");
                FindObjectOfType<BlenderPuzzle>().AddToBlender(ID - 17);
                break;

            case 23:
                //홍차 가루 사용
                ChatManager.Instance.SearchChat("홍차 가루를 넣었다");
                GameManager.Instance.NextProgress();
                inventory.DeleteItem(ID);
                break;

            case 24:
                //반죽 가루 사용
                item.transform.parent.Find("Key").GetChild(0).gameObject.SetActive(true);
                FindObjectOfType<OvenLever>().dustIn();
                item.GetComponent<BoxCollider2D>().enabled = false;
                break;

            case 25:
                //가루 병 넣기
                if (item.name == "BlenderCollider")
                {
                    FindObjectOfType<BlenderPuzzle>().AddToBlender(8);
                    ChatManager.Instance.SearchChat("바다가루, 허브가루를 넣었다.", "남은 병은 버렸다.");
                }
                else
                {
                    FindObjectOfType<Day2Day>().SetButton(true);
                    IsItemConsume = false;
                    item.work = 1;
                }
                break;

            case 26:
                //물 넣은 티팟 사용
                item.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("CutScene").SendMessage("Interact");
                SoundManager.Instance.PlaySound("물_끓는_소리", true);
                GameObject.Find("FireLever").GetComponent<BoxCollider2D>().enabled = false;
                break;

            case 28:
                //열쇠 사용
                item.GetComponent<SpriteRenderer>().enabled = false;
                item.GetComponent<BoxCollider2D>().enabled = false;
                item.transform.GetChild(0).gameObject.SetActive(true);
                break;
            
            case 29:
                // 청소 도구 사용
                IsItemConsume = false;

                Day3Day day = FindObjectOfType<Day3Day>();

                if (day.EraseTry == 0)
                {
                    item.transform.gameObject.SetActive(true);
                    day.EraseTry++;
                    day.SendMessage("EraseCall");
                }
                else
                {
                    item.transform.gameObject.SetActive(false);
                    day.EraseTry++;
                    day.erased++;
                }

                if (day.EraseTry <= 4 && day.EraseTry != 0)
                {
                    FindObjectOfType<PhoneManager>().Ring();
                    FindObjectOfType<PhoneManager>().StartWaitCoroutine();
                }
                // GameObject.Find("Day3Day").SendMessage("EraseCall");

                day.SendMessage("CheckClear");
                break;
            
            case 30:
                // 투명 크레파스 사용
                Debug.Log("투명 크레파스");
                IsItemConsume = false;
                FindObjectOfType<Day3Day>().hide++;
                item.transform.gameObject.SetActive(false);
                
                FindObjectOfType<Day3Day>().SendMessage("CheckClear");
                break;

            case 31:
                // 이끼열쇠 사용

                break;
        }

        if (IsItemConsume)
            inventory.DeleteItem(ID);
        //만약 case안에서 false로 안바꾼다면 아이템 제거
    }

    public void WrongItem(int ID, UseItem item)
        //아이템을 순서대로 사용하지 않았을 때
    {
        switch (ID)
        {
            case 3:
                //장작보다 먼저 불의 열매 사용
                ChatManager.Instance.SearchChat("먼저 불로 태울게 필요하다");
                break;

            case 12:
                //인형 머리 잘못 사용
                ChatManager.Instance.SearchChat("여기가 아니다.");
                break;

            case 13:
                //인형 몸 잘못 사용
                ChatManager.Instance.SearchChat("여기가 아니다.");
                break;

            case 14:
                //인형 팔다리 잘못 사용
                ChatManager.Instance.SearchChat("여기가 아니다.");
                break;

            case 16:
                //티팟 잘못 사용 (물부터 넣어야함)
                ChatManager.Instance.SearchChat("물부터 넣어야한다.");
                break;

            case 21:
                //시안 가루 사용 (잘못 사용 X)
                ChatManager.Instance.SearchChat("시안 가루를 넣었다.");
                FindObjectOfType<BlenderPuzzle>().AddToBlender(ID - 17);
                inventory.DeleteItem(ID);
                break;

            case 22:
                //화이트 가루 사용 (위와 같음)
                ChatManager.Instance.SearchChat("화이트 가루를 넣었다.");
                FindObjectOfType<BlenderPuzzle>().AddToBlender(ID - 17);
                inventory.DeleteItem(ID);
                break;

            case 23:
                //홍차 가루 사용 (잘못 사용 X)
                ChatManager.Instance.SearchChat("홍차 가루를 넣었다");
                GameManager.Instance.NextProgress();
                inventory.DeleteItem(ID);
                break;
        }
    }

    IEnumerator ObjectDisable(GameObject target, float time)
        //오브젝트를 일정시간 뒤에 사라지게하는 코루틴
    {
        yield return new WaitForSeconds(time);
        target.SetActive(false);
    }
}
