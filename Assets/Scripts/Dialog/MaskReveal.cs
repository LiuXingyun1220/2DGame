using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskReveal : MonoBehaviour
{
    public RectTransform maskRectTransform;
    public float revealSpeed = 1.0f;
    public float delayBetweenColumns = 1.0f;

    private Vector2 initialSize;
    private bool isFirstColumnRevealed = false;

    private void Start()
    {
        initialSize = maskRectTransform.sizeDelta;
        // 设置遮罩初始大小为 0（完全隐藏）
        maskRectTransform.sizeDelta = new Vector2(0, initialSize.y);
    }

    private void Update()
    {
        if (!isFirstColumnRevealed)
        {
            // 从右到左显示第一列
            float newSizeX = Mathf.Lerp(0, initialSize.x / 2, Time.time * revealSpeed / initialSize.x);
            maskRectTransform.sizeDelta = new Vector2(newSizeX, initialSize.y);

            if (newSizeX >= initialSize.x / 2)
            {
                isFirstColumnRevealed = true;
                Invoke("StartSecondColumnReveal", delayBetweenColumns);
            }
        }
    }

    private void StartSecondColumnReveal()
    {
        // 从上到下显示第二列
        StartCoroutine(RevealSecondColumn());
    }

    private System.Collections.IEnumerator RevealSecondColumn()
    {
        float startY = initialSize.y / 2;
        float targetY = initialSize.y;

        while (maskRectTransform.sizeDelta.y < targetY)
        {
            float newY = Mathf.Lerp(startY, targetY, Time.time * revealSpeed / (startY));
            maskRectTransform.sizeDelta = new Vector2(initialSize.x, newY);
            yield return null;
        }
    }
}
