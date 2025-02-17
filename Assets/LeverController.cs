using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    private float chargeTime;
    private bool isCharging;

    [SerializeField] private float maxForce = 100f; // 最大施加力
    [SerializeField] private float maxChargeTime = 2f; // 最大蓄力时间
    [SerializeField] private Transform forcePoint; // 本地坐标系施力点

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isCharging = true;
            chargeTime = 0;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            ApplyForce(chargeTime);
            isCharging = false;
        }
    }

    void FixedUpdate()
    {
        if (isCharging)
        {
            chargeTime += Time.fixedDeltaTime;
            Debug.Log(chargeTime);
            // 可添加视觉效果（如颜色渐变）
        }
    }

    private void ApplyForce(float chargeDuration)
    {
        // 计算蓄力比例（0-1）
        float t = Mathf.Clamp01(chargeDuration / maxChargeTime);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // 直接获取子物体的世界坐标
        Vector2 worldForcePoint = forcePoint.position;

        Vector2 forceDirection = Vector2.down * (maxForce * t); // 使用子物体自身朝向

        rb.AddForceAtPosition(forceDirection, worldForcePoint, ForceMode2D.Impulse);
    }
}
