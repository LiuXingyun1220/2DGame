using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 水动力学模拟（简化版）
public class WaterSimulation : MonoBehaviour
{
    [Header("基础参数")]
    public const float baseFlowRate = 5f;  // 基础流量（m³/s）
    public const float sedimentDensity = 1600f; // 泥沙密度（kg/m³）

    [Header("实时数据")]
    private float currentFlowRate;
    private float sedimentErosionRate;
    private float irrigationSupply;

    public void Calculate()
    {
        // 根据堰体参数计算流速
        currentFlowRate = CalculateFlowRate(
            GameManager.Height - GameManager.GetHeightData(),
            GameManager.GetWidthData()
        );

        // 计算泥沙冲刷量（Stokes定律简化）
        sedimentErosionRate = Mathf.Pow(currentFlowRate, 1.5f) * 0.02f;
        GameManager.SetSedimentErosionRateData(sedimentErosionRate);
        Debug.Log($"泥沙冲刷量为{sedimentErosionRate}");

        // 计算灌溉供水量
        irrigationSupply = baseFlowRate - currentFlowRate * 0.3f;
        GameManager.SetIrrigationData(-irrigationSupply);
        Debug.Log($"灌溉供水量为{-irrigationSupply}");
    }

    float CalculateFlowRate(float height, float width)
    {
        // 曼宁公式简化计算
        float slope = 1 / (height + 0.5f);
        return width * Mathf.Sqrt(slope) * 2.8f;
    }
}