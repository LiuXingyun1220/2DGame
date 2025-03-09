using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public Transform[] targets; // 目标点数组
    private int currentTargetIndex = 0; // 当前目标点的索引
    public float updateInterval = 0.1f; // 更新间隔
    public float arrivalDistance = 0.1f; // 到达目标点的距离阈值

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

            // 如果距离目标点足够近，切换到下一个目标
            if (direction.magnitude < arrivalDistance)
            {
                currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
                currentTarget = targets[currentTargetIndex];

                // 重新计算新的方向
                direction = currentTarget.position - transform.position;
                direction.z = 0;
            }

            // 计算角度，使箭头朝向目标
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            angle -= 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

}