using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class AbstractTransitionStrategy : ITransitionStrategy
{
    public IEnumerator StartTransition(TransitionManager manager, string fromScene, string toScene)
    {
        // 执行过渡前的效果
        yield return BeforeTransition(manager);

        // 卸载旧场景
        yield return SceneManager.UnloadSceneAsync(fromScene);
        Debug.Log("卸载了");

        // 加载新场景
        yield return SceneManager.LoadSceneAsync(toScene, LoadSceneMode.Additive);

        // 设置新场景为激活场景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
        Debug.Log("加载了");
        // 执行过渡后的效果
        yield return AfterTransition(manager);
    }

    // 抽象方法：过渡前效果
    protected abstract IEnumerator BeforeTransition(TransitionManager manager);

    // 抽象方法：过渡后效果
    protected abstract IEnumerator AfterTransition(TransitionManager manager);
}
