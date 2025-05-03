using UnityEngine;
using UnityEngine.UI;
public class ImageToggle : MonoBehaviour
{
    // 需要在Inspector面板拖拽赋值
    [Header("需要显示的Image")]
    public Image[] imagesToShow;
    [Header("需要隐藏的Image")]
    public Image[] imagesToHide;
    [Header("切换设置")]
    public BookPro bookPro;
    public int minimumPageRequired;

    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            if (bookPro != null && bookPro.currentPaper >= minimumPageRequired)
            {
                ToggleImages();
            }
        }
    }
    void ToggleImages()
    {
        // 显示目标Image
        foreach (Image img in imagesToShow)
        {
            if (img != null)
            {
                img.gameObject.SetActive(true);
            }
        }
        // 隐藏目标Image
        foreach (Image img in imagesToHide)
        {
            if (img != null)
            {
                img.gameObject.SetActive(false);
            }
        }
    }
}