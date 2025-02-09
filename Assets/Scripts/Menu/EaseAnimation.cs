using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EaseAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform backgroundImage;
    public float duration = 1.0f; // 动画持续时间

    private float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.height;

        backgroundImage.anchoredPosition = Vector2.zero;

        //执行动画
        AnimateTransition();
    }
    void AnimateTransition()
    {
        float screenHeight = Screen.height;
        float moveHeight=backgroundImage.rect.height-screenHeight;

        // 顶图下滑
        backgroundImage.DOAnchorPosY((float)(-moveHeight), duration).SetEase(Ease.InOutQuad);
    }
}
