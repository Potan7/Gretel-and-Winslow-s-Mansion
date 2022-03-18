using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManger : MonoBehaviour
{
    public GameObject nextButton;
    public Book book;
    public Image image;
    public GameObject Tuto;

    MenuManger menu;

    public void Start()
    {
        menu = FindObjectOfType<MenuManger>();
        nextButton = GameObject.Find("nextButton");
        book = GameObject.Find("Book").GetComponent<Book>();
        nextButton.SetActive(false);
        menu.ESCStop = true;
    }

    public void Update()
    {
        nextButton.SetActive(book.currentPage == book.bookPages.Length);
    }

    public void IntroEndButton()
    {
        image.gameObject.SetActive(true);
        //페이드 아웃용 이미지 활성화.
        StopAllCoroutines();
        StartCoroutine("FadeCoroutine");
        SoundManager.Instance.PlayBGM(-1);
        menu.ESCStop = false;

        Debug.Log("인트로 종료");
    }

    IEnumerator FadeCoroutine()
        //페이드 아웃
    {
        float fadeCount = 0;
        //처음 알파값
        while (fadeCount < 1.0f)
            //알파 1.0까지 반복.
        {
            fadeCount += 0.02f;
            yield return new WaitForSeconds(0.01f);
            //0.01초마다 실행
            image.color = new Color(0, 0, 0, fadeCount);
            //해당 변수값으로 알파값 설정.
        }

        SceneManager.LoadSceneAsync("DayTutorial");
    }

    public void TutoEnd()
    {
        Tuto.gameObject.SetActive(false);
    }
}
