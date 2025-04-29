using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EyeOpenRenderFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
        public bool showInSceneView = false;
    }
    
    [SerializeField] private Settings settings = new Settings();
    [SerializeField] private Shader shader;

    private EyeOpenPass m_RenderPass;

    private void OnEnable()
    {
        if (shader == null)
            shader = Shader.Find("Hidden/CustomEffects/EyeOpen");
    }
    
    public override void Create()
    {
        if (shader == null)
        {
            shader = Shader.Find("Hidden/CustomEffects/EyeOpen");
            if (shader == null)
            {
                Debug.LogWarning($"没有找到 Hidden/CustomEffects/EyeOpen Shader，EyeOpen效果将不会执行");
                return;
            }
        }
        
        // 匹配原始Pass构造函数签名 (RenderPassEvent, Shader, bool)
        m_RenderPass = new EyeOpenPass(settings.renderPassEvent, shader, settings.showInSceneView);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // 跳过预览摄像机
        if (renderingData.cameraData.cameraType == CameraType.Preview)
            return;
            
        if (m_RenderPass != null)
            renderer.EnqueuePass(m_RenderPass);
    }
}
