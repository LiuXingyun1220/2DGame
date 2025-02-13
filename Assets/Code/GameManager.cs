using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameManager
{
    private static Vector2 start;//起点
    private static Vector2 end;//终点
    private static float weirHeight;//高度
    private static float weirWidth;//宽度
    private static float irrigationSupply;//灌溉供水量
    private static float sedimentErosionRate;//泥沙冲刷量
    public const string TopViewScene = "TopViewScene";//俯视图场景
    public const string SectionalViewScene = "SectionalViewScene";//截面图场景
    public const string SucceedScene = "Succeed";//成功场景
    public const string DefeatScene = "Defeat";//失败场景
    public const float Height = 4.88f;//轴长
    public static void SetStartPosData(Vector2 startPos)
    {
        start = startPos;
    }
    public static Vector2 GetStartPosData()
    {
        return start;
    }
    public static void SetEndPosData(Vector2 endPos)
    {
        end = endPos;
    }
    public static Vector2 GetEndPosData()
    {
        return end;
    }
    public static void SetWidthData(float width)
    {
        weirWidth = width*83;
    }
    public static float GetWidthData()
    {
        return weirWidth;
    }
    public static void SetHeightData(float height)
    {
        weirHeight = height;
    }
    public static float GetHeightData()
    {
        return weirHeight;
    }
    public static void SetIrrigationData(float irrigation)
    {
        irrigationSupply = irrigation;
    }
    public static float GetIrrigationData()
    {
        return irrigationSupply;
    }
    public static void SetSedimentErosionRateData(float sedimentErosion)
    {
        sedimentErosionRate = sedimentErosion;
    }
    public static float GetSedimentErosionRateData()
    {
        return sedimentErosionRate;
    }
}
