using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManger : MonoBehaviour
{
    public GameObject[] Menu;
    // 1 - Menu
    // 2 - Check
    public bool GameisPaused = false;
    public bool ESCStop = false;
    public OptionManager option;
    enum YesButtonType { Exit, BackToTitle, Load }
    YesButtonType ButtonType;

    public Image LoadDescriptionText;
    public Image CreditImage;

    public Text CheckText;

    private void Update()
    //ESC ������
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !ESCStop)
        {
            if (GameisPaused)
            {
                Interact.StopInteract = false;
                SoundManager.Instance.SoundPause(true);

                Menu[0].SetActive(false);
                Menu[1].SetActive(false);
                Menu[2].SetActive(false);
                option.BackButton();
                Time.timeScale = 1f;
                GameisPaused = false;
            }
            //���� ��� �� ��ư ����
            else
            {
                Interact.StopInteract = true;
                SoundManager.Instance.SoundPause(false);

                Menu[0].SetActive(true);
                Menu[1].SetActive(true);
                Time.timeScale = 0f;
                GameisPaused = true;
            }
            //���� ���� �� ��ư Ȱ��ȭ
        }
    }

    public void BackTitleButton()
    {
        CheckText.text = "������ �����ðڽ��ϱ�?";
        Menu[1].SetActive(false);
        Menu[2].SetActive(true);
        ButtonType = YesButtonType.BackToTitle;
        //Back Title ��ư
    }

    public void ExitButton()
    {
        CheckText.text = "������ �����Ͻðڽ��ϱ�?";
        Menu[1].SetActive(false);
        Menu[2].SetActive(true);
        ButtonType = YesButtonType.Exit;
        //Exit ��ư
    }

    public void YesButton()
    {
        if (ButtonType == YesButtonType.BackToTitle)
        {
            GoTitle();
        }
        else if (ButtonType == YesButtonType.Exit) 
        {
            Gameoff();
        }
        else if (ButtonType == YesButtonType.Load)
        {
            GameManager.Instance.LoadGameData();
        }
    }

    public void NoButton()
    {
        Menu[2].SetActive(false);
        Menu[1].SetActive(true);
    }

    void GoTitle()
    {
        Interact.StopInteract = false;

        Menu[0].SetActive(false);
        Menu[1].SetActive(false);
        Menu[2].SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
        /*
        GameManager.Instance.SetProgress(0);
        Slot[] slots = FindObjectsOfType<Slot>();
        foreach (Slot slot in slots)
        {
            slot.RemoveItem();
        }
        */
        DestroyImmediate(GameManager.Instance.gameObject);
        DestroyImmediate(GameObject.Find("Canvas").gameObject);
        DestroyImmediate(GameObject.Find("display").gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    void Gameoff()
    {
        Debug.Log("����");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OptionButton()
    {
        option.SetOptionMenu(true);
    }

    public void CreditsButton()
    {
        CreditImage.gameObject.SetActive(true);
    }

    public void CreditsExitButton()
    {
        CreditImage.gameObject.SetActive(false);
    }

    public void LoadButton()
    {
        Interact.StopInteract = false;

        Menu[0].SetActive(false);
        Menu[1].SetActive(false);
        Menu[2].SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;

        int progress = GameManager.Instance.gameData.SceneProgress;

        DestroyImmediate(GameManager.Instance.gameObject);
        DestroyImmediate(GameObject.Find("Canvas").gameObject);
        DestroyImmediate(GameObject.Find("display").gameObject);

        SceneManager.LoadScene(progress);
    }

    public void LoadButtonEnter()
    {
        LoadDescriptionText.enabled = true;
    }

    public void LoadButtonExit()
    {
        LoadDescriptionText.enabled = false;
    }
}
