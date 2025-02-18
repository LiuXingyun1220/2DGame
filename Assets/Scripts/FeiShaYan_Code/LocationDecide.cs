using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // 引用SpriteRenderer组件
    private const float rate = 3.8f;// 用于调整河流宽度
    private Vector2 locationChange = new Vector2(0.6f,0.3f);// 用于调整图像位置
    private void Update()
    {
        // 获取Transform组件
        Transform transform = spriteRenderer.transform;
        // 设置位置（在世界坐标系中）
        transform.position = FeiShaYanManager.GetStartPosData() - locationChange;
        // 设置缩放
        transform.localScale = new Vector3(0.7f, FeiShaYanManager.GetWidthData()/(FeiShaYanManager.WidthRate * rate), 0.7f); // 注意Z轴通常保持为1，除非你有特殊需求
        // 设置旋转（以度为单位）
        transform.rotation = Quaternion.Euler(0f, 0f, -22.5f); // 例如，绕Z轴旋转45度
    }
}