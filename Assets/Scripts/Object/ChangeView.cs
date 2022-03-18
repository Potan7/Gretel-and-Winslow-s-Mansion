using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{
    //�ڽ��� ������ ȭ���� ��ȯ�Ǵ� ������Ʈ
    //�ڽ��� �ڽ� ������Ʈ�� Ȱ��ȭ�� ������ ���
    //��ȣ�ۿ��� ��Ȱ��ȭ���� �����Ƿ� ����� �ٸ� ������Ʈ�鵵 ������ ������.
    //���� ȭ���� ���� ������Ʈ�� �ö��̴��� �ٿ� ���ƾ���.

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
        //display�� ���¸� changedview�� �ٲٰ� �ڽ� ������Ʈ Ȱ��ȭ
        //�Ʒ� ��ư Ȱ��ȭ �� button�� �ڽ� ���� (��Ȱ��ȭ��)
    }

    public void disable()
    {
        display.state = displayManager.State.normal;
        child.SetActive(false);
        //�Ʒ� ��ư �������� �ʱ�ȭ
    }
}
