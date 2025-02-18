using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public void ChangeUnload(string from)
    {
        Scene scene = SceneManager.GetSceneByName(from);

        // 检查场景是否有效且已加载
        if (scene.IsValid() && scene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(from);
        }
    }

    public void ChangeLoad(string to)
    {
        Scene scene = SceneManager.GetSceneByName(to);
        if(!scene.IsValid() || !scene.isLoaded)
        {
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

            // 添加一个协程来等待加载完成
            StartCoroutine(WaitForSceneToLoad(loadOp));
        }
    }

    private IEnumerator WaitForSceneToLoad(AsyncOperation op)
    {
        // 等待直到场景加载完成
        while (!op.isDone)
        {
            yield return null;
        }

        // 设置新场景为激活场景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
    }
}