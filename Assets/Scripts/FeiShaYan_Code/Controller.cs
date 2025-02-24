using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 挖渠控制
public class Controller : MonoBehaviour
{
    private Vector2 start;//起点
    private Vector2 end;//终点
    private float width;//宽度
    private float height;//高度
    public Slider mySlider;//滑块条
    public TextMeshProUGUI startPositionText;//起点位置
    public TextMeshProUGUI endPositionText;//终点位置
    public TextMeshProUGUI widthText;//宽度
    public TextMeshProUGUI heightText;//高度
    private void Awake()
    {
        startPositionText.text = $"{FeiShaYanManager.GetStartPosData()}";
        endPositionText.text = $"{FeiShaYanManager.GetEndPosData()}";
        widthText.text = $"{FeiShaYanManager.GetWidthData()}";
        heightText.text = $"{FeiShaYanManager.Height - FeiShaYanManager.GetHeightData()}";
        mySlider.value = FeiShaYanManager.GetHeightData();
    }
    void Start()
    {
        if (mySlider != null && heightText != null)
        {
            mySlider.onValueChanged.AddListener(UpdateHeight);
        }
    }
    void UpdateHeight(float value)
    {
        FeiShaYanManager.SetHeightData(value);
        heightText.text = $"{FeiShaYanManager.Height - FeiShaYanManager.GetHeightData()}";
    }
    private void Update()
    {
        Scene scene = SceneManager.GetSceneByName(FeiShaYanManager.TopViewScene);
        //俯视图
        if (scene.IsValid())
        {
            //获取起点坐标
            if (Input.GetMouseButtonDown(0))
            {
                start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            //获取终点位置并计算宽度
            if (Input.GetMouseButtonUp(0))
            {
                end = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                width = Vector2.Distance(start, end);
                if (width > 1 && width < 5)
                {
                    FeiShaYanManager.SetStartPosData(start);
                    startPositionText.text = $"{FeiShaYanManager.GetStartPosData()}";
                    
                    FeiShaYanManager.SetEndPosData(end);
                    endPositionText.text = $"{FeiShaYanManager.GetEndPosData()}";
                    
                    FeiShaYanManager.SetWidthData(width);
                    widthText.text = $"{FeiShaYanManager.GetWidthData()}";
                }
            }
        }
    }
}