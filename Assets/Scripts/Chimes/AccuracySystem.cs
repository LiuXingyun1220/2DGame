using UnityEngine;

public static class AccuracySystem
{
    public enum Judgment { Perfect, Good, Miss }
    const float PERFECT_THRESHOLD = 0.08f;
    const float GOOD_THRESHOLD = 0.15f;

    public static Judgment Evaluate(float deltaTime)
    {
        float absDelta = Mathf.Abs(deltaTime);
        if (absDelta <= PERFECT_THRESHOLD) return Judgment.Perfect;
        if (absDelta <= GOOD_THRESHOLD) return Judgment.Good;
        return Judgment.Miss;
    }
}