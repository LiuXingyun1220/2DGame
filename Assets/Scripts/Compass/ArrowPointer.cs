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

            // 计算从箭头到目标的方向
            Vector3 direction = currentTarget.position - transform.position;
            direction.z = 0; // 确保箭头在2D平面上旋转

            // 检查是否到达目标点
            if (direction.magnitude < arrivalDistance)
            {
                // 切换到下一个目标点
                currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
                currentTarget = targets[currentTargetIndex];
            }

            // 设置箭头的旋转，使其指向目标方向
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90; // 调整角度，使其从Y轴正方向到目标方向
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

           
        }
    }
}