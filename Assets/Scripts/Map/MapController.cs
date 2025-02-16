using Unity.VisualScripting;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("目标对象")]
    public GameObject targetImage; // 需要控制显示的图片对象

    [Header("条件检测")]
    private bool isConditionMet = false;

    private void Update()
    {
        if(targetImage.name == "Map1")
        {
            isConditionMet = MapManager.GetMap1Condition();
        }
        else if(targetImage.name == "Map2")
        {
            isConditionMet = MapManager.GetMap2Condition();
        }
        else if (targetImage.name == "Map3")
        {
            isConditionMet = MapManager.GetMap3Condition();
        }
        else if (targetImage.name == "Map4")
        {
            isConditionMet = MapManager.GetMap4Condition();
        }
        // 每帧检测条件是否满足
        if (isConditionMet)
        {
            ShowImage();
        }
        else
        {
            HideImage();
        }
    }

    // 显示图片
    public void ShowImage()
    {
        if (targetImage != null)
        {
            // 如果是Sprite Renderer，启用渲染
            if (targetImage.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                spriteRenderer.enabled = true;
            }
            // 如果是UI Image，启用GameObject
            else
            {
                targetImage.SetActive(true);
            }
        }
    }

    // 隐藏图片
    public void HideImage()
    {
        if (targetImage != null)
        {
            // 如果是Sprite Renderer，禁用渲染
            if (targetImage.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                spriteRenderer.enabled = false;
            }
            // 如果是UI Image，禁用GameObject
            else
            {
                targetImage.SetActive(false);
            }
        }
    }
}