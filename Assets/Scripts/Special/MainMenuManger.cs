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
        // 세이브파일이 존재하지 않을 경우 불러오기 버튼을 비활성화함.
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
        //시작 버튼
    {
        if (GameManager.Instance.gameData.SceneProgress == 0)
        {
            GameStart();
        }
        else
        {
            CheckMsg.SetActive(true);
            // 세이브파일이 있을 경우 경고메세지 띄움.
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
        // 게임 시작
    {
        GameManager.Instance.ResetSaveFile();

        image.gameObject.SetActive(true);
        //페이드 아웃용 이미지 활성화.
        StopAllCoroutines();
        StartCoroutine("FadeCoroutine");
        //코루틴 실행
        menu.ESCStop = false;

        sound.PlayBGM(4);

        Debug.Log("게임 시작");
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
        //종료 버튼
    {
        Debug.Log("종료");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    IEnumerator FadeCoroutine()
        //페이드 아웃
    {
        float fadeCount = 0;
        //처음 알파값
        while (fadeCount < 1.0f)
            //알파 1.0까지 반복.
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            //0.01초마다 실행
            image.color = new Color(0, 0, 0, fadeCount);
            //해당 변수값으로 알파값 설정.
        }

        SceneManager.LoadSceneAsync("Intro");
    }

    public void OptionButton()
    {
        menu.OptionButton();
    }
}
