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
    public List<Transform> swordList = new();
    public Transform rows;
    private Transform swords;
    public int initialMaxSwordsNum;
    private int maxSwordsNum;
    public bool ishorizontal;
    private bool isBonusSound;
    #endregion

    #region Variables for Sounds
    private AudioSource audioSource;
    public AudioClip fruitSound, woodSound, bonusSound;
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
        audioSource = GetComponent<AudioSource>();
        swords = GameObject.FindGameObjectWithTag("Swords").transform;
    }

    public void SetMinLevel()
    {
        minLevel = 500;
        for (int i = 0; i < swordList.Count; i++)
        {
            if (swordList[i].GetComponent<SwordParentController>().level < minLevel)
                minLevel = swordList[i].GetComponent<SwordParentController>().level;
        }
    }

    #region gateEvents
    public void AddSwords(int num)
    {
        SetMinLevel();
        int addSword = num;
        maxSwordsNum = initialMaxSwordsNum;
        while (addSword > 0)
        {
            bool isFilled = false;
            while (!isFilled)
            {
                for (int i = 0; i < swords.childCount; i++)
                {
                    if (swords.GetChild(i).childCount > 1 && swords.GetChild(i).childCount <= maxSwordsNum)
                    {
                        if (addSword > 0)
                        {
                            CreateSwords(i);
                            addSword--;
                        }
                        else
                            isFilled = true;
                    }
                    if (swords.GetChild(i).childCount > maxSwordsNum)
                        isFilled = true;
                }
            }

            for (int i = 0; i < swords.childCount; i++)
            {
                while (swords.GetChild(i).childCount <= maxSwordsNum)
                {
                    if (addSword > 0)
                    {
                        CreateSwords(i);
                        addSword--;
                    }
                    else
                        break;
                }
            }
            if (addSword > 0)
                maxSwordsNum += initialMaxSwordsNum;

            if (GameManager.Instance.isFire)
                GameManager.Instance.OpenFire();
            else
                GameManager.Instance.CloseFire();
        }
    }

    public void RemoveSwords(int num)
    {
        int removeSword = num;
        while (removeSword > 0)
        {
            for (int i = 0; i < swords.childCount; i++)
            {
                if (swords.GetChild(i).childCount > 1)
                {
                    if (removeSword > 0)
                    {
                        swords.GetChild(i).GetChild(1).GetComponent<SwordParentController>().BrokenEvents(false);
                        //Remove swords
                        //removeSwrods from list
                        removeSword--;
                    }
                }
            }
            if (!GameManager.Instance.isGame)
                removeSword = 0;
        }
    }

    private void CreateSwords(int index)
    {
        Transform tempSword;
        if (!ishorizontal)
        {
            tempSword = GameManager.Instance.UseSword(swords.GetChild(0).GetChild(0).position, swords.GetChild(index), Vector3.one * GameManager.Instance.swordScale, Quaternion.Euler(-90, 0, 90)).transform;
        }
        else
        {
            tempSword = GameManager.Instance.UseSword(swords.GetChild(0).GetChild(0).position, swords.GetChild(index), Vector3.one * GameManager.Instance.swordScale, Quaternion.Euler(-90, 0, 90)).transform;
        }
        //tempSword.GetComponent<SwordParentController>().isSmoothPosZ = true;
        tempSword.GetChild(minLevel - 1).gameObject.SetActive(true);
        tempSword.GetComponent<SwordParentController>().level = minLevel;
        tempSword.GetComponent<Animator>().enabled = false;
        if (!ishorizontal)
            tempSword.rotation = Quaternion.Euler(GameManager.Instance.swordVerticalRot);
        else
            tempSword.rotation = Quaternion.Euler(GameManager.Instance.swordHorizontalRot);
        swordList.Add(tempSword);
    }

    public void RotateSwords()
    {
        ishorizontal = !ishorizontal;
        for (int i = 0; i < swordList.Count; i++)
        {
            swordList[i].GetComponent<SwordParentController>().SetRot();
            swordList[i].GetComponent<SwordParentController>().currentSword.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }

    public void PlaceSwords()
    {
        int swordCount;
        for (int k = 0; k < swords.childCount - 1; k++)
        {
            for (int i = 0; i < swords.childCount - 1; i++)
            {
                if (swords.GetChild(i).childCount == 1)
                {
                    swordCount = swords.GetChild(i + 1).childCount - 1;
                    for (int j = 0; j < swordCount; j++)
                    {
                        swords.GetChild(i + 1).GetChild(1).SetParent(swords.GetChild(i));
                    }
                }
            }
        }




    }

    public void PowerGateEvents(int value)
    {
        foreach (var item in swordList)
        {
            item.GetComponent<SwordParentController>().AddPower(value);
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

    #region Sounds
    public void PlayFruitSound()
    {
        audioSource.clip = fruitSound;
        audioSource.Play();
    }

    public void PlayWoodSound()
    {
        if (!isBonusSound)
        {
            audioSource.clip = woodSound;
            audioSource.Play();
        }
    }

    public void PlayBonusSound()
    {
        isBonusSound = true;
        audioSource.clip = bonusSound;
        audioSource.Play();
        StartCoroutine(WaitResetBonusSound());
    }

    IEnumerator WaitResetBonusSound()
    {
        yield return new WaitForSeconds(0.1f);
        isBonusSound = false;
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


