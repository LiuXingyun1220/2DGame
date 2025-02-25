using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AccuracySystem;

public class ChimesManager : MonoBehaviour
{
    public static ChimesManager Instance;

    [SerializeField]private List<ChimeController> chimes=new List<ChimeController>();
    private Dictionary<int,ChimeController> chimeDict= new Dictionary<int,ChimeController>();
    private Dictionary<int,float> activationTimes=new Dictionary<int, float>();

    private void Awake()
    {
        Instance = this;
        foreach(var chime in chimes)
        {
            chimeDict.Add(chime.chimeID, chime);
        }
    }

    public void ActivateChime(int chimeID)
    {
        if (chimeDict.TryGetValue(chimeID, out ChimeController chime))
        {
            chime.Activate();
            activationTimes[chimeID] = Time.time;
        }
    }

    public float GetActivationTime(int chimeID) { return activationTimes[chimeID]; }

    public void TryHitChime(int id,float accuracy)
    {
        if(chimeDict.TryGetValue(id, out ChimeController chime))
        {
            Judgment judgement=AccuracySystem.Evaluate(accuracy);
            if (judgement != Judgment.Miss)
            {
                chime.Deactivate();
                //ScoreManager.Instance.AddScore(judgment);
            }
        }
    }
}
