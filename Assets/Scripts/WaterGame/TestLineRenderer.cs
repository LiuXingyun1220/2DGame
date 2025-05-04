using UnityEngine;
using System.Collections.Generic;

public class TestLineRenderer : MonoBehaviour
{
    public GameObject water;
    private Vector3 lastPos;
    private List<LineRenderer> lines = new List<LineRenderer>();
    private List<List<BoxCollider2D>> collidersPerLine = new List<List<BoxCollider2D>>();
    private float eraseDistance = 0.5f;
    private float minWidth = 0.04f, maxWidth = 0.3f; // 线条最细和最粗
    private float maxSpeed = 5f;

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
        newLine.material = new Material(Shader.Find("Sprites/Default"));
        newLine.colorGradient = GetBrushGradient();
        newLine.numCapVertices = 5;
        newLine.widthMultiplier = 1f; // 让 `widthCurve` 生效

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
            if (Vector3.Distance(nowPos, lastPos) > 0.01f)
            {
                LineRenderer currentLine = lines[lines.Count - 1];
                currentLine.positionCount += 1;
                currentLine.SetPosition(currentLine.positionCount - 1, nowPos);

                UpdateWidthCurve(currentLine); // 更新笔触宽度
                lastPos = nowPos;
            }
        }
    }

    /// <summary>
    /// 根据线条长度调整宽度曲线
    /// </summary>
    void UpdateWidthCurve(LineRenderer line)
    {
        int pointCount = line.positionCount;
        if (pointCount < 2) return;

        AnimationCurve widthCurve = new AnimationCurve();
        widthCurve.AddKey(0f, 1.0f); // 起点最粗
        widthCurve.AddKey(0.5f, 0.7f); // 中间稍微变细
        widthCurve.AddKey(1f, 0.1f); // 尾部变细

        line.widthCurve = widthCurve;
        line.startWidth = maxWidth;
        line.endWidth = minWidth;
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
                collider.size = new Vector2(length, 0.1f);
                colliders.Add(collider);
            }
        }
    }

    Gradient GetBrushGradient()
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.black, 0.0f),
                new GradientColorKey(new Color(0, 0, 0, 0.8f), 0.5f),
                new GradientColorKey(new Color(0, 0, 0, 0), 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(0.8f, 0.5f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );
        return gradient;
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 5;
        return Camera.main.ScreenToWorldPoint(pos);
    }
}
