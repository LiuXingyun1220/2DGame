using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // 游戏结束UI面板
    public GameObject scoreUI;       // 分数Text对象（例如“Score”）
    public GameObject background;
    ///public GameObject Water;
    //public GameObject DirtyWater;

    private bool isGameOver = false;

    void Start()
    {
        Data.Score = 0;
        gameOverPanel.SetActive(false);
        scoreUI.SetActive(true);
    }

    void Update()
    {
        int score= Data.Score;
        if (!isGameOver && score < 0)
        {
            background.SetActive(false);
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        //Water.SetActive(false);
        //DirtyWater.SetActive(false);
        gameOverPanel.SetActive(true);  // 显示Game Over
        scoreUI.SetActive(false);       // 隐藏分数字
    }

    // 重开按钮调用这个
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
