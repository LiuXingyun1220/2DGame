using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BrushPainting : MonoBehaviour
{
    private RenderTexture texRender;  // 画布
    public Material mat;  // 给定的 shader 新建材质
    public Texture brushTypeTexture;  // 画笔纹理，半透明
    private Camera mainCamera;
    private float brushScale = 0.5f;
    public Color brushColor = Color.black;
    public RawImage raw;  // 使用 UGUI 的 RawImage 显示，方便进行添加 UI，pivot 设为 (0.5,0.5)
    private float lastDistance;
    private Vector3[] PositionArray = new Vector3[3];
    private int a = 0;
    private Vector3[] PositionArray1 = new Vector3[4];
    private int b = 0;
    private float[] speedArray = new float[4];
    private int s = 0;
    [SerializeField]
    private int num = 50; // 画的两点之间插件点的个数
    [SerializeField]
    private float widthPower = 0.5f; // 关联粗细

    Vector2 rawMousePosition;  // raw 图片的左下角对应鼠标位置
    float rawWidth;  // raw 图片宽度
    float rawHeight;  // raw 图片长度

    void Start()
    {
        rawWidth = raw.rectTransform.sizeDelta.x;
        rawHeight = raw.rectTransform.sizeDelta.y;

        Vector2 rawAnchorPosition = new Vector2(raw.rectTransform.anchoredPosition.x - raw.rectTransform.sizeDelta.x / 2.0f, raw.rectTransform.anchoredPosition.y - raw.rectTransform.sizeDelta.y / 2.0f);

        // 计算 Canvas 位置偏差
        Canvas canvas = raw.canvas;
        Vector2 canvasOffset = RectTransformUtility.WorldToScreenPoint(Camera.main, canvas.transform.position) - canvas.GetComponent<RectTransform>().sizeDelta / 2;

        // 最终鼠标相对画布的位置
        rawMousePosition = rawAnchorPosition + new Vector2(Screen.width / 2.0f, Screen.height / 2.0f) + canvasOffset;

        texRender = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
        Clear(texRender);
    }

    Vector3 startPosition = Vector3.zero;
    Vector3 endPosition = Vector3.zero;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 左键按下时开始绘画
        {
            startPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }

        if (Input.GetMouseButton(0))  // 左键持续按下时画线
        {
            OnMouseMove(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }

        if (Input.GetMouseButtonUp(0))  // 左键松开时结束绘画
        {
            OnMouseUp();
        }

        if (Input.GetMouseButtonDown(1))  // 右键按下时开始擦除
        {
            EraseTexture(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }

        if (Input.GetKeyDown(KeyCode.C))  // 按C键清空画布
        {
            OnClickClear();
        }

        DrawImage();
    }

    [SerializeField] private RawImage saveImage;

    void SaveTexture()
    {
        RenderTexture newRenderTexture = new RenderTexture(texRender);
        Graphics.Blit(texRender, newRenderTexture);
    }

    void OnMouseUp()
    {
        startPosition = Vector3.zero;
    }

    // 设置画笔宽度
    float SetScale(float distance)
    {
        float scale = (distance < 100) ? 0.8f - 0.005f * distance : 0.425f - 0.00125f * distance;
        return Mathf.Max(scale, 0.05f) * widthPower;
    }

    void OnMouseMove(Vector3 pos)
    {
        if (startPosition != Vector3.zero)
        {
            endPosition = pos;
            float distance = Vector3.Distance(startPosition, endPosition);
            brushScale = SetScale(distance);
            ThreeOrderBézierCurse(pos, distance, 4.5f);
            startPosition = endPosition;
            lastDistance = distance;
        }
    }

    void Clear(RenderTexture destTexture)
    {
        Graphics.SetRenderTarget(destTexture);
        GL.PushMatrix();
        GL.Clear(true, true, Color.white);  // 背景颜色设置为白色
        GL.PopMatrix();
    }

    void DrawBrush(RenderTexture destTexture, int x, int y, Texture sourceTexture, Color color, float scale)
    {
        DrawBrush(destTexture, new Rect(x, y, sourceTexture.width, sourceTexture.height), sourceTexture, color, scale);
    }

    void DrawBrush(RenderTexture destTexture, Rect destRect, Texture sourceTexture, Color color, float scale)
    {
        float left = (destRect.xMin - rawMousePosition.x) * Screen.width / rawWidth - destRect.width * scale / 2.0f;
        float right = (destRect.xMin - rawMousePosition.x) * Screen.width / rawWidth + destRect.width * scale / 2.0f;
        float top = (destRect.yMin - rawMousePosition.y) * Screen.height / rawHeight - destRect.height * scale / 2.0f;
        float bottom = (destRect.yMin - rawMousePosition.y) * Screen.height / rawHeight + destRect.height * scale / 2.0f;

        Graphics.SetRenderTarget(destTexture);

        GL.PushMatrix();
        GL.LoadOrtho();

        mat.SetTexture("_MainTex", brushTypeTexture);
        mat.SetColor("_Color", color);
        mat.SetPass(0);

        GL.Begin(GL.QUADS);

        GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(left / Screen.width, top / Screen.height, 0);
        GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(right / Screen.width, top / Screen.height, 0);
        GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(right / Screen.width, bottom / Screen.height, 0);
        GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(left / Screen.width, bottom / Screen.height, 0);

        GL.End();
        GL.PopMatrix();
    }

    void DrawImage()
    {
        raw.texture = texRender;
    }

    public void OnClickClear()
    {
        Clear(texRender);
    }

    // 擦除功能
    void EraseTexture(Vector3 pos)
    {
        // 通过将指定区域的颜色设置为透明来实现擦除
        DrawBrush(texRender, (int)pos.x, (int)pos.y, brushTypeTexture, Color.white, brushScale); // 用白色背景覆盖
    }

    private void ThreeOrderBézierCurse(Vector3 pos, float distance, float targetPosOffset)
    {
        PositionArray1[b] = pos;
        b++;
        speedArray[s] = distance;
        s++;

        if (b == 4)
        {
            Vector3 temp1 = PositionArray1[1];
            Vector3 temp2 = PositionArray1[2];

            Vector3 middle = (PositionArray1[0] + PositionArray1[2]) / 2;
            PositionArray1[1] = (PositionArray1[1] - middle) * 1.5f + middle;
            middle = (temp1 + PositionArray1[3]) / 2;
            PositionArray1[2] = (PositionArray1[2] - middle) * 2.1f + middle;

            for (int index1 = 0; index1 < num / 1.5f; index1++)
            {
                float t1 = (1.0f / num) * index1;
                Vector3 target = Mathf.Pow(1 - t1, 3) * PositionArray1[0] +
                                 3 * PositionArray1[1] * t1 * Mathf.Pow(1 - t1, 2) +
                                 3 * PositionArray1[2] * t1 * t1 * (1 - t1) + PositionArray1[3] * Mathf.Pow(t1, 3);

                float deltaSpeed = (float)(speedArray[3] - speedArray[0]) / num;
                float randomOffset = Random.Range(-targetPosOffset, targetPosOffset);
                DrawBrush(texRender, (int)(target.x + randomOffset), (int)(target.y + randomOffset), brushTypeTexture, brushColor, SetScale(speedArray[0] + (deltaSpeed * index1)));
            }

            PositionArray1[0] = temp1;
            PositionArray1[1] = temp2;
            b = 0;
        }
    }
}
