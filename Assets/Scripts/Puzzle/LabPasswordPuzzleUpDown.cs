using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabPasswordPuzzleUpDown : MonoBehaviour
{
    public bool UpDown;
    LabPasswordPuzzleWord puzzleWord;

    private void Start()
    {
        puzzleWord = transform.parent.GetComponent<LabPasswordPuzzleWord>();
    }

    void Interact()
    {
        puzzleWord.MoveAlpabet(UpDown);
    }
}
