using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class MapFragment
{
    public int fragmentID; // 地图碎片的唯一标识
    public int requiredLevel; // 解锁该碎片所需的关卡编号
    public Sprite fragmentSprite; // 地图碎片的图片
    public bool isUnlocked; // 是否已解锁
}
