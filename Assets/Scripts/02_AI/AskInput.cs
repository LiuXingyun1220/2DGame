//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;

//public class AskInput : MonoBehaviour
//{
//    public TMP_InputField inputField;

//    private void Start()
//    {
//        inputField.onSubmit.AddListener(OnSubmit);
//    }

//    // 当用户按下回车键时调用
//    void OnSubmit(string text)
//    {
//        // 获取输入框内容
//        string userInput = inputField.text;
//        Debug.Log("用户输入: " + userInput);

//        // 处理用户输入
//        ProcessUserInput(userInput);

//        // 清空输入框（可选）
//        inputField.text = "";

//        // 可选：重新聚焦到输入框
//        inputField.ActivateInputField();
//    }

//    // 处理用户输入的方法
//    void ProcessUserInput(string input)
//    {
//        // 在这里添加您的处理逻辑
//        Debug.Log(input);
        
//    }
//}
