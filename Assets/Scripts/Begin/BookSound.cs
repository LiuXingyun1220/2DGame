using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayMusic("cartoon");
    }

    
}
