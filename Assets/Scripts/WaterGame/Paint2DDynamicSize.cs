using UnityEngine;

public class Paint2DDynamicSize : MonoBehaviour
{
    public LineRenderer lineRenderer;  // 通过Inspector关联LineRenderer
    public float MaxSize = 1.0f;
    public float MinSize = 0.1f;
    public float rate = 5;

    private float currentWidth;  // 当前的宽度

    void Start()
    {
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();  // 如果没有手动设置，自动查找LineRenderer组件
        }

        currentWidth = lineRenderer.startWidth;  // 初始宽度
    }

    void Update()
    {
        if (lineRenderer != null)
        {
            if (Input.GetMouseButton(0))  // 鼠标按下时，增加宽度
            {
                if (currentWidth < MaxSize)
                    currentWidth += Time.deltaTime * rate;
            }
            else  // 鼠标松开时，逐渐减小宽度
            {
                currentWidth = Mathf.Clamp(currentWidth - Time.deltaTime * rate, MinSize, MaxSize);
            }

            // 更新LineRenderer的宽度
            lineRenderer.startWidth = currentWidth;
            lineRenderer.endWidth = currentWidth;
        }
    }
}
