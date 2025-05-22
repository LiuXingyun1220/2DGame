using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class Teleport : MonoBehaviour
{
    public string sceneFrom;
    public string sceneToGo;
    public TrasitionType trasitionType;

    public void TeleportToScene()
    {
        ITransitionStrategy strategy = GetTransitionStrategy(trasitionType);
        TransitionManager.Instance.SetTransitionStrategy(strategy);
        TransitionManager.Instance.Transition(sceneFrom, sceneToGo);
    }

    // 根据 transitionType 返回对应的 ITransitionStrategy
    private ITransitionStrategy GetTransitionStrategy(TrasitionType trasitionType)
    {
        switch (trasitionType)
        {
            case TrasitionType.BlackFacde:
                return new BlackFadeStartegy(1f);
            //case TransitionType.Slide:
            //    return new SlideTransitionStrategy(1f, new Vector2(1000, 0), Vector2.zero); // 滑动过渡
            //// 可以添加更多策略
            default:
                return new BlackFadeStartegy(1f); // 默认使用淡入淡出
        }
    }
}
