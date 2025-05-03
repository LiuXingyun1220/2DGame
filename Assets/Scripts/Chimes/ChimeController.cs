using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChimeController : MonoBehaviour
{
    public int chimeID;
    private bool isActive;

    public GameObject chimeHighlight;
    public ParticleSystem deactivateParticlePrefab;

    public void Activate()
    {
        isActive = true;
        chimeHighlight.SetActive(true);
        //Debug.Log(chimeID + " " + "亮了");
        StartCoroutine(AutoDeactivate());
    }

    private IEnumerator AutoDeactivate()
    {
        yield return new WaitForSeconds(1f); // 判定窗口时间
        chimeHighlight.SetActive(false);
        //if (isActive) Debug.Log("错过了");
    }

    public void Deactivate()
    {
        if (isActive&& deactivateParticlePrefab != null)
        {
            ScoreManager.Instance.AddScore(1);
            // 在风铃位置生成粒子，自动旋转匹配对象方向
            Instantiate(deactivateParticlePrefab, transform.position, transform.rotation);

            // 如果要附加到风铃对象上：
            // Instantiate(deactivateParticlePrefab, transform.position, 
            //     transform.rot
        }

        isActive = false;
        chimeHighlight.SetActive(false);
       
    }
}
