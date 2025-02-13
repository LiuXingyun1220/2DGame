using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// 评价系统
public class EvaluationSystem : MonoBehaviour
{
    [System.Serializable]
    public class HistoricalData
    {
        public float optimalHeight = 2.1f;
        public float optimalWidth = 240f;
        public float minIrrigation = 100f;
    }

    public HistoricalData realData;

    public void EvaluatePerformance()
    {
        float heightScore = Mathf.Clamp01(
            1 - Mathf.Abs(GameManager.GetHeightData() - realData.optimalHeight) / 0.5f
        );

        float widthScore = Mathf.Clamp01(
            1 - Mathf.Abs(GameManager.GetWidthData() - realData.optimalWidth) / 3f
        );

        float irrigationScore = (GameManager.GetIrrigationData() > realData.minIrrigation) ? 1 : 0;

        float totalScore = (heightScore * 0.4f + widthScore * 0.3f + irrigationScore * 0.3f) * 100f;
        Debug.Log(totalScore);

        if (totalScore > 25)
        {
            //成功
            SceneManager.LoadScene(GameManager.SucceedScene);
        }
        else
        {
            //失败
            SceneManager.LoadScene(GameManager.DefeatScene);
        }
    }
}
