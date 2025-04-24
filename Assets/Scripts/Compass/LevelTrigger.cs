using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    public string levelName; // 要加载的关卡名称
     

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 确保只有 "Player" 碰到后才触发关卡跳转，并且只触发一次
        if (other.CompareTag("Player"))
        {
            
            Debug.Log("玩家触碰到关卡目标: " + levelName);

             StartCoroutine(FadeAndLoadScene());

            // 直接加载关卡
            SceneManager.LoadScene(levelName);
        }
    }

    // 可选：平滑过渡效果的协程
    private System.Collections.IEnumerator FadeAndLoadScene()
    {
        // 这里可以添加淡出效果
        yield return new WaitForSeconds(1f); // 模拟淡出时间
        SceneManager.LoadScene(levelName);
    }
}