using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SupersonicWisdomSDK;

public class GameManager : MonoBehaviour
{
    #region Variables for General
    public static GameManager Instance;
    private CameraManager cameraManager;
    private UIManager uiManager;
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
        cameraManager = CameraManager.Instance;
        uiManager = UIManager.Instance;

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
        cameraManager.SetMergePos();
        mergePanel.SetActive(true);
        uiManager.mergePanel.SetActive(true);
    }
    private void CloseMergeScene()
    {

    }
    #endregion

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

    public void UseFx(Vector3 pos, Transform parent,Color color) //particle
    {
        GameObject tempBullet = PoolingManager.Instance?.UseFx();
        tempBullet.transform.SetParent(parent);
        tempBullet.transform.position = pos - Vector3.up * 0.99f;
        tempBullet.transform.localScale = new Vector3(10, 10, 10);

        ParticleSystem.MainModule settings = tempBullet.GetComponent<ParticleSystem>().main;
        settings.startColor = color;

        //Close transparentcy
        /*
        ParticleSystem ps = tempBullet.GetComponent<ParticleSystem>();
        var col = ps.colorOverLifetime;
        col.enabled = false;
        */

        tempBullet.transform.rotation = Quaternion.Euler(90, 0, 0);
        //tempBulletSplash.transform.position -= tempBulletSplash.transform.forward*0.4f;

        tempBullet.SetActive(true);
    }
    #endregion
}
