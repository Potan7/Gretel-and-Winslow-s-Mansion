using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderPuzzleTeaLeaf : MonoBehaviour
{
    BlenderPuzzle puzzle;

    private void Start()
    {
        puzzle = FindObjectOfType<BlenderPuzzle>();
    }

    void Interact()
    {
        puzzle.AddLeaf(transform.GetSiblingIndex());
    }
}
