using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_ExitGame : MonoBehaviour
{
    public Button exitButton;

    void Start()
    {
        // 在Start方法中为按钮添加点击事件监听器
        exitButton.onClick.AddListener(Exit);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}