using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public Transform[] targets; // 目标点数组
    private int currentTargetIndex = 0; // 当前目标点的索引
    public float updateInterval = 0.1f; // 更新间隔


    private float timer = 0f;

    void Start()
    {
        // 确保箭头的初始旋转为0度
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer = 0f;
            UpdateArrowDirection();
        }
    }

    void UpdateArrowDirection()
    {
        if (targets.Length > 0)
        {
            Transform currentTarget = targets[currentTargetIndex];

            // 计算方向
            Vector3 direction = currentTarget.position - transform.position;
            direction.z = 0; // 确保在 2D 平面上

            // 计算角度，使箭头朝向目标
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            angle -= 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void SetNextLevelTarget()
    {
        // 获取下一关的名称
        string nextLevel = GetNextUnlockedLevel();
        if (!string.IsNullOrEmpty(nextLevel))
        {
            // 设置新目标为下一关的起点
            Transform nextTarget = GetTargetFromLevel(nextLevel);
            if (nextTarget != null)
            {
                targets = new Transform[] { nextTarget };
                currentTargetIndex = 0;
            }
        }
    }

    private string GetNextUnlockedLevel()
    {
        // 查找下一个未解锁的关卡
        for (int i = 1; i <= LevelComplete.Instance.unlockedLevels.Count; i++)
        {
            string levelName = "Level" + i;
            if (!LevelComplete.Instance.IsLevelUnlocked(levelName))
            {
                return levelName;
            }
        }
        return null;
    }

    private Transform GetTargetFromLevel(string levelName)
    {
        // 根据关卡名称获取目标点（需要在场景中设置）
        GameObject targetObj = GameObject.Find(levelName + "Target");
        if (targetObj != null)
        {
            return targetObj.transform;
        }
        return null;
    }
}