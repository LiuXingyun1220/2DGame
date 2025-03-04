using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackpackManager : MonoBehaviour
{
    public static BackpackManager Instance { get; private set; }
    public Transform fragmentContainer; // 地图碎片的容器

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 刷新背包界面
    public void RefreshFragmentUI()
    {
        foreach (Transform child in fragmentContainer)
        {
            int fragmentID = int.Parse(child.name); // 假设每个子对象的名称为碎片的 ID
            MapFragment fragment = MapFragmentManager.Instance.GetFragmentByID(fragmentID);

            if (fragment != null)
            {
                Image image = child.GetComponent<Image>();
                image.sprite = fragment.fragmentSprite;

                // 根据解锁状态更新显示效果
                if (fragment.isUnlocked)
                {
                    image.color = Color.white; // 解锁后显示为正常颜色
                }
                else
                {
                    image.color = new Color(0.5f, 0.5f, 0.5f, 0.5f); // 未解锁时显示为暗色
                }
            }
        }
    }

    private void Start()
    {
        RefreshFragmentUI(); // 初始化背包界面
    }
}