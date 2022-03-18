using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvenLever : MonoBehaviour
{
    ChangeSprite lever;
    public GameObject door;

    public GameObject dust;
    public GameObject key;

    bool clear = false;

    private void Start()
    {
        lever = transform.GetChild(0).gameObject.GetComponent<ChangeSprite>();
    }

    void Interact()
    {
        if (door.activeSelf == true)
        {
            ChatManager.Instance.SearchChat("오븐 문을 먼저 닫아야한다.");
        }
        else if (clear == true)
        {
            lever.SendMessage("Interact");
            StartCoroutine(waitforOpen());
        }
        else
        {
            lever.SendMessage("Interact");
            dust.SetActive(false);
            key.SetActive(true);
            clear = true;
            StartCoroutine(waitforOpen());
        }
    }

    public void dustIn()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    IEnumerator waitforOpen()
    {
        yield return new WaitWhile(() => door.activeSelf == false);

        lever.SendMessage("Interact");
    }
}
