using System.Collections;
using System.Collections.Generic;
using MyGame.Inventory;
using UnityEngine;

public class PlatformCollide : MonoBehaviour
{
    private void Start()
    {
        CursorManager.Instance.SetBrushCursor();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Åö×²¼ì²âµ½: " + collision.gameObject.name);
        if (collision.collider.CompareTag("DirtyWater"))
        {
            Destroy(collision.gameObject);
        }

        Debug.Log("Åö×²¼ì²âµ½: " + collision.gameObject.name);
        if (collision.collider.CompareTag("Water"))
        {
            Destroy(collision.gameObject);
        }
    }
}
