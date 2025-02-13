using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    // 这个方法可以被Button组件的OnClick事件调用
    public void ChangeScene(string sceneName)
    {
        // 加载新的场景，sceneName是你要跳转的场景的名称
        SceneManager.LoadScene(sceneName);
    }
}