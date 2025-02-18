using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScaleAndFadeIn : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float delaySeconds = 5f;
    [SerializeField] private float fadeDuration = 1f;

    void Start()
    {
        // 初始状态：缩小到0.1倍，完全透明
        targetImage.transform.localScale = Vector3.one * 0.1f;
        targetImage.color = new Color(1, 1, 1, 0);

        StartCoroutine(DelayedFadeRoutine());
    }

    IEnumerator DelayedFadeRoutine()
    {
        // 等待
        yield return new WaitForSeconds(delaySeconds);

        // 创建复合动画
        Sequence sequence = DOTween.Sequence();
        sequence.Append(targetImage.transform.DOScale(1, fadeDuration).SetEase(Ease.OutBack))
                .Join(targetImage.DOFade(1, fadeDuration)); // 同步执行缩放和淡入
    }
}