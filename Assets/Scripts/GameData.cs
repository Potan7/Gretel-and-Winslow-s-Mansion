using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class GameData
{
    public int Nighttime = 0;
    //밤을 보낸 회수
    public int TakeOrder = 0;
    //히든 미션 수행 회수
    public int SceneProgress = 0;
    //진행한 씬의 위치
    public int condition1 = 0;
    //노말엔딩 조건 - Day3 규칙
}
