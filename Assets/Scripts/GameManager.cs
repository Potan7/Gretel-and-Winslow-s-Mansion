using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    #region �̱���
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
                    Debug.Log("Ȱ��ȭ�� �Ŵ��� ������Ʈ�� �����ϴ�.");
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

    #region ���ε��ȣ��
    void OnEnable()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
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
    //���� �������
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
    //���� ���� ȸ��
    public int TakeOrder = 0;
    //������ ���ø� ���� ȸ��

    [SerializeField]
    private int progress = 0;
    public int Progress => progress;

    void NewLoad()
        //�� �ε�� ȣ��Ǵ� �Լ�
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
            Debug.Log("�ҷ����� ����!");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
            TakeOrder = _gameData.TakeOrder;
        }
        else
        {
            Debug.Log("���ο� ���� ����");

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
        Debug.Log("���� �Ϸ�");
    }

    public void ResetSaveFile()
    {
        _gameData = new GameData();

        OnlySaveFile();
        LoadGameData();
    }
}
