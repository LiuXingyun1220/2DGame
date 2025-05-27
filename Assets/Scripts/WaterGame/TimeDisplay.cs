using TMPro;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float timer = 0f;
    private bool isTiming = true;

    void Update()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;
            timeText.text = $"用时:{timer:F2}";
        }
    }

    public void StopTimer()
    {
        isTiming = false;
    }

    public float GetTimeUsed()
    {
        return timer;
    }
}
