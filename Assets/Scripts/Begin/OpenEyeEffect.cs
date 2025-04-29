using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class OpenEyeEffect : MonoBehaviour
{
    public Image eyeMask;
    public float duration = 2f; // 渐变持续时间，默认设置为 2 秒

    private float elapsedTime = 0f;

    void Update()
    {
        // 只执行一次的渐变效果（在游戏开始时触发）
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Color maskColor = eyeMask.color;
            maskColor.a = Mathf.Lerp(1f, 0f, elapsedTime / duration); // 从全黑到透明
            eyeMask.color = maskColor;
        }
    }

    // 提供一个方法，方便在其他地方（如通过按钮点击事件等）触发动画
    public void StartOpenEyeEffect()
    {
        elapsedTime = 0f;
    }
}
