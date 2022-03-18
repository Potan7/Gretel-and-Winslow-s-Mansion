using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumDollPuzzleHand : MonoBehaviour
{
    public bool isRight = false;
    DrumDollPuzzle puzzle;

    private void Start()
    {
        puzzle = transform.parent.GetComponent<DrumDollPuzzle>();
    }

    void Interact()
    {
        puzzle.UseHand(isRight);
    }
}
