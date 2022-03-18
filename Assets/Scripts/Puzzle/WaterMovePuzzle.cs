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
            //ó�� �������� ���
        }
        else if (SelectedBottle == bottle)
        {
            SelectedBottle = null;
            StopAllCoroutines();
            StartCoroutine(Animation(false, bottle));
            //������ �ѹ� ���������� ���
        }
        else if (SelectedBottle.WaterAmount == 0 || bottle.WaterAmount == bottle.WaterMax)
        {
            SelectBottle(SelectedBottle);
            //���� ������ ��Ⱑ ����ų� ���� ��Ⱑ ��á�ٸ� ���
        }
        else
        {
            for (int i = bottle.WaterAmount; i < bottle.WaterMax; i++)
            {
                if (SelectedBottle.WaterAmount == 0) break;
                SelectedBottle.WaterAmount--;
                bottle.WaterAmount++;
            }
            //�� �ֱ�
            bottle.WaterUpdate();
            SelectedBottle.WaterUpdate();
            //�� ���� ������Ʈ
            SoundManager.Instance.PlaySound("��_��������_�Ҹ�");
            SelectBottle(SelectedBottle);
            //�Ҹ� ��� �� �ʱ�ȭ
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
