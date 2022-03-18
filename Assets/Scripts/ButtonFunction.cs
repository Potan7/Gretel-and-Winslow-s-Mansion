using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour
    //Canvas�� ������.
    //EventSystem�� ��� ���� ���۽� ��ũ��Ʈ�� ���淡 Canvas�� �ű�.
{
    /* [���⼭ ���� ���ϴ� ��ư]
     * 1. ���� �޴� ��ư
     * 2. Map ���� ��ư
     * 3. ESC�޴� ���� ��ư
     * 4. ToDo Exit ��ư
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
    //���� Public���� �ؼ� ��������Ǵµ� �̹� GetFade ����� ���� ������ private ����


    public bool DontDownButton = false;

    #region �ı�X
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
        //ī�޶� �ʱ�ȭ�� ������ġ ����.

        display = FindObjectOfType<displayManager>();
        //display�� �Ǽ��� ��� display ����� ���������� �ۼ�
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
        //���ʹ�ư
    {
        display.displayMove(-1);
        //�������� �̵�.
    }

    public void RightMoveButton()
        //������ ��ư
    {
        display.displayMove(1);
        //���������� �̵�
    }

    public void DownButton()
        //�Ʒ���ư
    {
        if (display.state == displayManager.State.zoom)
        {
            Camera.main.orthographicSize = CameraSize;
            Camera.main.transform.position = CameraPosition;
            display.state = displayManager.State.normal;
            //ī�޶�� ���� �ʱ�ȭ.

            ZoomObject.ZoomDisable();
            //ZoomObject Ȱ��ȭ
            if (!display.isOnlyOneWall)
                SetDownButton(false);
        }

        else if (display.state == displayManager.State.changedview)
        {
            ChangeObject.disable();
            if (!display.isOnlyOneWall)
                SetDownButton(false);
            //ȭ�� �ʱ�ȭ
        }

        else
        {
            display.HallLoad(display.beforeRoom.name, display.beforenum);
            display.isOnlyOneWall = false;
            SetDownButton(false);
            //�� ���°� �ƴҶ� �Ʒ���ư ������ �� ������ ������
            //��κ��� �Ʒ���ư���� ������ ���� ������ �̾����� ��й游 ������ ������.
            //�̺κ��� ���� ������ �ʿ�.
        }

        if (DontDownButton)
            //�̸��� DontDownButton������ ������ �̵� ��ư �ڷΰ����ص� �ȳ����� �ϴ� ����
        {
            buttons[2].gameObject.SetActive(false);
            buttons[1].gameObject.SetActive(false);
            buttons[0].gameObject.SetActive(false);
        }
    }

    public void SetDownButton(bool OnOff)
    //�Ʒ� ��ư On/Off
    {
        buttons[0].gameObject.SetActive(!OnOff);
        buttons[1].gameObject.SetActive(!OnOff);
        buttons[2].gameObject.SetActive(OnOff);
    }

    public void SetAllbuttons(bool OnOff)
        //��� ��ư ����. 
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

    // ��, ��, �Ʒ� ��ư ���� ���� ���� �Լ�
    // ��ǻ� �¿� �߿� �ϳ��� manual�ϰ� �ѵη��� ���� �Լ�
    public void SetEachButtons(bool val, bool val1, bool val2)
    {
        buttons[0].gameObject.SetActive(val);
        buttons[1].gameObject.SetActive(val1);
        buttons[2].gameObject.SetActive(val2);
    }

    //Fade�� �ı����� �ʴٺ��� Fade�� �������� ���� ���� �Լ�.
    public Image GetFade()
    {
        return fade;
    }

    public PhoneManager GetPhone()
    {
        return phoneButton;
    }
}
