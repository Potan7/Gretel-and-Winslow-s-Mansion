using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInfo : MonoBehaviour
{
    public string MoveTo;
    //이동할 위치
    public int ChangeFloor = 0;
    public int Hallnum;
    public bool isHall = false;
    public bool isDoor = true;


    displayManager display;

    private void Start()
    {
        display = GameObject.Find("display").GetComponent<displayManager>();
    }

    void Interact()
    //Interact에서 호출되는 Interact 함수
    {
        if (isHall)
            display.HallLoad(MoveTo, Hallnum, ChangeFloor);
        else
        {
            display.RoomLoad(MoveTo);
            
        }
        //display의 displayManager RoomLoad 함수 실행.

        if (isDoor)
            SoundManager.Instance.PlaySound("Door_Open");
    }
}
