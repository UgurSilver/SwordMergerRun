using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SupersonicWisdomSDK;

public class UIManager : MonoBehaviour
{
    #region Variables for General
    public static UIManager Instance;
    private GameManager gameManager;
    #endregion

    #region Variables for Game
    public TMPro.TextMeshProUGUI moneyText, levelText, gameMoneyText;
    public GameObject startTutorial;
    public GameObject winPanel, failPanel;
    #endregion

    #region Variables for merge
    public GameObject mergePanel;
    public TMPro.TextMeshProUGUI mergePriceText, mergeLevelText, MergeCountText;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }
    void Start()
    {
        gameManager = GameManager.Instance;
        SetMoneyText();
        SetLevelText();
        SetMergeValues();
    }

    public void SetMoneyText()
    {
        moneyText.text = "$" + gameManager.datas.money.ToString();
    }

    private void SetLevelText()
    {
        levelText.text = "Level " + gameManager.datas.level.ToString();
    }

    #region Merge

    public void SetMergeValues()
    {
        SetMergePrice();
        SetMergeLevel();
        SetMergeCount();
        SetMergeImage();
    }
    public void SetMergePrice()
    {
        mergePriceText.text = gameManager.datas.mergePrice.ToString();
    }

    public void SetMergeLevel()
    {
        mergeLevelText.text = gameManager.datas.mergeLevel.ToString();
    }

    public void SetMergeCount()
    {
        MergeCountText.text = gameManager.datas.mergeCount.ToString()+"/"+gameManager.mergepanelController.maxBuyCount;
    }

    public void SetMergeImage()
    {

    }
    #endregion

    #region Win&Fail
    private void CloseCanvas()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void OpenWinpanel()
    {
        StartCoroutine(WaitWinPanel());
        //SupersonicWisdom.Api.NotifyLevelCompleted(gameManager.datas.level, null);
    }

    IEnumerator WaitWinPanel()
    {
        GameManager.Instance.isGame = false;
        //gameMoneyText.text = "$" + gameManager.gameMoney.ToString();
        yield return new WaitForSeconds(1.5f);
        CloseCanvas();
        winPanel.SetActive(true);
    }

    public void OpenFailPanel()
    {
        StartCoroutine(WaitFailPanel());
        //SupersonicWisdom.Api.NotifyLevelFailed(gameManager.datas.level, null);
    }

    IEnumerator WaitFailPanel()
    {
        GameManager.Instance.isGame = false;
        yield return new WaitForSeconds(1.5f);
        CloseCanvas();
        failPanel.SetActive(true);
    }
    #endregion
}
