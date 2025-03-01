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

    // 引用温度脚本
    private RockTemperature rockTemperature;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rockTemperature = GetComponent<RockTemperature>();
    }

    private void Update()
    {
        UpdateRockAppearance();
    }

    void UpdateRockAppearance()
    {
        if (rockSprites.Length == 0 || rockTemperature == null) return;

        // 根据温度计算图片索引
        int index = CalculateIndexByTemperature(rockTemperature.currentTemperature);
        spriteRenderer.sprite = rockSprites[index];
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
}
