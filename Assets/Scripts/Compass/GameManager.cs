using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentLevel; // 当前玩家完成的关卡编号

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

    // 更新关卡进度
    public void UpdateLevel(int level)
    {
        currentLevel = level;
        // 通知地图碎片管理器更新
        MapFragmentManager.Instance.UpdateMapFragments(currentLevel);
    }
}
