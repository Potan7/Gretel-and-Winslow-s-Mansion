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
        //���̵� �ƿ��� �̹��� Ȱ��ȭ.
        StopAllCoroutines();
        StartCoroutine("FadeCoroutine");
        SoundManager.Instance.PlayBGM(-1);
        menu.ESCStop = false;

        Debug.Log("��Ʈ�� ����");
    }

    IEnumerator FadeCoroutine()
        //���̵� �ƿ�
    {
        float fadeCount = 0;
        //ó�� ���İ�
        while (fadeCount < 1.0f)
            //���� 1.0���� �ݺ�.
        {
            fadeCount += 0.02f;
            yield return new WaitForSeconds(0.01f);
            //0.01�ʸ��� ����
            image.color = new Color(0, 0, 0, fadeCount);
            //�ش� ���������� ���İ� ����.
        }

        SceneManager.LoadSceneAsync("DayTutorial");
    }

    public void TutoEnd()
    {
        Tuto.gameObject.SetActive(false);
    }
}
