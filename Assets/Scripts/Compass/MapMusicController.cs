using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMusicController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic("compass");
    }

}
