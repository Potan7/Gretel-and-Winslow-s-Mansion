using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabPasswordPuzzleWord : MonoBehaviour
{
    int ID;
    LabPasswordPuzzle puzzle;
    SpriteRenderer sprite;

    public Sprite[] sprites;
    public int position = 0;

    private void Start()
    {
        puzzle = transform.parent.GetComponent<LabPasswordPuzzle>();
        ID = transform.GetSiblingIndex();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void MoveAlpabet(bool UpDown)
    {
        if (UpDown)
        {
            if (position == 25)
            {
                position = 0;
            }
            else
            {
                position++;
            }
        }
        else
        {
            if (position == 0)
            {
                position = 25;
            }
            else
            {
                position--;
            }
        }
        sprite.sprite = sprites[position];
        puzzle.CheckStatus(ID, position);
    }
}
