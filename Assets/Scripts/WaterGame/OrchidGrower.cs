using UnityEngine;

public class OrchidGrower : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite sproutSprite;
    public Sprite youngSprite;
    public Sprite matureSprite;
    public Sprite bloomSprite;

    void Update()
    {
        int score = Data.Score;

        if (score < 2)
            spriteRenderer.sprite = sproutSprite;
        else if (score < 4)
            spriteRenderer.sprite = youngSprite;
        else if (score < 5)
            spriteRenderer.sprite = matureSprite;
        else
            spriteRenderer.sprite = bloomSprite;
    }
}
