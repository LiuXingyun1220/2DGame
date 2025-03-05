using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public MapFragment mapFragment; // 引用地图碎片管理器
    public int currentLevelIndex; // 当前关卡索引

    void Start()
    {
        // 假设玩家通过了关卡，解锁对应的碎片
        mapFragment.UnlockFragment(currentLevelIndex);
    }
}