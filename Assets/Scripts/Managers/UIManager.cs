using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//using SupersonicWisdomSDK;

public class UIManager : MonoBehaviour
{
    #region Variables for General
    public static UIManager Instance;
    public MergeBuyButton mergeBuyButton;
    public MergeRunButton mergeRunButton;
    public GameObject buyTutorial,mergeTutorial;
    #endregion

    #region Variables for Game
    public TMPro.TextMeshProUGUI moneyText, levelText, gameMoneyText, winMoneyText;
    public GameObject startTutorial;
    public GameObject winPanel, failPanel;
    public Image fireFilledBar;
    #endregion

    #region Variables for merge
    public GameObject mergePanel;
    public TMPro.TextMeshProUGUI mergePriceText, mergeLevelText, MergeCountText;
    public Image mergeWeaponImage;
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
        SetMoneyText();
        SetLevelText();
        SetMergeValues();
    }

    public void SetMoneyText()
    {
        moneyText.text = GameManager.Instance.datas.money.ToString();
    }

    private void SetLevelText()
    {
        levelText.text = "Level " + GameManager.Instance.datas.level.ToString();
    }

    public void OpenFireBar(float time)
    {
        DOTween.Kill(25);
        fireFilledBar.fillAmount = 1;
        fireFilledBar.transform.parent.GetComponent<Animator>().enabled = true;
        fireFilledBar.transform.parent.parent.gameObject.SetActive(true);
        DOTween.To(/*x sabit kalacak*/x => /*Degisecek deger*/ fireFilledBar.fillAmount = x,/*start*/ 1, /*end*/0,/*time*/ time).SetId(25).SetEase(Ease.Linear).OnStepComplete(()=> CloseFireBar());
    }

    public void CloseFireBar()
    {
        print("CloseFireBar");
        fireFilledBar.transform.parent.GetComponent<Animator>().enabled = false;
        fireFilledBar.transform.parent.DOScale(Vector3.zero, 0.2f).OnStepComplete(() => fireFilledBar.transform.parent.parent.gameObject.SetActive(false));

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
        mergePriceText.text = GameManager.Instance.datas.mergePrice.ToString();
    }

    public void SetMergeLevel()
    {
        mergeLevelText.text ="Lv"+ GameManager.Instance.datas.mergeLevel.ToString();
    }

    public void SetMergeCount()
    {
        MergeCountText.text = GameManager.Instance.datas.mergeCount.ToString() + "/" + GameManager.Instance.mergepanelController.maxBuyCount;
    }

    public void SetMergeImage()
    {
        mergeWeaponImage.sprite = GameManager.Instance.mergepanelController.swordIcons[GameManager.Instance.datas.mergeLevel - 1];
    }

    public void CloseMergeUI()
    {
        mergePanel.SetActive(false);
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
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.isGame = false;
        winMoneyText.text =  GameManager.Instance.earnedMoney.ToString();
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
        yield return new WaitForSeconds(0.5f);
        CloseCanvas();
        failPanel.SetActive(true);
    }

    public void FailButton()
    {
        LevelManager.Instance.RestartLevel();
    }

    public void WinButton()
    {
        LevelManager.Instance.NextLevel();

    }
    #endregion
}
