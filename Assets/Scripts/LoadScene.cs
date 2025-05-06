using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("需要加载的场景名称或索引")]
    public string sceneName1 = "PersistnetScene"; // 第一个场景（已在Build Settings中）
    public string sceneName2 = "MainMenu";    // 第二个场景（需叠加加载的场景）

    void Start()
    {
        // 默认加载第一个场景（已由构建设置启动）
        // 叠加加载第二个场景
        SceneManager.LoadScene(sceneName2, LoadSceneMode.Additive);
    }
}