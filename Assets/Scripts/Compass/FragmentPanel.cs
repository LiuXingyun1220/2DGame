using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FragmentPanel : MonoBehaviour
{
    public Image[] fragmentImages; // 关联所有 Fragment 的图片
    public Button[] fragmentButtons; // 关联所有 Fragment 的按钮
    private bool[] isUnlocked; // 存储每个 Fragment 是否解锁
    int unLockedLevelIndex;

    void Start()
    {
        isUnlocked = new bool[fragmentImages.Length];
        unLockedLevelIndex = PlayerPrefs.GetInt("unLockedLevelIndex");
       
        for (int i = 0; i < fragmentImages.Length; i++)
        {
            fragmentImages[i].color = new Color(0.5f, 0.5f, 0.5f, 1); // 设置为暗淡颜色
            fragmentButtons[i].interactable = false; // 禁用按钮
            int index = i; // 解决闭包问题
            fragmentButtons[i].onClick.AddListener(() => OnFragmentClick(index));
        }
        for (int i = 0; i < unLockedLevelIndex; i++)
        {
            fragmentImages[i].color = Color.white;
            fragmentButtons[i].interactable = true;
            isUnlocked[i] = true;

        }
    }
  


    // 点击碎片跳转到 Story
    public void OnFragmentClick(int index)
    {
        if (isUnlocked[index])
        {
            if (index == 0)
            {
                Debug.Log($"📖 跳转到 HeatAndCold_book");
                SceneManager.LoadScene("HeatAndCold_book");
            }
            else if (index == 1)
            {
                Debug.Log($"📖 跳转到 YuZui_book");
                SceneManager.LoadScene("YuZui_book");
            }
            else if (index == 2)
            {
                Debug.Log($"📖 跳转到 FeiShaYan_book");
                SceneManager.LoadScene("FeiShaYan_book");
            }
            else if (index == 3)
            {
                Debug.Log($"📖 跳转到 Compass_book");
                SceneManager.LoadScene("Compass_book");
            }
        }
    }
}