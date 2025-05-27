using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameBingoManager : MonoBehaviour
{
    public GameObject gameBingoPanel;
    public GameObject scoreUI;
    public GameObject background;
    public GameObject timeUI;
    public int winScoreThreshold = 4;
    public TextMeshProUGUI succeedText; // GameBingoPanel 下的得分显示组件

    private bool isGameBingo = false;

    void Start()
    {
        Data.GameEnded = false;
        gameBingoPanel.SetActive(false);
        scoreUI.SetActive(true);
        timeUI.SetActive(true);
    }

    void Update()
    {
        if (!isGameBingo && !Data.GameEnded && Data.Score > winScoreThreshold)
        {
            background.SetActive(false);
            GameBingo();
        }
    }

    void GameBingo()
    {
        isGameBingo = true;
        Data.GameEnded = true;

        TimerDisplay timer = FindObjectOfType<TimerDisplay>();
        if (timer != null)
        {
            timer.StopTimer();
            Data.TimeElapsed = timer.GetTimeUsed();
        }

        Data.FinalScore = Mathf.Max(0, 100 - Mathf.FloorToInt(Data.TimeElapsed)/6);

        if (succeedText != null){
            succeedText.text = $"得分: {Data.FinalScore}";
	    Debug.Log($"游戏胜利，最终得分: {Data.FinalScore}");
	}

        gameBingoPanel.SetActive(true);
        scoreUI.SetActive(false);
        timeUI.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
