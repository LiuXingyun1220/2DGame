using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BlackFadeStrategy : AbstractTransitionStrategy
{
    protected override IEnumerator BeforeTransition(TransitionManager manager)
    {
        manager.progressSlider.gameObject.SetActive(false);
        yield return Fade(manager,1);
    }
    protected override IEnumerator AfterTransition(TransitionManager manager)
    {
        yield return Fade(manager,0);
    }

    private IEnumerator Fade(TransitionManager manager,float targetAlpha)
    {
        var canvasGroup = manager.fadePanel.GetComponent<CanvasGroup>();
        var image = manager.fadePanel.GetComponent<Image>();

        //设置颜色为黑色
        Color color = image.color;
        color.r = 0;
        color.g = 0;
        color.b = 0;
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
