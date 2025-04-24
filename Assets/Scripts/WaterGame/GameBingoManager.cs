using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBingoManager : MonoBehaviour
{
    public GameObject gameBingoPanel; // 游戏胜利UI面板
    public GameObject scoreUI;        // 分数UI对象
    public int winScoreThreshold = 6; // 触发胜利的分数阈值

    private bool isGameBingo = false;

    void Start()
    {
        gameBingoPanel.SetActive(false); // 默认隐藏胜利UI
        scoreUI.SetActive(true);         // 显示分数
    }

    void Update()
    {
        if (!isGameBingo && Data.Score > winScoreThreshold)
        {
            GameBingo();
        }
    }

    void GameBingo()
    {
        isGameBingo = true;
        gameBingoPanel.SetActive(true);  // 显示胜利UI
        scoreUI.SetActive(false);        // 隐藏分数UI
    }

    // 重新开始游戏
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
