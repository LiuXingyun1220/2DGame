//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Collide : MonoBehaviour
//{
//    private void OnCollisionEnter(Collision collision)
//    {
//        Debug.Log("Collision detected with: " + collision.gameObject.name);
//        if (collision.collider.tag.Equals("Water"))
//        {
//            Data.Score += 1;
//            Destroy(collision.gameObject);
//        }
//    }


//}

using System;
using UnityEngine;

public class Collide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("碰撞检测到: " + collision.gameObject.name);
        if (collision.collider.CompareTag("Water"))
        {
            Data.Score += 1;
            Debug.Log("分数增加");
            Destroy(collision.gameObject);
        }
    }
}
