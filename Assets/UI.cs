using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text Text;
    private void Update()
    {
        Text.text = Data.Score + "";
    }
}