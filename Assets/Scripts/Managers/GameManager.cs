using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SupersonicWisdomSDK;

public class GameManager : MonoBehaviour
{
    #region Variables for General
    public static GameManager Instance;
    public PhysicMaterial bounceMat;
    #endregion

    #region Variables for GamePlay
    public Datas datas;
    [HideInInspector] public bool isGame, isFirstTouch, isLevelEnd;
    public int earnedMoney;
    public int totalMoney;
    public int sliceMoney;
    #endregion

    #region Variables for Fire
    public bool isFire;
    public float fireTime;
    private float fireTimer;
    public int fireMultiplier;
    private IEnumerator fireCoroutine;
    #endregion

    #region Variables for MergeScene
    public bool isMergeScene;
    public GameObject mergePanel;
    public MergepanelController mergepanelController;
    #endregion

    #region Variables for Swords
    public float swordsZDistance, swordsYDistance;
    public float swordsZfollowSpeed, swordsXFollowSpeed, swordsYFollowSpeed;
    public float swordScale;
    public Vector3 swordVerticalRot, swordHorizontalRot;
    public float horizontalSwordHeight;
    #endregion


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DataManager.LoadData(datas);
        totalMoney = datas.money;
    }

    private void Start()
    {
        if (isMergeScene)
            openMergeScene();
        fireCoroutine = WaitFireEnd();



    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //Reset Data
        {
            DataManager.ResetData(datas);
        }

        if (Input.GetMouseButtonDown(0) && !isFirstTouch)
        {
            StartGame();
        }

        FireTimeEvents();
    }

    #region MergeEvents
    private void openMergeScene()
    {
        CameraManager.Instance.SetMergePos();
        mergePanel.SetActive(true);
        UIManager.Instance.mergePanel.SetActive(true);
    }
    #endregion

    public void EndMerge()
    {
        UIManager.Instance.startTutorial.SetActive(true);
        isMergeScene = false;
    }
    public void StartGame() //Start game events
    {
        if (!isMergeScene)
        {
            isGame = true;
            UIManager.Instance?.startTutorial.SetActive(false);
            isFirstTouch = true;
            CameraManager.Instance.SetTarget();
            CameraManager.Instance.isFollow = true;
            print("StartGame");
            //SupersonicWisdom.Api.NotifyLevelStarted(datas.level, null);
        }
    }

    public void StartFireSystem()
    {
        fireTimer=0;
        isFire = true;
        OpenFire();
        print("Fire");
        UIManager.Instance.OpenFireBar(fireTime);
    }

    private void FireTimeEvents()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireTime)
        {
            CloseFire();
            fireTimer = 0;
            isFire = false;
        }
    }
    

    IEnumerator WaitFireEnd()
    {
      
      
       
       
        yield return new WaitForSeconds(fireTime);
       
    }

    public void OpenFire()
    {
        foreach (var item in PlayerManager.Instance.swordList)
        {
            item.GetChild(item.childCount - 1).gameObject.SetActive(true);
        }
    }

    public void CloseFire()
    {
        foreach (var item in PlayerManager.Instance.swordList)
        {
            item.GetChild(item.childCount - 1).gameObject.SetActive(false);
        }
    }
    #region Poolings

    public GameObject UseSword(Vector3 pos, Transform parent, Vector3 scale, Quaternion rotation)
    {
        GameObject tempSword = PoolingManager.Instance.UseSword();
        tempSword.transform.SetParent(parent);
        tempSword.transform.position = pos;
        tempSword.transform.localScale = scale;
        tempSword.transform.rotation = rotation;

        for (int i = 0; i < tempSword.transform.childCount; i++)
        {
            tempSword.transform.GetChild(i).gameObject.SetActive(false);
        }

        //tempSword.GetComponent<SwordParentController>().isSmoothPosZ = true;
        tempSword.SetActive(true);
        return tempSword;
    }

    public GameObject UseSliceMoneyText(Vector3 pos, Transform parent)
    {
        GameObject tempSliceMoneyText = PoolingManager.Instance.UseSliceMoneyText();
        tempSliceMoneyText.transform.SetParent(parent);
        tempSliceMoneyText.transform.localPosition = pos;
        tempSliceMoneyText.transform.SetParent(null);
        if (!GameManager.Instance.isFire)
            tempSliceMoneyText.GetComponent<TMPro.TextMeshPro>().text = "+" + sliceMoney;
        else
            tempSliceMoneyText.GetComponent<TMPro.TextMeshPro>().text = "+" + (sliceMoney * GameManager.Instance.fireMultiplier);
        tempSliceMoneyText.SetActive(true);
        return tempSliceMoneyText;
    }

    public GameObject UseSliceFx(Vector3 pos, Color color)
    {
        GameObject tempSliceFx = PoolingManager.Instance.UseSliceFx();
        tempSliceFx.transform.SetParent(null);
        tempSliceFx.transform.position = pos;
        ParticleSystem.MainModule settings = tempSliceFx.GetComponent<ParticleSystem>().main;
        ParticleSystem.MainModule settings1 = tempSliceFx.transform.GetChild(0).GetComponent<ParticleSystem>().main;
        //ParticleSystem.MainModule settings2 = tempSliceFx.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().main;
        settings.startColor = color;
        settings1.startColor = color;
        //settings2.startColor = color;

        tempSliceFx.SetActive(true);
        return tempSliceFx;
    }

    /*public void UseFx(Vector3 pos, Transform parent, Color color) //particle
    {
        GameObject tempBullet = PoolingManager.Instance?.UseFx();
        tempBullet.transform.SetParent(parent);
        tempBullet.transform.position = pos - Vector3.up * 0.99f;
        tempBullet.transform.localScale = new Vector3(10, 10, 10);

        ParticleSystem.MainModule settings = tempBullet.GetComponent<ParticleSystem>().main;
        settings.startColor = color;

        //Close transparentcy

        ParticleSystem ps = tempBullet.GetComponent<ParticleSystem>();
        var col = ps.colorOverLifetime;
        col.enabled = false;


        tempBullet.transform.rotation = Quaternion.Euler(90, 0, 0);
        //tempBulletSplash.transform.position -= tempBulletSplash.transform.forward*0.4f;

        tempBullet.SetActive(true);
    }*/
    #endregion
}
