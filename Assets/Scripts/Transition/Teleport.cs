using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class Teleport : MonoBehaviour
{
    public string sceneFrom;
    public string sceneToGo;
    public TransitionType trasitionType;

    public void TeleportToScene()
    {
        ITransitionStrategy strategy = GetTransitionStrategy(trasitionType);
        TransitionManager.Instance.SetTransitionStrategy(strategy);
        TransitionManager.Instance.Transition(sceneFrom, sceneToGo);
    }

    // 根据 transitionType 返回对应的 ITransitionStrategy
    private ITransitionStrategy GetTransitionStrategy(TransitionType trasitionType)
    {
        switch (trasitionType)
        {
            case TransitionType.BlackFacde:
                return new BlackFadeStrategy();
            case TransitionType.CloudFade:
                return new CloudFadeStrategy();
            default:
                return new BlackFadeStrategy(); // 默认使用淡入淡出
        }
    }
}
