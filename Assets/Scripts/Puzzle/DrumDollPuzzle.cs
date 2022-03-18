using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumDollPuzzle : MonoBehaviour
{
    Animator anim;

    public BoxCollider2D box;
    public GameObject Door;

    public List<bool> handList = new List<bool>();
    public List<bool> Answer = new List<bool>();

    float time = 0.0f;
    bool startCheck = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void UseHand(bool isRight)
    {
        SoundManager.Instance.PlaySound("ºÏ¼Ò¸®");

        if (isRight)
        {
            StartCoroutine(UseRight());
        }
        else
        {
            StartCoroutine(UseLeft());
        }

        startCheck = true;
        time = 0.0f;
        handList.Add(isRight);
    }

    private void Update()
    {
        if (startCheck) {
            time += Time.deltaTime;

            if (time > 1.5f)
            {
                if (handList.Count == Answer.Count)
                {
                    for (int i = 0; i < Answer.Count; i++)
                    {
                        if (handList[i] != Answer[i])
                        {
                            PuzzleFail();
                            return;
                        }
                    }
                    Complete();
                }
                else
                {
                    PuzzleFail();
                }
            }
        }
    }

    void Complete()
    {
        ButtonFunction button = FindObjectOfType<ButtonFunction>();

        button.DownButton();
        button.SetAllbuttons(false);
        button.DontDownButton = true;

        SoundManager.Instance.PlaySound("Door_Open");
        box.enabled = false;
        Door.SetActive(true);
        startCheck = false;
        time = 0.0f;
    }
    
    void PuzzleFail()
    {
        handList.Clear();
        startCheck = false;
        time = 0.0f;

    }

    IEnumerator UseRight()
    {
        anim.SetBool("UseLeft", false);
        StopCoroutine(UseLeft());
        anim.SetBool("UseRight", true);

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("UseRight", false);
    }

    IEnumerator UseLeft()
    {
        anim.SetBool("UseRight", false);
        StopCoroutine(UseRight());
        anim.SetBool("UseLeft", true);

        yield return new WaitForSeconds(0.5f);

        anim.SetBool("UseLeft", false);
    }
}
