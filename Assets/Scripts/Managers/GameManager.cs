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
    public int gameMoney;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = null;
        else
            Destroy(this);

        DataManager.LoadData(datas);
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
}
