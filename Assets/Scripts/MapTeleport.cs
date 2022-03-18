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
        //마우스 올라갈 때 실행.
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
        //마우스 나갈 때 실행
    {
        sprite.color = new Color(255, 255, 255, 0);
    }

    private void OnMouseDown()
        //마우스 눌렀을 때 실행.
    {
        if (map.TeleportStop) return;
        //텔레포트 중지시 텔레포트 X

        Debug.Log(MoveTo + HallNum + "로 텔레포트");
        GameObject.Find("display").GetComponent<displayManager>().HallLoad(MoveTo, HallNum, Floor);
        map.MapTeleportSet();
    }
}
