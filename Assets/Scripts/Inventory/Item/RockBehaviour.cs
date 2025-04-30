using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RockBehaviour : MonoBehaviour
{
    [SerializeField] Sprite[] rockSprites; // 存储岩石图片
    private SpriteRenderer spriteRenderer;


    [Header("碎裂动画帧")]
    public Sprite[] breakFrames;  // 碎裂过程序列帧
    public float frameInterval = 0.1f;

    public bool isBroken = false;
    private Collider2D[] colliders;

    // 引用温度脚本
    private RockTemperature rockTemperature;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rockTemperature = GetComponent<RockTemperature>();
        colliders = GetComponents<Collider2D>();
    }

    private void Update()
    {
        UpdateRockAppearance();
    }

    void UpdateRockAppearance()
    {
        if (rockSprites.Length == 0 || rockTemperature == null) return;

        if (!isBroken)
        {
            // 根据温度计算图片索引
            int index = CalculateIndexByTemperature(rockTemperature.currentTemperature);
            spriteRenderer.sprite = rockSprites[index];
        }
        else
        {
            StartBreakAnimation();
        }


    }

    // 根据温度映射到图片索引（根据需求调整逻辑）
    int CalculateIndexByTemperature(float temperature)
    {
        // 示例：假设温度范围是 0~100，均匀分割为5段
        float maxTemp = 100f;
        float segmentSize = maxTemp / rockSprites.Length;

        // 计算索引（限制在 0~Length-1 范围内）
        int index = Mathf.FloorToInt(temperature / segmentSize);
        index = Mathf.Clamp(index, 0, rockSprites.Length - 1);

        return index;
    }

    public void StartBreakAnimation()
    {
        if (!isBroken)
            StartCoroutine(PlayBreakAnimation());
    }

    private IEnumerator PlayBreakAnimation()
    {
        isBroken = true;

        // 禁用所有碰撞器
        foreach (Collider2D col in colliders)
            col.enabled = false;

        // 播放帧动画
        foreach (Sprite frame in breakFrames)
        {
            spriteRenderer.sprite = frame;
            yield return new WaitForSeconds(frameInterval);
        }

        // 动画结束后隐藏对象
        gameObject.SetActive(false);
        // 或 Destroy(gameObject);
    }
}
