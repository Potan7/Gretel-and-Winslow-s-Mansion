using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollDisplayPuzzle : MonoBehaviour
{
    public SpriteRenderer[] spriteDolls;
    public DollDisplayPuzzlePiece[] PuzzlePieces;

    int trashbin = 0;

    public Dialogue dialogue;

    public void MoveDoll()
    {
        int check = 0;
        for (int i = 0; i < 9; i++)
        {
            spriteDolls[i].sprite = PuzzlePieces[i].image.sprite;

            if (PuzzlePieces[i].ID == i)
            {
                check++;
            }
        }
        if (check > 8)
        {
            Completed();
        }
    }

    public void Trashbin()
    {
        trashbin++;

        if (trashbin > 2)
        {
            GameObject.Find("Trashbin").GetComponent<BoxCollider2D>().enabled = false;
            FindObjectOfType<ButtonFunction>().DownButton();
            GameManager.Instance.NextProgress();
            ProgressCheck();
        }
    }

    void Completed()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        FindObjectOfType<ButtonFunction>().DownButton();
        GameManager.Instance.NextProgress();
        ChatManager.Instance.SearchChat("장식장 정리를 완료했다");
        ProgressCheck();
    }

    void ProgressCheck()
    {
        if (GameManager.Instance.Progress > 2)
        {
            FindObjectOfType<TodoManager>().ToDoCheck(1);
            StartCoroutine(NextDo());
        }
    }

    IEnumerator NextDo()
    {
        yield return new WaitForSeconds(1f);

        ChatManager.Instance.StartDialogue(dialogue);
    }
}
