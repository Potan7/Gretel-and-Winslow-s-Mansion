using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    displayManager display;

    public Text floorText;
    //�� ���� ��ư �ؽ�Ʈ
    public GameObject[] UIList;
    // 0 - Map
    // 1 - Normal

    public GameObject[] Halls;
    //�ڷ���Ʈ�Ҷ� ���� ���� ������Ʈ

    public GameObject Postion;
    //��ġ ǥ�ÿ� ������Ʈ�� ����������Ʈ

    SpriteRenderer sprite;

    public Sprite[] sprites;
    //�� �̹���
    int _floor;

    public bool TeleportStop;

    private void Start()
    {
        display = FindObjectOfType<displayManager>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void HighlightMyPosition()
        //�ڽ��� ��ġ �Ͼ�� �����ϴ� �Լ�.
    {
        string room;
        if (display.currectObject.name == "1FHall")
        {
            room = display.ChildObjects[display.roomnum].name;
            //1�� ������ ��� ���� ������Ʈ�̸��� ������.
        }
        else
        {
            room = display.currectObject.name;
            //1�� ������ �ƴҰ�� �ش� ������Ʈ�� ������.
        }
        
        for (int i = 0; i < 13; i++)
        {
            Postion.transform.GetChild(i).gameObject.SetActive(false);
        }
        //��� ��ġ ������Ʈ�� �� ����

        if (_floor == display.floor)
        {
            Postion.transform.Find(room).gameObject.SetActive(true);
            //���� �̸��� ���߾� �ش��ϴ� ��ġ ������Ʈ�� Ŵ.
            //���� ���� �ٸ��� ǥ�� X
        }
    }

    void FloorLoad(int change)
        //�ش� ������ ����.
    {
        _floor = change;
        //�� ����
        sprite.sprite = sprites[_floor - 1];
        //��������Ʈ ����.
        floorText.text = _floor + "F";
        //�� ���� ��ư �ؽ�Ʈ ����.
        HighlightMyPosition();
        //��ġ ǥ��.
        for (int i = 0; i < 2; i++)
        {
            Halls[i].SetActive(false);
        }
        Halls[_floor - 1].SetActive(true);
        //�ڷ���Ʈ On
        //���δ� ���� �ش��ϴ°͸� Ű�� ���.
    }
    public void MapOpen()
        //�� ��ư ������ ��
    {
        if (display.state != displayManager.State.normal) return;
        
        UIList[0].SetActive(true);
        UIList[1].SetActive(false);
        //������ ��ư Off, �� ��ư On
        sprite.enabled = true;
        //�� ��������Ʈ ����.
        HighlightMyPosition();
        FloorLoad(display.floor);
        //�� �ҷ����� �� ��ġ ǥ��.

        Interact.StopInteract = true;
    }

    public void MapClose()
        //�� �ݴ� ��ư ������ ��.
    {
        sprite.enabled = false;
        UIList[0].SetActive(false);
        UIList[1].SetActive(true);
        //��ư ��Ȱ��ȭ, �� ��������Ʈ ������.

        _floor = 0;
        HighlightMyPosition();

        for (int i = 0; i < 2; i++)
        {
            Halls[i].SetActive(false);
        }
        //�ڷ���Ʈ Off

        Interact.StopInteract = false;
    }

    public void MapChange()
        //�� �� ���� ��ư ������ ��
    {
        if (_floor == 1)
        {
            FloorLoad(2);
        }
        else
        {
            FloorLoad(1);
        }
    }

    public void MapTeleportSet()
    {
        HighlightMyPosition();
        FindObjectOfType<ButtonFunction>().SetDownButton(false);
        //���� �濡�� �ڷ���Ʈ�� �������� �Ʒ���ư ���ſ�
    }
}
