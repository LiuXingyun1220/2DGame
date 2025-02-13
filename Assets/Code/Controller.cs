using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        startPositionText.text = $"{GameManager.GetStartPosData()}";
        endPositionText.text = $"{GameManager.GetEndPosData()}";
        widthText.text = $"{GameManager.GetWidthData()}";
        heightText.text = $"{GameManager.Height - GameManager.GetHeightData()}";
        mySlider.value = GameManager.GetHeightData();
    }

    void Update()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        //俯视图
        if (currentSceneName == GameManager.TopViewScene)
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
                if (width > 1)
                {
                    GameManager.SetStartPosData(start);
                    startPositionText.text = $"{GameManager.GetStartPosData()}";
                    GameManager.SetEndPosData(end);
                    endPositionText.text = $"{GameManager.GetEndPosData()}";
                    GameManager.SetWidthData(width);
                    widthText.text = $"{GameManager.GetWidthData()}";
                }
            }
        }
        //截面图
        if (currentSceneName == GameManager.SectionalViewScene)
        {
            height = mySlider.value;
            GameManager.SetHeightData(height);
            heightText.text = $"{GameManager.Height - GameManager.GetHeightData()}";
        }
    }
}