using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public GameObject targetImage;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetImage.name == "Map1")
        {
            boxCollider.enabled = MapManager.GetMap1Condition();
        }
        else if (targetImage.name == "Map2")
        {
            boxCollider.enabled = MapManager.GetMap2Condition();
        }
        else if (targetImage.name == "Map3")
        {
            boxCollider.enabled = MapManager.GetMap3Condition();
        }
        else if (targetImage.name == "Map4")
        {
            boxCollider.enabled = MapManager.GetMap4Condition();
        }
    }
}
