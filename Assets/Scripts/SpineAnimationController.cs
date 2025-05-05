using Spine;
using Spine.Unity;
using UnityEngine;
using System.Collections;

public class SpineAnimationController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public GameObject dialogObject;
    public CanvasGroup fadeCanvasGroup;

    private void Start()
    {
        // 确保对话框一开始是隐藏的
        if (dialogObject != null)
            dialogObject.SetActive(false);

        // 开始播放动画
        PlayAnimationAndShowDialog("animation");
    }

    private void PlayAnimationAndShowDialog(string animName)
    {
        // 获取动画状态
        var animState = skeletonAnimation.AnimationState;

        // 播放动画
        TrackEntry trackEntry = animState.SetAnimation(0, animName, false);

        // 添加动画完成事件监听
        trackEntry.Complete += OnAnimationComplete;
    }

    private void OnAnimationComplete(TrackEntry trackEntry)
    {
        // 移除事件监听，防止重复触发
        trackEntry.Complete -= OnAnimationComplete;

        // 使用协程处理黑屏和显示对话框
        StartCoroutine(BlackScreenAndShowDialog());
    }

    private IEnumerator BlackScreenAndShowDialog()
    {
        // 黑屏
        fadeCanvasGroup.blocksRaycasts = true;
        fadeCanvasGroup.alpha = 1; // 设置为全黑
        Debug.Log("黑了");

        // 等待2秒
        yield return new WaitForSeconds(2f);

        // 显示对话框
        if (dialogObject != null)
            dialogObject.SetActive(true);

        // 可选：淡出黑屏
        fadeCanvasGroup.alpha = 0;
        fadeCanvasGroup.blocksRaycasts = false;
    }
}