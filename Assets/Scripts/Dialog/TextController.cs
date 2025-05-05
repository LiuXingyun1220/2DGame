using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextController : MonoBehaviour
{
    public TMP_Text testText;
    public float fadeInDuration = 1f; // 每个字符淡入的持续时间
    public float fadeOutDuration = 2f; // 全部文本淡出的持续时间
    public float delayBeforeFadeOut = 1f; // 完成打字后等待多久开始淡出

    private void Start()
    {
        StartCoroutine(TypeTextWithGradient(testText, "天机玄妙不可言\r\n\r\n\r\n\r\n切勿纠缠留余波", 0.25f));
    }

    IEnumerator TypeTextWithGradient(TMP_Text tmpText, string fullText, float typingInterval)
    {
        tmpText.text = string.Empty;
        // 创建新的TMP_TextInfo来存储字符信息
        tmpText.ForceMeshUpdate();

        // 对字符串中的每个字符
        for (int charIndex = 0; charIndex <= fullText.Length; charIndex++)
        {
            // 更新可见部分的文本
            tmpText.maxVisibleCharacters = charIndex;

            // 设置文本为完整内容以确保正确的网格生成
            if (charIndex == 0)
                tmpText.text = fullText;

            // 强制更新网格以应用可见性更改
            tmpText.ForceMeshUpdate();

            // 对当前字符应用淡入效果
            if (charIndex > 0 && charIndex <= fullText.Length)
            {
                StartCoroutine(FadeInCharacter(tmpText, charIndex - 1, fadeInDuration));
            }

            // 在显示下一个字符之前等待
            yield return new WaitForSeconds(typingInterval);
        }

        // 等待一段时间后开始全部淡出
        yield return new WaitForSeconds(delayBeforeFadeOut);

        // 全部文本淡出
        yield return StartCoroutine(FadeOutAllText(tmpText, fadeOutDuration));
    }

    IEnumerator FadeInCharacter(TMP_Text tmpText, int charIndex, float duration)
    {
        TMP_TextInfo textInfo = tmpText.textInfo;

        // 确保我们有有效数据
        if (charIndex >= textInfo.characterCount)
            yield break;

        // 仅处理可见字符
        if (!textInfo.characterInfo[charIndex].isVisible)
            yield break;

        // 获取此字符的材质索引和顶点颜色
        int materialIndex = textInfo.characterInfo[charIndex].materialReferenceIndex;
        Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

        // 获取此字符的顶点索引
        int vertexIndex = textInfo.characterInfo[charIndex].vertexIndex;

        // 起始颜色，alpha = 0（透明）
        Color32 startColor = vertexColors[vertexIndex];
        startColor.a = 0;

        // 目标结束颜色，完全不透明
        Color32 endColor = startColor;
        endColor.a = 255;

        // 设置初始透明度
        vertexColors[vertexIndex] = startColor;
        vertexColors[vertexIndex + 1] = startColor;
        vertexColors[vertexIndex + 2] = startColor;
        vertexColors[vertexIndex + 3] = startColor;

        // 更新网格的新颜色
        tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

        // 随时间逐渐改变alpha
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            byte alpha = (byte)Mathf.Lerp(0, 255, t);

            // 更新顶点颜色
            vertexColors[vertexIndex].a = alpha;
            vertexColors[vertexIndex + 1].a = alpha;
            vertexColors[vertexIndex + 2].a = alpha;
            vertexColors[vertexIndex + 3].a = alpha;

            // 应用颜色变化
            tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保最终状态有完全的alpha
        vertexColors[vertexIndex].a = 255;
        vertexColors[vertexIndex + 1].a = 255;
        vertexColors[vertexIndex + 2].a = 255;
        vertexColors[vertexIndex + 3].a = 255;
        tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    // 新增：整体文本淡出的协程
    IEnumerator FadeOutAllText(TMP_Text tmpText, float duration)
    {
        TMP_TextInfo textInfo = tmpText.textInfo;

        // 记录起始时间
        float startTime = Time.time;
        float endTime = startTime + duration;

        // 在指定时间内逐渐降低透明度
        while (Time.time < endTime)
        {
            // 计算当前的透明度值
            float normalizedTime = (Time.time - startTime) / duration;
            byte targetAlpha = (byte)Mathf.Lerp(255, 0, normalizedTime);

            // 更新所有可见字符的透明度
            for (int i = 0; i < textInfo.characterCount; i++)
            {
                // 跳过不可见字符
                if (!textInfo.characterInfo[i].isVisible)
                    continue;

                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

                // 更新所有4个顶点的alpha值
                vertexColors[vertexIndex].a = targetAlpha;
                vertexColors[vertexIndex + 1].a = targetAlpha;
                vertexColors[vertexIndex + 2].a = targetAlpha;
                vertexColors[vertexIndex + 3].a = targetAlpha;
            }

            // 应用更改
            tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

            yield return null;
        }

        // 确保最终状态是完全透明的
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible)
                continue;

            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;

            vertexColors[vertexIndex].a = 0;
            vertexColors[vertexIndex + 1].a = 0;
            vertexColors[vertexIndex + 2].a = 0;
            vertexColors[vertexIndex + 3].a = 0;
        }

        tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}