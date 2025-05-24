using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : Singleton<TransitionManager>
{
    [Header("过渡策略")]
    private ITransitionStrategy _strategy;

    [Header("淡出设置")]
    public bool isFade;//是否淡出
    public GameObject fadePanel;
    public float fadeDuration;//淡出持续时间

    [Header("加载界面")]
    public GameObject cloudGroup;
    public Slider progressSlider;


    public void SetTransitionStrategy(ITransitionStrategy strategy)
    {
        _strategy = strategy;
    }

    //场景切换
    public void Transition(string from, string to)
    {
        if (!isFade&&_strategy!=null)
        {
            StartCoroutine(_strategy.StartTransition(this,from,to));
        }
    }
}
