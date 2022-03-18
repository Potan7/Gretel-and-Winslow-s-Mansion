using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class displayManager : MonoBehaviour
{
    #region 파괴X
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
    //벽면의 스프라이트가 저장될 곳.

    GameObject Room;
    GameObject[] Objects;
    //게임내 방들

    public GameObject currectObject;
    //현재 방
    public GameObject beforeRoom;
    //이전 방.
    public GameObject[] ChildObjects;
    //방의 벽면 오브젝트 배열.

    public int floor = 1;
    //현재 층을 보여주는 수
    public int beforenum { get; set; }
    //이전 방의 번호 (복도 시점 복귀용)

    public bool isOnlyOneWall { get; set; }

    public int roomnum;
    //현재 방의 번호

    public string WillLoadRoomName { get; set; }

    public enum State
    {
        normal,
        zoom,
        changedview
        //상태 확인용.
    };

    public bool isHall = false;

    public State state { get; set; }

    void AfterAwake()
    {
        Sprite = GetComponent<SpriteRenderer>();
        roomnum = 0;
        WillLoadRoomName = "Lobby";
        //로비에서 시작
        state = State.normal;
        //상태 일반
    }

    #region 씬로드시호출
    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
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
    //화면 이동.
    {
        if (beforenum == -1) return;

        if (roomnum == sprites.Length - 1 && dir > 0)
            roomnum = 0;
        else if (roomnum == 0 && dir < 0)
            roomnum = sprites.Length - 1;
        else
            roomnum += dir;
        //만약 자리의 끝이라면 반대쪽으로 이동, 아니라면 한칸 이동.

        Sprite.sprite = sprites[roomnum];
        ObjectChildLoad();
        //이미지 및 오브젝트 불러오기.
    }

    public void ObjectChildLoad()
    //해당 벽면의 오브젝트 활성화.
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
        //모든 벽면 오브젝트를 둘러보며 맞는 것은 활성화, 아니면 비활성화.
    }

    //방 변경 함수
    public void RoomLoad(string name)
    {
        beforenum = roomnum;
        Debug.Log("RommLoad " + name);
        isHall = false;
        //복도 아님.
        sprites = Resources.LoadAll<Sprite>("Wall/" + name);
        roomnum = 0;
        Sprite.sprite = sprites[roomnum];
        //들어온 경로의 스프라이트를 불러와 변경한다.

        ObjectLoad(name);



        if (currectObject.transform.childCount == 1)
        //벽면이 하나일경우 아래버튼 활성화.
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

    //여러번 갈아엎다보니 비효율적인 부분이 있을 수 있음.
    //더 좋은 방법이 있다면 언제든지 바꿔주세요.

    //복도 로드 함수.
    public void HallLoad(string name, int Hallnum, int Floor = 0)
    //층 변경 및 특정 벽면에서 복귀.
    {
        beforenum = roomnum;
        sprites = Resources.LoadAll<Sprite>("Hall/" + name);
        //복도 스프라이트를 불러옴.
        roomnum = Hallnum;
        //이전 시점으로 불러옴.
        Sprite.sprite = sprites[roomnum];
        Debug.Log("복도 " + name);
        ObjectLoad(name);
        if (Floor != 0)
        {
            floor = Floor;
        }
        //층 변경 있다면 층 변경.
        isHall = true;
        //복도모드 설정.
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
        //해당 방의 오브젝트 활성화.
        //모든 방 오브젝트를 둘러보며 맞는것은 활성화, 아닌것은 비활성화.

        ChildObjects = new GameObject[currectObject.transform.childCount];
        for (int i = 0; i < currectObject.transform.childCount; i++)
        {
            ChildObjects[i] = currectObject.transform.GetChild(i).gameObject;
        }
        //해당 방의 벽면 오브젝트 불러오기.
        //배열을 개수만큼 확장한뒤 하나하나 집어넣는 방식.

    }
}
