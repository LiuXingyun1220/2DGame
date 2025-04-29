using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 200f;
    private Rigidbody2D rb;
    private RiverBoundary riverBoundary;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        riverBoundary = GameObject.FindGameObjectWithTag("RiverBoundary").GetComponent<RiverBoundary>();
    }

    void Update()
    {
        // 获取输入
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 移动角色
        Vector2 movement = new Vector2(horizontal, vertical);
        rb.velocity = movement * speed;

        // 检查角色是否在河流区域内
        CheckBoundary();
    }

    void CheckBoundary()
    {
        Vector2 closestPoint = riverBoundary.collider.ClosestPoint(transform.position);
        if (!riverBoundary.collider.bounds.Contains(transform.position))
        {
            transform.position = closestPoint;
        }
    }
}