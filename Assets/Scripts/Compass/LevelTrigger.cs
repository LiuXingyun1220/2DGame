using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    public string levelName; // 要加载的关卡名称

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("玩家触碰到关卡目标: " + levelName);

            if (LevelComplete.Instance.IsLevelUnlocked(levelName))
            {
                TransitionManager.Instance.SetTransitionStrategy(new LoadingBarStrategy());
                TransitionManager.Instance.Transition(SceneManager.GetActiveScene().name, levelName);

                Scene persistentScene = SceneManager.GetSceneByName("PersistentScene");
                if (!persistentScene.IsValid())
                {
                    SceneManager.LoadScene("PersistentScene", LoadSceneMode.Additive);
                }
            }
            else
            {
                Debug.Log("关卡 " + levelName + " 未解锁，无法进入！");
            }
        }
    }

}