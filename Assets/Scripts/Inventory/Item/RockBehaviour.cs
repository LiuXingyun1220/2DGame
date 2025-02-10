using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    private bool isRed = false;
    private Renderer rockRenderer;

    private void Start()
    {
        rockRenderer = GetComponent<Renderer>();
    }

    public void ChangeColor()
    {
        if (!isRed)
        {
            isRed = true;
            rockRenderer.material.color = Color.red;
        }
    }

    public bool IsRed() { return isRed; }

    public void CrackRock()
    {
        if (isRed)
        {
            Debug.Log("ÑÒÊ¯¿ªÊ¼ËéÁÑ");

            rockRenderer.material.color = Color.blue;
            isRed = false;
        }
    }
}
