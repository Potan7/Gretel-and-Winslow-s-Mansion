using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DollDisplayPuzzleContainer : MonoBehaviour
{
    public Image image;
    public int ID;

    private void Start()
    {
        image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void SetData(Sprite sprite, int _id)
    {
        ID = _id;
        image.sprite = sprite;
    }
}
