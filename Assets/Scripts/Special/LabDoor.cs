using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabDoor : MonoBehaviour
{
    Image fade;
    ButtonFunction button;

    public Image GilbertJumpScare;

    void Start()
    {
        button = FindObjectOfType<ButtonFunction>();
        fade = button.GetFade();
    }

    void Interact()
    {
        button.SetAllbuttons(false);
        StartCoroutine(JumpScare());
    }

    IEnumerator JumpScare()
    {
        fade.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(0.01f, true, fade));
        yield return new WaitForSeconds(1f);
        SoundManager.Instance.PlayBGM(-1);


        GilbertJumpScare.gameObject.SetActive(true);

        yield return new WaitForSeconds(3.5f);
        SoundManager.Instance.PlaySound("������_����ȭ��", true);
        yield return new WaitForSeconds(2.3f);

        SoundManager.Instance.PlaySound();

        GilbertJumpScare.gameObject.SetActive(false);

        FindObjectOfType<Inventory>().DeleteItem(15);

        GameManager.Instance.Nighttime = 1;
        // ���μ��� ������ ���� ���⼭�� �߰� �� �� -> 2���� ���� �����ִ� ������?
        //GameManager.Instance.NextProgress();
        GameManager.Instance.NextScene("GuestRoom");
    }

    IEnumerator Fade(float plus, bool isBlack, Image image)
        //���̵� In/Out �ڷ�ƾ
        //plus�� ���� ���� In/Out ����
        //���� ����� ������, Day 1 �޼����� �Ͼ���̶� ���� ����
    {
        float fadeCount;
        if (plus > 0)
        {
            fadeCount = 0.0f;
            while (fadeCount < 1.0f)
            {
                fadeCount += plus;
                yield return new WaitForSeconds(0.01f);
                if (isBlack)
                    image.color = new Color(0, 0, 0, fadeCount);
                else
                    image.color = new Color(255, 255, 255, fadeCount);

            }
        }
        else
        {
            fadeCount = 1.0f;
            while (fadeCount > 0)
            {
                fadeCount += plus;
                yield return new WaitForSeconds(0.01f);
                if (isBlack)
                    image.color = new Color(0, 0, 0, fadeCount);
                else
                    image.color = new Color(255, 255, 255, fadeCount);
            }
        }
    }
}
