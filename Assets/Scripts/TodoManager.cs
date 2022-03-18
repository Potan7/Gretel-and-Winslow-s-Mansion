using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoManager : MonoBehaviour
{
    GameObject ToDoList;
    GameObject[] Checks;

    private void Start()
    {
        ToDoList = gameObject.transform.GetChild(0).gameObject;
        GameObject CheckParent = ToDoList.transform.Find("Checks").gameObject;
        Checks = new GameObject[CheckParent.transform.childCount];

        for (int i = 0; i < CheckParent.transform.childCount; i++)
        {
            Checks[i] = CheckParent.transform.GetChild(i).gameObject;
        }
    }

    public void ToDoOpen()
    {
        ToDoList.SetActive(true);
        Interact.StopInteract = true;
    }

    public void ToDoCheck(int index)
    {
        Checks[index].SetActive(true);
    }

    public void ToDoExitButton()
    {
        ToDoList.SetActive(false);
        Interact.StopInteract = false;
    }
}
