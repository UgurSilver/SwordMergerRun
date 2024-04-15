using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class MergepanelController : MonoBehaviour
{
    public GameObject gridPrefab;
    private GameObject tempGrid;
    public int totalRow,totalColumn;
    public float gridDistance;
    public Vector3 startpoint;

    private void OnEnable()
    {
        CreateGrids();
    }

    private void CreateGrids()
    {
        for (int i = 0; i < totalRow; i++)
        {
            for (int j = 0; j < totalColumn; j++)
            {
                tempGrid = Instantiate(gridPrefab);
                tempGrid.transform.SetParent(transform);
                tempGrid.transform.position = startpoint + new Vector3(j * gridDistance, transform.position.y, i * gridDistance);
            }
        }
    }
}
