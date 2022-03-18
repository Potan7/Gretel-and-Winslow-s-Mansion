using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int id = 0;

    // public Sprite[] Charsprite;
    //캐릭터의 스프라이트.
    // 대화 내용에서 넘겨주는 식으로 교체

    [TextArea(3, 10)]
    public string[] sentences;
    //대화 내용
}
