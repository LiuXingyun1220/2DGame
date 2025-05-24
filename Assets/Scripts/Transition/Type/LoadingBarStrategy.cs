using System.Collections;
using UnityEngine;
using DG.Tweening;  // Make sure DOTween is installed

public class LoadingBarStrategy : AbstractTransitionStrategy
{
    protected override IEnumerator BeforeTransition(TransitionManager manager)
    {
        var canvasGroup = manager.fadePanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        manager.progressSlider.gameObject.SetActive(true);

        manager.progressSlider.value = 0f;
        manager.progressSlider.DOValue(1f, manager.fadeDuration).SetEase(Ease.Linear);

        // Wait until the slider animation is complete
        yield return new WaitForSeconds(manager.fadeDuration);
    }

    protected override IEnumerator AfterTransition(TransitionManager manager)
    {
        var canvasGroup = manager.fadePanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;

        manager.progressSlider.value = 0f;

        yield return null;
    }
}
