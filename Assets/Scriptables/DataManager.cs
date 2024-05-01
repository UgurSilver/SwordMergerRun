using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public static void SaveData(Datas gameData) //Save edilecek yerde cagirilir
    {
        string dataString = JsonUtility.ToJson(gameData);
        Debug.Log("Save");
        PlayerPrefs.SetString("data", dataString);
    }
    public static void LoadData(Datas gameData) //gameManager awakede cagirilir
    {
        if (!PlayerPrefs.HasKey("data"))
        {
            Debug.Log("Load");
            SaveData(gameData);
            return;
        }

        string dataString = PlayerPrefs.GetString("data");
        JsonUtility.FromJsonOverwrite(dataString, gameData);

    }

    public static void ResetData(Datas gameData)
    {
        Debug.Log("ResetData");
        PlayerPrefs.DeleteAll();

        //Level
        gameData.level = 1; //1
        gameData.sceneLevel = 1; //1
        gameData.money = 0; //0

        //Merge
        gameData.mergePrice = 20; //20
        gameData.mergeLevel = 1; //1
        gameData.mergeCount = 0; //0
        gameData.mergeImage = null;
        gameData.buyTutorial = false;
        gameData.mergeTutorial = false;
        gameData.runTutorial = false;

        //Grid
        gameData.gridLevels = new int[15];
        gameData.gridLevels[7] = 1;

        SaveData(gameData);
    }

}
