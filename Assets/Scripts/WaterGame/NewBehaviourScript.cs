using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;


    // Update is called once per frame
    void Update()
    {
        scoreText.text = "·ÖÊý:" + Data.Score.ToString();
    }
}
