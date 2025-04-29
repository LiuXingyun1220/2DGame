using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FragmentPanel : MonoBehaviour
{
    public Image[] fragmentImages; // 关联所有 Fragment 的图片
    public Button[] fragmentButtons; // 关联所有 Fragment 的按钮
    private bool[] isUnlocked; // 存储每个 Fragment 是否解锁

    private void Start()
    {
        isUnlocked = new bool[fragmentImages.Length];

        // 初始化碎片状态
        for (int i = 0; i < fragmentImages.Length; i++)
        {
            fragmentImages[i].color = new Color(0.5f, 0.5f, 0.5f, 1); // 设置为暗淡颜色
            fragmentButtons[i].interactable = false; // 禁用按钮
            int index = i; // 解决闭包问题
            fragmentButtons[i].onClick.AddListener(() => OnFragmentClick(index));
        }

        // 根据关卡解锁状态更新碎片状态
        UpdateFragmentStatus();
    }

    public void UpdateFragmentStatus()
    {
        for (int i = 0; i < fragmentImages.Length; i++)
        {
            // 检查对应关卡是否已解锁
            string[] levelKeys = { "D_baoping_B", "D_yuzui_B", "D_music_B" };
            if (i < levelKeys.Length && LevelComplete.Instance.IsLevelUnlocked(levelKeys[i]))
            {
                fragmentImages[i].color = Color.white;
                fragmentButtons[i].interactable = true;
                isUnlocked[i] = true;
            }
        }
    }

    public void OnFragmentClick(int index)
    {
        if (isUnlocked[index])
        {
            // 使用 TransitionManager 进行场景过渡
            string targetScene = GetTargetSceneName(index);
            TransitionManager.Instance.Transition(SceneManager.GetActiveScene().name, targetScene);
        }
    }

    private string GetTargetSceneName(int index)
    {
        switch (index)
        {
            case 0:
                return "HeatAndCold_Book";
            case 1:
                return "YuZui_Book";
            case 2:
                return "Compass_Book";
            default:
                return "Compass";
        }
    }
}