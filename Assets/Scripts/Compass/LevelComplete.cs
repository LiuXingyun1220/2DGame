using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public static LevelComplete Instance { get; private set; }

    public Dictionary<string, bool> unlockedLevels = new Dictionary<string, bool>();
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
    }

    private void InitializeLevels()
    {
        unlockedLevels = new Dictionary<string, bool>
        {
            { "D_baoping_B", true }, // 第一关默认解锁
            { "D_yuzui_B", false },
            { "D_music_B", false },
            
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

    public void UnlockLevel(string levelName)
    {
        if (unlockedLevels.ContainsKey(levelName))
        {
            unlockedLevels[levelName] = true;
            SaveLevelsToPlayerPrefs();
        }
    }

    private void SaveLevelsToPlayerPrefs()
    {
        foreach (var pair in unlockedLevels)
        {
            PlayerPrefs.SetInt(pair.Key, pair.Value ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    private void LoadLevelsFromPlayerPrefs()
    {
        foreach (var pair in unlockedLevels)
        {
            unlockedLevels[pair.Key] = PlayerPrefs.GetInt(pair.Key, pair.Value ? 1 : 0) == 1;
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
        // 遍历所有关卡，检查是否已解锁并更新对应图片
        int i = 0;
        foreach (var pair in unlockedLevels)
        {
            if (IsLevelUnlocked(pair.Key) && i < imagesToChange.Length && i < newSprites.Length)
            {
                imagesToChange[i].sprite = newSprites[i];
            }
            i++;
        }
    }
}