using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static AccuracySystem;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    //[Header("Scoring Settings")]
    //[SerializeField] private int perfectPoints = 100;
    //[SerializeField] private int goodPoints = 50;
    //[SerializeField] private int missPoints = 0;
    //[SerializeField] private int comboMultiplierThreshold = 10; // Every X combo increases multiplier
    //[SerializeField] private float comboMultiplierValue = 0.1f; // Multiplier increase per threshold

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreText1;
    //[SerializeField] private TextMeshProUGUI comboText;
    //[SerializeField] private TextMeshProUGUI multiplierText;
    //[SerializeField] private TextMeshProUGUI judgmentText;
    //[SerializeField] private float judgmentDisplayTime = 1f;

    private int currentScore = 0;
    //private int currentCombo = 0;
    //private int maxCombo = 0;
    //private float currentMultiplier = 1f;
    //private Coroutine judgmentCoroutine;

    // Statistics
    //private int perfectCount = 0;
    //private int goodCount = 0;
    //private int missCount = 0;
    //private int totalNotes = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize UI
        UpdateScoreUI();
        //UpdateComboUI();
        //UpdateMultiplierUI();

        //if (judgmentText != null)
        //{
        //    judgmentText.gameObject.SetActive(false);
        //}
    }

    public void AddScore(int score)
    {
        currentScore+=score;
        //totalNotes++;

        // Calculate points based on judgment
        //int basePoints = GetPointsForJudgment(judgment);

        // Update combo
        //if (judgment != Judgment.Miss)
        //{
        //    //currentCombo++;
        //    //if (currentCombo > maxCombo)
        //    //{
        //    //    maxCombo = currentCombo;
        //    //}

        //    //// Update multiplier based on combo
        //    //currentMultiplier = 1f + (Mathf.Floor(currentCombo / comboMultiplierThreshold) * comboMultiplierValue);
        //}
        //else
        //{
        //    currentCombo = 0;
        //    currentMultiplier = 1f;
        //}

        // Add points with multiplier
        //int pointsToAdd = Mathf.RoundToInt(basePoints * currentMultiplier);
        //currentScore += pointsToAdd;

        // Update statistics
        //UpdateStatistics(judgment);

        // Update UI
        UpdateScoreUI();
        //UpdateComboUI();
        //UpdateMultiplierUI();
        //DisplayJudgment(judgment, pointsToAdd);
    }

    //private int GetPointsForJudgment(Judgment judgment)
    //{
    //    switch (judgment)
    //    {
    //        case Judgment.Perfect: return perfectPoints;
    //        case Judgment.Good: return goodPoints;
    //        case Judgment.Miss: return missPoints;
    //        default: return 0;
    //    }
    //}

    //private void UpdateStatistics(Judgment judgment)
    //{
    //    switch (judgment)
    //    {
    //        case Judgment.Perfect:
    //            perfectCount++;
    //            break;
    //        case Judgment.Good:
    //            goodCount++;
    //            break;
    //        case Judgment.Miss:
    //            missCount++;
    //            break;
    //    }
    //}

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"ตรทึ: {currentScore}";
        }
        if (scoreText1 != null)
        {
            scoreText1.text = $" {currentScore}";
        }
    }

    //private void UpdateComboUI()
    //{
    //    if (comboText != null)
    //    {
    //        comboText.text = currentCombo > 1 ? $"Combo: {currentCombo}" : "";
    //    }
    //}

    //private void UpdateMultiplierUI()
    //{
    //    if (multiplierText != null && currentMultiplier > 1f)
    //    {
    //        multiplierText.text = $"x{currentMultiplier:F1}";
    //    }
    //    else if (multiplierText != null)
    //    {
    //        multiplierText.text = "";
    //    }
    //}

    //private void DisplayJudgment(Judgment judgment, int points)
    //{
    //    if (judgmentText == null) return;

    //    string judgmentString = judgment.ToString();

    //    // Set color based on judgment
    //    switch (judgment)
    //    {
    //        case Judgment.Perfect:
    //            judgmentText.color = Color.yellow;
    //            break;
    //        case Judgment.Good:
    //            judgmentText.color = Color.green;
    //            break;
    //        case Judgment.Miss:
    //            judgmentText.color = Color.red;
    //            break;
    //    }

    //    judgmentText.text = $"{judgmentString}\n+{points}";
    //    judgmentText.gameObject.SetActive(true);

    //    // Cancel previous coroutine if exists
    //    if (judgmentCoroutine != null)
    //    {
    //        StopCoroutine(judgmentCoroutine);
    //    }

    //    // Start new coroutine
    //    judgmentCoroutine = StartCoroutine(HideJudgmentAfterDelay());
    //}

    //private IEnumerator HideJudgmentAfterDelay()
    //{
    //    yield return new WaitForSeconds(judgmentDisplayTime);
    //    if (judgmentText != null)
    //    {
    //        judgmentText.gameObject.SetActive(false);
    //    }
    //}

    // Call this at the end of the level to show results
    //public void ShowResults()
    //{
    //    float accuracy = totalNotes > 0 ?
    //        (float)(perfectCount * 100 + goodCount * 50) / (totalNotes * 100) * 100f : 0f;

    //    Debug.Log($"Level Complete!\nScore: {currentScore}\nMax Combo: {maxCombo}\n" +
    //             $"Perfect: {perfectCount}\nGood: {goodCount}\nMiss: {missCount}\n" +
    //             $"Accuracy: {accuracy:F1}%");

    //    // Here you could show a UI panel with these results
    //}

    // For saves or high scores
    //public GameResult GetGameResult()
    //{
    //    return new GameResult
    //    {
    //        Score = currentScore,
    //        MaxCombo = maxCombo,
    //        PerfectCount = perfectCount,
    //        GoodCount = goodCount,
    //        MissCount = missCount,
    //        TotalNotes = totalNotes
    //    };
    //}

    // Reset the score manager for a new game
    public void ResetScore()
    {
        currentScore = 0;
        //currentCombo = 0;
        //maxCombo = 0;
        //currentMultiplier = 1f;

        //perfectCount = 0;
        //goodCount = 0;
        //missCount = 0;
        //totalNotes = 0;

        UpdateScoreUI();
        //UpdateComboUI();
        //UpdateMultiplierUI();
    }

    public int getScore()
    {
        return currentScore;
    }
}

// Structure to hold game results
//[System.Serializable]
//public struct GameResult
//{
//    public int Score;
//    public int MaxCombo;
//    public int PerfectCount;
//    public int GoodCount;
//    public int MissCount;
//    public int TotalNotes;

//    public float AccuracyPercentage => TotalNotes > 0 ?
//        (float)(PerfectCount * 100 + GoodCount * 50) / (TotalNotes * 100) * 100f : 0f;
//}
