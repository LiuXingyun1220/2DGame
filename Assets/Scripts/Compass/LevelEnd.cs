using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string nextLevelName; // 下一关的名称

    private void Start()
    {
        // 在场景加载完成后触发关卡完成逻辑
        Debug.Log("关卡完成，解锁下一关: " + nextLevelName);

        // 解锁下一关
        LevelComplete.Instance.UnlockLevel(nextLevelName);

        
    }
}