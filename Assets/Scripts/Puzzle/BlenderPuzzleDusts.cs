using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderPuzzleDusts : MonoBehaviour
{
    void Interact()
    {
        FindObjectOfType<BlenderPuzzle>().RemoveDust(transform.GetSiblingIndex());
        GetComponent<Item>().GetItem();
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
