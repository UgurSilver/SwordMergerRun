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
        
        gameData.level = 1;
        gameData.sceneLevel = 1;
        gameData.money = 0;

        SaveData(gameData);
    }

}
