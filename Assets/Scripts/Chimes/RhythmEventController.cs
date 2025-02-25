using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;

public class RhythmEventController : MonoBehaviour
{
    [SerializeField] private string eventID = "chimesEvent";

    private void OnEnable()
    {
        Koreographer.Instance.RegisterForEvents(eventID, TriggerChime);
    }

    private void TriggerChime(KoreographyEvent evt)
    {
        //Debug.Log("º§ªÓ¡À");
        int targetID=evt.GetIntValue();
        ChimesManager.Instance.ActivateChime(targetID);
    }
}
