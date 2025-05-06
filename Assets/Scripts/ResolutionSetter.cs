using UnityEngine;

public class ResolutionSetter : MonoBehaviour
{
    // 与编辑器Game视图相同的分辨率
    public int targetWidth = 2560;
    public int targetHeight = 1440;

    void Awake()
    {
        // 设置窗口模式分辨率
        Screen.SetResolution(targetWidth, targetHeight, FullScreenMode.Windowed);

        // 或强制全屏（根据需求选择）
        // Screen.SetResolution(targetWidth, targetHeight, FullScreenMode.ExclusiveFullScreen);

        // 禁止分辨率缩放（针对高分屏）
        Screen.fullScreenMode = FullScreenMode.Windowed;
        QualitySettings.vSyncCount = 0; // 关闭垂直同步
        Application.targetFrameRate = 60; // 固定帧率
    }
}