using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeBuyButton : MonoBehaviour
{
   public void MergeBuy()
    {
        GameManager.Instance.datas.money -= GameManager.Instance.datas.mergePrice;
        GameManager.Instance.datas.mergeCount++;


        UIManager.Instance.SetMoneyText();
        UIManager.Instance.SetMergeValues();
        DataManager.SaveData(GameManager.Instance.datas);
    }
}
