using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RockTemperature : MonoBehaviour
{
    [Header("岩石温度设置")]
    [SerializeField] private float maxTemperature = 100f;
    [SerializeField] private float minTemperature = 0f;
    [SerializeField] private float naturalCoolingRate = 0.5f;
    [SerializeField] private float heatedThreshold = 80f;
    [SerializeField] private float freezedThreshold = 70f;

    public float currentTemperature = 0f;
    private bool isHeated = false;

    private void Start()
    {
        debugStyle = new GUIStyle();
        debugStyle.fontSize = 40;  // 设置字体大小
        debugStyle.normal.textColor = Color.yellow; // 设置字体颜色
    }

    private void Update()
    {
        ApplyNaturalCooling();
        CheckTemperatureState();
    }

    public void ApplyHeat(float heatAmount)
    {
        currentTemperature = Mathf.Clamp(
            currentTemperature + heatAmount,
            minTemperature,
            maxTemperature
        );
    }

    public void ApplyCooling(float coolAmount)
    {
        currentTemperature = Mathf.Clamp(
            currentTemperature - coolAmount,
            minTemperature,
            maxTemperature
        );
    }

    private void ApplyNaturalCooling()
    {
        if (currentTemperature > minTemperature)
        {
            currentTemperature -= naturalCoolingRate * Time.deltaTime;
        }
    }

    private void CheckTemperatureState()
    {
        if (!isHeated && currentTemperature >= heatedThreshold)
        {
            isHeated = true;
            AudioManager.Instance.PlayMusic("heat");
            Debug.Log("加热过了");
        }
        if (isHeated && currentTemperature <= freezedThreshold)
        {
            DestroyRock();
        }
    }

    private void DestroyRock()
    {
        RockBehaviour rockBehaviour=GetComponent<RockBehaviour>();
        AudioManager.Instance.PlayMusic("broke");
        rockBehaviour.StartBreakAnimation();
        //Debug.Log("破裂了");
    }

    // 在RockTemperature类中添加
    private GUIStyle debugStyle;

    void OnGUI()
    {
        // 调整显示区域大小以适应新字体（宽度300，高度40）
        GUI.Label(new Rect(10, 10, 300, 40),
            $"Temperature: {currentTemperature:F1}",
            debugStyle);
    }
}
