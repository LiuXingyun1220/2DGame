using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log(other.gameObject);
        // ����Ƿ�����ʯ������ײ
        if (other.CompareTag("Rock"))
        {
            RockBehaviour rock = other.GetComponent<RockBehaviour>();
            if (rock != null)
            {
                // �ı���ʯ
                rock.CrackRock();
                Destroy(gameObject);
            }
        }
    }
}
