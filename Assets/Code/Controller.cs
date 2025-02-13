using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ��������
public class Controller : MonoBehaviour
{
    private Vector2 start;//���
    private Vector2 end;//�յ�
    private float width;//���
    private float height;//�߶�
    public Slider mySlider;//������
    public TextMeshProUGUI startPositionText;//���λ��
    public TextMeshProUGUI endPositionText;//�յ�λ��
    public TextMeshProUGUI widthText;//���
    public TextMeshProUGUI heightText;//�߶�

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
        //����ͼ
        if (currentSceneName == GameManager.TopViewScene)
        {
            //��ȡ�������
            if (Input.GetMouseButtonDown(0))
            {
                start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            //��ȡ�յ�λ�ò�������
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
        //����ͼ
        if (currentSceneName == GameManager.SectionalViewScene)
        {
            height = mySlider.value;
            GameManager.SetHeightData(height);
            heightText.text = $"{GameManager.Height - GameManager.GetHeightData()}";
        }
    }
}