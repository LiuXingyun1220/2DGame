using UnityEngine;

public class OrchidGrower : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite sproutSprite;
    public Sprite youngSprite;
    public Sprite matureSprite;
    public Sprite bloomSprite;
    public Sprite fullBloomSprite; // 第5种状态（完全绽放）

    void Update()
    {
        int score = Data.Score;

        if (score < 1)
            spriteRenderer.sprite = sproutSprite;
        else if (score < 2)
            spriteRenderer.sprite = youngSprite;
        else if (score < 3)
            spriteRenderer.sprite = matureSprite;
        else if (score < 4)
            spriteRenderer.sprite = bloomSprite;
        else
            spriteRenderer.sprite = fullBloomSprite;
    }
}