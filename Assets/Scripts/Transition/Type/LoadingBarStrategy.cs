using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;  // Make sure DOTween is installed

public class LoadingBarStrategy : AbstractTransitionStrategy
{
    protected override IEnumerator BeforeTransition(TransitionManager manager)
    {
        //…Ë÷√±≥æ∞Õº∆¨
        var image = manager.fadePanel.GetComponent<Image>();
        if (manager.backgroundImage != null)
        {
            image.sprite = manager.backgroundImage;  // Set the sprite for the background image
        }

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

        var image = manager.fadePanel.GetComponent<Image>();
        if (manager.backgroundImage != null)
        {
            image.sprite = null;  // Set the sprite for the background image
        }

        yield return null;
    }
}
