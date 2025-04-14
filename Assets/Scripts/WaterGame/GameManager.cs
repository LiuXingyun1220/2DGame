using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel; // 游戏结束UI面板
    public GameObject scoreUI;       // 分数Text对象（例如“Score”）

    private bool isGameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false);
        scoreUI.SetActive(true);
    }

    void Update()
    {
        int score= score = Data.Score;
        if (!isGameOver && score < 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverPanel.SetActive(true);  // 显示Game Over
        scoreUI.SetActive(false);       // 隐藏分数字
    }

    // 重开按钮调用这个
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
