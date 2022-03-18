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
        SoundManager.Instance.PlaySound("공포의_불협화음", true);
        yield return new WaitForSeconds(2.3f);

        SoundManager.Instance.PlaySound();

        GilbertJumpScare.gameObject.SetActive(false);

        FindObjectOfType<Inventory>().DeleteItem(15);

        GameManager.Instance.Nighttime = 1;
        // 프로세스 구분을 위해 여기서는 추가 안 함 -> 2일차 낮에 맞춰주는 것으로?
        //GameManager.Instance.NextProgress();
        GameManager.Instance.NextScene("GuestRoom");
    }

    IEnumerator Fade(float plus, bool isBlack, Image image)
        //페이드 In/Out 코루틴
        //plus의 값에 따라 In/Out 결정
        //검은 배경은 검은색, Day 1 메세지는 하얀색이라 따로 결정
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
