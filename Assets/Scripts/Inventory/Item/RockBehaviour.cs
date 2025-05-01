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

    [Header("关卡完成界面")]
    [SerializeField] private GameObject levelCompleteUI; // 关卡完成UI面板
    [SerializeField] private float delayBeforeShowUI = 0.5f; // 破碎后显示UI的延迟时间


    // 时间相关
    [Header("时间追踪")]
    public float creationTime; // 创建时间
    public float breakTime;    // 破碎时间
    public float lifeTime;     // 生命周期时间


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rockTemperature = GetComponent<RockTemperature>();
        colliders = GetComponents<Collider2D>();

        // 确保关卡完成UI在开始时是隐藏的
        if (levelCompleteUI != null)
            levelCompleteUI.SetActive(false);

        // 记录创建时间
        creationTime = GameTimer.Instance.GetGameTime();
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
        {
            // 记录破碎时间
            breakTime = GameTimer.Instance.GetGameTime();
            Debug.Log(breakTime);
            StartCoroutine(PlayBreakAnimation());
        }
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
        spriteRenderer.enabled = false;

        // 等待指定延迟后显示关卡完成UI
        yield return new WaitForSeconds(delayBeforeShowUI);
        ShowLevelCompleteUI();
    }

    // 显示关卡完成UI
    private void ShowLevelCompleteUI()
    {
        if (levelCompleteUI != null)
        {
            levelCompleteUI.SetActive(true);

            // 更新UI上的时间信息（如果有的话）
            UpdateCompletionStats();
        }
        else
        {
            Debug.LogWarning("关卡完成UI未设置！请在RockBehaviour组件上指定levelCompleteUI");
        }
    }

    // 更新完成统计信息
    private void UpdateCompletionStats()
    {
        // 获取总共用时
        float totalTime = breakTime - creationTime;

        // 查找并更新UI上的时间文本（如果有的话）
        TextMeshProUGUI text = levelCompleteUI.GetComponentInChildren<TextMeshProUGUI>();
        if (text.name.Contains("TimeText"))
        {
            text.text = string.Format("完成时间: {0:0.00}秒", totalTime);
        }

        // 此处还可以添加其他统计信息，比如分数、星级等
    }

}
