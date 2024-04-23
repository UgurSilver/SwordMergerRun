using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    #region Variables for General
    public static PlayerManager Instance;
    public int minLevel;
    #endregion

    #region Variables for Swords
    public Transform rows;
    private Transform swords;
    public int initialMaxSwordsNum;
    private int maxSwordsNum;


    private bool ishorizontal;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        swords = GameObject.FindGameObjectWithTag("Swords").transform;
    }

    #region gateEvents
    public void AddSwords(int num)
    {
        int addSword = num;
        maxSwordsNum = initialMaxSwordsNum;
        while (addSword > 0)
        {
            for (int i = 0; i < swords.childCount; i++)
            {
                while (swords.GetChild(i).childCount <= maxSwordsNum)
                {
                    if (addSword > 0)
                    {
                        Transform tempSword;
                        if (!ishorizontal)
                        {
                            tempSword = GameManager.Instance.UseSword(swords.GetChild(i).GetChild(0).position, swords.GetChild(i), Vector3.one * GameManager.Instance.swordScale, Quaternion.Euler(-90, 0, 90)).transform;
                        }
                        else
                        {
                            tempSword = GameManager.Instance.UseSword(swords.GetChild(i).GetChild(0).position, swords.GetChild(i), Vector3.one * GameManager.Instance.swordScale, Quaternion.Euler(-90, 0, 90)).transform;
                        }

                        tempSword.GetChild(minLevel - 1).gameObject.SetActive(true);
                        tempSword.GetComponent<SwordParentController>().level = minLevel;
                        tempSword.GetComponent<Animator>().enabled = false;
                        addSword--;
                    }

                    else
                        break;
                }
            }
            if (addSword > 0)
                maxSwordsNum += initialMaxSwordsNum;

        }

    }
    #endregion
    #region Win&Fail

    public void Win()
    {
        UIManager.Instance?.OpenWinpanel();
    }

    public void Fail()
    {
        UIManager.Instance?.OpenFailPanel();
    }
    #endregion

    [System.Serializable]
    public class Movement
    {
        public float forwardSpeed, sideSpeed;
    }
    public Movement movement;

    [System.Serializable]
    public class CollisionDetection
    {
        public int adana;
        public void CollectableCollision()
        {

        }
    }
    public CollisionDetection collisionDetection;
}


