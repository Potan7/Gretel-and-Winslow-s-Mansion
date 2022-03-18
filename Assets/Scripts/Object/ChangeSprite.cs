using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxcollider;

    displayManager display;

    public bool isdisable = true;
    public bool isChange = false;

    private void Start()
    {
        display = FindObjectOfType<displayManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
    }

    void Interact()
    {
        if (display.state == displayManager.State.zoom) return;
           
        spriteRenderer.enabled = isChange;
        if (isdisable)
            boxcollider.enabled = isChange;
        //Ŭ���ϸ� ��������Ʈ ���� �� �ö��̴� ��Ȱ��ȭ �� �ڽ� ������Ʈ Ȱ��ȭ
        //�̶� isdisable�� false��� ��Ȱ��ȭ X

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(!isChange);
        }
        isChange = !isChange;
    }
}
