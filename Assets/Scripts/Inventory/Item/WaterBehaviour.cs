using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other.gameObject);
        // 检查是否与岩石发生碰撞
        if (other.CompareTag("Rock"))
        {
            RockBehaviour rock = other.GetComponent<RockBehaviour>();
            if (rock != null)
            {
                // 改变岩石
                rock.CrackRock();
                Destroy(gameObject);
            }
        }
    }
}
