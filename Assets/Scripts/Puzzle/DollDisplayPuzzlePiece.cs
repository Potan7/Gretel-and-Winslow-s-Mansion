using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DollDisplayPuzzlePiece : MonoBehaviour, IDragHandler, IBeginDragHandler, IDropHandler, IEndDragHandler
{

    public Image image;
    public int ID;
    public DollDisplayPuzzleContainer container;
    public DollDisplayPuzzle puzzle;

    bool isDragging = false;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
        //��������
    {
        container.gameObject.SetActive(true);
        container.SetData(image.sprite, ID);
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
        //�巡�� �Ҷ�
    {
        if (isDragging)
        {
            container.transform.position = eventData.position;
        }
    }

    public void OnDrop(PointerEventData eventData)
        //���콺 �� ��
    {
        if (container.image.sprite != null)
        {
            Sprite tmpSprite = image.sprite;
            int tmpID = ID;

            SetData(container.image.sprite, container.ID);

            container.SetData(tmpSprite, tmpID);
        }
        else
        {
            container.image.sprite = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
        //���콺 �巡�� ���� (Drop ����)
    {
        if (isDragging)
        {
            if (container.image.sprite != null)
            {
                SetData(container.image.sprite, container.ID);
            }
            else
            {
                image.sprite = null;
            }
        }

        isDragging = false;

        container.SetData(null, -1);
        container.gameObject.SetActive(false);
        puzzle.MoveDoll();
    }

    void SetData(Sprite sprite, int _id)
    {
        ID = _id;
        image.sprite = sprite;
    }
}
