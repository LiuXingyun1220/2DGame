using UnityEngine;
using UnityEngine.UI;

public class LoopingImageMover : MonoBehaviour
{
    public float speed = 100f; // 每秒移动像素
    private RectTransform rectTransform;
    private float imageWidth;
    private float screenHalfWidth;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        imageWidth = rectTransform.rect.width;
        screenHalfWidth = Screen.width / 2f;
    }

    void Update()
    {
        // 移动图像中心点向左
        rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;

        // 判断图片右侧是否离开屏幕左边缘
        float imageRightEdge = rectTransform.anchoredPosition.x + (imageWidth / 2f);
        if (imageRightEdge < -screenHalfWidth)
        {
            // 重置到屏幕右侧之外
            rectTransform.anchoredPosition = new Vector2(screenHalfWidth + imageWidth / 2f, rectTransform.anchoredPosition.y);
        }
    }
}
