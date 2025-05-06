using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using static DeepSeekAPI;

public class DeepSeekAPI : MonoBehaviour
{
    [Header("API Settings")]
    [SerializeField]
    private string apiKey = "sk-";
    [SerializeField]
    private string modelName = "deepseek-chat";
    [SerializeField]
    private string apiUrl = "https://api.deepseek.com/v1/chat/completions";

    //对话参数
    [Header("Dialogue Settings")]
    [Range(0, 2)] public float temperatrue = 0.5f;
    [Range(1, 1000)] public int maxTokens = 100;//控制回复长度

    //角色设定
    [System.Serializable]
    public class NPCCharacter
    {
        public string name = "助手";
        [TextArea(3, 10)]
        public string personalityPrompt = "你是物理知识助手，擅长各种物理知识以及相关物理知识起源的古籍。";
    }
    [SerializeField] public NPCCharacter npcCharacter;

    public delegate void DialogueCallback(string content, bool isSuccess);

    // Start is called before the first frame update
    private void Start()
    {
        //SendMessageToDeepSeek("你好啊", null);
    }
    public void SendMessageToDeepSeek(string message, DialogueCallback callback)
    {
        StartCoroutine(PostRequest(message, callback));
    }
    /// <summary>
    /// 处理对话请求的协程
    /// </summary>
    /// <param name="message">玩家输入的内容</param>
    /// <param name="callback">回调函数，用于处理API响应</param>
    /// <returns></returns>
    IEnumerator PostRequest(string message, DialogueCallback callback)
    {
        //构建消息列表
        List<Message> messages = new List<Message>
        {
            new Message{role = "system", content = npcCharacter.personalityPrompt },
            new Message {role = "user", content = message}
        };
        //构建请求体
        ChatRequest requestBody = new ChatRequest
        {
            model = modelName,
            messages = messages,
            temperatrue = temperatrue,
            max_tokens = maxTokens
        };

        

        // 使用Newtonsoft.Json序列化
        //string jsonBody = JsonConvert.SerializeObject(requestBody);
        //JsonUtility序列化 string
        string jsonBody = JsonUtility.ToJson(requestBody);
        Debug.Log(jsonBody);
        //yield return null;
        // 创建UnityWebRequest
        UnityWebRequest request = CreateWebRequest(jsonBody);
        // 发送请求
        yield return request.SendWebRequest();

        if (IsRequestError(request))
        {
            if(request.responseCode == 429)//速率限制
            {
                Debug.LogWarning("速率限制达到，延迟重试中...");
                yield return new WaitForSeconds(5);
                StartCoroutine(PostRequest(message, callback));
                yield break;
            }
            else
            {
                Debug.LogError($"API Error: {request.responseCode}\n{request.downloadHandler.text}");
                callback?.Invoke($"请求失败：{request.downloadHandler.text}", false);
                yield break;
            }
        }

        Debug.Log(request.downloadHandler.text);
        DeepSeekResponse response = ParseResponse(request.downloadHandler.text);
        if (response != null && response.choices.Length > 0) 
        {
            Debug.Log("Reply" + request.downloadHandler.text);
            string npcReply = response.choices[0].message.content;
            Debug.Log(npcReply);
            callback.Invoke(npcReply, true);
        }
        else
        {
            callback?.Invoke(name + "（陷入沉默）", false);
        }
        request.Dispose();
    }
    /// <summary>
    /// 创建unitywebRequest对象
    /// </summary>
    /// <param name="jsonBody"></param>
    /// <returns></returns>
    private UnityWebRequest CreateWebRequest(string jsonBody)
    {
        
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);//设置上传处理器
        request.downloadHandler = new DownloadHandlerBuffer();// 设置下载处理器
        request.SetRequestHeader("Content-Type", "application/json");//设置请求头
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");//设置认证头
        request.SetRequestHeader("Accept", "application/json");
        return request;
    }
    /// <summary>
    /// 检查请求是否出错
    /// </summary>
    /// <param name="request"></param>
    /// <returns>出错返回true</returns>
    private bool IsRequestError(UnityWebRequest request)
    {
        return request.result == UnityWebRequest.Result.ConnectionError ||
               request.result == UnityWebRequest.Result.ProtocolError ||
               request.result == UnityWebRequest.Result.DataProcessingError;
    }

    /// <summary>
    /// 解析API响应
    /// </summary>
    /// <param name="jsonResponse"></param>
    /// <returns></returns>
    private DeepSeekResponse ParseResponse(string jsonResponse)
    {
        try
        {
            DeepSeekResponse response = JsonUtility.FromJson<DeepSeekResponse>(jsonResponse);
            if (jsonResponse == null || response.choices == null || response.choices.Length == 0)
            {
                Debug.LogError("API响应格式错误或未包含有效数据");
                return null;
            }
            return response;
        }
        catch (System.Exception e) 
        { 
            Debug.LogError($"JSON解析失败：{e.Message}\n响应内容：{jsonResponse}");
            return null;
        }


    }
    [System.Serializable]
    public class Message
    {
        public string role;//角色system/user/assistant
        public string content;//消息内容
    }
   

    [System.Serializable]
    private class ChatRequest
    {
        public string model; //模型名称
        public List<Message> messages;//消息列表
        public float temperatrue;//温度参数
        public int max_tokens;//最大令牌数
    }
    [System.Serializable]
    private class Choice
    {
        public Message message;//生成的消息
    }
    
    [System.Serializable]
    private class DeepSeekResponse
    {
        public Choice[] choices;
    }
}

