using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Datas")]

public class Datas : ScriptableObject
{
    [Header("Level")]
    public int level;
    public int sceneLevel;
    public int money;

    [Header("Merge")]
    public int mergePrice;
    public int mergeLevel;
    public int mergeCount;
    public Sprite mergeImage;
    public bool buyTutorial;
    public bool mergeTutorial;

    [Header("Grid")]
    public int[] gridLevels;

}
