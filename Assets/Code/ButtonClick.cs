using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    // ����������Ա�Button�����OnClick�¼�����
    public void ChangeScene(string sceneName)
    {
        // �����µĳ�����sceneName����Ҫ��ת�ĳ���������
        SceneManager.LoadScene(sceneName);
    }
}