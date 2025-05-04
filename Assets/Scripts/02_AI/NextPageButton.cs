using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextPageButton : MonoBehaviour
{
    public TMP_Text textComponent;
    public Button nextPageButton;
    private int currentPage = 1;
    private int totalPages = 1;

    // 防止快速连续点击
    private bool isClicking = false;
    public float clickCooldown = 0.5f; // 冷却时间

    void Start()
    {
        // 确保文本使用Page模式
        textComponent.overflowMode = TextOverflowModes.Page;

        // 注册按钮点击事件（先清除所有监听器，防止重复注册）
        if (nextPageButton != null)
        {
            nextPageButton.onClick.RemoveAllListeners();
            nextPageButton.onClick.AddListener(GoToNextPage);
        }

        // 初始化时强制更新文本信息
        textComponent.ForceMeshUpdate();
        UpdateTotalPages();
    }

    public void GoToNextPage()
    {
        // 如果正在处理中，直接返回
        if (isClicking) return;

        StartCoroutine(HandleNextPageClickCoroutine());
    }

    private IEnumerator HandleNextPageClickCoroutine()
    {
        isClicking = true;

        // 同步当前显示的页面（防止不同步）
        currentPage = textComponent.pageToDisplay;

        // 更新总页数
        UpdateTotalPages();

        Debug.Log($"当前页: {currentPage}, 总页数: {totalPages}");

        // 判断是否到最后一页
        if (currentPage >= totalPages)
            currentPage = 1;
        else
            currentPage++;

        // 设置显示页面
        textComponent.pageToDisplay = currentPage;

        Debug.Log($"切换到页: {currentPage}");

        // 等待冷却时间
        yield return new WaitForSeconds(clickCooldown);
        isClicking = false;
    }

    // 更新总页数的方法
    private void UpdateTotalPages()
    {
        textComponent.ForceMeshUpdate();
        totalPages = textComponent.textInfo.pageCount;
    }

    // 设置文本
    public void SetText(string newText)
    {
        textComponent.text = newText;
        currentPage = 1;
        StartCoroutine(SetTextCoroutine());
    }

    private IEnumerator SetTextCoroutine()
    {
        // 等待一帧确保文本更新完成
        yield return null;
        textComponent.pageToDisplay = currentPage;
        UpdateTotalPages();
    }
}