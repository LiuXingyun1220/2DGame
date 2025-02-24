using Unity.VisualScripting;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("目标对象")]
    public GameObject targetImage1; // 需要控制显示的图片对象
    public GameObject targetImage2; // 需要控制显示的图片对象(对应部分)

    [Header("条件检测")]
    private bool isConditionMet = false;

    private void Update()
    {
        if(targetImage1.name == "Map1")
        {
            MapManager.SetMap1Condition(true);
            isConditionMet = MapManager.GetMap1Condition();
        }
        else if(targetImage1.name == "Map2")
        {
            MapManager.SetMap2Condition(true);
            isConditionMet = MapManager.GetMap2Condition();
        }
        else if (targetImage1.name == "Map3")
        {
            MapManager.SetMap3Condition(true);
            isConditionMet = MapManager.GetMap3Condition();
        }
        else if (targetImage1.name == "Map4")
        {
            //MapManager.SetMap4Condition(true);
            isConditionMet = MapManager.GetMap4Condition();
        }
        // 每帧检测条件是否满足
        if (isConditionMet)
        {
            ShowImage(targetImage1);
            HideImage(targetImage2);

        }
        else
        {
            HideImage(targetImage1);
            ShowImage(targetImage2);
        }
    }

    // 显示图片
    public void ShowImage(GameObject obj)
    {
        if (obj != null)
        {
            // 渲染
            if (obj.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                spriteRenderer.enabled = true;
            }
            else if(obj.TryGetComponent<Collider2D>(out var collider2D))
            {
                collider2D.enabled = true;
            }
            foreach (Transform child in obj.transform)
            {
                ShowImage(child.gameObject);
            }
        }
    }

    // 隐藏图片
    public void HideImage(GameObject obj)
    {
        if (obj != null)
        {
            // 渲染
            if (obj.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                spriteRenderer.enabled = false;
            }
            else if (obj.TryGetComponent<Collider2D>(out var collider2D))
            {
                collider2D.enabled = false;
            }
            foreach (Transform child in obj.transform)
            {
                HideImage(child.gameObject);
            }
        }
    }
}