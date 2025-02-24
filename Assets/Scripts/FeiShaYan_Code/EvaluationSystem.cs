using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// 评价系统
public class EvaluationSystem : MonoBehaviour
{
    [System.Serializable]
    public class HistoricalData
    {
        public float optimalHeight = 2.15f;
        public float optimalWidth = 240f;
        public float minIrrigation = 300f;
        public float minSedimentErosion = 140f;
        public Vector2 realpoint = new Vector2(4.15f, 2.2f);
    }

    public HistoricalData realData;

    public void EvaluatePerformance()
    {
        FeiShaYanManager.SetHeightScore(Mathf.Clamp01(
            1 - Mathf.Abs(FeiShaYanManager.GetHeightData() - realData.optimalHeight) / 4.88f
        ));
        
        FeiShaYanManager.SetWidthScore(Mathf.Clamp01(
            1 - Mathf.Abs(FeiShaYanManager.GetWidthData() - realData.optimalWidth) / 850f
        ));
        
        Vector2 midpoint = (FeiShaYanManager.GetStartPosData() + FeiShaYanManager.GetEndPosData()) * 0.5f;
        float distance = Vector2.Distance(realData.realpoint, midpoint);
        FeiShaYanManager.SetDistanceScore(distance*FeiShaYanManager.WidthRate < 100f ? 1 : 0);
       
        FeiShaYanManager.SetIrrigationScore((FeiShaYanManager.GetIrrigationData() > realData.minIrrigation) ? 1 : 0);
        
        FeiShaYanManager.SetSedimentErosionScore((FeiShaYanManager.GetSedimentErosionData() > realData.minSedimentErosion) ? 1 : 0);
        
        float totalScore = (FeiShaYanManager.GetHeightScore() * 0.2f + FeiShaYanManager.GetWidthScore() * 0.2f + FeiShaYanManager.GetDistanceScore() * 0.1f + FeiShaYanManager.GetIrrigationScore() * 0.25f + FeiShaYanManager.GetSedimentErosionScore() * 0.25f) * 100f;
        Debug.Log(totalScore);

        Scene TopViewScene = SceneManager.GetSceneByName(FeiShaYanManager.TopViewScene);
        Scene SectionalViewScene = SceneManager.GetSceneByName(FeiShaYanManager.SectionalViewScene);
        if (TopViewScene.IsValid())
        {
            if (totalScore > 60)
            {
                //成功
                MapManager.SetMap4Condition(true);
                ChangeScene(FeiShaYanManager.TopViewScene, FeiShaYanManager.SucceedScene);
            }
            else
            {
                //失败
                ChangeScene(FeiShaYanManager.TopViewScene, FeiShaYanManager.DefeatScene);
            }
        }
        else if (SectionalViewScene.IsValid())
        {
            if (totalScore > 60)
            {
                //成功
                MapManager.SetMap4Condition(true);
                ChangeScene(FeiShaYanManager.SectionalViewScene, FeiShaYanManager.SucceedScene);
            }
            else
            {
                //失败
                ChangeScene(FeiShaYanManager.SectionalViewScene, FeiShaYanManager.DefeatScene);
            }
        }
    }
    private void ChangeScene(string from, string to)
    {
        Scene scene = SceneManager.GetSceneByName(from);
        // 检查场景是否有效且已加载
        if (scene.IsValid() && scene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(from);
        }
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);
        // 添加一个协程来等待加载完成
        StartCoroutine(WaitForSceneToLoad(loadOp));
    }
    private IEnumerator WaitForSceneToLoad(AsyncOperation op)
    {
        // 等待直到场景加载完成
        while (!op.isDone)
        {
            yield return null;
        }
        // 设置新场景为激活场景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
    }
}
