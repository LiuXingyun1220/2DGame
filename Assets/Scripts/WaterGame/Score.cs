using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text Text;
    private void Update()
    {
        Text.text = "·ÖÊý£º"+Data.Score + "";
    }
}