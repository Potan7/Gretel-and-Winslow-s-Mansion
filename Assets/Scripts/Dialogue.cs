using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int id = 0;

    // public Sprite[] Charsprite;
    //ĳ������ ��������Ʈ.
    // ��ȭ ���뿡�� �Ѱ��ִ� ������ ��ü

    [TextArea(3, 10)]
    public string[] sentences;
    //��ȭ ����
}
