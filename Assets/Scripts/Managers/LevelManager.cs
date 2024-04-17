using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    #region Variables for General
    public static LevelManager Instance;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void NextLevel()
    {
        DOTween.KillAll();

        print("Win");

        //SetLevel
        GameManager.Instance.datas.level++;
        GameManager.Instance.datas.sceneLevel++;

        DataManager.SaveData(GameManager.Instance.datas);

        //Next Level
        if (GameManager.Instance.datas.sceneLevel < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(GameManager.Instance.datas.sceneLevel);
        //loop Level
        else
        {
            GameManager.Instance.datas.sceneLevel = 3;
            DataManager.SaveData(GameManager.Instance.datas);
            SceneManager.LoadScene(GameManager.Instance.datas.sceneLevel);
        }
    }

    public void RestartLevel()
    {
        DOTween.KillAll();
        print("Fail");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
