using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovePuzzle : MonoBehaviour
{
    WaterMovePuzzleBottle SelectedBottle;
    public WaterMovePuzzleBottle Goalbottle;

    public Sprite sprite;

    public bool IsComplete = false;

    public void SelectBottle(WaterMovePuzzleBottle bottle)
    {
        if (SelectedBottle == null)
        {
            SelectedBottle = bottle;
            StopAllCoroutines();
            StartCoroutine(Animation(true, bottle));
            //처음 눌렀을때 들기
        }
        else if (SelectedBottle == bottle)
        {
            SelectedBottle = null;
            StopAllCoroutines();
            StartCoroutine(Animation(false, bottle));
            //같은거 한번 더눌렀으면 취소
        }
        else if (SelectedBottle.WaterAmount == 0 || bottle.WaterAmount == bottle.WaterMax)
        {
            SelectBottle(SelectedBottle);
            //만약 선택한 용기가 비었거나 넣을 용기가 꽉찼다면 취소
        }
        else
        {
            for (int i = bottle.WaterAmount; i < bottle.WaterMax; i++)
            {
                if (SelectedBottle.WaterAmount == 0) break;
                SelectedBottle.WaterAmount--;
                bottle.WaterAmount++;
            }
            //물 넣기
            bottle.WaterUpdate();
            SelectedBottle.WaterUpdate();
            //물 상태 업데이트
            SoundManager.Instance.PlaySound("물_떨어지는_소리");
            SelectBottle(SelectedBottle);
            //소리 재생 및 초기화
            if (Goalbottle.WaterAmount == 4)
            {
                Complete();
            }
        }
    }

    void Complete()
    {
        IsComplete = true;
        Goalbottle.SendMessage("GetItem");
        GameObject.Find("Cabinet").GetComponent<SpriteRenderer>().sprite = sprite;
    }

    IEnumerator Animation(bool UpDown, WaterMovePuzzleBottle bottle)
    {
        if (UpDown)
        {
            while (bottle.transform.position.y < 1)
            {
                bottle.transform.position = bottle.transform.position + new Vector3(0, 0.08f, 0);

                yield return null;
            }
        }
        else
        {
            while (bottle.transform.position.y > bottle.myPosition)
            {
                bottle.transform.position = bottle.transform.position + new Vector3(0, -0.08f, 0);

                yield return null;
            }
        }
    }
}
