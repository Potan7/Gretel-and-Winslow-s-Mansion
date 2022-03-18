using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    displayManager display;

    public Text floorText;
    //층 변경 버튼 텍스트
    public GameObject[] UIList;
    // 0 - Map
    // 1 - Normal

    public GameObject[] Halls;
    //텔레포트할때 쓰일 복도 오브젝트

    public GameObject Postion;
    //위치 표시용 오브젝트의 상위오브젝트

    SpriteRenderer sprite;

    public Sprite[] sprites;
    //맵 이미지
    int _floor;

    public bool TeleportStop;

    private void Start()
    {
        display = FindObjectOfType<displayManager>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void HighlightMyPosition()
        //자신의 위치 하얗게 강조하는 함수.
    {
        string room;
        if (display.currectObject.name == "1FHall")
        {
            room = display.ChildObjects[display.roomnum].name;
            //1층 복도일 경우 하위 오브젝트이름을 가져옴.
        }
        else
        {
            room = display.currectObject.name;
            //1층 복도가 아닐경우 해당 오브젝트를 가져옴.
        }
        
        for (int i = 0; i < 13; i++)
        {
            Postion.transform.GetChild(i).gameObject.SetActive(false);
        }
        //모든 위치 오브젝트를 끈 다음

        if (_floor == display.floor)
        {
            Postion.transform.Find(room).gameObject.SetActive(true);
            //이후 이름에 맞추어 해당하는 위치 오브젝트를 킴.
            //만약 층이 다르면 표시 X
        }
    }

    void FloorLoad(int change)
        //해당 층으로 변경.
    {
        _floor = change;
        //층 변경
        sprite.sprite = sprites[_floor - 1];
        //스프라이트 변경.
        floorText.text = _floor + "F";
        //층 변경 버튼 텍스트 변경.
        HighlightMyPosition();
        //위치 표시.
        for (int i = 0; i < 2; i++)
        {
            Halls[i].SetActive(false);
        }
        Halls[_floor - 1].SetActive(true);
        //텔레포트 On
        //전부다 끄고 해당하는것만 키는 방식.
    }
    public void MapOpen()
        //맵 버튼 눌렀을 때
    {
        if (display.state != displayManager.State.normal) return;
        
        UIList[0].SetActive(true);
        UIList[1].SetActive(false);
        //기존의 버튼 Off, 맵 버튼 On
        sprite.enabled = true;
        //맵 스프라이트 띄우기.
        HighlightMyPosition();
        FloorLoad(display.floor);
        //층 불러오기 및 위치 표시.

        Interact.StopInteract = true;
    }

    public void MapClose()
        //맵 닫는 버튼 눌렀을 때.
    {
        sprite.enabled = false;
        UIList[0].SetActive(false);
        UIList[1].SetActive(true);
        //버튼 비활성화, 맵 스프라이트 가리기.

        _floor = 0;
        HighlightMyPosition();

        for (int i = 0; i < 2; i++)
        {
            Halls[i].SetActive(false);
        }
        //텔레포트 Off

        Interact.StopInteract = false;
    }

    public void MapChange()
        //맵 층 변경 버튼 눌렀을 때
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
        //작은 방에서 텔레포트로 나갔을때 아래버튼 제거용
    }
}
