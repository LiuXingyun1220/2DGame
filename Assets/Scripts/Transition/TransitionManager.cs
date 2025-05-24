using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{
    private ITransitionStrategy _strategy;
    public bool isFade;//是否淡出
    public GameObject fadePanel;
    public float fadeDuration;//淡出持续时间
    public GameObject cloudGroup;

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
