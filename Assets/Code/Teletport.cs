using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletport : MonoBehaviour
{
    [SceneName]public string sceneFrom;
    [SceneName] public string sceneToGo;

    public void TeleportToScene()
    {
        //if (TransitionManager.Instance == null)
        //{
        //    Debug.LogError("TransitionManager.Instance is null! Ensure the TransitionManager object exists in the scene.");
        //}
        //else
        //{
        //    Debug.Log("TransitionManager.Instance is initialized.");
        //}

        TransitionManager.Instance.Transition(sceneFrom, sceneToGo);
    }
}
