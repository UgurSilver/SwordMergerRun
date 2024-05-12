using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelEndDesign : MonoBehaviour
{
    public Transform woods, bonusObjects;
    public float distance;

    [Header("Bonus Object Size")]
    public float startSize;
    public float decreaseSize;

    private void OnEnable()
    {
        for (int i = 0; i < woods.childCount; i++)
        {
            woods.GetChild(i).localPosition = new Vector3(0, 1.2f, i * distance);
        }

        for (int i = 0; i < bonusObjects.childCount; i++)
        {
            bonusObjects.GetChild(i).localPosition = new Vector3(bonusObjects.GetChild(i).localPosition.x, 1.2f, i * distance);

            for (int j = 0; j < bonusObjects.GetChild(i).childCount; j++)
            {
                bonusObjects.GetChild(i).GetChild(0).transform.localPosition = new Vector3(startSize - i * decreaseSize, 0, 0);
                bonusObjects.GetChild(i).GetChild(0).transform.localScale = new Vector3(0.17f, 0.17f, startSize - i * decreaseSize);

                bonusObjects.GetChild(i).GetChild(1).transform.localPosition = new Vector3(-(startSize - i * decreaseSize), 0, 0);
                bonusObjects.GetChild(i).GetChild(1).transform.localScale = new Vector3(0.17f, 0.17f, startSize - i * decreaseSize);

                bonusObjects.GetChild(i).GetChild(2).transform.localScale = new Vector3(0.4f, startSize - i * decreaseSize, 0.4f);

            }
        }

    }
}
