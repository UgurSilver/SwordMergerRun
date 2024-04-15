using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SupersonicWisdomSDK;

public class UIManager : MonoBehaviour
{
    #region Variables for General
    public static UIManager Instance;
    #endregion

    #region Variables for Game
    public TMPro.TextMeshProUGUI moneyText, levelText, gameMoneyText;
    public GameObject startTutorial;
    public GameObject winPanel, failPanel;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = null;
        else
            Destroy(this);

    }
    void Start()
    {
        SetMoneyText();
        SetLevelText();
    }

    public void SetMoneyText()
    {
        //moneyText.text = "$" + GameManager.Instance.datas.money.ToString();
    }

    private void SetLevelText()
    {
        //levelText.text = "Level " + GameManager.Instance.datas.level.ToString();
    }

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
