using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nametrigger : MonoBehaviour
{
    public GameObject[] name;
    public Transform[] targets;
    public float minDistanceThreshold = 1f;
    private PlayerController playerController;

    private int currentTargetIndex = 0;
    public float updateInterval = 0.1f;
    private float timer = 0f;

    public void OpenPanel(GameObject gameobject)
    {
        
        if (gameobject != null)
        {
            gameobject.SetActive(true);
        }
        else
        {
            Debug.Log("章节标无");
        }
    }

    void Awake()
    {
        // 获取玩家控制器引用
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("找不到 PlayerController 组件！");
        }

        // 初始化并检查每个目标的激活状态
        for (int i = 0; i < targets.Length; i++)
        {
            string targetName = targets[i].name;
            currentTargetIndex = i;
            string key = "ChapterActive_" + targetName;

            // 检查是否已经激活过
            if (PlayerPrefs.HasKey(key) && PlayerPrefs.GetInt(key) == 1)
            {
                Debug.Log("章节标已激活: " + targetName);
                OpenPanel(name[currentTargetIndex]);
            }
        }
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
        if (targets.Length == 0 || playerController == null) return;

        for (int i = 0; i < targets.Length; i++)
        {
            currentTargetIndex = i;
            Transform currentTarget = targets[currentTargetIndex];
            string targetName = currentTarget.name;
            string key = "ChapterActive_" + targetName;

            // 使用玩家位置替代箭头自身位置
            Vector3 playerPosition = playerController.transform.position;

            // 检测玩家与目标的距离
            float distanceToTarget = Vector3.Distance(playerPosition, currentTarget.position);

            if (!PlayerPrefs.HasKey(key) || PlayerPrefs.GetInt(key) == 0)
            {
                if (distanceToTarget <= minDistanceThreshold)
                {
                    // 如果距离小于最小距离，调用 OpenPanel 方法
                    Debug.Log("打开章节标: " + targetName);
                    OpenPanel(name[currentTargetIndex]);

                    // 保存激活状态到 PlayerPrefs
                    PlayerPrefs.SetInt(key, 1);
                    PlayerPrefs.Save();
                }
            }
        }
    }
}