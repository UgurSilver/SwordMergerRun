using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    public int hp;
    public Material insideMat;
    public Color sliceFxColor;
    
    public void SetSliceable()
    {
        Invoke(nameof(CloseSliceable), 0.1f);
    }

    private void CloseSliceable()
    {
        GameManager.Instance.UseSliceMoneyText(transform.GetComponent<Rigidbody>().centerOfMass,transform);
        GameManager.Instance.earnedMoney += GameManager.Instance.sliceMoney;
        GameManager.Instance.datas.money += GameManager.Instance.sliceMoney;
        UIManager.Instance.SetMoneyText();
        print("Add Money");
        transform.tag = "Untagged";
    }
}
