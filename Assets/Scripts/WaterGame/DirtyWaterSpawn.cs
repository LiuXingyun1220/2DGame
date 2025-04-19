using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DirtyWaterSpawn : MonoBehaviour
{
    public GameObject waterPrefab; // 小球预制件
    public float speed = 5f; // 小球飞行速度

    private void Start()
    {
        SpawnBall();
    }

    private float time = 0f;
    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 3f)
        {
            SpawnBall();
            time = 0f;
        }
    }

    void SpawnBall()
    {
        // 随机选择一个方向生成小球
        Vector2 spawnPosition = Vector2.zero;
        Vector2 direction = Vector2.zero;

        // 随机选择四个方向
        int directionIndex = Random.Range(0, 3);
        switch (directionIndex)
        {
            case 0: // 从上面飞来
                spawnPosition = new Vector2(Random.Range(-5f, 5f), 5f);
                direction = Vector2.down;
                break;
            // case 1: // 从下面飞来
            //     spawnPosition = new Vector3(Random.Range(-5f, 5f), -5f, -5f);
            //     direction = Vector3.up;
            //     break;
            case 1: // 从左边飞来
                spawnPosition = new Vector2(-5f, Random.Range(-5f, 5f));
                direction = Vector2.right;
                break;
            case 2: // 从右边飞来
                spawnPosition = new Vector2(5f, Random.Range(-5f, 5f));
                direction = Vector2.left;
                break;
        }

        // 创建小球并设置其初速度
        GameObject water = Instantiate(waterPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = water.GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed; // 给小球一个初始速度
        water.tag = "DirtyWater";
    }
}