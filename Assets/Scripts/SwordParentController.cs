using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwordParentController : MonoBehaviour
{
    public int level;
    public int startHp;
    public int currentHp;
    private Transform followObject;
    public bool isSmoothPosZ;

    public void ReplacePool()
    {
        Knife knifeSc;
        level = 0;
        startHp = 0;
        currentHp = 0;
        isSmoothPosZ = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            knifeSc = transform.GetChild(i).GetComponent<Knife>();
            knifeSc.isCanSlice = true;
            knifeSc.isCheckSlice = true;
            knifeSc.sliceableHp = 0;
        }
        PlayerManager.Instance.swordList.Remove(transform);
        PoolingManager.Instance.ReplacingSword(this.gameObject);
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.isGame)
        {
            if (!PlayerManager.Instance.ishorizontal)
            {
                if (!isSmoothPosZ)
                {
                    followObject = transform.parent.GetChild(transform.GetSiblingIndex() - 1);
                    transform.position = new Vector3(Mathf.Lerp(transform.position.x, followObject.position.x, GameManager.Instance.swordsXFollowSpeed * Time.smoothDeltaTime),
                     followObject.position.y, followObject.position.z - GameManager.Instance.swordsZDistance);
                    /*   Mathf.MoveTowards(transform.position.z, followObject.position.z - GameManager.Instance.swordsZDistance, GameManager.Instance.swordsZfollowSpeed * Time.smoothDeltaTime));*/

                }
                else
                {
                    followObject = transform.parent.GetChild(transform.GetSiblingIndex() - 1);
                    transform.position = new Vector3(Mathf.Lerp(transform.position.x, followObject.position.x, GameManager.Instance.swordsXFollowSpeed*5 * Time.smoothDeltaTime),
                     followObject.position.y, Mathf.MoveTowards(transform.position.z, followObject.position.z - GameManager.Instance.swordsZDistance, GameManager.Instance.swordsZfollowSpeed * Time.smoothDeltaTime));
                    if (Mathf.Abs(transform.position.z - (followObject.position.z - GameManager.Instance.swordsZDistance)) < 0.1f)
                        isSmoothPosZ = false;
                }
            }
            else
            {
                followObject = transform.parent.GetChild(transform.GetSiblingIndex() - 1);
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, followObject.position.x, GameManager.Instance.swordsXFollowSpeed * Time.smoothDeltaTime),
                 Mathf.Lerp(transform.position.y, followObject.position.y, GameManager.Instance.swordsYFollowSpeed * Time.smoothDeltaTime),
                  followObject.position.z - GameManager.Instance.swordsZDistance);
            }
        }
    }

    public void SetRot()
    {
        if (!PlayerManager.Instance.ishorizontal)
            transform.DORotate(GameManager.Instance.swordVerticalRot, 0.5f);
        else
            transform.DORotate(GameManager.Instance.swordHorizontalRot, 0.5f);
    }

    public void SetHp(int damage)
    {
        currentHp -= damage;
        if (currentHp < 0)
            ReplacePool();
    }

}
