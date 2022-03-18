using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugFunction : MonoBehaviour
{
    public GameObject debug;
    public Text[] texts;
    public Slider[] sliders;
    public Toggle Day3Mission;
    public Button Accept;

    public string[] sceneNames;
    int day3Mission = 0;

    private void Start()
    {
        debug.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && debug.activeSelf == false)
        {
            debug.SetActive(true);
        }
    }

    public void SceneSlider()
    {
        int value = (int)sliders[0].value;

        texts[0].text = sceneNames[value];

        Day3Mission.gameObject.SetActive(false);

        switch (value)
        {
            case 0:
                //1老瞒
                setMaxValue(0, 0);
                break;
            case 1:
                //1老瞒 广
                setMaxValue(0, 0);
                break;
            case 2:
                //2老瞒 撤
                setMaxValue(0, 1);
                break;
            case 3:
                //2老瞒 广
                setMaxValue(2, 1);
                break;
            case 4:
                //3老瞒 撤
                setMaxValue(2, 2);
                break;
            case 5:
                //3老瞒 广
                setMaxValue(3, 2);
                break;
            case 6:
                //4老瞒
                setMaxValue(3, 3);
                Day3Mission.gameObject.SetActive(true);
                break;
        }
    }

    void setMaxValue(int v1, int v2)
    {
        sliders[1].maxValue = v1;
        sliders[2].maxValue = v2;
    }

    public void nightSlider()
    {
        texts[1].text = sliders[1].value.ToString();

        if (sliders[1].value == 3)
        {
            Day3Mission.isOn = true;
            sliders[2].value = 3;
            texts[2].text = sliders[2].value.ToString();
            day3Toggle();
        }
    }

    public void orderSlider()
    {
        texts[2].text = sliders[2].value.ToString();

        if (sliders[2].value == 3)
        {
            Day3Mission.isOn = true;
            sliders[1].value = 3;
            texts[1].text = sliders[1].value.ToString();
            day3Toggle();
        }
    }

    public void day3Toggle()
    {
        day3Mission = Day3Mission.isOn ? 2 : 0;
    }

    public void accept()
    {
        GameData game = GameManager.Instance._gameData;

        game.SceneProgress = (int) sliders[0].value + 3;
        game.Nighttime = (int)sliders[1].value;
        game.TakeOrder = (int)sliders[2].value;
        game.condition1 = day3Mission;

        GameManager.Instance._gameData = game;
        GameManager.Instance.OnlySaveFile();

        debug.SetActive(false);
    }
}
