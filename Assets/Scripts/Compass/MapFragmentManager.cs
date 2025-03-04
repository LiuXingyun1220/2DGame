using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFragmentManager : MonoBehaviour
{
    public static MapFragmentManager Instance { get; private set; }

    public List<MapFragment> mapFragments = new List<MapFragment>(); // 地图碎片列表

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 根据关卡进度更新地图碎片的解锁状态
    public void UpdateMapFragments(int currentLevel)
    {
        foreach (var fragment in mapFragments)
        {
            fragment.isUnlocked = fragment.requiredLevel <= currentLevel;
        }
    }

    // 获取地图碎片数据
    public MapFragment GetFragmentByID(int fragmentID)
    {
        return mapFragments.Find(f => f.fragmentID == fragmentID);
    }
}