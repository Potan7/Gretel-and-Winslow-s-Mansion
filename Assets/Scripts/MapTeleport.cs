using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTeleport : MonoBehaviour
{
    SpriteRenderer sprite;
    MapManager map;

    public string MoveTo;
    public int HallNum;
    public int Floor;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        map = GameObject.Find("MapManager").GetComponent<MapManager>();
    }
    private void OnMouseEnter()
        //���콺 �ö� �� ����.
    {
        if (map.TeleportStop)
        {
            sprite.color = new Color(255, 0, 0, 1);
        }
        else
        {
            sprite.color = new Color(255, 255, 255, 1);
        }
    }

    private void OnMouseExit()
        //���콺 ���� �� ����
    {
        sprite.color = new Color(255, 255, 255, 0);
    }

    private void OnMouseDown()
        //���콺 ������ �� ����.
    {
        if (map.TeleportStop) return;
        //�ڷ���Ʈ ������ �ڷ���Ʈ X

        Debug.Log(MoveTo + HallNum + "�� �ڷ���Ʈ");
        GameObject.Find("display").GetComponent<displayManager>().HallLoad(MoveTo, HallNum, Floor);
        map.MapTeleportSet();
    }
}
