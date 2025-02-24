using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Water"))
        {
            Data.Score += 1;
            Destroy(collision.gameObject);
        }
    }
}
