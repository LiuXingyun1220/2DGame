using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public static LevelComplete Instance { get; private set; }

    public Dictionary<string, bool> unlockedLevels = new Dictionary<string, bool>();
    public Dictionary<string, bool> completedLevels = new Dictionary<string, bool>();
    public Image[] imagesToChange; // 要更换的图片数组
    public Sprite[] newSprites; // 新的图片素材数组

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

        InitializeLevels();
        UpdateImages();
    }

    private void InitializeLevels()
    {
        unlockedLevels = new Dictionary<string, bool>
        {
            { "D_baoping_B", true }, // 第一关默认解锁
            { "D_yuzui_B", false },
            { "D_music_B", false },
            {"End", false },

        };
        completedLevels = new Dictionary<string, bool>
        {
            { "D_baoping_B", false },//第二关解锁时 第一关算完成
            { "D_yuzui_B", false },//第三关解锁时 第二关和第一关才算完成
            { "D_music_B", false },
            {"End", false },
        };
        LoadLevelsFromPlayerPrefs();
    }

    public bool IsLevelUnlocked(string levelName)
    {
        if (unlockedLevels.TryGetValue(levelName, out bool isUnlocked))
        {
            return isUnlocked;
        }
        return false;
    }
    public bool IsLevelCompleted(string levelName)
    {
        if (completedLevels.TryGetValue(levelName, out bool isCompleted))
        {
            return isCompleted;
        }
        return false;
    }

    public void UnlockLevel(string levelName)
    {
        if(unlockedLevels.ContainsKey(levelName))
    {
            unlockedLevels[levelName] = true;

            // 更新 completedLevels 状态
            if (levelName == "D_yuzui_B")
            {
                completedLevels["D_baoping_B"] = true;
            }
            else if (levelName == "D_music_B")
            {
                completedLevels["D_baoping_B"] = true;
                completedLevels["D_yuzui_B"] = true;
            }else if(levelName == "End")
            {
                completedLevels["D_baoping_B"] = true;
                completedLevels["D_yuzui_B"] = true;
                completedLevels["D_music_B"] = true;
            }

            SaveLevelsToPlayerPrefs();
        }
    }

    private void SaveLevelsToPlayerPrefs()
    {
        // 保存解锁状态
        foreach (var pair in unlockedLevels)
        {
            PlayerPrefs.SetInt("Unlocked_" + pair.Key, pair.Value ? 1 : 0);
        }
        // 保存完成状态
        foreach (var pair in completedLevels)
        {
            PlayerPrefs.SetInt("Completed_" + pair.Key, pair.Value ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    private void LoadLevelsFromPlayerPrefs()
    {
        // 加载解锁状态
        List<string> unlockedKeys = new List<string>(unlockedLevels.Keys);
        foreach (var key in unlockedKeys)
        {
            unlockedLevels[key] = PlayerPrefs.GetInt("Unlocked_" + key, unlockedLevels[key] ? 1 : 0) == 1;
            Debug.Log("Loaded Unlocked_" + key + ": " + unlockedLevels[key]);
        }

        // 加载完成状态
        List<string> completedKeys = new List<string>(completedLevels.Keys);
        foreach (var key in completedKeys)
        {
            completedLevels[key] = PlayerPrefs.GetInt("Completed_" + key, completedLevels[key] ? 1 : 0) == 1;
            Debug.Log("Loaded Completed_" + key + ": " + completedLevels[key]);
        }
    }
    private void OnApplicationQuit()
    {
        SaveLevelsToPlayerPrefs();
    }

    private void Start()
    {
        // 在主场景加载完成后更新碎片和图片
        if (SceneManager.GetActiveScene().name == "Compass")
        {
            UpdateFragmentStatus();
            UpdateImages();
        }
    }

    private void UpdateFragmentStatus()
    {
        FragmentPanel fragmentPanel = FindObjectOfType<FragmentPanel>();
        if (fragmentPanel != null)
        {
            fragmentPanel.UpdateFragmentStatus();
        }
    }


    private void UpdateImages()
    {
        int i = 0;
        foreach (var pair in completedLevels)
        {
            if (IsLevelCompleted(pair.Key) && i < imagesToChange.Length && i < newSprites.Length)
            {
                imagesToChange[i].sprite = newSprites[i];
                Debug.Log("更新图片：" +  pair.Key);
            }
            i++;
            Debug.Log("未更新图片：" + pair.Key);
        }
    }

}

