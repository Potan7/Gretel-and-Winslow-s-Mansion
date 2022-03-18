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
            //마우스 왼클릭시
            //중지 메뉴가 UI라 메뉴 뒤에 있는 오브젝트가 클릭되어서 중지상태일땐 작동 중지.
        {
            //Debug.Log("클릭 감지함");
            if (ChatManager.Instance.chatState == ChatManager.ChatState.Search)
            {
                ChatManager.Instance.SearchOff();
                //만약 조사메세지 상태라면 조사메세지 끄기.
            }

            else if (ChatManager.Instance.chatState == ChatManager.ChatState.Chat)
            {
                ChatManager.Instance.DisplayNextSentence();
                //만약 대화상태라면 다음 메세지로 이동.
            }

            else if (fade.activeSelf) return;

            else if (inventory.SelectedSlotId != 0 && !StopInteract)
                //만약 아이템을 선택한 상태라면.
            {
                Vector2 rayPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(rayPostion, Vector2.zero, 10f);
                //레이캐스트 실행

                if (hit && hit.transform.GetComponent<UseItem>() != null)
                {
                    hit.transform.gameObject.SendMessage("ItemUse", inventory.SelectedSlotId);
                    //UseItem 스크립트를 가진 물체와 닿으면 ItemUse 실행.
                }
                inventory.SelectedItem(-1, 0);
                //선택한 아이템 취소.
            }

            else if (!StopInteract)
            {
                Vector2 rayPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(rayPostion, Vector2.zero, 10f);
                //마우스 위치에 레이캐스트 실행.

                if (hit && hit.transform.CompareTag("Interactable"))
                {
                    //Debug.Log("이 물건은 Interactable함.");
                    hit.transform.gameObject.SendMessage("Interact");
                    //레이캐스트 맞춘 대상이 Interactable 태그일 경우
                    //해당 오브젝트의 Interact 함수 실행.
                }
            }
        }
    }

    public void SetStopInteract(bool OnOff)
    {
        StopInteract = OnOff;
    }
}
