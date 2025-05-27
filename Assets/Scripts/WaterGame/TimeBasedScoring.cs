using UnityEngine;
using TMPro;

public class TimeBasedScoring : MonoBehaviour
{
    public TimerDisplay timerDisplay; // 拖入 TimerDisplay 脚本
    public TextMeshProUGUI scoreText;

    private bool hasScored = false;

    void Update()
    {
        // 示例：按下空格计算一次分数
        if (Input.GetKeyDown(KeyCode.Space) && !hasScored)
        {
            float usedTime = timerDisplay.GetTimeUsed();

            // 假设基础满分为100，时间越短分数越高，超过30秒最低为0分
            float maxTime = 30f;
            float score = Mathf.Clamp(100f * (1f - usedTime / maxTime), 0f, 100f);

            scoreText.text = $"得分: {score:F1}";
            hasScored = true;

            timerDisplay.StopTimer(); // 同时停止计时
        }
    }
}
