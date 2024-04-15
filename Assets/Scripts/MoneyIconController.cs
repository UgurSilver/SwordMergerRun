using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyIconController : MonoBehaviour
{

    #region Variables for Games
    private bool isMove;
    public float moveSpeed;
    #endregion

    private void OnEnable()
    {
        isMove = true;
    }

    void Update()
    {
        MoveToUI();
    }

    private void MoveToUI()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, UIManager.Instance.moneyText.transform.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, UIManager.Instance.moneyText.transform.position) <= 10)
            {
                isMove = false;
                IncreaseMoney();
            }

        }
    }

    private void IncreaseMoney()
    {
        //gameManager.datas.money += gameManager.moneyValue;
        //gameManager.gameMoney += gameManager.moneyValue;
        UIManager.Instance?.SetMoneyText();
        ResetToPool();
    }

    private void ResetToPool()
    {
        transform.SetParent(PoolingManager.Instance.transform.GetChild(2));
        //poolingManager.ReplacingMoneyIcon(this.gameObject);
    }
}
