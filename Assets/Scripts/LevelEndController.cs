using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndController : MonoBehaviour
{

    #region Variables for General
    public static LevelEndController Instance;
    #endregion

    public Transform bonusObjects, woods;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }

    private void OnEnable()
    {
        int childCount = woods.childCount;
        for (int i = 0; i < childCount; i++)
        {
            woods.GetChild(i).GetComponent<WoodController>().bonusFx = bonusObjects.GetChild(i).gameObject;
        }

        for (int i = 0; i < childCount; i++)
        {
            woods.GetChild(0).SetParent(null);
        }

    }
}
