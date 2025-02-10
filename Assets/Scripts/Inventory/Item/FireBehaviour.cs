using UnityEngine;

public class FireBehaviour : MonoBehaviour
{
    // ��ÿ֡������Ƿ�����ʯ�����ص�
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ����Ƿ�����ʯ������ײ
        if (other.CompareTag("Rock"))
        {
            RockBehaviour rock = other.GetComponent<RockBehaviour>();
            if (rock != null)
            {
                // �ı���ʯ��ɫΪ��ɫ
                rock.ChangeColor();
                Destroy(gameObject);
            }
        }
    }
}