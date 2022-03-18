using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public AfterDialogue afterDialogue;
    ChatSpecial chatSpecial;

    public Text SearchText;
    //Ȯ�� �޼���
    public GameObject Search;

    public enum ChatState
    {
        normal,
        Chat,
        Search
    }
    public ChatState chatState = ChatState.normal;
    //���� ��ȭ ���� ǥ��

    public Text ChatText;

    public Animator Chatanimator;

    public SpriteRenderer CharImage;
    public Animator Charanimator;

    Queue<string> sentences;
    //����� ��ȭ��.
    // Queue<Sprite> sprites;

    Sprite[] CharSprites;

    int DialogueID;

    #region �̱���
    private static ChatManager _instance = null;

    public static ChatManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (ChatManager)FindObjectOfType(typeof(ChatManager));
                if (_instance == null)
                {
                    Debug.Log("Ȱ��ȭ�� �Ŵ��� ������Ʈ�� �����ϴ�.");
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(this.gameObject);
            AwakeAfter();
        }
    }
    #endregion
    private void AwakeAfter()
    {
        sentences = new Queue<string>();
        // sprites = new Queue<Sprite>();
        CharSprites = Resources.LoadAll<Sprite>("Sprites/ChatManager");
    }

    public void StartDialogue(Dialogue dialogue)
        //��ȭ�� ����.
    {
        Chatanimator.SetBool("IsOpen", true);
        Charanimator.SetBool("IsOpen", true);
        chatState = ChatState.Chat;
        //��ȭâ ���� �� ��ȭ ���·� ����

        DialogueID = dialogue.id;

        sentences.Clear();
        // sprites.Clear();

        foreach (string setence in dialogue.sentences)
        {
            sentences.Enqueue(setence);
            //�޾ƿ� ���� ���� �ֱ�.
        }

        /*
         if (dialogue.Charsprite.Length == 1)
        {
            for (int i = 0; i < dialogue.sentences.Length; i++)
            {
                sprites.Enqueue(dialogue.Charsprite[0]);
            }
        }
        else
        {
            foreach (Sprite sprite in dialogue.Charsprite)
            {
                sprites.Enqueue(sprite);
            }
        }
        */
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
        //��ȭ�� ����
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
            //���� ������ �����̶�� ����.
        }

        // if (sprites.Count != 0)
        //     CharImage.sprite = sprites.Dequeue();
        
        string[] sentence = sentences.Dequeue().Split(':');
        //���� ������ �Է�.

        for (int i = 0; i < sentence.Length; i++)
        {
            // �ʱ� �̹��� None ����
            if (i == 0) CharImage.sprite = null;
            
            switch (i)
            {
                case 1:
                    // ��������Ʈ ���, -1: None, �� ��: ��������Ʈ ����
                    int idx = int.Parse(sentence[i]);
                    CharImage.sprite = (idx != -1 ? CharSprites[idx] : null);
                    break;

                case 2:
                    // �Լ� ����
                    if (chatSpecial == null) chatSpecial = FindObjectOfType<ChatSpecial>();
                    chatSpecial.ChatFunction(sentence[i]);
                    break;
            }
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence[0]));
        //���� �ִϸ��̼ǿ� �ڷ�ƾ ����.

    }

    IEnumerator TypeSentence(string sentence)
    {
        ChatText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            ChatText.text += letter;
            yield return null;
            //������ ������ �ϳ��� �Է�.
        }
    }

    void EndDialogue()
    {
        Chatanimator.SetBool("IsOpen", false);
        Charanimator.SetBool("IsOpen", false);
        //��ȭâ ����
        chatState = ChatState.normal;
        //��ȭ���� ����.
        afterDialogue.DialougeFunction(DialogueID);
    }

    public void SearchChat(string Msg, string Msg2 = "")
    {
        chatState = ChatState.Search;
        if (Msg2 == "")
        {
            SearchText.text = Msg;
        }
        else
        {
            SearchText.text = Msg + "\n" + Msg2;
        }
        Search.SetActive(true);
        SoundManager.Instance.PlaySound("Search");
        //�޼����� �޾� �Է��� ����
        //���� �� �޼����� ���� ����, �ƴϸ� ���ٿ� ǥ��.
    }

    public void SearchOff()
    {
        Search.SetActive(false);
        //Ȯ�� â ����
        chatState = ChatState.normal;
    }
}
