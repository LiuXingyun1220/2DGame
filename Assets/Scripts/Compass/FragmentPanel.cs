using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FragmentPanel : MonoBehaviour
{
    [Header("UI Components")]
    public Image[] fragmentImages; // 碎片图片数组
    public Button[] fragmentButtons; // 碎片按钮数组

    [Header("Level Settings")]
    [Tooltip("关卡顺序必须与按钮索引对应")]
    public string[] levelKeys = { "D_baoping_B", "D_yuzui_B", "D_music_B" };

    private void Start()
    {
        InitializeFragments();
        UpdateFragmentStatus();
    }

    // 初始化碎片为锁定状态
    private void InitializeFragments()
    {
        for (int i = 0; i < fragmentImages.Length; i++)
        {
            fragmentImages[i].color = new Color(0.5f, 0.5f, 0.5f, 1);
            fragmentButtons[i].interactable = false;
        }
    }

    // 更新碎片解锁状态
    public void UpdateFragmentStatus()
    {
        for (int i = 0; i < levelKeys.Length; i++)
        {
            if (i >= fragmentImages.Length) break;

            bool isUnlocked = LevelComplete.Instance.IsLevelCompleted(levelKeys[i]);
            fragmentImages[i].color = isUnlocked ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1);
            fragmentButtons[i].interactable = isUnlocked;
        }
    }

    // 公开的点击方法（通过Inspector配置参数）
    public void OnFragmentClick(int index)
    {
        if (index < 0 || index >= levelKeys.Length) return;

        string targetScene = GetTargetSceneName(index);
        Debug.Log($"跳转到场景: {targetScene}");
        TransitionManager.Instance.Transition(SceneManager.GetActiveScene().name, targetScene);
    }

    // 获取场景名称
    private string GetTargetSceneName(int index)
    {
        switch (index)
        {
            case 0: return "HeatAndCold_Book";
            case 1: return "YuZui_Book";
            case 2: return "Compass_Book";
            default: return "Compass";
        }
    }
}