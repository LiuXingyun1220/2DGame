using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverBoundary : MonoBehaviour
{
    public PolygonCollider2D collider;

    void Start()
    {
        if (collider == null)
        {
            collider = GetComponent<PolygonCollider2D>();
        }
    }
}