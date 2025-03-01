using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    [SerializeField] private float coolPerSecond = 25f;
    [SerializeField] private float destroyDelay = 0.2f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查是否与岩石发生碰撞
        if (other.CompareTag("Rock"))
        {
            RockTemperature rock = other.GetComponent<RockTemperature>();
            if (rock != null)
            {
                // 改变岩石温度
                rock.ApplyCooling(coolPerSecond);
                Destroy(gameObject, destroyDelay);
            }
        }
    }
}
