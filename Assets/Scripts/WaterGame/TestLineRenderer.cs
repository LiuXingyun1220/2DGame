using UnityEngine;
using System.Collections.Generic;

public class TestLineRenderer : MonoBehaviour
{
    // public Material m;
    public GameObject water;  // 小球
    private Vector3 nowPos;
    private List<LineRenderer> lines = new List<LineRenderer>(); // 存储所有画的线
    private List<List<BoxCollider2D>> collidersPerLine = new List<List<BoxCollider2D>>(); // 每条线的碰撞体列表
    private float eraseDistance = 0.5f; // 设置擦除的阈值距离

    void Start()
    {
        // 添加一个初始的 LineRenderer 组件
        LineRenderer initialLine = new GameObject("Line").AddComponent<LineRenderer>();
        initialLine.transform.SetParent(transform); // 让新创建的线条成为当前对象的子对象
        initialLine.startWidth = 0.02f;
        initialLine.endWidth = 0.02f;
        initialLine.positionCount = 0; // 初始化点数为 0
        initialLine.loop = false;
        lines.Add(initialLine); // 添加到线条列表中

        collidersPerLine.Add(new List<BoxCollider2D>()); // 添加对应的碰撞体列表
    }

    void Update()
    {
        DrawLine();
        EraseLine();
        UpdateColliders(); // 更新每条线条的碰撞体
    }

    /// <summary>
    /// 画线
    /// </summary>
    void DrawLine()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LineRenderer newLine = new GameObject("Line").AddComponent<LineRenderer>();
            newLine.transform.SetParent(transform);
            newLine.startWidth = 0.02f;
            newLine.endWidth = 0.02f;
            newLine.positionCount = 0;
            newLine.loop = false;
            lines.Add(newLine); // 将新线条加入列表

            collidersPerLine.Add(new List<BoxCollider2D>()); // 为新线条添加碰撞体列表
        }

        if (Input.GetMouseButton(0))
        {
            nowPos = Input.mousePosition;
            nowPos.z = 5;
            nowPos = Camera.main.ScreenToWorldPoint(nowPos);

            LineRenderer currentLine = lines[lines.Count - 1];
            currentLine.positionCount += 1;
            currentLine.SetPosition(currentLine.positionCount - 1, nowPos);
        }
    }

    /// <summary>
    /// 擦除线条上的点
    /// </summary>
    void EraseLine()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mouseWorldPos = Input.mousePosition;
            mouseWorldPos.z = 5;
            mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseWorldPos);

            foreach (var line in lines)
            {
                for (int i = 0; i < line.positionCount; i++)
                {
                    Vector3 point = line.GetPosition(i);

                    if (Vector3.Distance(mouseWorldPos, point) < eraseDistance)
                    {
                        BreakLineAtPoint(line, i);
                        break;
                    }
                }
            }
        }
    }

    void BreakLineAtPoint(LineRenderer line, int index)
    {
        if (index < 0 || index >= line.positionCount)
            return;

        line.positionCount = index; // 保留触碰点之前的部分
    }

    /// <summary>
    /// 更新每条线的碰撞体
    /// </summary>
    void UpdateColliders()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            LineRenderer line = lines[i];
            List<BoxCollider2D> colliders = collidersPerLine[i];

            // 清除旧的碰撞体
            foreach (var collider in colliders)
            {
                Destroy(collider.gameObject);
            }

            colliders.Clear();

            // 为每一对相邻的点创建一个 BoxCollider2D
            for (int j = 0; j < line.positionCount - 1; j++)
            {
                Vector3 pointA = line.GetPosition(j);
                Vector3 pointB = line.GetPosition(j + 1);

                // 计算碰撞体的中心和大小
                Vector3 center = (pointA + pointB) / 2;
                float length = Vector3.Distance(pointA, pointB);

                // 创建碰撞体并设置属性
                GameObject colliderObject = new GameObject("Collider_" + i + "_" + j);
                BoxCollider2D collider = colliderObject.AddComponent<BoxCollider2D>();
                collider.transform.SetParent(transform);
                collider.offset = center; // 设置偏移
                collider.size = new Vector2(length, 0.1f); // 线段的大小
                colliders.Add(collider); // 添加到当前线条的碰撞体列表
            }
        }
    }

    /// <summary>
    /// 碰撞检测：检测水滴与线条的碰撞
    /// </summary>
    void CheckBallCollision()
    {
        // 给水滴添加 Rigidbody2D 和 Collider2D
        Rigidbody2D waterRigidbody = water.GetComponent<Rigidbody2D>();
        Collider2D ballCollider = water.GetComponent<Collider2D>();

        foreach (var colliders in collidersPerLine)
        {
            foreach (var collider in colliders)
            {
                if (ballCollider.bounds.Intersects(collider.bounds))
                {
                    Debug.Log("Ball collided with line!");
                    // 在这里处理碰撞后的逻辑，例如水滴的反弹等
                }
            }
        }
    }
}



