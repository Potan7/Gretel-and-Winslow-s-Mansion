using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class displayManager : MonoBehaviour
{
    #region �ı�X
    private void Awake()
    {
        
        var obj = FindObjectsOfType<displayManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            AfterAwake();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    #endregion

    SpriteRenderer Sprite;
    Sprite[] sprites;
    //������ ��������Ʈ�� ����� ��.

    GameObject Room;
    GameObject[] Objects;
    //���ӳ� ���

    public GameObject currectObject;
    //���� ��
    public GameObject beforeRoom;
    //���� ��.
    public GameObject[] ChildObjects;
    //���� ���� ������Ʈ �迭.

    public int floor = 1;
    //���� ���� �����ִ� ��
    public int beforenum { get; set; }
    //���� ���� ��ȣ (���� ���� ���Ϳ�)

    public bool isOnlyOneWall { get; set; }

    public int roomnum;
    //���� ���� ��ȣ

    public string WillLoadRoomName { get; set; }

    public enum State
    {
        normal,
        zoom,
        changedview
        //���� Ȯ�ο�.
    };

    public bool isHall = false;

    public State state { get; set; }

    void AfterAwake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        roomnum = 0;
        WillLoadRoomName = "Lobby";
        //�κ񿡼� ����
        state = State.normal;
        //���� �Ϲ�
    }

    #region ���ε��ȣ��
    void OnEnable()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        NewPlace();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    void NewPlace()
    {
        Room = GameObject.Find("Room");
        GameObject[] Floors = new GameObject[Room.transform.childCount];
        int RoomCount = 0;
        for (int i = 0; i < Room.transform.childCount; i++)
        {
            Floors[i] = Room.transform.GetChild(i).gameObject;
            RoomCount += Floors[i].transform.childCount;
        }

        Objects = new GameObject[RoomCount];
        int k = 0;
        for (int i = 0; i < Floors.Length; i++)
        {
            for (int j = 0; j < Floors[i].transform.childCount; j++)
            {
                Objects[k++] = Floors[i].transform.GetChild(j).gameObject;
            }
        }
        floor = 1;
        RoomLoad(WillLoadRoomName);
    }

    public void displayMove(int dir)
    //ȭ�� �̵�.
    {
        if (beforenum == -1) return;

        if (roomnum == sprites.Length - 1 && dir > 0)
            roomnum = 0;
        else if (roomnum == 0 && dir < 0)
            roomnum = sprites.Length - 1;
        else
            roomnum += dir;
        //���� �ڸ��� ���̶�� �ݴ������� �̵�, �ƴ϶�� ��ĭ �̵�.

        Sprite.sprite = sprites[roomnum];
        ObjectChildLoad();
        //�̹��� �� ������Ʈ �ҷ�����.
    }

    public void ObjectChildLoad()
    //�ش� ������ ������Ʈ Ȱ��ȭ.
    {
        for (int i = 0; i < ChildObjects.Length; i++)
        {
            if (Sprite.sprite.name == ChildObjects[i].name)
            {
                ChildObjects[i].SetActive(true);
            }
            else
            {
                ChildObjects[i].SetActive(false);
            }
        }
        //��� ���� ������Ʈ�� �ѷ����� �´� ���� Ȱ��ȭ, �ƴϸ� ��Ȱ��ȭ.
    }

    //�� ���� �Լ�
    public void RoomLoad(string name)
    {
        beforenum = roomnum;
        Debug.Log("RommLoad " + name);
        isHall = false;
        //���� �ƴ�.
        sprites = Resources.LoadAll<Sprite>("Wall/" + name);
        roomnum = 0;
        Sprite.sprite = sprites[roomnum];
        //���� ����� ��������Ʈ�� �ҷ��� �����Ѵ�.

        ObjectLoad(name);



        if (currectObject.transform.childCount == 1)
        //������ �ϳ��ϰ�� �Ʒ���ư Ȱ��ȭ.
        {
            FindObjectOfType<ButtonFunction>().SetDownButton(true);
            isOnlyOneWall = true;
        }
        else
        {
            isOnlyOneWall = false;
        }

        ObjectChildLoad();
    }

    //������ ���ƾ��ٺ��� ��ȿ������ �κ��� ���� �� ����.
    //�� ���� ����� �ִٸ� �������� �ٲ��ּ���.

    //���� �ε� �Լ�.
    public void HallLoad(string name, int Hallnum, int Floor = 0)
    //�� ���� �� Ư�� ���鿡�� ����.
    {
        beforenum = roomnum;
        sprites = Resources.LoadAll<Sprite>("Hall/" + name);
        //���� ��������Ʈ�� �ҷ���.
        roomnum = Hallnum;
        //���� �������� �ҷ���.
        Sprite.sprite = sprites[roomnum];
        Debug.Log("���� " + name);
        ObjectLoad(name);
        if (Floor != 0)
        {
            floor = Floor;
        }
        //�� ���� �ִٸ� �� ����.
        isHall = true;
        //������� ����.
        ObjectChildLoad();
    }

    void ObjectLoad(string name)
    {
        for (int i = 0; i < Objects.Length; i++)
        {
            if (Objects[i].name == name)
            {
                beforeRoom = currectObject;
                currectObject = Objects[i];
                currectObject.SetActive(true);
            }
            else
            {
                Objects[i].SetActive(false);
            }
        }
        //�ش� ���� ������Ʈ Ȱ��ȭ.
        //��� �� ������Ʈ�� �ѷ����� �´°��� Ȱ��ȭ, �ƴѰ��� ��Ȱ��ȭ.

        ChildObjects = new GameObject[currectObject.transform.childCount];
        for (int i = 0; i < currectObject.transform.childCount; i++)
        {
            ChildObjects[i] = currectObject.transform.GetChild(i).gameObject;
        }
        //�ش� ���� ���� ������Ʈ �ҷ�����.
        //�迭�� ������ŭ Ȯ���ѵ� �ϳ��ϳ� ����ִ� ���.

    }
}
