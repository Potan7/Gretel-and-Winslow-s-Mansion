using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovePuzzleBottle : MonoBehaviour
{
    public int WaterAmount;
    public int WaterMax;

    SpriteRenderer Water;

    public Sprite[] WaterStatus;

    public float myPosition { get; private set; }

    WaterMovePuzzle puzzle;

    private void Start()
    {
        Water = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        puzzle = FindObjectOfType<WaterMovePuzzle>();
        myPosition = transform.position.y;
    }

    void Interact()
    {
        if (!puzzle.IsComplete)
            puzzle.SelectBottle(this);
    }

    public void WaterUpdate()
    {
        Water.sprite = WaterStatus[WaterAmount];
    }
}
