using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    // 新增玩家控制器引用
    private PlayerController playerController;
    public Transform[] targets;
    private int currentTargetIndex = 0;
    public float updateInterval = 0.1f;

    private float timer = 0f;

    void Start()
    {
        // 获取玩家控制器引用
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("找不到 PlayerController 组件！");
        }

        // 初始化旋转
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer = 0f;
            SetNextLevelTarget();
            UpdateArrowDirection();
        }
    }

    void UpdateArrowDirection()
    {
        if (targets.Length == 0 || playerController == null) return;

        Transform currentTarget = targets[currentTargetIndex];

        // 使用玩家位置替代箭头自身位置
        Vector3 playerPosition = playerController.transform.position;

        // 计算方向
        Vector3 direction = currentTarget.position - playerPosition;
        direction.z = 0;

        // 计算角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // 其他方法保持不变...
    public void SetNextLevelTarget()
    {
        string nextLevels = GetNextUnlockedLevel();
        string nextLevel = "";
        if (nextLevels == "D_baoping_B")
        {
            nextLevel = "1_trigger";
            currentTargetIndex = 0;
        }
        else if (nextLevels == "D_yuzui_B")
        {
            nextLevel = "2_trigger";
            currentTargetIndex = 1;
        }
        else if (nextLevels == "D_music_B")
        {
            nextLevel = "3_trigger";
            currentTargetIndex = 2;
        }
        else
        {
            nextLevel = "1_trigger";
            currentTargetIndex = 0;
        }
        
    }

    private string GetNextUnlockedLevel()
    {
        for (int i = 0; i < LevelComplete.Instance.completedLevels.Count; i++)
        {
            string[] levelKeys = { "D_baoping_B", "D_yuzui_B", "D_music_B", "End" };
            if (!LevelComplete.Instance.IsLevelCompleted(levelKeys[i]))
            {
                return levelKeys[i]; 
            }
        }
        return null;
    }
    
}