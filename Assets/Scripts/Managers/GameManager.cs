using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SupersonicWisdomSDK;

public class GameManager : MonoBehaviour
{
    #region Variables for General
    public static GameManager Instance;
    #endregion

    #region Variables for GamePlay
    public Datas datas;
    [HideInInspector] public bool isGame, isFirstTouch;
    public int earnedMoney;
    public int totalMoney;
    #endregion

    #region Variables for MergeScene
    public bool isMergeScene;
    public GameObject mergePanel;
    public MergepanelController mergepanelController;
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //Reset Data
        {
            DataManager.ResetData(datas);
        }

        /*  if (Input.GetMouseButtonDown(0) && !isFirstTouch)
          {
              StartGame();
          }*/
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
    }
    public void StartGame() //Start game events
    {
        if (!isFirstTouch)
        {
            isGame = true;
            UIManager.Instance?.startTutorial.SetActive(false);
            isFirstTouch = true;
            print("StartGame");
            //SupersonicWisdom.Api.NotifyLevelStarted(datas.level, null);
        }
    }

    #region Poolings

    public GameObject UseSword(Vector3 pos, Transform parent,Vector3 scale,Quaternion rotation)
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
        tempSword.SetActive(true);
        return tempSword;
    }

    /* public void UseFx(Vector3 pos, Transform parent,Color color) //particle
     {
         GameObject tempBullet = PoolingManager.Instance?.UseFx();
         tempBullet.transform.SetParent(parent);
         tempBullet.transform.position = pos - Vector3.up * 0.99f;
         tempBullet.transform.localScale = new Vector3(10, 10, 10);

         ParticleSystem.MainModule settings = tempBullet.GetComponent<ParticleSystem>().main;
         settings.startColor = color;

         //Close transparentcy
         *//*
         ParticleSystem ps = tempBullet.GetComponent<ParticleSystem>();
         var col = ps.colorOverLifetime;
         col.enabled = false;
         *//*

         tempBullet.transform.rotation = Quaternion.Euler(90, 0, 0);
         //tempBulletSplash.transform.position -= tempBulletSplash.transform.forward*0.4f;

         tempBullet.SetActive(true);
     }*/
    #endregion
}
