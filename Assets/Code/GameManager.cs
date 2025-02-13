using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class GameManager
{
    private static Vector2 start;//���
    private static Vector2 end;//�յ�
    private static float weirHeight;//�߶�
    private static float weirWidth;//���
    private static float irrigationSupply;//��ȹ�ˮ��
    private static float sedimentErosionRate;//��ɳ��ˢ��
    public const string TopViewScene = "TopViewScene";//����ͼ����
    public const string SectionalViewScene = "SectionalViewScene";//����ͼ����
    public const string SucceedScene = "Succeed";//�ɹ�����
    public const string DefeatScene = "Defeat";//ʧ�ܳ���
    public const float Height = 4.88f;//�᳤
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
