using UnityEngine;

public class ClickActivate : MonoBehaviour
{
    public GameObject targetObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ö±½ÓÇÐ»»¼¤»î×´Ì¬
            targetObject.SetActive(!targetObject.activeSelf);
        }
    }
}