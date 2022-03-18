using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Inventory inventory;
    public static bool StopInteract = false;

    GameObject fade;

    private void Start()
    {
        fade = FindObjectOfType<ButtonFunction>().GetFade().gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            //���콺 ��Ŭ����
            //���� �޴��� UI�� �޴� �ڿ� �ִ� ������Ʈ�� Ŭ���Ǿ ���������϶� �۵� ����.
        {
            //Debug.Log("Ŭ�� ������");
            if (ChatManager.Instance.chatState == ChatManager.ChatState.Search)
            {
                ChatManager.Instance.SearchOff();
                //���� ����޼��� ���¶�� ����޼��� ����.
            }

            else if (ChatManager.Instance.chatState == ChatManager.ChatState.Chat)
            {
                ChatManager.Instance.DisplayNextSentence();
                //���� ��ȭ���¶�� ���� �޼����� �̵�.
            }

            else if (fade.activeSelf) return;

            else if (inventory.SelectedSlotId != 0 && !StopInteract)
                //���� �������� ������ ���¶��.
            {
                Vector2 rayPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(rayPostion, Vector2.zero, 10f);
                //����ĳ��Ʈ ����

                if (hit && hit.transform.GetComponent<UseItem>() != null)
                {
                    hit.transform.gameObject.SendMessage("ItemUse", inventory.SelectedSlotId);
                    //UseItem ��ũ��Ʈ�� ���� ��ü�� ������ ItemUse ����.
                }
                inventory.SelectedItem(-1, 0);
                //������ ������ ���.
            }

            else if (!StopInteract)
            {
                Vector2 rayPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(rayPostion, Vector2.zero, 10f);
                //���콺 ��ġ�� ����ĳ��Ʈ ����.

                if (hit && hit.transform.CompareTag("Interactable"))
                {
                    //Debug.Log("�� ������ Interactable��.");
                    hit.transform.gameObject.SendMessage("Interact");
                    //����ĳ��Ʈ ���� ����� Interactable �±��� ���
                    //�ش� ������Ʈ�� Interact �Լ� ����.
                }
            }
        }
    }

    public void SetStopInteract(bool OnOff)
    {
        StopInteract = OnOff;
    }
}
