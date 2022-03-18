using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    Animator anim;
    bool isRing = false;

    private void Start()
    {
        GetComponent<Animator>();
    }
    
    public void PhoneButton()
    {
        if (!isRing) return;

        SoundManager.Instance.PlayRepeatSound();
        SoundManager.Instance.PlaySound("Phone_Open");
        //anim.SetBool("Ring", false);

        switch (GameManager.Instance.Progress)
        {
            case 7:
                GameObject.Find("PhoneDialogue").GetComponent<DialogueTrigger>().SendMessage("ActiveDialogue");
                Interact.StopInteract = false;
                break;
            
            case 8:
                GameObject.Find("Day3DayStart").GetComponent<Day3Day>().SendMessage("EraseCall");
                Interact.StopInteract = false;
                StopCoroutine("WaitRingCall");


                break;

            /*
            case 9:
                GameObject.Find("Day3DayStart").GetComponent<Day3Day>().SendMessage("Thanks");
                Interact.StopInteract = false;
                StopCoroutine(WaitRingCall());
                break;
            마지막 감사 전화 - 직접 받는것에서 이벤트로 수정.
            */

            case 12:
                GameObject.Find("SecretRoom").SendMessage("Interact");
                Interact.StopInteract = false;
                break;

        }
        isRing = false;
    }

    public void Ring()
    {
        SoundManager.Instance.PlayRepeatSound("Phone_Ring");
        isRing = true;
        //anim.SetBool("Ring", true);
    }

    public void StartWaitCoroutine()
    {
        StartCoroutine("WaitRingCall");
    }

    IEnumerator WaitRingCall()
    {
        yield return new WaitForSeconds(7f);
        SoundManager.Instance.PlayRepeatSound();
        isRing = false;
    }
}
