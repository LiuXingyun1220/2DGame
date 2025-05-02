using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panel;
    public void OpenPanel()
    {
        if(panel != null)
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }
        else
        {
            Debug.Log("±³°üÃæ°åÎÞ");
        }
    }

   
}
