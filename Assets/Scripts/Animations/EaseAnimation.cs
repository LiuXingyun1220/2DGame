using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EaseAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform easeObject;
    public float duration = 5.0f; // 动画持续时间

    private float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.height;

        easeObject.anchoredPosition = Vector2.zero;

        AudioManager.Instance.PlayMusic("start");

        //执行动画
        AnimateTransition();
    }
    void AnimateTransition()
    {
        CanvasScaler scaler = GetComponent<CanvasScaler>();
        Vector2 refRes = scaler.referenceResolution;

        float screenHeight = refRes.y;
        float moveHeight = easeObject.rect.height  - screenHeight;
        //Debug.Log(moveHeight);

        // 下滑
        easeObject.DOAnchorPosY((float)(-moveHeight), duration).SetEase(Ease.InOutQuad);
    }
}