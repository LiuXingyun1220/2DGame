using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    [SerializeField] private float heatPerSecond = 15f;
    [SerializeField] private float destroyDelay = 0.2f;

    // 在每帧检测火堆是否与岩石发生重叠
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否与岩石发生碰撞
        if (other.CompareTag("Rock"))
        {
            RockTemperature rock = other.gameObject.GetComponent<RockTemperature>();
            if (rock != null)
            {
                rock.ApplyHeat(heatPerSecond);
                Destroy(gameObject, destroyDelay);
            }
        }
    }
}