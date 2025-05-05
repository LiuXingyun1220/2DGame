using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        AudioManager.instance.ToggleMusic();
        AudioManager.instance.StopMusic();
    }

    
}
