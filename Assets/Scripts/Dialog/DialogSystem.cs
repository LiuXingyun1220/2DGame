using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    // UI组件
    public TextMeshProUGUI textLabel;// 文本
    public RectTransform textTransform;// 调整文本位置
    public TextMeshProUGUI nameLabel;// 姓名
    public RectTransform nameTransform;// 调整姓名位置
    public Image avatar;// 头像
    public RectTransform avatarTransform;// 调整头像位置
    public Image dialogBox;// 对话框
    public RectTransform dialogBoxTransform;// 调整对话框位置
    public GameObject teleport;// 跳转

    Vector3 offset = new Vector3(650f, 450f, 0);

    //文本文件
    public TextAsset textFile;
    //头像
    public Sprite LiErlang;
    public Sprite LiBing;
    public Sprite Physicist;

    int index;
    public float textSpeed;// 文字播放速度

    bool textFinished;//是否完成打字
    bool cancelTyping;//取消打字

    List<string> textList = new List<string>();

    void Awake()
    {
        GetTextFromFile(textFile);
        teleport.SetActive(false);
    }
    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        textFinished = true;
        StartCoroutine(SetTextUI());
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && index == textList.Count)
        {
            gameObject.SetActive(false);
            teleport.SetActive(true);
            index = 0;
            return;
        }
        if (Input.GetMouseButtonDown(0) && textFinished && !cancelTyping)
        {
            //textLabel.text = textList[index];
            //index++;
            StartCoroutine(SetTextUI());
        }
        else if (Input.GetMouseButtonDown(0) && !textFinished && !cancelTyping)
        {
            cancelTyping = true;
        }
    }
    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineDate = file.text.Split('\n');// 按行切割

        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
    }
    IEnumerator SetTextUI()
    {
        textFinished = false;
        textLabel.text = "";

        if (string.IsNullOrWhiteSpace(textList[index]))
        {
            index++;
            yield break;
        }

        //"\t"的问题导致 !=
        switch (textList[index].Trim())
        {
            case "我":
                MyLocation();
                avatar.sprite = LiErlang;
                nameLabel.text = textList[index];
                index++;
                break;
            case "李二郎":
                MyLocation();
                avatar.sprite = LiErlang;
                nameLabel.text = textList[index];
                index++;
                break;
            case "李冰":
                OtherLocation();
                avatar.sprite = LiBing;
                nameLabel.text = textList[index];
                index++;
                break;
            case "NPC":
                OtherLocation();
                avatar.sprite = Physicist;
                nameLabel.text = textList[index];
                index++;
                break;
            default:
                break;
        }

        //for (int i = 0; i < textList[index].Length; i++)
        //{
        //    textLabel.text += textList[index][i];

        //    yield return new WaitForSeconds(textSpeed);
        //}
        int letter = 0;
        while(!cancelTyping && letter < textList[index].Length)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        };
        textLabel.text = textList[index];
        cancelTyping = false;
        textFinished = true;
        index++;
    }
    void MyLocation()
    {
        dialogBoxTransform.eulerAngles = new Vector3(0, 0, 0);
        avatarTransform.position = new Vector3(-330, -88, 0) + offset;
        avatarTransform.localScale = new Vector3(3.6f, 6.21f, 3.6f);
        textTransform.position = new Vector3(770, -190, 0) + offset;
        nameTransform.position = new Vector3(220, 0, 0) + offset;
    }
    void OtherLocation()
    {
        dialogBoxTransform.eulerAngles = new Vector3(0, -180, 0);
        avatarTransform.position = new Vector3(1600, 0, 0) + offset;
        avatarTransform.localScale = new Vector3(3,5.175f,3);
        textTransform.position = new Vector3(500, -190, 0) + offset;
        nameTransform.position = new Vector3(1300, 0, 0) + offset;
    }
}
