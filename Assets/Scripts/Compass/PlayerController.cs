using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 250f;
    private Rigidbody2D rb;
    private RiverBoundary riverBoundary;
    private bool isMoving = false;

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

        // 判断角色是否静止并播放音效
        CheckMovementState();

    }

    void CheckBoundary()
    {
        Vector2 closestPoint = riverBoundary.collider.ClosestPoint(transform.position);
        if (!riverBoundary.collider.bounds.Contains(transform.position))
        {
            transform.position = closestPoint;
        }
    }
    void CheckMovementState()
    {
        bool currentlyMoving = rb.velocity.magnitude > 0;

        if (currentlyMoving != isMoving) // 仅当状态变化时播放音效
        {
            isMoving = currentlyMoving;
            if (isMoving)
            {
                AudioManager.instance.PlaySFX("boat");
            }
        }
    }
}