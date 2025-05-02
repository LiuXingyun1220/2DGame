using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 将此脚本添加到您的Image按钮对象上
public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // 在Inspector中可调整的参数
    [Header("悬停效果")]
    [SerializeField] private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    [SerializeField] private float hoverBrightness = 1.2f;
    [SerializeField] private Color hoverColor = Color.white; // 设置一个颜色以应用色调

    [Header("点击效果")]
    [SerializeField] private Vector3 pressedScale = new Vector3(0.9f, 0.9f, 0.9f);

    [Header("动画设置")]
    [SerializeField] private float animationSpeed = 10f;

    // 内部变量
    private Vector3 originalScale;
    private Image buttonImage;
    private Color originalColor;
    private bool isHovering = false;
    private bool isPressed = false;

    void Start()
    {
        // 保存原始值
        originalScale = transform.localScale;
        buttonImage = GetComponent<Image>();

        if (buttonImage != null)
        {
            originalColor = buttonImage.color;
        }
    }

    void Update()
    {
        // 根据当前状态平滑过渡到目标比例
        Vector3 targetScale = originalScale;
        Color targetColor = originalColor;

        if (isPressed)
        {
            // 点击状态
            targetScale = Vector3.Scale(originalScale, pressedScale);
        }
        else if (isHovering)
        {
            // 悬停状态
            targetScale = Vector3.Scale(originalScale, hoverScale);

            // 调整颜色亮度
            if (buttonImage != null)
            {
                targetColor = new Color(
                    originalColor.r * hoverColor.r * hoverBrightness,
                    originalColor.g * hoverColor.g * hoverBrightness,
                    originalColor.b * hoverColor.b * hoverBrightness,
                    originalColor.a);
            }
        }

        // 平滑缩放过渡
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animationSpeed);

        // 平滑颜色过渡
        if (buttonImage != null)
        {
            buttonImage.color = Color.Lerp(buttonImage.color, targetColor, Time.deltaTime * animationSpeed);
        }
    }

    // 鼠标/触摸悬停在按钮上
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    // 鼠标/触摸离开按钮
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        isPressed = false;
    }

    // 鼠标/触摸按下按钮
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    // 鼠标/触摸释放按钮
    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}