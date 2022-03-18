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
            //ī�޶� ũ�� * ZoomPower (Ȯ��)
            Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);
            //ī�޶� ��ġ ���� ��ġ�� ������.
            display.state = displayManager.State.zoom;
            //���÷��� ���¸� Ȯ�� ���·� ����.
            button.SetDownButton(true);
            //�ٿ� ��ư�� On
            Box.enabled = false;
            //�浹 ���� ����
            button.ZoomObject = this;
            if (child != null) child.SetActive(true);
            //�ؿ� ������Ʈ�� ������ Ȱ��ȭ
        }
    }

    public void ZoomDisable()
    {
        if (child != null) child.SetActive(false);
        Box.enabled = true;
    }
}
