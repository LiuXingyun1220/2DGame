using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOnly : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float delaySeconds = 5f;
    [SerializeField] private float fadeDuration = 1f;

    void Start()
    {
        // 初始状态：完全透明
        Color initialColor = targetImage.color;
        initialColor.a = 0f;
        targetImage.color = initialColor;

        StartCoroutine(DelayedFadeRoutine());
    }

    IEnumerator DelayedFadeRoutine()
    {
        // 等待指定的延迟时间
        yield return new WaitForSeconds(delaySeconds);

        // 执行淡入动画
        targetImage.DOFade(1f, fadeDuration);
    }
}
