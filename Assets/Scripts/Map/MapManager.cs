using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapManager
{
    private static bool Map1Condition;
    private static bool Map2Condition;
    private static bool Map3Condition;
    private static bool Map4Condition;

    public static void SetMap1Condition(bool condition)
    {
        Map1Condition = condition;
    }
    public static bool GetMap1Condition()
    {
        return Map1Condition;
    }
    public static void SetMap2Condition(bool condition)
    {
        Map2Condition = condition;
    }
    public static bool GetMap2Condition()
    {
        return Map2Condition;
    }
    public static void SetMap3Condition(bool condition)
    {
        Map3Condition = condition;
    }
    public static bool GetMap3Condition()
    {
        return Map3Condition;
    }
    public static void SetMap4Condition(bool condition)
    {
        Map4Condition = condition;
    }
    public static bool GetMap4Condition()
    {
        return Map4Condition;
    }
}
