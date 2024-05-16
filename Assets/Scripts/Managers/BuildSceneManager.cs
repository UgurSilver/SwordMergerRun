using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using SupersonicWisdomSDK;

public class BuildSceneManager : MonoBehaviour
{
    #region Variables for General
    public Image loodingBar;
    private float looadingBarValue;
    public Datas datas;
    #endregion

    private void Awake()
    {
        DataManager.LoadData(datas);
        SupersonicWisdom.Api.AddOnReadyListener(SDKLevel);
        SupersonicWisdom.Api.Initialize();
        SDKLevel();
    }
    private void Update()
    {
        //  loodingBar.fillAmount = looadingBarValue;
    }

    public void OpenLevel()
    {
        if (datas.sceneLevel < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(datas.sceneLevel);
        else
        {
            datas.sceneLevel = 2;
            DataManager.SaveData(datas);
            SceneManager.LoadScene(datas.sceneLevel);
        }
    }

    void SDKLevel()
    {
        OpenLevel();
        //StartCoroutine(WaitOpenLevel());
    }

    //IEnumerator WaitOpenLevel()
    //{
    //    DOTween.To(/*x sabit kalacak*/x => looadingBarValue = x,/*start*/ 0, /*end*/0.75f,/*time*/ 1.7f);
    //    yield return new WaitForSeconds(1.71f);
    //    DOTween.To(/*x sabit kalacak*/x => looadingBarValue = x,/*start*/ 0.75f, /*end*/1,/*time*/ 0.3f);
    //    yield return new WaitForSeconds(0.35f);
    //    OpenLevel();
    //}
}

