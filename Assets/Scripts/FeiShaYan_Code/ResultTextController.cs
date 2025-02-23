using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultTextController : MonoBehaviour
{
    public TextMeshProUGUI sedimentErosionRateText;//泥沙冲刷量
    public TextMeshProUGUI irrigationSupplyText;//灌溉供水量
    public TextMeshProUGUI reasonText;
    public TextMeshProUGUI adviseText;
    // Start is called before the first frame updated
    void Start()
    {
        sedimentErosionRateText.text = $"{FeiShaYanManager.GetSedimentErosionData()}";
        irrigationSupplyText.text = $"{FeiShaYanManager.GetIrrigationData()}";
        Scene DefeatScene = SceneManager.GetSceneByName(FeiShaYanManager.DefeatScene);
        if (DefeatScene.IsValid())
        {
            if (FeiShaYanManager.GetSedimentErosionScore() == 0)
            {
                reasonText.text += "泥沙冲刷量不足";
            }
            if (FeiShaYanManager.GetIrrigationScore() == 0)
            {
                reasonText.text += "灌溉供水量不足";
            }
            if (FeiShaYanManager.GetWidthScore() < 0.6)
            {
                adviseText.text += "修改宽度 ";
            }
            if (FeiShaYanManager.GetHeightScore() < 0.6)
            {
                adviseText.text += "修改高度 ";
            }
            if (FeiShaYanManager.GetDistanceScore() == 0)
            {
                adviseText.text += "调整位置 ";
            }
        }
    }
}
