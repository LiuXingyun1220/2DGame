using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Volume))]
public class EyeOpenAnimator : MonoBehaviour
{
    [Tooltip("动画曲线：X轴为时间(秒)，Y轴为睁眼程度(0-1)")]
    public AnimationCurve animationCurve = new AnimationCurve(
        new Keyframe(0, 0),
        new Keyframe(1, 1)
    );

    [Tooltip("是否循环播放")]
    public bool loop = false;

    [Tooltip("循环播放的间隔时间(秒)")]
    public float loopInterval = 1f;

    private Volume volume;
    private EyeOpen eyeOpenEffect;
    private float currentTime = 0f;
    private float intervalTimer = 0f;
    private bool isPlaying = false;
    private bool isWaitingInterval = false;

    private void Awake()
    {
        volume = GetComponent<Volume>();
        if (volume.profile.TryGet<EyeOpen>(out eyeOpenEffect))
        {
            currentTime = 0f;
            volume.weight = 0f;
            // 场景进入时立即激活并播放动画
            volume.weight = 1f;
            Play();
        }
        else
        {
            Debug.LogError("未在Volume中找到EyeOpen效果！");
            enabled = false;
        }
    }

    private void Update()
    {
        if (!isPlaying) return;

        if (isWaitingInterval)
        {
            intervalTimer += Time.deltaTime;
            if (intervalTimer >= loopInterval)
            {
                isWaitingInterval = false;
                intervalTimer = 0f;
                currentTime = 0f;
            }
            return;
        }

        currentTime += Time.deltaTime;
        float duration = animationCurve[animationCurve.length - 1].time;

        if (currentTime >= duration)
        {
            if (loop)
            {
                isWaitingInterval = true;
                intervalTimer = 0f;
            }
            else
            {
                isPlaying = false;
                currentTime = duration;
            }
        }

        // 更新_Strength值
        if (eyeOpenEffect != null)
        {
            eyeOpenEffect._Strength.value = animationCurve.Evaluate(currentTime);
        }
    }

    // 开始播放动画
    public void Play()
    {
        currentTime = 0f;
        intervalTimer = 0f;
        isWaitingInterval = false;
        isPlaying = true;
    }

    // 暂停动画
    public void Pause()
    {
        isPlaying = false;
    }

    // 继续播放
    public void Resume()
    {
        isPlaying = true;
    }

    // 停止动画并重置
    public void Stop()
    {
        isPlaying = false;
        isWaitingInterval = false;
        currentTime = 0f;
        intervalTimer = 0f;
        if (eyeOpenEffect != null)
        {
            eyeOpenEffect._Strength.value = animationCurve.Evaluate(0);
        }
    }
}