using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string sceneFrom;
    public string sceneToGo;

    public void TeleportToScene()
    {
        Debug.Log("ÇÐ»»°É");
        TransitionManager.Instance.Transition(sceneFrom, sceneToGo);
    }
}
