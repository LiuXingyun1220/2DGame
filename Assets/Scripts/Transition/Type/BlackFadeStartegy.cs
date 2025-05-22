using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BlackFadeStartegy : AbstractTransitionStrategy
{
    private readonly float fadeDuration;

    public BlackFadeStartegy(float duration)
    {
        fadeDuration = duration;
    }
    protected override IEnumerator BeforeTransition(TransitionManager manager)
    {
        yield return Fade(manager,1);
    }
    protected override IEnumerator AfterTransition(TransitionManager manager)
    {
        yield return Fade(manager,0);
    }

    private IEnumerator Fade(TransitionManager manager,float targetAlpha)
    {

        manager.isFade = true;
        manager.fadeCanvasGroup.blocksRaycasts = true;//隐藏鼠标点击
        float speed = Mathf.Abs(manager.fadeCanvasGroup.alpha - targetAlpha) / manager.fadeDuration;//淡出速度
        while (!Mathf.Approximately(manager.fadeCanvasGroup.alpha, targetAlpha))
        {
            manager.fadeCanvasGroup.alpha = Mathf.MoveTowards(manager.fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        manager.fadeCanvasGroup.blocksRaycasts = false;
        manager.isFade = false;
    }
}
