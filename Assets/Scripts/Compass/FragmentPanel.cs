using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FragmentPanel : MonoBehaviour
{
    public Image[] fragmentImages; // 关联所有 Fragment 的图片
    public Button[] fragmentButtons; // 关联所有 Fragment 的按钮
    private bool[] isUnlocked; // 存储每个 Fragment 是否解锁

    void Start()
    {
        int totalFragments = fragmentImages.Length;
        isUnlocked = new bool[totalFragments];

        // 读取存储的解锁状态
        for (int i = 0; i < totalFragments; i++)
        {
            isUnlocked[i] = PlayerPrefs.GetInt($"FragmentUnlocked_{i}", 0) == 1;

            if (isUnlocked[i])
            {
                fragmentImages[i].color = Color.white; // 亮色
                fragmentButtons[i].interactable = true; // 启用按钮
            }
            else
            {
                fragmentImages[i].color = new Color(0.5f, 0.5f, 0.5f, 1); // 变灰
                fragmentButtons[i].interactable = false; // 禁用按钮
            }

            // 绑定点击事件
            int index = i;
            fragmentButtons[i].onClick.AddListener(() => OpenStory(index));
        }
    }

    // 解锁指定的 Fragment
    public void UnlockFragment(int index)
    {
        if (index >= 0 && index < fragmentImages.Length)
        {
            fragmentImages[index].color = Color.white; // 亮色
            fragmentButtons[index].interactable = true; // 启用按钮
            isUnlocked[index] = true;

            // 存储解锁状态
            PlayerPrefs.SetInt($"FragmentUnlocked_{index}", 1);
            PlayerPrefs.Save();
        }
    }

    // 点击 Fragment，进入相应故事页面
    public void OpenStory(int index)
    {
        if (isUnlocked[index])
        {
            Debug.Log($"打开故事 {index}");
            SceneManager.LoadScene($"Story_{index}"); // 这里 Scene 名称要与 Unity 场景匹配
        }
    }
}
