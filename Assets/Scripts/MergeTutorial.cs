using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeTutorial : MonoBehaviour
{
    public Transform target1, target2;
    private bool isFirstTarget, isSecondtarget;
    private bool isMoveTarget1;
    public float speed;

    private void OnEnable()
    {
        for (int i = 0; i < GameManager.Instance.datas.gridLevels.Length; i++)
        {
            if (GameManager.Instance.datas.gridLevels[i] == 1)
            {
                if (!isSecondtarget && isFirstTarget)
                {
                    target2 = GameManager.Instance.mergepanelController.grids[i].transform;
                    isSecondtarget = true;
                }

                if (!isFirstTarget)
                {
                    target1 = GameManager.Instance.mergepanelController.grids[i].transform;
                    isFirstTarget = true;
                }

            }
        }

        transform.position = Camera.main.WorldToScreenPoint(target1.position);
    }

    private void Update()
    {
        if (transform.position == Camera.main.WorldToScreenPoint(target1.position))
            isMoveTarget1 = false;

        if (transform.position == Camera.main.WorldToScreenPoint(target2.position))
            isMoveTarget1 = true;
        if (!isMoveTarget1)
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.WorldToScreenPoint(target2.position), speed * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, Camera.main.WorldToScreenPoint(target1.position), speed * Time.deltaTime);

    }
}
