using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BottlePuzzleNameTag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
    public DollDisplayPuzzleContainer container;
    public BottlePuzzle puzzle;
    public int ID;
    public int StoredID;
    bool isDragging = false;
    public bool isBottle = false;

    public Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isBottle) return;

        container.gameObject.SetActive(true);
        container.SetData(image.sprite, ID);
        image.enabled = false;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            container.transform.position = eventData.position;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!isBottle) return;
        if (container.image.sprite == null) return;

        StoredID = container.ID;
        image.sprite = container.image.sprite;
        container.SetData(null, -1);
        image.color = new Color(255, 255, 255, 1);
        puzzle.Addlabel(ID, ID == StoredID);
        SoundManager.Instance.PlaySound("Sticker");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            if (container.ID == -1)
            {
                gameObject.SetActive(false);
            }
        }

        image.enabled = true;
        isDragging = false;
        container.SetData(null, -1);
        container.gameObject.SetActive(false);
    }
}
