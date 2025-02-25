using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [System.Serializable]
    public class KeyChimePair
    {
        public KeyCode key;
        public int chimeID;
    }

    public List<KeyChimePair> keyMappings = new List<KeyChimePair>();
    private Dictionary<KeyCode,int> keyDict = new Dictionary<KeyCode,int>();

    private void Start()
    {
        foreach(var pair in keyMappings)
        {
            keyDict.Add(pair.key,pair.chimeID);
        }
    }

    private void Update()
    {
        foreach(KeyCode key in keyDict.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                Debug.Log("∞¥œ¬¡À" + key);
                int targetID = keyDict[key];
                float accuracy = Time.time - ChimesManager.Instance.GetActivationTime(targetID);
                ChimesManager.Instance.TryHitChime(targetID, accuracy);
            }
        }
    }

}
