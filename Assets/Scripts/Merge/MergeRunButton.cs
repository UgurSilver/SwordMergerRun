using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MergeRunButton : MonoBehaviour
{
    private Transform tempSword;
    private Transform swords;

    private void OnEnable()
    {
        swords = GameObject.FindGameObjectWithTag("Swords").transform;
    }
    public void MergeRun()
    {
        PlaceSword();
        CloseMergePanel();
        SetSwordPos();
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

                tempSword.DOLocalMove(new Vector3(0, 0, -0.5f * j), 0.5f);
            }
        }
        SetCam();
        SetLight();
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
}
