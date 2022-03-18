using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeManager : MonoBehaviour
{
    private GameObject[] Eyes;

    public int answer, phase;

    public bool solved = false;

    private ButtonFunction button;
    private displayManager display;
    Day2Night day;

    public BoxCollider2D Stair;

    void Awake()
    {
        button = FindObjectOfType<ButtonFunction>();
        display = FindObjectOfType<displayManager>();
        day = FindObjectOfType<Day2Night>();
        
        Eyes = new GameObject[transform.childCount];
    }

    private void OnEnable()
    {
        solved = false;
        answer = Random.Range(0, Eyes.Length);
        button.SetEachButtons(false, false, false);

        // 눈 배열에 넣고 작동
        for (var i = 0; i < transform.childCount; i++)
        {
            Eyes[i] = transform.GetChild(i).gameObject;
            Eyes[i].SetActive(true);
        }

        // 정답 눈 체크
        Eyes[answer].GetComponent<Eye>().isAnswer = true;
    }

    // 클릭 시 발생
    public void Clicked(Eye eye)
    {
        if (transform.GetChild(answer).gameObject.GetComponent<Eye>() == eye)
        {
            solved = true;
            StartCoroutine(Correct());
        }
        else
        {
            StartCoroutine(Fail());
        }
    }

    IEnumerator Correct()
    {
        foreach (GameObject child in Eyes)
        {
            child.SetActive(false);
        }
        Eyes[answer].GetComponent<Eye>().isAnswer = false;

        switch (phase)
        {
            case 1:
                button.SetEachButtons(false, true, false);
                break;
            
            case 2:
                Stair.enabled = true;
                break;
        }

        yield return null;
    }

    IEnumerator Fail()
    {
        display.HallLoad("1FHall", 0);
        Stair.enabled = false; 

        switch (day.failCount++)
        {
            case 1:
                Dialogue dialogue = new Dialogue() { sentences = new string[2] };
                dialogue.sentences[0] = "점점 몸이 피곤해져... :0";
                dialogue.sentences[1] = "마지막 기회인 것 같다. :-1";
                ChatManager.Instance.StartDialogue(dialogue);
                break;

            case 2:
                day.StartCoroutine(day.NextScene());
                break;
        }

        yield return null;
    }
}
