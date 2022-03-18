using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabPuzzleBottle : MonoBehaviour
{
    LabPuzzle puzzle;

    public int ID;
    public float y;

    public SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        puzzle = FindObjectOfType<LabPuzzle>();
        y = transform.position.y;
    }

    void Interact()
    {
        if (puzzle.isComplete) return;

        puzzle.SelecetBottle(this);
    }
}
