using UnityEngine;
using System.Collections.Generic;

public class TestLineRenderer : MonoBehaviour
{
    public Material brushMaterial; 
    private Vector3 lastPos;
    private List<LineRenderer> lines = new List<LineRenderer>();
    private List<List<BoxCollider2D>> collidersPerLine = new List<List<BoxCollider2D>>();
    private float eraseDistance = 0.5f;
    private float lineWidth = 2f;  // 线条宽度，5太粗了，改为0.1比较合适，按需调节

    void Start()
    {
        CreateNewLine();
    }

    void Update()
    {
        DrawLine();
        EraseLine();
        UpdateColliders();
    }

    void CreateNewLine()
    {
        LineRenderer newLine = new GameObject("Line").AddComponent<LineRenderer>();
        newLine.transform.SetParent(transform);
        newLine.positionCount = 0;
        newLine.loop = false;

        if (brushMaterial != null)
        {
            newLine.material = new Material(brushMaterial);  // 实例化材质防止冲突
        }
        else
        {
            newLine.material = new Material(Shader.Find("Sprites/Default"));
        }

        SetSolidColorGradient(newLine, Color.black);
        newLine.numCapVertices = 10; // 圆滑端点
        newLine.widthMultiplier = 1f;
        newLine.textureMode = LineTextureMode.Stretch;

        SetWidthCurve(newLine, lineWidth);

        lines.Add(newLine);
        collidersPerLine.Add(new List<BoxCollider2D>());
    }

    void DrawLine()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateNewLine();
            lastPos = GetMouseWorldPos();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 nowPos = GetMouseWorldPos();
            if (Vector3.Distance(nowPos, lastPos) > 0.005f)
            {
                LineRenderer currentLine = lines[lines.Count - 1];
                currentLine.positionCount += 1;
                currentLine.SetPosition(currentLine.positionCount - 1, nowPos);

                // 保持宽度曲线一致
                SetWidthCurve(currentLine, lineWidth);

                lastPos = nowPos;
            }
        }
    }

    void SetWidthCurve(LineRenderer line, float width)
    {
        AnimationCurve widthCurve = new AnimationCurve(
            new Keyframe(0f, width),
            new Keyframe(1f, width)
        );
        line.widthCurve = widthCurve;
        line.startWidth = width;
        line.endWidth = width;
    }

    void SetSolidColorGradient(LineRenderer line, Color color)
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(color, 0f),
                new GradientColorKey(color, 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );
        line.colorGradient = gradient;
    }

    void EraseLine()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 mousePos = GetMouseWorldPos();

            for (int i = 0; i < lines.Count; i++)
            {
                LineRenderer line = lines[i];

                for (int j = 0; j < line.positionCount; j++)
                {
                    if (Vector3.Distance(mousePos, line.GetPosition(j)) < eraseDistance)
                    {
                        BreakLineAtPoint(line, j);
                        break;
                    }
                }
            }
        }
    }

    void BreakLineAtPoint(LineRenderer line, int index)
    {
        if (index > 0)
        {
            line.positionCount = index;
        }
    }

    void UpdateColliders()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            LineRenderer line = lines[i];
            List<BoxCollider2D> colliders = collidersPerLine[i];

            foreach (var collider in colliders)
            {
                Destroy(collider.gameObject);
            }
            colliders.Clear();

            for (int j = 0; j < line.positionCount - 1; j++)
            {
                Vector3 pointA = line.GetPosition(j);
                Vector3 pointB = line.GetPosition(j + 1);
                float length = Vector3.Distance(pointA, pointB);

                GameObject colliderObject = new GameObject("Collider_" + i + "_" + j);
                BoxCollider2D collider = colliderObject.AddComponent<BoxCollider2D>();
                collider.transform.SetParent(transform);
                collider.offset = (pointA + pointB) / 2;
                collider.size = new Vector2(length, lineWidth); // 用lineWidth作厚度
                colliders.Add(collider);
            }
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 5f;
        return Camera.main.ScreenToWorldPoint(pos);
    }
}
