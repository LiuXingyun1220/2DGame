using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FragmentPanel : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject[] fragments;
    public GameObject text;// 碎片按钮数组

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
        for (int i = 0; i < fragments.Length; i++)
        {
            fragments[i].SetActive(false);
            
        }
    }

    // 更新碎片解锁状态
    public void UpdateFragmentStatus()
    {
       
        for (int i = 0; i < levelKeys.Length; i++)
        {
            
            if (i >= fragments.Length) break;

            bool isUnlocked = LevelComplete.Instance.IsLevelCompleted(levelKeys[i]);
            fragments[i].SetActive(isUnlocked);
            if (isUnlocked)
            {
                text.SetActive(false);
            }
        }
    }

    // 公开的点击方法（通过Inspector配置参数）
    public void OnFragmentClick(int index)
    {
        if (index < 0 || index >= levelKeys.Length) return;

        string targetScene = GetTargetSceneName(index);
        Debug.Log($"跳转到场景: {targetScene}");
        TransitionManager.Instance.Transition(SceneManager.GetActiveScene().name, targetScene);
        SceneManager.LoadScene(targetScene);

    }

    // 获取场景名称
    private string GetTargetSceneName(int index)
    {
        switch (index)
        {
            case 0: return "HeatAndCold_Book";
            case 1: return "YuZui_Book";
            case 2: return "Music_Book";
            default: return "Compass";
        }
    }
}