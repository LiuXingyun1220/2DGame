using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class AwakeScreenEffect : MonoBehaviour
{
    public Shader shader;

    [SerializeField]
    Material material;
    Material Material
    {
        get
        {
            if (material == null)
            {
                material = new Material(shader);
                material.hideFlags = HideFlags.DontSave;
            }
            return material;
        }
    }

    void OnDisable()
    {
        if (material)
        {
            DestroyImmediate(material);
        }
    }

    [Range(0f, 1f)]
    [Tooltip("苏醒进度")]
    public float progress = 1f;

    [Range(0, 4)]
    [Tooltip("模糊迭代次数")]
    public int blurIterations = 3;
    [Range(.2f, 3f)]
    [Tooltip("每次模糊迭代时的模糊大小扩散")]
    public float blurSpread = .6f;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Material.SetFloat("_Progress", progress);

        if (progress < 1)
        {
            int rtW = src.width;
            int rtH = src.height;
            var buffer0 = RenderTexture.GetTemporary(rtW, rtH, 0);
            buffer0.filterMode = FilterMode.Bilinear;
            Graphics.Blit(src, buffer0, Material, 0);   // 眼皮Pass

            // 模糊
            float blurSize;
            for (int i = 0; i < blurIterations; i++)
            {
                blurSize = 1f + i * blurSpread;
                blurSize = blurSize - blurSize * progress;
                Material.SetFloat("_BlurSize", blurSize);

                // 竖直方向的Pass
                var buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);
                Graphics.Blit(buffer0, buffer1, Material, 1);
                RenderTexture.ReleaseTemporary(buffer0);
                buffer0 = buffer1;

                // 水平方向的Pass
                var buffer2 = RenderTexture.GetTemporary(rtW, rtH, 0);
                Graphics.Blit(buffer0, buffer2, Material, 2);
                RenderTexture.ReleaseTemporary(buffer0);
                buffer0 = buffer2;
            }
            Graphics.Blit(buffer0, dest);
            RenderTexture.ReleaseTemporary(buffer0);
        }
        else
        {
            // 完全苏醒则无需处理，直接blit
            Graphics.Blit(src, dest);
        }
    }
}