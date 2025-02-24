using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    // 在每帧检测火堆是否与岩石发生重叠
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否与岩石发生碰撞
        if (other.CompareTag("Rock"))
        {
            Debug.Log("碰撞了");
            RockBehaviour rock = other.GetComponent<RockBehaviour>();
            if (rock != null)
            {
                // 改变岩石颜色为红色
                rock.ChangeColor();
                Destroy(gameObject);
            }
        }
    }
}