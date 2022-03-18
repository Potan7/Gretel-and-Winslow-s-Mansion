using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{
    //자신을 누르면 화면이 전환되는 오브젝트
    //자신의 자식 오브젝트를 활성화해 덮어씌우는 방식
    //상호작용을 비활성화하지 않으므로 방안의 다른 오브젝트들도 여전히 반응함.
    //따라서 화면을 덮을 오브젝트에 컬라이더를 붙여 막아야함.

    displayManager display;
    ButtonFunction button;

    GameObject child;

    private void Start()
    {
        child = transform.GetChild(0).gameObject;
        display = FindObjectOfType<displayManager>();
        button = FindObjectOfType<ButtonFunction>();
    }

    void Interact()
    {
        child.SetActive(true);
        display.state = displayManager.State.changedview;
        button.ChangeObject = this;
        button.SetDownButton(true);
        //display의 상태를 changedview로 바꾸고 자식 오브젝트 활성화
        //아래 버튼 활성화 및 button에 자신 대입 (비활성화용)
    }

    public void disable()
    {
        display.state = displayManager.State.normal;
        child.SetActive(false);
        //아래 버튼 눌렀을때 초기화
    }
}
