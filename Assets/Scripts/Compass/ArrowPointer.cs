using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public Transform[] targets; // Ŀ�������
    private int currentTargetIndex = 0; // ��ǰĿ��������
    public float updateInterval = 0.1f; // ���¼��
    public float arrivalDistance = 0.1f; // ����Ŀ���ľ�����ֵ

    private float timer = 0f;

    void Start()
    {
        // ȷ����ͷ�ĳ�ʼ��תΪ0��
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer = 0f;
            UpdateArrowDirection();
        }
    }

    void UpdateArrowDirection()
    {
        if (targets.Length > 0)
        {
            Transform currentTarget = targets[currentTargetIndex];

            // ����Ӽ�ͷ��Ŀ��ķ���
            Vector3 direction = currentTarget.position - transform.position;
            direction.z = 0; // ȷ����ͷ��2Dƽ������ת

            // ����Ƿ񵽴�Ŀ���
            if (direction.magnitude < arrivalDistance)
            {
                // �л�����һ��Ŀ���
                currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
                currentTarget = targets[currentTargetIndex];
            }

            // ���ü�ͷ����ת��ʹ��ָ��Ŀ�귽��
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90; // �����Ƕȣ�ʹ���Y��������Ŀ�귽��
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

           
        }
    }
}