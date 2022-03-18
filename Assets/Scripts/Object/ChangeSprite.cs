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
        //클릭하면 스프라이트 제거 및 컬라이더 비활성화 후 자식 오브젝트 활성화
        //이때 isdisable이 false라면 비활성화 X

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(!isChange);
        }
        isChange = !isChange;
    }
}
