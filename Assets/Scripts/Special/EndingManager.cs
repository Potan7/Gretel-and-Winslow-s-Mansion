using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndingManager : MonoBehaviour
{
    MenuManger menu;

    public Text EndingText;
    public Image EndingImage;
    public Sprite[] Endings;

    public Animator anim;

    public Image Door;
    Image fade;

    public GameObject Background;

    public Dialogue[] dialogues;

    bool nowCanEnd = false;
    int progress = 0;

    private void Start()
    {
        fade = FindObjectOfType<ButtonFunction>().GetFade();
        menu = FindObjectOfType<MenuManger>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && nowCanEnd)
        {
            nowCanEnd = false;
            if (progress == 0)
            {
                StopCoroutine("WaitForEnd");
                StartCoroutine(EndOff());
            }
            else if (progress == 1)
            {
                EndGame();
            }
        } 
    }

    public void BadEnding()
    {
        menu.ESCStop = true;
        EndingImage.sprite = Endings[0];
        StartCoroutine(BadEnd());
    }

    IEnumerator BadEnd()
    {
        yield return StartCoroutine(Fade(0.005f, EndingImage));

        anim.SetBool("Zoom", true);

        yield return new WaitForSeconds(8f);

        yield return StartCoroutine(TypeSentence("Bad Ending. 순종적인 꼭두각시"));

        StartCoroutine("WaitForEnd");
    }

    public void NormalEnding1()
    {
        menu.ESCStop = true;
        EndingImage.sprite = Endings[1];
        StartCoroutine(normalEnd1());
    }

    IEnumerator normalEnd1()
    {
        yield return StartCoroutine(Fade(0.005f, fade, false));

        yield return StartCoroutine(DoorOpen());

        ChatManager.Instance.StartDialogue(dialogues[0]);

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        yield return StartCoroutine(Fade(-0.01f, Door));

        yield return StartCoroutine(Fade(0.002f, EndingImage));

        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(TypeSentence("Normal Ending 1. 감추어진 진실"));

        StartCoroutine("WaitForEnd");
    }

    public void NormalEnding2()
    {
        menu.ESCStop = true;
        EndingImage.sprite = Endings[1];
        StartCoroutine(normalEnd2());
    }

    IEnumerator normalEnd2()
    {
        yield return StartCoroutine(Fade(0.005f, fade, false));

        yield return StartCoroutine(DoorOpen());

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(Fade(-0.01f, Door));

        yield return StartCoroutine(Fade(0.002f, EndingImage));

        Background.SetActive(true);

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(Fade(-0.004f, EndingImage));
        EndingImage.sprite = Endings[2];

        yield return StartCoroutine(Fade(0.004f, EndingImage));

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(Fade(-0.004f, EndingImage));
        EndingImage.sprite = Endings[3];

        yield return StartCoroutine(Fade(0.004f, EndingImage));

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(TypeSentence("Normal Ending 2. 불행의 파랑새"));

        StartCoroutine("WaitForEnd");
    }

    public void TrueEnding()
    {
        menu.ESCStop = true;
        EndingImage.sprite = Endings[4];
        StartCoroutine(trueEnd());
    }

    IEnumerator trueEnd()
    {
        yield return StartCoroutine(Fade(0.005f, fade, false));

        yield return StartCoroutine(DoorOpen());

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(Fade(-0.01f, Door));
        Background.SetActive(true);

        ChatManager.Instance.StartDialogue(dialogues[1]);
        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        yield return StartCoroutine(Fade(0.002f, EndingImage));

        ChatManager.Instance.StartDialogue(dialogues[2]);
        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        yield return StartCoroutine(Fade(-0.01f, EndingImage));
        EndingImage.sprite = Endings[5];

        yield return StartCoroutine(Fade(0.01f, EndingImage));

        yield return new WaitForSeconds(1f);

        ChatManager.Instance.StartDialogue(dialogues[3]);
        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        yield return StartCoroutine(Fade(-0.01f, EndingImage));
        EndingImage.sprite = Endings[6];

        yield return StartCoroutine(Fade(0.01f, EndingImage));

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(TypeSentence("True Ending. 그레텔과 헨젤"));

        StartCoroutine("WaitForEnd");
    }

    IEnumerator DoorOpen()
    {
        Door.gameObject.SetActive(true);

        SoundManager.Instance.PlaySound("EndingDoor", true);

        yield return new WaitForSeconds(7f);
    }

    IEnumerator TypeSentence(string sentence)
    {
        EndingText.gameObject.SetActive(true);
        EndingText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            EndingText.text += letter;
            yield return new WaitForSeconds(0.1f);
            //문장을 분해해 하나씩 입력.
        }
    }

    public IEnumerator Fade(float plus, Image fade, bool isWhite = true)
    {
        int _color = 0;
        if (isWhite)
        {
            _color = 255;
        }

        float fadeCount;
        if (plus > 0)
        {
            fade.gameObject.SetActive(true);
            fadeCount = 0.0f;
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
                yield return new WaitForSeconds(0.005f);
                fade.color = new Color(_color, _color, _color, fadeCount);

            }
        }
    }

    IEnumerator WaitForEnd()
    {
        nowCanEnd = true;
        yield return new WaitForSeconds(5f);
        nowCanEnd = false;
        StartCoroutine(EndOff());
    }

    IEnumerator EndOff()
    {
        EndingText.gameObject.SetActive(false);
        yield return Fade(-0.005f, EndingImage);

        anim.SetBool("Zoom", false);
        EndingImage.sprite = Endings[7];

        yield return Fade(0.005f, EndingImage);

        nowCanEnd = true;
        progress = 1;

        yield return new WaitForSeconds(5f);

        yield return Fade(-0.005f, EndingImage);

        EndingImage.sprite = Endings[8];

        yield return Fade(0.005f, EndingImage);

        yield return new WaitForSeconds(1f);

        EndGame();
    }

    void EndGame()
    {
        GameManager.Instance._gameData = null;

        DestroyImmediate(GameManager.Instance.gameObject);
        DestroyImmediate(GameObject.Find("Canvas").gameObject);
        DestroyImmediate(GameObject.Find("display").gameObject);

        SceneManager.LoadScene("MainMenu");
    }
}
