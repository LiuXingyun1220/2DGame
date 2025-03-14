using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement; // 引入场景管理

public class LevelTrigger : MonoBehaviour
{
    public string levelName; // 要加载的关卡名称

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 确保只有 "Player" 碰到后才触发关卡跳转
        if (other.CompareTag("Player"))
        {
            Debug.Log("玩家触碰到关卡目标: " + levelName);
            SceneManager.LoadScene(levelName); // 加载对应的关卡
        }
    }
}
