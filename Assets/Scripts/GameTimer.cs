using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : Singleton<GameTimer>
{

    // 时间显示UI组件
    [SerializeField] private TextMeshProUGUI timerText;

    // 计时相关变量
    private float gameTime = 0f;
    private bool isTimerRunning = false;

    // 格式化设置
    [Header("时间格式设置")]
    [SerializeField] private bool showHours = true;
    [SerializeField] private bool showMinutes = true;
    [SerializeField] private bool showSeconds = true;
    [SerializeField] private bool showMilliseconds = false;

    private void Start()
    {
        StartTimer(); // 默认游戏开始就启动计时
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            gameTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    /// <summary>
    /// 开始计时
    /// </summary>
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    /// <summary>
    /// 暂停计时
    /// </summary>
    public void PauseTimer()
    {
        isTimerRunning = false;
    }

    /// <summary>
    /// 重置计时器
    /// </summary>
    public void ResetTimer()
    {
        gameTime = 0f;
        UpdateTimerDisplay();
    }

    /// <summary>
    /// 更新时间显示
    /// </summary>
    private void UpdateTimerDisplay()
    {
        if (timerText == null) return;

        int hours = Mathf.FloorToInt(gameTime / 3600f);
        int minutes = Mathf.FloorToInt((gameTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(gameTime % 60f);
        int milliseconds = Mathf.FloorToInt((gameTime * 1000) % 1000);

        string timeString = "";

        if (showHours)
            timeString += hours.ToString("00") + ":";

        if (showMinutes)
            timeString += minutes.ToString("00") + ":";

        if (showSeconds)
            timeString += seconds.ToString("00");

        if (showMilliseconds)
            timeString += "." + milliseconds.ToString("000");

        timerText.text = timeString;
    }

    /// <summary>
    /// 获取当前游戏时间（秒）
    /// </summary>
    public float GetGameTime()
    {
        return gameTime;
    }

    /// <summary>
    /// 获取格式化的时间字符串
    /// </summary>
    public string GetFormattedTime()
    {
        int hours = Mathf.FloorToInt(gameTime / 3600f);
        int minutes = Mathf.FloorToInt((gameTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(gameTime % 60f);
        int milliseconds = Mathf.FloorToInt((gameTime * 1000) % 1000);

        return string.Format("{0:00}:{1:00}:{2:00}.{3:000}", hours, minutes, seconds, milliseconds);
    }
}
