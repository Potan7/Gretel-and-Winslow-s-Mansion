using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInfo : MonoBehaviour
{
    public string MoveTo;
    //�̵��� ��ġ
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
    //Interact���� ȣ��Ǵ� Interact �Լ�
    {
        if (isHall)
            display.HallLoad(MoveTo, Hallnum, ChangeFloor);
        else
        {
            display.RoomLoad(MoveTo);
            
        }
        //display�� displayManager RoomLoad �Լ� ����.

        if (isDoor)
            SoundManager.Instance.PlaySound("Door_Open");
    }
}
