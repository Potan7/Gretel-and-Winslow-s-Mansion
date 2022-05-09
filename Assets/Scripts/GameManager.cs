using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (GameManager)FindObjectOfType(typeof(GameManager));
                if (_instance == null)
                {
                    Debug.Log("활성화된 매니저 오브젝트가 없습니다.");
                }
            }
            return _instance;
        }
    }


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            AwakeAfter();
        }
    }
    #endregion

    #region 씬로드시호출
    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        NewLoad();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    displayManager display;

    public string GameDataFileName = "SaveData.json";

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    void AwakeAfter()
    {
        display = FindObjectOfType<displayManager>();

        LoadGameData();
    }

    public int SceneProgress { get; private set; }
    //씬의 진행상태
    public int Nighttime
    {
        get
        {
            return gameData.Nighttime;
        }
        set
        {
            gameData.Nighttime = value;
        }
    }
    //밤을 보낸 회수
    public int TakeOrder = 0;
    //헨젤의 지시를 따른 회수

    [SerializeField]
    private int progress = 0;
    public int Progress => progress;

    void NewLoad()
        //씬 로드시 호출되는 함수
    {
        SceneProgress = SceneManager.GetActiveScene().buildIndex;

        if (SceneProgress > 1)
            SaveGameData();
    }

    public void NextScene(string RoomName)
    {
        display.WillLoadRoomName = RoomName;
        SceneManager.LoadScene(SceneProgress + 1);
    }

    public void NextProgress(int _Progress = 1)
    {
        progress += _Progress;
    }

    public void SetProgress(int set)
    {
        progress = set;
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        if (File.Exists(filePath))
        {
            Debug.Log("불러오기 성공!");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
            TakeOrder = _gameData.TakeOrder;
        }
        else
        {
            Debug.Log("새로운 파일 생성");

            _gameData = new GameData();
            TakeOrder = 0;
            SceneProgress = 0;
        }
    }

    public void SaveGameData()
    {
        _gameData.TakeOrder = TakeOrder;
        gameData.SceneProgress = SceneProgress;

        OnlySaveFile();
    }

    public void OnlySaveFile()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("저장 완료");
    }

    public void ResetSaveFile()
    {
        _gameData = new GameData();

        OnlySaveFile();
        LoadGameData();
    }
}
