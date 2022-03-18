using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public AfterDialogue afterDialogue;
    ChatSpecial chatSpecial;

    public Text SearchText;
    //확인 메세지
    public GameObject Search;

    public enum ChatState
    {
        normal,
        Chat,
        Search
    }
    public ChatState chatState = ChatState.normal;
    //현재 대화 상태 표시

    public Text ChatText;

    public Animator Chatanimator;

    public SpriteRenderer CharImage;
    public Animator Charanimator;

    Queue<string> sentences;
    //진행될 대화문.
    // Queue<Sprite> sprites;

    Sprite[] CharSprites;

    int DialogueID;

    #region 싱글톤
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
                    Debug.Log("활성화된 매니저 오브젝트가 없습니다.");
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
        //대화문 실행.
    {
        Chatanimator.SetBool("IsOpen", true);
        Charanimator.SetBool("IsOpen", true);
        chatState = ChatState.Chat;
        //대화창 실행 및 대화 상태로 변경

        DialogueID = dialogue.id;

        sentences.Clear();
        // sprites.Clear();

        foreach (string setence in dialogue.sentences)
        {
            sentences.Enqueue(setence);
            //받아온 문장 전부 넣기.
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
        //대화문 진행
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
            //만약 마지막 문장이라면 종료.
        }

        // if (sprites.Count != 0)
        //     CharImage.sprite = sprites.Dequeue();
        
        string[] sentence = sentences.Dequeue().Split(':');
        //문장 꺼내서 입력.

        for (int i = 0; i < sentence.Length; i++)
        {
            // 초기 이미지 None 설정
            if (i == 0) CharImage.sprite = null;
            
            switch (i)
            {
                case 1:
                    // 스프라이트 출력, -1: None, 그 외: 스프라이트 따라감
                    int idx = int.Parse(sentence[i]);
                    CharImage.sprite = (idx != -1 ? CharSprites[idx] : null);
                    break;

                case 2:
                    // 함수 실행
                    if (chatSpecial == null) chatSpecial = FindObjectOfType<ChatSpecial>();
                    chatSpecial.ChatFunction(sentence[i]);
                    break;
            }
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence[0]));
        //글자 애니메이션용 코루틴 실행.

    }

    IEnumerator TypeSentence(string sentence)
    {
        ChatText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            ChatText.text += letter;
            yield return null;
            //문장을 분해해 하나씩 입력.
        }
    }

    void EndDialogue()
    {
        Chatanimator.SetBool("IsOpen", false);
        Charanimator.SetBool("IsOpen", false);
        //대화창 종료
        chatState = ChatState.normal;
        //대화상태 변경.
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
        //메세지를 받아 입력해 띄우기
        //만약 두 메세지가 오면 두줄, 아니면 한줄에 표시.
    }

    public void SearchOff()
    {
        Search.SetActive(false);
        //확인 창 끄기
        chatState = ChatState.normal;
    }
}
