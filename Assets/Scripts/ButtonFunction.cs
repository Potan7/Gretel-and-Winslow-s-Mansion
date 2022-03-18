using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
    //Canvas에 들어가있음.
    //EventSystem의 경우 게임 시작시 스크립트가 들어가길래 Canvas로 옮김.
{
    /* [여기서 관리 안하는 버튼]
     * 1. 메인 메뉴 버튼
     * 2. Map 관련 버튼
     * 3. ESC메뉴 관련 버튼
     * 4. ToDo Exit 버튼
     */
    public ZoomInObject ZoomObject { private get; set; }
    public ChangeView ChangeObject { private get; set; }

    public Button[] buttons;
    //0 - Left
    //1 - Right
    //2 - Down

    displayManager display;
    TodoManager ToDo;

    float CameraSize;
    Vector3 CameraPosition;

    [SerializeField]
    private Image fade;
    [SerializeField]
    private PhoneManager phoneButton;
    //원래 Public으로 해서 가져오면되는데 이미 GetFade 방식이 많이 쓰여서 private 유지


    public bool DontDownButton = false;

    #region 파괴X
    private void Awake()
    {
        var obj = FindObjectsOfType<ButtonFunction>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
            AwakeAfter();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    #endregion

    void AwakeAfter()
    {
        CameraSize = Camera.main.orthographicSize;
        CameraPosition = Camera.main.transform.position;
        //카메라 초기화용 원본위치 저장.

        display = FindObjectOfType<displayManager>();
        //display는 실수로 모든 display 등록을 날려버려서 작성
    }

    void ReLoad()
    {
        ToDo = FindObjectOfType<TodoManager>();
    }

    public void ToDoButton()
    {
        if (ToDo == null) ReLoad();

        ToDo.ToDoOpen();
    }

    public void LeftMoveButton()
        //왼쪽버튼
    {
        display.displayMove(-1);
        //왼쪽으로 이동.
    }

    public void RightMoveButton()
        //오른쪽 버튼
    {
        display.displayMove(1);
        //오른쪽으로 이동
    }

    public void DownButton()
        //아래버튼
    {
        if (display.state == displayManager.State.zoom)
        {
            Camera.main.orthographicSize = CameraSize;
            Camera.main.transform.position = CameraPosition;
            display.state = displayManager.State.normal;
            //카메라와 상태 초기화.

            ZoomObject.ZoomDisable();
            //ZoomObject 활성화
            if (!display.isOnlyOneWall)
                SetDownButton(false);
        }

        else if (display.state == displayManager.State.changedview)
        {
            ChangeObject.disable();
            if (!display.isOnlyOneWall)
                SetDownButton(false);
            //화면 초기화
        }

        else
        {
            display.HallLoad(display.beforeRoom.name, display.beforenum);
            display.isOnlyOneWall = false;
            SetDownButton(false);
            //줌 상태가 아닐때 아래버튼 누르면 전 복도로 나가기
            //대부분의 아래버튼으로 나가는 방은 복도로 이어지나 비밀방만 방으로 나가짐.
            //이부분은 따로 수정이 필요.
        }

        if (DontDownButton)
            //이름은 DontDownButton이지만 실제론 이동 버튼 뒤로가기해도 안나오게 하는 역할
        {
            buttons[2].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            buttons[0].gameObject.SetActive(false);
        }
    }

    public void SetDownButton(bool OnOff)
    //아래 버튼 On/Off
    {
        buttons[0].gameObject.SetActive(!OnOff);
        buttons[1].gameObject.SetActive(!OnOff);
        buttons[2].gameObject.SetActive(OnOff);
    }

    public void SetAllbuttons(bool OnOff)
        //모든 버튼 설정. 
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(OnOff);
        }

        if (OnOff)
        {
            SetDownButton(display.isOnlyOneWall);
        }
    }

    // 좌, 우, 아래 버튼 각자 설정 위한 함수
    // 사실상 좌우 중에 하나만 manual하게 켜두려고 만든 함수
    public void SetEachButtons(bool val, bool val1, bool val2)
    {
        buttons[0].gameObject.SetActive(val);
        buttons[1].gameObject.SetActive(val1);
        buttons[2].gameObject.SetActive(val2);
    }

    //Fade가 파괴되지 않다보니 Fade를 가져오기 위해 만든 함수.
    public Image GetFade()
    {
        return fade;
    }

    public PhoneManager GetPhone()
    {
        return phoneButton;
    }
}
