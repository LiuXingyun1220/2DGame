using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using TMPro;
using UnityEngine;

public class RhythmEventController : MonoBehaviour
{
    [SerializeField] private string eventID = "chimesEvent";

    [Header("关卡完成界面")]
    [SerializeField] private GameObject levelCompleteUI; // 关卡完成UI面板
    [SerializeField] private float delayBeforeShowUI = 0.5f;

    private void OnEnable()
    {
        Koreographer.Instance.RegisterForEvents(eventID, TriggerChime);
    }

    private void TriggerChime(KoreographyEvent evt)
    {
        //Debug.Log("激活了");
        int targetID=evt.GetIntValue();
        if (targetID == 0) 
        {
            levelCompleteUI.SetActive(true);
            Debug.Log("更新了");
            // 查找并更新UI上的时间文本（如果有的话）
            TextMeshProUGUI text = levelCompleteUI.GetComponentInChildren<TextMeshProUGUI>();
            if (text.name.Contains("ScoreText"))
            {
                text.text = string.Format("Score: {0}", ScoreManager.Instance.getScore());
            }
        }
        else 
        {
            ChimesManager.Instance.ActivateChime(targetID);
        }
            
    }
}
