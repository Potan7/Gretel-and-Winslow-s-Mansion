using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Inventory inventory;
    public ButtonFunction button;

    Sprite[] sprites;
    //������ ��� �Լ����� ���� ��������Ʈ��

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
                //�ʴ��� ���
                GameObject.Find("TutorialManager").GetComponent<DayTutorialManager>().UseCard();
                break;

            case 2:
                //���� ���
                ChatManager.Instance.SearchChat("������ �����ο� �־���");
                item.gameObject.GetComponent<SearchInfo>().Message = "���ۿ� ���� ���� ���� �ʿ��ϴ�";
                item.work = 1;
                break;

            case 3:
                //���� ���� ���
                item.gameObject.SendMessage("ActiveDialogue");
                GameObject.Find("Lobby").transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
                FindObjectOfType<TodoManager>().ToDoCheck(0);
                item.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                item.gameObject.GetComponent<SearchInfo>().Message = "�� Ÿ�� �ִ� �����δ�";
                break;

            case 4:
                //�޺� ���� ���
                item.gameObject.GetComponent<Animator>().SetTrigger("Disapper");
                GameObject.Find("Case").GetComponent<SpriteRenderer>().sprite = sprites[0];
                StartCoroutine(ObjectDisable(item.gameObject, 0.7f));
                break;

            case 5:
                //���� ���� ���
                GameObject.Find("Flowerpot").transform.Find("FireFruit").gameObject.SetActive(true);
                break;

            case 6:
                //���� ���
                if (item.gameObject.name == "FireFruit")
                {
                    IsItemConsume = false;
                    item.gameObject.GetComponent<Item>().GetItem();
                }
                else if (item.gameObject.name == "DollDummy")
                {
                    Dialogue dialogue = new Dialogue();
                    dialogue.sentences = new string[1];
                    dialogue.sentences[0] = "������ �ڸ��ٰ� ������ ��������...:0";
                    ChatManager.Instance.StartDialogue(dialogue);
                    item.transform.parent.Find("Dummies").gameObject.SetActive(true);
                }
                break;

            case 7:
                //���̵� �� ���
                item.gameObject.transform.Find("Water").gameObject.SetActive(true);
                SoundManager.Instance.PlaySound("Statue_Open", true);
                break;

            case 8:
                //�հ� ���
                item.gameObject.transform.Find("QueensCrown").gameObject.SetActive(true);
                SoundManager.Instance.PlaySound("Statue_Open", true);
                break;

            case 9:
                //�ܴ��� ���
                item.gameObject.GetComponent<Animator>().SetTrigger("UseHoney");
                break;
            
            case 10:
                //��̿��� ���
                // todo
                break;
            
            case 11:
                //�Ķ��� ���� ���
                // todo
                break;

            case 12:
                //���� �Ӹ� ���
                ChatManager.Instance.SearchChat("�� �����ߴ�.");
                FindObjectOfType<DollDisplayPuzzle>().Trashbin();
                break;

            case 13:
                //���� �� ���
                ChatManager.Instance.SearchChat("�� �����ߴ�.");
                FindObjectOfType<DollDisplayPuzzle>().Trashbin();
                break;

            case 14:
                //���� �ȴٸ�
                ChatManager.Instance.SearchChat("�� �����ߴ�.");
                FindObjectOfType<DollDisplayPuzzle>().Trashbin();
                break;

            case 16:
                //Ƽ�� ���ֱ�
                inventory.DeleteItem(ID);
                item.gameObject.transform.GetChild(0).gameObject.SendMessage("GetItem");
                ChatManager.Instance.SearchChat("���� �־���");
                break;

            case 20:
                //���� ���� �ֱ�
                ChatManager.Instance.SearchChat("����Ÿ ���縦 �־���.");
                FindObjectOfType<BlenderPuzzle>().AddToBlender(ID - 17);
                break;

            case 23:
                //ȫ�� ���� ���
                ChatManager.Instance.SearchChat("ȫ�� ���縦 �־���");
                GameManager.Instance.NextProgress();
                inventory.DeleteItem(ID);
                break;

            case 24:
                //���� ���� ���
                item.transform.parent.Find("Key").GetChild(0).gameObject.SetActive(true);
                FindObjectOfType<OvenLever>().dustIn();
                item.GetComponent<BoxCollider2D>().enabled = false;
                break;

            case 25:
                //���� �� �ֱ�
                if (item.name == "BlenderCollider")
                {
                    FindObjectOfType<BlenderPuzzle>().AddToBlender(8);
                    ChatManager.Instance.SearchChat("�ٴٰ���, ��갡�縦 �־���.", "���� ���� ���ȴ�.");
                }
                else
                {
                    FindObjectOfType<Day2Day>().SetButton(true);
                    IsItemConsume = false;
                    item.work = 1;
                }
                break;

            case 26:
                //�� ���� Ƽ�� ���
                item.transform.GetChild(0).gameObject.SetActive(true);
                GameObject.Find("CutScene").SendMessage("Interact");
                SoundManager.Instance.PlaySound("��_����_�Ҹ�", true);
                GameObject.Find("FireLever").GetComponent<BoxCollider2D>().enabled = false;
                break;

            case 28:
                //���� ���
                item.GetComponent<SpriteRenderer>().enabled = false;
                item.GetComponent<BoxCollider2D>().enabled = false;
                item.transform.GetChild(0).gameObject.SetActive(true);
                break;
            
            case 29:
                // û�� ���� ���
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
                // ���� ũ���Ľ� ���
                Debug.Log("���� ũ���Ľ�");
                IsItemConsume = false;
                FindObjectOfType<Day3Day>().hide++;
                item.transform.gameObject.SetActive(false);
                
                FindObjectOfType<Day3Day>().SendMessage("CheckClear");
                break;

            case 31:
                // �̳����� ���

                break;
        }

        if (IsItemConsume)
            inventory.DeleteItem(ID);
        //���� case�ȿ��� false�� �ȹٲ۴ٸ� ������ ����
    }

    public void WrongItem(int ID, UseItem item)
        //�������� ������� ������� �ʾ��� ��
    {
        switch (ID)
        {
            case 3:
                //���ۺ��� ���� ���� ���� ���
                ChatManager.Instance.SearchChat("���� �ҷ� �¿�� �ʿ��ϴ�");
                break;

            case 12:
                //���� �Ӹ� �߸� ���
                ChatManager.Instance.SearchChat("���Ⱑ �ƴϴ�.");
                break;

            case 13:
                //���� �� �߸� ���
                ChatManager.Instance.SearchChat("���Ⱑ �ƴϴ�.");
                break;

            case 14:
                //���� �ȴٸ� �߸� ���
                ChatManager.Instance.SearchChat("���Ⱑ �ƴϴ�.");
                break;

            case 16:
                //Ƽ�� �߸� ��� (������ �־����)
                ChatManager.Instance.SearchChat("������ �־���Ѵ�.");
                break;

            case 21:
                //�þ� ���� ��� (�߸� ��� X)
                ChatManager.Instance.SearchChat("�þ� ���縦 �־���.");
                FindObjectOfType<BlenderPuzzle>().AddToBlender(ID - 17);
                inventory.DeleteItem(ID);
                break;

            case 22:
                //ȭ��Ʈ ���� ��� (���� ����)
                ChatManager.Instance.SearchChat("ȭ��Ʈ ���縦 �־���.");
                FindObjectOfType<BlenderPuzzle>().AddToBlender(ID - 17);
                inventory.DeleteItem(ID);
                break;

            case 23:
                //ȫ�� ���� ��� (�߸� ��� X)
                ChatManager.Instance.SearchChat("ȫ�� ���縦 �־���");
                GameManager.Instance.NextProgress();
                inventory.DeleteItem(ID);
                break;
        }
    }

    IEnumerator ObjectDisable(GameObject target, float time)
        //������Ʈ�� �����ð� �ڿ� ��������ϴ� �ڷ�ƾ
    {
        yield return new WaitForSeconds(time);
        target.SetActive(false);
    }
}
