using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManger : MonoBehaviour
{
    public Image image;
    public Button loadButton;
    public GameObject CheckMsg;

    MenuManger menu;
    SoundManager sound;

    private void Start()
    {
        sound = SoundManager.Instance;
        menu = FindObjectOfType<MenuManger>();

        sound.PlayBGM(5);
        menu.ESCStop = true;
    }

    private void Update()
        // ���̺������� �������� ���� ��� �ҷ����� ��ư�� ��Ȱ��ȭ��.
    {
        if (GameManager.Instance.gameData.SceneProgress == 0)
        {
            loadButton.interactable = false;
        }
        else
        {
            loadButton.interactable = true;
        }
    }


    public void StartButton()
        //���� ��ư
    {
        if (GameManager.Instance.gameData.SceneProgress == 0)
        {
            GameStart();
        }
        else
        {
            CheckMsg.SetActive(true);
            // ���̺������� ���� ��� ���޼��� ���.
        }
    }

    public void checkMsgYes()
    {
        CheckMsg.SetActive(false);
        GameStart();
    }

    public void checkMsgNo()
    {
        CheckMsg.SetActive(false);
    }

    void GameStart()
        // ���� ����
    {
        GameManager.Instance.ResetSaveFile();

        image.gameObject.SetActive(true);
        //���̵� �ƿ��� �̹��� Ȱ��ȭ.
        StopAllCoroutines();
        StartCoroutine("FadeCoroutine");
        //�ڷ�ƾ ����
        menu.ESCStop = false;

        sound.PlayBGM(4);

        Debug.Log("���� ����");
    }

    public void LoadButton()
    {
        sound.PlayBGM(-1);
        Debug.Log("Load " + GameManager.Instance.gameData.SceneProgress);
        menu.ESCStop = false;
        GameManager.Instance.LoadGameData();

        SceneManager.LoadScene(GameManager.Instance.gameData.SceneProgress);
    }

    public void ExitButton()
        //���� ��ư
    {
        Debug.Log("����");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator FadeCoroutine()
        //���̵� �ƿ�
    {
        float fadeCount = 0;
        //ó�� ���İ�
        while (fadeCount < 1.0f)
            //���� 1.0���� �ݺ�.
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            //0.01�ʸ��� ����
            image.color = new Color(0, 0, 0, fadeCount);
            //�ش� ���������� ���İ� ����.
        }

        SceneManager.LoadSceneAsync("Intro");
    }

    public void OptionButton()
    {
        menu.OptionButton();
    }
}
