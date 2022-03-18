using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderPuzzle : MonoBehaviour
{
    Inventory inventory;
    ButtonFunction button;
    Interact interact;

    public Transform Blender;
    public Transform Leafs;
    public GameObject Mix;
    public BoxCollider2D useItem;
    GameObject[] Dusts;

    public bool isComplete = false;
    bool Mixing = false;

    List<int> Inside = new List<int>();

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        button = FindObjectOfType<ButtonFunction>();
        interact = FindObjectOfType<Interact>();

        Dusts = new GameObject[Blender.childCount];
        for (int i = 0; i < Blender.childCount; i++)
        {
            Dusts[i] = Blender.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if (inventory.SelectedSlotId != 0)
        {
            useItem.enabled = true;
        }
        else
        {
            useItem.enabled = false;
        }
    }

    void Interact()
    {
        if (!Mixing && Inside.Count > 0)
        {
            StartCoroutine(StartMixing());
        }
    }

    IEnumerator StartMixing()
    {
        Mixing = true;
        interact.SetStopInteract(true);
        button.SetEachButtons(false, false, false);
        for (int i = 0; i < Dusts.Length; i++)
        {
            Dusts[i].SetActive(false);
        }
        Mix.SetActive(true);
        SoundManager.Instance.PlaySound("BlenderSound");
        yield return new WaitForSeconds(4.6f);
        Mix.SetActive(false);

        int result = 0;

        if (MixingCheck(20, 2, 1, 2))
        {
            result = 3;
        }
        else if (MixingCheck(21, 2, 0, 1))
        {
            result = 4;
        }
        else if (MixingCheck(22, 3, 0, 1, 2))
        {
            result = 5;
        }
        else if (MixingCheck(23, 3, 3, 4, 5))
        {
            result = 6;
            isComplete = true;
        }
        else if (Inside.Contains(8))
        {
            result = 7;
            GameObject.Find("OvenDoor").GetComponent<BoxCollider2D>().enabled = false;
        }

        Inside.Clear();
        if (result != 0)
        {
            Dusts[result].SetActive(true);
            Inside.Add(result);
        }

        button.SetEachButtons(false, false, true);
        interact.SetStopInteract(false);
        Mixing = false;
    }

    bool MixingCheck(int resultID, int Count, int id1, int id2, int id3 = 0)
    {
        if (Inside.Count == Count && !inventory.ItemCheck(resultID))
        {
            if (Inside.Contains(id1) && Inside.Contains(id2))
            {
                if (Count == 3 && Inside.Contains(id3))
                {
                    return true;
                }
                else if (Count == 2) return true;
            }
        }
        return false;
    }

    public void AddLeaf(int position)
        //ÂþÀÙ ÁÖ°¡
    {
        if (isComplete) return;

        AddToBlender(position);
    }

    public void AddToBlender(int id)
        //Àç·á Ãß°¡
    {
        if (Mixing) return;

        if (!Inside.Contains(id))
        {
            Dusts[id].SetActive(true);
            Inside.Add(id);
        }
    }

    public void RemoveDust(int ID)
    {
        Dusts[ID].SetActive(false);
        Inside.Remove(ID);
    }

    private void OnEnable()
        //ÂþÀÙ Ä«¿îÅÍ À§·Î ¿Ã¸®±â
    {
        for (int i = 0; i < 3; i++)
        {
            if (inventory.ItemCheck(i + 17))
            {
                Leafs.transform.GetChild(i).gameObject.SetActive(true);
                inventory.DeleteItem(i + 17);
            }
        }
    }
}
