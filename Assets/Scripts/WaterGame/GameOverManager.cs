using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // 失败UI面板
    public GameObject scoreUI;       // 游戏中分数UI
    public GameObject background;
    public GameObject timeUI;
    public TextMeshProUGUI defeatText; // 失败面板下用于显示分数的Text

    private bool isGameOver = false;

    void Start()
    {
        Data.GameEnded = false;
        Data.Score = 0;
        gameOverPanel.SetActive(false);
        scoreUI.SetActive(true);
        timeUI.SetActive(true);
    }

    void Update()
    {
        if (!isGameOver && !Data.GameEnded && Data.Score < 0)  // 触发失败条件
        {
            background.SetActive(false);
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Data.GameEnded = true;

        TimerDisplay timer = FindObjectOfType<TimerDisplay>();
        if (timer != null)
        {
            timer.StopTimer();
            Data.TimeElapsed = timer.GetTimeUsed();
        }

        //Data.FinalScore = Mathf.Max(0, 100 - Mathf.FloorToInt(Data.TimeElapsed)/5);
	Data.FinalScore =20;

        if (defeatText != null)
        {
            //defeatText.text = $"得分: {Data.FinalScore-20}";
	    //defeatText.text = $"<size=100>得分:</size> <size=250><b>{Data.FinalScore-20}</b></size>";
	    defeatText.text = $"<size=500><b>{Data.FinalScore - 20}</b></size><size=100><voffset=-60>\u2005分</voffset></size>";
	    Debug.Log($"游戏失败，最终得分: {Data.FinalScore}");
        }

        gameOverPanel.SetActive(true);
        scoreUI.SetActive(false);
        timeUI.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
