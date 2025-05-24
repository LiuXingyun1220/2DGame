using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudFadeStrategy : AbstractTransitionStrategy
{
    protected override IEnumerator BeforeTransition(TransitionManager manager)
    {
        manager.cloudGroup.SetActive(false);
        yield return Fade(manager,1);
    }
    protected override IEnumerator AfterTransition(TransitionManager manager)
    {
        manager.cloudGroup.SetActive(true);
        yield return Fade(manager, 0);
    }

    private IEnumerator Fade(TransitionManager manager, float targetAlpha)
    {
        var canvasGroup = manager.fadePanel.GetComponent<CanvasGroup>();
        var image = manager.fadePanel.GetComponent<Image>();

        //设置颜色为黑色
        Color color = image.color;
        color.r = 255;
        color.g = 255;
        color.b = 255;
        image.color = color;

        manager.isFade = true;
        canvasGroup.blocksRaycasts = true;//隐藏鼠标点击
        float speed = Mathf.Abs(canvasGroup.alpha - targetAlpha) / manager.fadeDuration;//淡出速度
        while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        canvasGroup.blocksRaycasts = false;
        manager.isFade = false;
    }
}
