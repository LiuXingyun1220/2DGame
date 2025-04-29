using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class TextController : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text testText;

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
        StartCoroutine(TypeText(testText, "ÇÐÎð¾À²øÁôÓà²¨ \r\n\r\n\r\n\r\nÌì»úÐþÃî²»¿ÉÑÔ", 0.15f));
    }


}
