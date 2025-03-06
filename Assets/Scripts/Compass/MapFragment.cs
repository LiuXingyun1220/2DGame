using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapFragment : MonoBehaviour
{
    public Image[] fragmentImages; // 地图碎片的Image数组
    public Button[] fragmentButtons; // 地图碎片的Button数组
    private bool[] isUnlocked; // 记录每个碎片是否解锁

    void Start()
    {
        isUnlocked = new bool[fragmentImages.Length];
        for (int i = 0; i < fragmentImages.Length; i++)
        {
            fragmentImages[i].color = new Color(0.5f, 0.5f, 0.5f, 1); // 设置暗淡颜色
            fragmentButtons[i].interactable = false; // 禁用按钮
            // 绑定点击事件
            fragmentButtons[i].onClick.AddListener(() => OnFragmentClick(i));
        }
    }

    // 解锁指定的碎片
    public void UnlockFragment(int index)
    {
        if (index >= 0 && index < fragmentImages.Length)
        {
            fragmentImages[index].color = Color.white; // 设置为明亮状态
            fragmentButtons[index].interactable = true; // 启用按钮
            isUnlocked[index] = true; // 标记为解锁状态
        }
    }

    // 碎片点击事件
    public void OnFragmentClick(int index)
    {
        if (isUnlocked[index])
        {
            Debug.Log($"故事 {index} 被触发");
            // 在这里添加讲述故事的逻辑
        }
    }
}