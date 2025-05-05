using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using DG.Tweening;

public class TextController : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text testText;
    public Canvas canvas;

    IEnumerator TypeText(TMP_Text tMP_Text, string str, float interval)
    {
        int i = 0;
        while (i <= str.Length)
        {
            tMP_Text.text = str.Substring(0, i++);
            yield return new WaitForSeconds(interval);
        }
        
    }
    private void Start()
    {
        StartCoroutine(TypeText(testText, "Ìì»úÐþÃî²»¿ÉÑÔ\r\n\r\n\r\n\r\nÇÐÎð¾À²øÁôÓà²¨", 0.25f));
        
    }


}
