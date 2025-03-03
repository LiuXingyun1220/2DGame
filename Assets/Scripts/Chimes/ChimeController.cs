using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChimeController : MonoBehaviour
{
    public int chimeID;
    public KeyCode triggerKey;
    private bool isActive;

    public GameObject chimeHighlight;

    public void Activate()
    {
        isActive = true;
        chimeHighlight.SetActive(true);
        Debug.Log(chimeID + " " + "亮了");
        StartCoroutine(AutoDeactivate());
    }

    private IEnumerator AutoDeactivate()
    {
        yield return new WaitForSeconds(0.5f); // 判定窗口时间
        chimeHighlight.SetActive(false);
        if (isActive) Debug.Log("错过了");
    }

    public void Deactivate()
    {
        isActive = false;
        Debug.Log("按下了");
    }
}
