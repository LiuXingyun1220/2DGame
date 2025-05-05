using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    // 引用和配置
    [Header("References")]
    [SerializeField] private DeepSeekAPI deepSeekAPI;
    [SerializeField] private TMP_InputField inputField; // TMP输入框
    [SerializeField] private TextMeshProUGUI dialogueText; // TMP文本组件
    private string characterName;

    [Header("Settings")]
    [SerializeField] private GameObject loadingIndicator;
    public GameObject askPanel;
    public GameObject answerPanel;

    void Start()
    {
        characterName = deepSeekAPI.npcCharacter.name;
        inputField.onSubmit.AddListener(HandleInputSubmit);
    }

    private void HandleInputSubmit(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            Debug.LogWarning("输入内容为空，请重新输入。");
            return;
        }

        inputField.text = ""; // 清空输入框
        loadingIndicator.SetActive(true);
        askPanel.SetActive(false);
        answerPanel.SetActive(true);
        deepSeekAPI.SendMessageToDeepSeek(text, HandleAIResponse);
    }

    private void HandleAIResponse(string content, bool isSuccess)
    {
        loadingIndicator.SetActive(false);

        // 直接显示完整文本
        dialogueText.text = isSuccess ?
            $"</b> {content}" :
            $"</b>（通讯中断）";
    }
}