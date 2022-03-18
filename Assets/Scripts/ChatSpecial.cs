using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatSpecial : MonoBehaviour
{
    Dialogue dialogue = new Dialogue();
    public void ChatFunction(string name)
    {
        switch (name)
        {
            case "Tea1":
                StartCoroutine(Tea1());
                dialogue.sentences = new string[1];
                dialogue.sentences[0] = "ÀÌ»óÇÏ´Ù... Âø°¢ÀÎ°¡? :0";
                break;

            case "Bird":
                StartCoroutine(Bird());
                break;

            case "RingOn":
                SoundManager.Instance.PlayRepeatSound("Phone_Ring");
                break;

            case "Phone":
                SoundManager.Instance.PlayRepeatSound();
                SoundManager.Instance.PlaySound("Phone_Open");
                break;

            case "BirdEnd":
                StartCoroutine(Fade(-0.02f, transform.GetChild(0).gameObject.GetComponent<Image>(), true));
                StopCoroutine(Bird());
                break;

            case "DollSound":
                SoundManager.Instance.PlaySound("³¥³¥°Å¸®´Â", true);
                break;

            case "SoundEffect":
                SoundManager.Instance.PlaySound("SoundEffect");
                break;
        }
    }

    IEnumerator Tea1()
    {
        Interact interact = FindObjectOfType<Interact>();
        interact.SetStopInteract(true);
        yield return StartCoroutine(Fade(0.01f, transform.GetChild(0).gameObject.GetComponent<Image>()));
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Fade(0.01f, transform.GetChild(1).gameObject.GetComponent<Image>()));

        ChatManager.Instance.StartDialogue(dialogue);
        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        transform.GetChild(0).gameObject.SetActive(false);
        yield return StartCoroutine(Fade(-0.02f, transform.GetChild(1).gameObject.GetComponent<Image>()));
        interact.SetStopInteract(false);

    }

    IEnumerator Bird()
    {
        yield return StartCoroutine(Fade(0.1f, transform.GetChild(0).gameObject.GetComponent<Image>(), true));

        yield return new WaitWhile(() => ChatManager.Instance.chatState == ChatManager.ChatState.Chat);

        yield return StartCoroutine(Fade(-0.2f, transform.GetChild(0).gameObject.GetComponent<Image>(), true));
    }

    IEnumerator Fade(float plus, Image fade, bool isWhite = true)
    {
        int _color = 0;
        if (isWhite)
        {
            _color = 255;
        }

        float fadeCount;
        if (plus > 0)
        {
            fadeCount = 0.0f;
            fade.gameObject.SetActive(true);
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
                yield return new WaitForSeconds(0.01f);
                fade.color = new Color(_color, _color, _color, fadeCount);

            }
            fade.gameObject.SetActive(false);
        }
    }
}
