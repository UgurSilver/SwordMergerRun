using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeBuyButton : MonoBehaviour
{
    public Button buyButton;
    public Image weaponImage, moneyIcon;
    public TMPro.TextMeshProUGUI priceText, levelText;

    public Color enableColor, disableColor;

    private GridController selectedGrid;
    private Transform tempSword;


   
    private void Start()
    {
        CheckActivate();
        CheckTutorial();
    }
    public void MergeBuy()
    {
        //SetButtonValues
        GameManager.Instance.datas.money -= GameManager.Instance.datas.mergePrice;
        GameManager.Instance.datas.mergeCount++;

        //Print ButtpnValues
        UIManager.Instance.SetMoneyText();
        UIManager.Instance.SetMergeValues();

        //SelectGrid
        GameManager.Instance.mergepanelController.SelectGrid();
        selectedGrid = GameManager.Instance.mergepanelController.selectedGrid;
        selectedGrid.level = GameManager.Instance.datas.mergeLevel;
        selectedGrid.SetLevelText();
        selectedGrid.isFilled = true;

        //SwordEvents
        tempSword = GameManager.Instance.UseSword(selectedGrid.transform.position, selectedGrid.transform, Vector3.one * 0.15f, Quaternion.Euler(0, -45, 0)).transform;
        tempSword.GetChild(GameManager.Instance.datas.mergeLevel - 1).gameObject.SetActive(true);
        tempSword.GetComponent<SwordParentController>().level = GameManager.Instance.datas.mergeLevel;

        //Tutorial
        GameManager.Instance.datas.buyTutorial = true;
        UIManager.Instance.buyTutorial.SetActive(false);
        SaveGridLevel();
        //Save
        DataManager.SaveData(GameManager.Instance.datas);

        CheckActivate();
        CheckTutorial();
    }

    #region Button Activity
    public void CheckActivate()
    {
        GameManager.Instance.mergepanelController.CheckEmptyGrids();

        if (GameManager.Instance.datas.mergeCount == GameManager.Instance.mergepanelController.maxBuyCount)
        {
            GameManager.Instance.datas.mergeCount = 0;
            UIManager.Instance.SetMergeValues();
            if (GameManager.Instance.datas.mergeLevel < GameManager.Instance.mergepanelController.swordIcons.Count)
                GameManager.Instance.datas.mergeLevel++;
            UIManager.Instance.SetMergeLevel();
            UIManager.Instance.SetMergeImage();

            DataManager.SaveData(GameManager.Instance.datas);
        }

        if (GameManager.Instance.datas.money >= GameManager.Instance.datas.mergePrice)
            ActivateButton();
        if (!GameManager.Instance.mergepanelController.isFilled)
            ActivateButton();




        if (GameManager.Instance.datas.money < GameManager.Instance.datas.mergePrice)
            DeactivateButton();
        if (GameManager.Instance.mergepanelController.isFilled)
            DeactivateButton();

       /* if (isDeactiveButton())
            DeactivateButton();
        else
            ActivateButton();*/
    }

    public void CheckTutorial()
    {
        if (GameManager.Instance.datas.money > GameManager.Instance.datas.mergePrice)
        {
            if (!GameManager.Instance.datas.buyTutorial)
                UIManager.Instance.buyTutorial.SetActive(true);
            //else
            //    UIManager.Instance.buyTutorial.SetActive(false);

            if (!GameManager.Instance.datas.mergeTutorial)
                UIManager.Instance.mergeRunButton.DeactivateButton();
        }

        if (GameManager.Instance.datas.buyTutorial && !GameManager.Instance.datas.mergeTutorial)
            UIManager.Instance.mergeTutorial.SetActive(true);

        if (GameManager.Instance.datas.mergeTutorial)
            UIManager.Instance.mergeRunButton.ActivateButton();

    }

    public void ActivateButton()
    {
        buyButton.interactable = true;
        priceText.color = enableColor;
        weaponImage.color = enableColor;
        levelText.color = enableColor;
        moneyIcon.color = enableColor;
    }
    public void DeactivateButton()
    {
        buyButton.interactable = false;
        priceText.color = disableColor;
        weaponImage.color = disableColor;
        levelText.color = disableColor;
        moneyIcon.color = disableColor;

    }


    #endregion

    public void SaveGridLevel()
    {
        for (int i = 0; i < GameManager.Instance.mergepanelController.grids.Count; i++)
        {
            GameManager.Instance.datas.gridLevels[i] = GameManager.Instance.mergepanelController.grids[i].level;
        }
    }

    public bool isDeactiveButton()
    {
        if (GameManager.Instance.datas.money < GameManager.Instance.datas.mergePrice || GameManager.Instance.mergepanelController.isFilled)
            return true;
        else
            return false;

    }

    
}
