using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public MenuManger menu;
    bool isOptionOn = false;

    public GameObject[] menuUI;
    // 0 - 배경
    // 1 - 설정메뉴
    // 2 - 확인메뉴

    public Text resolutionText;
    public Text FullScreenText;

    int isFullscreen;
    /// <summary>
    /// 960 * 540
    /// 1280 * 720
    /// 1440 * 810
    /// 1600 * 900
    /// 1760 * 990
    /// 1920 * 1080
    /// </summary>

    int[,] resolList;
    int width;
    int height;
    int pos;

    public Slider EffectSlider;
    public Slider BGMSlider;

    float EffectVolume;
    float BGMVolume;

    private void Start()
    {
        resolList = new int[6, 2];

        resolList[0, 0] = 960;
        resolList[1, 0] = 1280;
        resolList[2, 0] = 1440;
        resolList[3, 0] = 1600;
        resolList[4, 0] = 1760;
        resolList[5, 0] = 1920;

        for (int i = 0; i < 6; i++)
        {
            resolList[i, 1] = resolList[i, 0] / 16 * 9;
        }

        if (PlayerPrefs.HasKey("ResH") == false)
        {
            width = resolList[5, 0];
            height = resolList[5, 1];
            pos = 5;
            isFullscreen = 0;
            EffectVolume = 1.0f;
            BGMVolume = 1.0f;

            PlayerPrefs.SetInt("ResW", width);
            PlayerPrefs.SetInt("ResH", height);
            PlayerPrefs.SetInt("Pos", pos);
            PlayerPrefs.SetInt("isFull", isFullscreen);
            PlayerPrefs.SetFloat("Effect", EffectVolume);
            PlayerPrefs.SetFloat("BGM", BGMVolume);
        }

        width = PlayerPrefs.GetInt("ResW");
        height = PlayerPrefs.GetInt("ResH");
        pos = PlayerPrefs.GetInt("Pos");
        isFullscreen = PlayerPrefs.GetInt("isFull");
        EffectVolume = PlayerPrefs.GetFloat("Effect");
        BGMVolume = PlayerPrefs.GetFloat("BGM");

        setResolution();
        ChangePos();
        SetFullScreen();
        SetSlider();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isOptionOn)
        {
            BackButton();
        }
    }

    public void SetOptionMenu(bool OnOff)
    {
        for (int i = 0; i < 2; i++)
        {
            menuUI[i].SetActive(OnOff);
        }
        menu.ESCStop = OnOff;
        isOptionOn = OnOff;
    }

    public void ResolutionRight()
    {
        if (pos != 5)
        {
            pos++;
            ChangePos();
        }
    }

    public void ResolutionLeft()
    {
        if (pos != 0)
        {
            pos--;
            ChangePos();
        }
    }

    public void BackButton()
    {
        if (isChange())
        {
            OnCheckMenu();
        }
        else
        {
            SetOptionMenu(false);
        }
    }

    void ChangePos()
    {
        width = resolList[pos, 0];
        height = resolList[pos, 1];
        string text = width.ToString() + "x" + height.ToString();
        resolutionText.text = text;
    }

    public void setResolution()
        // 해상도 적용 -> 적용 버튼으로 범위가 커졌으나 이름은 변경하지 않음.
    {
        PlayerPrefs.SetInt("ResW", width);
        PlayerPrefs.SetInt("ResH", height);
        PlayerPrefs.SetInt("Pos", pos);
        PlayerPrefs.SetInt("isFull", isFullscreen);
        Screen.SetResolution(width, height, isFullscreen == 1 ? true : false);

        PlayerPrefs.SetFloat("Effect", EffectVolume);
        PlayerPrefs.SetFloat("BGM", BGMVolume);
        SetSlider();

        SoundManager.Instance.SoundChange(EffectVolume, BGMVolume);
    }

    public void FullScreenRight()
    {
        if (isFullscreen == 0)
        {
            isFullscreen = 1;

        }
        else if (isFullscreen == 1)
        {
            isFullscreen = 0;
        }
        SetFullScreen();
    }

    public void FullScreenLeft()
    {
        if (isFullscreen == 0)
        {
            isFullscreen = 1;

        }
        else if (isFullscreen == 1)
        {
            isFullscreen = 0;
        }
        SetFullScreen();
    }

    void SetFullScreen()
    {
        if (isFullscreen == 0)
        {
            FullScreenText.text = "ON";
        }
        else
        {
            FullScreenText.text = "OFF";
        }
    }

    bool isChange()
    {
        if (isFullscreen != PlayerPrefs.GetInt("isFull")) return true;
        if (pos != PlayerPrefs.GetInt("Pos")) return true;
        if (EffectVolume != PlayerPrefs.GetFloat("Effect")) return true;
        if (BGMVolume != PlayerPrefs.GetFloat("BGM")) return true;

        return false;
    }

    void OnCheckMenu()
    {
        menuUI[0].SetActive(false);
        menuUI[1].SetActive(false);
        menuUI[2].SetActive(true);
    }

    public void CheckMenuNoButton()
    {
        isFullscreen = PlayerPrefs.GetInt("isFull");
        pos = PlayerPrefs.GetInt("Pos");
        EffectVolume = PlayerPrefs.GetFloat("Effect");
        BGMVolume = PlayerPrefs.GetFloat("BGM");
        SetFullScreen();
        SetSlider();
        ChangePos();
        SetOptionMenu(false);
        menuUI[2].SetActive(false);
    }

    public void CheckMenuYesButton()
    {
        setResolution();
        SetOptionMenu(false);
        menuUI[2].SetActive(false);
    }

    public void ChangeEffect()
    {
        EffectVolume = EffectSlider.value;
    }

    public void ChangeBGM()
    {
        BGMVolume = BGMSlider.value;
    }

    void SetSlider()
    {
        EffectSlider.value = EffectVolume;
        BGMSlider.value = BGMVolume;
    }
}
