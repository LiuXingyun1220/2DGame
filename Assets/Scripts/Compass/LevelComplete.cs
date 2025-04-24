using UnityEngine;

public class LevelComplete : MonoBehaviour
{

    public int fragmentIndex; // 关卡对应的 Fragment 编号

    void Start()
    {
        

        OnLevelComplete();
    }

    public void OnLevelComplete()
    {
        // 获取当前已解锁关卡的数值（默认值为 0）
        int unlockedLevelIndex = PlayerPrefs.GetInt("unLockedLevelIndex", 0);

        // 假设当前关卡的索引等于 unlockedLevelIndex，则通关后解锁下一个关卡
        unlockedLevelIndex = fragmentIndex;

        // 保存更新后的关卡进度
        PlayerPrefs.SetInt("unLockedLevelIndex", unlockedLevelIndex);
        PlayerPrefs.Save();

        Debug.Log("关卡通关！更新后的未解锁关卡索引为："+ unlockedLevelIndex);
    }
}
