using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabPuzzle : MonoBehaviour
{
    SpriteRenderer sprite;

    public LabPuzzleBottle selected;
    public LabPuzzlePlace[] places;

    public GameObject bottleParent;
    public SpriteRenderer[] bottleEvent;
    public Sprite[] bottleSprites;

    public GameObject[] mapFloor;
    public Sprite[] mapImage;

    public Dialogue dialogue;
    public GameObject stair;

    public Sprite[] RoomAndDraw;
    public Image roomImage;
    public Image drawImage;
    public Image fadeImage;
    public GameObject canvas;
    public GameObject cutScene;

    public GameObject DeskDisable;

    bool is2F = false;
    public bool isComplete = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        mapFloor[1].SetActive(false);
        bottleParent.SetActive(false);
    }

    void Interact()
    {
        if (isComplete) return;

        int index = is2F ? 1 : 0;
        sprite.sprite = mapImage[index];
        mapFloor[index].SetActive(true);
        mapFloor[is2F ? 0 : 1].SetActive(false);

        is2F = !is2F;
    }

    public void SelecetBottle(LabPuzzleBottle bottle)
    {
        if (selected == null)
        {
            StartCoroutine(MoveBottleUp(bottle));
            BottleEvent(bottle.ID);
            selected = bottle;
        }
        else if (selected == bottle)
        {
            StartCoroutine(MoveBottleDown(bottle));
            selected = null;
        }
        else
        {
            StartCoroutine(MoveBottleDown(selected));
            StartCoroutine(MoveBottleUp(bottle));
            BottleEvent(bottle.ID);
            selected = bottle;
        }
    }

    public void CheckPotion()
    {
        for (int i = 0; i < places.Length; i++)
        {
            if (!places[i].hasEffect) return;
        }

        for (int i = 0; i < places.Length; i++)
        {
            if (!places[i].isRight)
            {
                for (int j = 0; j < places.Length; j++)
                {
                    places[j].EffectReset();
                }
                ChatManager.Instance.SearchChat("잘못된 듯 하다");
                return;
            }
        }

        isComplete = true;
        stair.SetActive(false);
        StartCoroutine(CompleteCutScene());
    }

    void BottleEvent(int ID)
    {
        int index1 = 0, index2 = 1;
        switch (ID)
        {
            case 0:
                //빨강 - 윈슬로
                break;

            case 1:
                //보라 - 로비
                index1 = 2;
                index2 = 3;
                break;

            case 2:
                //초록 - 길버트
                index1 = 4;
                index2 = 5;
                break;

            case 3:
                //파랑 - 손님방
                index1 = 6;
                index2 = 7;
                break;
        }
        bottleEvent[0].sprite = bottleSprites[index1];
        bottleEvent[1].sprite = bottleSprites[index2];
    }

    IEnumerator MoveBottleUp(LabPuzzleBottle target)
    {
        float y = target.y + 0.7f;

        while (target.transform.position.y < y)
        {
            target.transform.position += new Vector3(0, 0.1f, 0);

            yield return null;
        }
    }

    IEnumerator MoveBottleDown(LabPuzzleBottle target)
    {
        float y = target.y;
        while (target.transform.position.y > y)
        {
            target.transform.position += new Vector3(0, -0.1f, 0);

            yield return null;
        }
    }

    IEnumerator CompleteCutScene()
    {
        canvas.SetActive(true);
        yield return StartCoroutine(Fade(0.02f, fadeImage));
        cutScene.SetActive(true);

        int index = 0;
        while (index < 8)
        {
            roomImage.sprite = RoomAndDraw[index++];
            drawImage.sprite = RoomAndDraw[index++];

            yield return StartCoroutine(Fade(-0.02f, fadeImage));
            yield return StartCoroutine(Fade(0.01f, drawImage, true));

            yield return new WaitForSeconds(1.5f);

            yield return StartCoroutine(Fade(0.02f, fadeImage));
            drawImage.color = new Color(255, 255, 255, 0);
        }
        yield return new WaitForSeconds(0.3f);

        cutScene.SetActive(false);
        DeskDisable.SetActive(true);
        FindObjectOfType<ButtonFunction>().DownButton();
        ChatManager.Instance.StartDialogue(dialogue);
    }

    IEnumerator Fade(float plus, Image fade, bool isWhite = false)
    {
        int _color = 0;
        if (isWhite)
        {
            _color = 255;
        }

        float fadeCount;
        if (plus > 0)
        {
            fadeCount = 0.0f;
            while (fadeCount < 1.0f)
            {
                fadeCount += plus;
                yield return new WaitForSeconds(0.01f);
                fade.color = new Color(_color, _color, _color, fadeCount);
            }
        }
        else
        {
            fadeCount = 1.0f;
            while (fadeCount > 0)
            {
                fadeCount += plus;
                yield return new WaitForSeconds(0.01f);
                fade.color = new Color(_color, _color, _color, fadeCount);

            }
        }
    }
}
