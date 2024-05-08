using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MergeRunButton : MonoBehaviour
{
    private Transform tempSword;
    private Transform swords;
    private bool isRow0Filled, isRow1Filled, isRow2Filled, isRow3Filled, isRow4Filled;
    private Animator animator;
    public Material swordMat;

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        swords = GameObject.FindGameObjectWithTag("Swords").transform;
        OpenSwordOutline();
    }
    public void MergeRun()
    {
        transform.localScale = Vector3.one;
        UIManager.Instance.levelText.gameObject.SetActive(true);
        animator.SetBool("Blob", false);
        PlaceSword();
        CloseMergePanel();
        SetSwordPos();
        CloseSwordOutline();
    }

    private void PlaceSword()
    {
        for (int i = 0; i < GameManager.Instance.mergepanelController.rows.childCount; i++)
        {
            for (int j = 0; j < GameManager.Instance.mergepanelController.rows.GetChild(i).childCount; j++)
            {
                if (GameManager.Instance.mergepanelController.rows.GetChild(i).GetChild(j).GetComponent<GridController>().isFilled)
                {
                    tempSword = GameManager.Instance.mergepanelController.rows.GetChild(i).GetChild(j).GetChild(GameManager.Instance.mergepanelController.rows.GetChild(i).GetChild(j).childCount - 1);
                    tempSword.SetParent(swords.GetChild(i));
                }
            }
        }
    }

    private void CloseMergePanel()
    {
        GameManager.Instance.mergepanelController.gameObject.SetActive(false);
        UIManager.Instance.CloseMergeUI();
    }

    private void SetSwordPos()
    {
        for (int i = 0; i < swords.childCount; i++)
        {
            for (int j = 1; j < swords.GetChild(i).childCount; j++)
            {
                tempSword = swords.GetChild(i).GetChild(j);
                tempSword.GetComponent<Animator>().enabled = false;
                tempSword.DORotate(new Vector3(-90, 90, 0), 0.5f);

                tempSword.DOLocalMove(new Vector3(0, 0, -GameManager.Instance.swordsZDistance * j), 0.5f);
                tempSword.DOScale(Vector3.one * GameManager.Instance.swordScale, 0.3f);

                PlayerManager.Instance.swordList.Add(tempSword);

                if (PlayerManager.Instance.minLevel == 0)
                    PlayerManager.Instance.minLevel = tempSword.GetComponent<SwordParentController>().level;
                else
                {
                    if (tempSword.GetComponent<SwordParentController>().level < PlayerManager.Instance.minLevel)
                        PlayerManager.Instance.minLevel = tempSword.GetComponent<SwordParentController>().level;
                }
            }
        }
        FillEmptyRows();
        SetLight();
        SetCam();
    }

    private void SetCam()
    {
        CameraManager.Instance.SetRunnerPos(1);
    }

    private void SetLight()
    {
        LightManager.Instance.mergeLight.SetActive(false);
        LightManager.Instance.runnerLight.SetActive(true);
    }

    private void FillEmptyRows()
    {
        int chilCount;
        for (int k = 0; k < 10; k++)
        {
            if (swords.GetChild(0).childCount == 1)
            {
                if (swords.GetChild(1).childCount != 1)
                {
                    chilCount = swords.GetChild(1).childCount;
                    for (int i = 1; i < chilCount; i++)
                    {
                        swords.GetChild(1).GetChild(1).SetParent(swords.GetChild(0));
                    }
                }
            }

            if (swords.GetChild(0).childCount == 1)
            {
                if (swords.GetChild(2).childCount != 1)
                {
                    chilCount = swords.GetChild(2).childCount;
                    for (int i = 1; i < chilCount; i++)
                    {
                        swords.GetChild(2).GetChild(1).SetParent(swords.GetChild(1));
                    }
                }
            }

            if (swords.GetChild(1).childCount == 1)
            {
                if (swords.GetChild(3).childCount != 1)
                {
                    chilCount = swords.GetChild(3).childCount;
                    for (int i = 1; i < chilCount; i++)
                    {
                        swords.GetChild(3).GetChild(1).SetParent(swords.GetChild(1));
                    }
                }
            }
            if (swords.GetChild(2).childCount == 1)
            {
                if (swords.GetChild(4).childCount != 1)
                {
                    chilCount = swords.GetChild(4).childCount;
                    for (int i = 1; i < chilCount; i++)
                    {
                        swords.GetChild(4).GetChild(1).SetParent(swords.GetChild(2));
                    }
                }
            }
        }
    }

    public void DeactivateButton()
    {
        GetComponent<Button>().interactable = false;
    }

    public void ActivateButton()
    {
        GetComponent<Button>().interactable = true;
    }

    public void StartAnim()
    {
        if (GameManager.Instance.datas.mergeTutorial && !GameManager.Instance.datas.runTutorial)
        {
            print("Blob");
            animator.SetBool("Blob",true);
            GameManager.Instance.datas.runTutorial = true;
        }
    }

    private void CloseSwordOutline()
    {
        swordMat.SetFloat("_Outline", 0.5f);
    }

    private void OpenSwordOutline()
    {
        swordMat.SetFloat("_Outline", 5);
    }
}
