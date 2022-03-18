using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thinking : MonoBehaviour
{
    SpriteRenderer sprite;
    bool isWorking = false;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(255, 255, 255, 0);
    }
    void Interact()
    {
        if (!isWorking)
            StartCoroutine(ShowMe());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        sprite.color = new Color(255, 255, 255, 0);
        isWorking = false;
    }

    IEnumerator ShowMe()
    {
        isWorking = true;
        yield return StartCoroutine(Fade(true));

        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(Fade(false));
        isWorking = false;
    }

    IEnumerator Fade(bool InOut)
    {
        float fadeCount = 0.9f;
        float plus = -0.01f;
        if (InOut)
        {
            fadeCount = 0.1f;
            plus = 0.01f;
        }

        while (fadeCount < 1.0f && fadeCount > 0.0f)
        {
            sprite.color = new Color(255, 255, 255, fadeCount);
            yield return null;
            fadeCount += plus;
        }
    }
}
