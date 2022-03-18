using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInObject : MonoBehaviour
{
    public float ZoomPower = 0.5f;
    displayManager display;
    ButtonFunction button;
    BoxCollider2D Box;

    GameObject child;

    void Start()
    {
        display = FindObjectOfType<displayManager>();
        button = FindObjectOfType<ButtonFunction>();
        Box = GetComponent<BoxCollider2D>();

        if (transform.childCount > 0)
        {
            child = transform.GetChild(0).gameObject;
        }
    }

    void Interact()
    {
        if (display.state == displayManager.State.normal)
        {
            Camera.main.orthographicSize *= ZoomPower;
            //카메라 크기 * ZoomPower (확대)
            Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
            //카메라 위치 줌인 위치로 재조정.
            display.state = displayManager.State.zoom;
            //디스플레이 상태를 확대 상태로 변경.
            button.SetDownButton(true);
            //다운 버튼만 On
            Box.enabled = false;
            //충돌 판정 제거
            button.ZoomObject = this;
            if (child != null) child.SetActive(true);
            //밑에 오브젝트가 있으면 활성화
        }
    }

    public void ZoomDisable()
    {
        if (child != null) child.SetActive(false);
        Box.enabled = true;
    }
}
