using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EyeOpenPass : ScriptableRenderPass
{
    private readonly EyeOpen eyeopenVolume;
    private readonly Material eyeopenMaterial;
    private readonly ProfilingSampler m_ProfilingSampler;
    private readonly bool showInSceneView;

    public EyeOpenPass(RenderPassEvent evt, Shader shader, bool showInSceneView)
    {
        renderPassEvent = evt;
        if (shader == null)
        {
            Debug.LogError("无法找到Shader");
            return;
        }
        
        m_ProfilingSampler = new ProfilingSampler("EyeOpen");
        eyeopenMaterial = CoreUtils.CreateEngineMaterial(shader);
        this.showInSceneView = showInSceneView;
    }
    
    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        ConfigureInput(ScriptableRenderPassInput.Color);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (eyeopenMaterial == null)
            return;
            
        if (renderingData.cameraData.cameraType != CameraType.Game && 
            (!showInSceneView && renderingData.cameraData.cameraType == CameraType.SceneView))
            return;

        VolumeStack stack = VolumeManager.instance.stack;
        var volumeComponent = stack.GetComponent<EyeOpen>();
        
        if (volumeComponent == null || !volumeComponent.IsActive())
            return;

        CommandBuffer cmd = CommandBufferPool.Get();
        using (new ProfilingScope(cmd, m_ProfilingSampler))
        {
            // 设置材质属性
            volumeComponent.load(eyeopenMaterial);
            
            // 获取相机颜色目标
            var source = renderingData.cameraData.renderer.cameraColorTargetHandle;
            var desc = renderingData.cameraData.cameraTargetDescriptor;
            desc.depthBufferBits = 0;
            
            // 使用传统的RenderTexture方法
            var tempRT = RenderTexture.GetTemporary(
                desc.width, 
                desc.height, 
                0, 
                desc.graphicsFormat
            );

            // 使用基本的CommandBuffer.Blit
            cmd.Blit(source, tempRT, eyeopenMaterial, 0);
            cmd.Blit(tempRT, source);
            
            RenderTexture.ReleaseTemporary(tempRT);
        }

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}
