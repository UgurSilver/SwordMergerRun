using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwordParentController : MonoBehaviour
{
    public int level;
    public int startHp;
    public int currentHp;
    public int power;
    private Transform followObject;
    public bool isSmoothPosZ;
    Knife knifeSc;
    public Transform currentSword;
    private bool isDead;
    public bool isGetPower;

    public void ReplacePool()
    {

        level = 0;
        startHp = 0;
        currentHp = 0;
        //isSmoothPosZ = true;

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            knifeSc = transform.GetChild(i).GetComponent<Knife>();
            knifeSc.isCanSlice = true;
            knifeSc.isCheckSlice = true;
            knifeSc.sliceableHp = 0;
        }
        PoolingManager.Instance.ReplacingSword(this.gameObject);
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.isGame && !isDead)
        {
            followObject = transform.parent.GetChild(transform.GetSiblingIndex() - 1);
            if (Mathf.Abs(transform.position.z - (followObject.position.z - GameManager.Instance.swordsZDistance)) <= 0.01f)
                isSmoothPosZ = false;
            else
                isSmoothPosZ = true;


            if (!PlayerManager.Instance.ishorizontal)
            {
                if (!isSmoothPosZ)
                {
                    //ilk kilic ise Smooth gitmeyeecek 
                    if (transform.GetSiblingIndex() == 0)
                        transform.position = new Vector3(followObject.position.x, followObject.position.y, followObject.position.z - GameManager.Instance.swordsZDistance);

                    else
                    {
                        transform.position = new Vector3(
                            Mathf.Lerp(transform.position.x, followObject.position.x, GameManager.Instance.swordsXFollowSpeed * Time.smoothDeltaTime),
                         followObject.position.y,
                         followObject.position.z - GameManager.Instance.swordsZDistance);
                    }
                    /*   Mathf.MoveTowards(transform.position.z, followObject.position.z - GameManager.Instance.swordsZDistance, GameManager.Instance.swordsZfollowSpeed * Time.smoothDeltaTime));*/

                }
                else
                {
                    if (transform.GetSiblingIndex() == 0)
                        transform.position = new Vector3(followObject.position.x, followObject.position.y, followObject.position.z - GameManager.Instance.swordsZDistance);

                    else
                    {
                        transform.position = new Vector3(
                                      Mathf.Lerp(transform.position.x, followObject.position.x, GameManager.Instance.swordsXFollowSpeed * Time.smoothDeltaTime),
                               followObject.position.y,
                               Mathf.MoveTowards(transform.position.z, followObject.position.z - GameManager.Instance.swordsZDistance, GameManager.Instance.swordsZfollowSpeed * Time.smoothDeltaTime));
                    }
                }
            }
            else
            {
                if (transform.GetSiblingIndex() == 0)
                    transform.position = new Vector3(followObject.position.x, followObject.position.y, followObject.position.z - GameManager.Instance.swordsZDistance);

                else
                {
                    transform.position = new Vector3(
                          Mathf.Lerp(transform.position.x, followObject.position.x, GameManager.Instance.swordsXFollowSpeed * Time.smoothDeltaTime),
                       Mathf.Lerp(transform.position.y, followObject.position.y, GameManager.Instance.swordsYFollowSpeed * Time.smoothDeltaTime),
                        followObject.position.z - GameManager.Instance.swordsZDistance);
                }
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
        if (!GameManager.Instance.isFire)
        {
            if (!GameManager.Instance.isBonus)
                currentHp -= damage;
            else
                currentHp -= (int)(damage * 0.5f);
            if (currentHp < 0)
                BrokenEvents(false);
        }


    }

    public void BrokenEvents(bool isObstacle)
    {
        currentSword = transform.GetChild(level - 1);

        knifeSc = currentSword.GetComponent<Knife>();
        knifeSc.isCanSlice = false;
        knifeSc.isCheckSlice = false;

        transform.GetChild(transform.childCount - 1).SetParent(currentSword);

        isDead = true;
        transform.SetParent(null);
        currentSword.GetComponent<Collider>().enabled = false;

        Rigidbody rb = currentSword.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        currentSword.GetChild(0).gameObject.SetActive(false);
        currentSword.GetChild(1).gameObject.SetActive(true);

        print("SwordDead");
        int rnd = Random.Range(0, 2);
        rb.AddForce(Vector3.up * 150);
        rb.AddForce(Vector3.forward * 150);
        if (rnd == 0)
        {
            rb.AddForce(Vector3.right * 70);
            rb.AddTorque(Vector3.right * 70);
        }
        else
        {
            rb.AddForce(Vector3.left * 70);
            rb.AddTorque(Vector3.left * 70);
        }

        PlayerManager.Instance.swordList.Remove(transform);

        CheckFail();
        StartCoroutine(WaitForReplacePool());

        if (isObstacle)
            PlayerManager.Instance.PlaceSwords();

    }

    private void CheckFail()
    {
        if (PlayerManager.Instance.swordList.Count == 0)
        {
            if (!GameManager.Instance.isLevelEnd)
            {
                PlayerManager.Instance.Fail();
            }

            else
            {
                PlayerManager.Instance.Win();
            }
        }
    }

    IEnumerator WaitForReplacePool()
    {
        yield return new WaitForSeconds(1.5f);
        ReplacePool();
    }

    public void AddPower(int value)
    {
        power += value;
        print("Power efekt");
        if (CheckEvolve())
        {
            for (int i = GameManager.Instance.swordPower.Count - 1; i >= 0; i--)
            {
                if (power >= GameManager.Instance.swordPower[i])
                {
                    currentSword = transform.GetChild(i);
                    for (int j = 0; j < transform.childCount - 1; j++)
                    {
                        transform.GetChild(j).gameObject.SetActive(false);
                    }
                    currentSword.gameObject.SetActive(true);
                    SwordController swordSc = currentSword.GetComponent<SwordController>();
                    swordSc.SetParentHp();
                    swordSc.SetParentLevel();

                    break;
                }
            }
        }
        PlayerManager.Instance.SetMinLevel();
        GameManager.Instance.UsePowerFx(currentSword);
    }


    private bool CheckEvolve()
    {
        if (currentSword.GetSiblingIndex() != GameManager.Instance.swordPower.Count - 1)
        {
            if (power >= GameManager.Instance.swordPower[currentSword.GetSiblingIndex() + 1])
                return true;
            else
                return false;
        }
        else
            return false;

    }
}
