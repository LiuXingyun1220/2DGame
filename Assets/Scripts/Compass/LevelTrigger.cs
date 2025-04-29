using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    public string levelName; // 要加载的关卡名称

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 确保只有 "Player" 碰到后才触发关卡跳转
        if (other.CompareTag("Player"))
        {
            Debug.Log("玩家触碰到关卡目标: " + levelName);

            // 检查目标关卡是否已解锁
            if (LevelComplete.Instance.IsLevelUnlocked(levelName))
            {
                // 使用 TransitionManager 进行场景过渡
                TransitionManager.Instance.Transition(SceneManager.GetActiveScene().name, levelName);
            }
            else
            {
                Debug.Log("关卡 " + levelName + " 未解锁，无法进入！");
                // 可以在这里添加提示玩家的 UI 逻辑
            }
        }
    }
}