using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MergepanelController : MonoBehaviour
{
    #region Variables for Merge
    public int maxBuyCount;
    public List<GridController> grids = new();
    public List<Sprite> swordIcons = new();
    [HideInInspector] public GridController selectedGrid;

    private int emptyGrids;
    public bool isFilled;

    public bool isMoving;
    public Transform movingSword;
    public GridController movingSwordGrid;
    private GridController tempGrid;
    public float resetTime;
    #endregion

    #region Variables for Ray
    private RaycastHit hit;
    public LayerMask layerMaskGrid, layerMaskMove;
    private Vector2 mousePos;
    #endregion


    private void Start()
    {
        SetGridStartSwords();
    }

    private void SetGridStartSwords()
    {
        for (int i = 0; i < GameManager.Instance.datas.gridLevels.Length; i++)
        {
            if (GameManager.Instance.datas.gridLevels[i] == 0)
            {
                grids[i].isFilled = false;
            }
            else
            {
                Transform tempSword;
                tempSword = GameManager.Instance.UseSword(grids[i].transform.position, grids[i].transform, Vector3.one * 0.2f, Quaternion.Euler(0, -45, 0)).transform;
                tempSword.GetChild(GameManager.Instance.datas.gridLevels[i] - 1).gameObject.SetActive(true);
                grids[i].isFilled = true;
            }
            grids[i].isInteractable = true;
            grids[i].level = GameManager.Instance.datas.gridLevels[i];
            DataManager.SaveData(GameManager.Instance.datas);
        }
    }



    private void Update()
    {
        DrawRay();
        MouseClickEvents();
        MoveSword();
        MouseUnclickEvents();
    }

    public void SelectGrid()
    {
        int rnd = Random.Range(0, grids.Count);
        if (!grids[rnd].isFilled)
            selectedGrid = grids[rnd];
        else
            SelectGrid();
    }

    public void CheckEmptyGrids()
    {
        isFilled = false;
        emptyGrids = 0;
        for (int i = 0; i < grids.Count; i++)
        {
            if (!grids[i].isFilled)
                emptyGrids++;
        }

        if (emptyGrids > 0)
            isFilled = false;
        else
            isFilled = true;
    }

    public void DrawRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 20, layerMaskGrid))
            tempGrid = hit.transform.GetComponent<GridController>();
        else
            tempGrid = null;

    }

    private void MouseClickEvents()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isMoving && tempGrid is not null)
            {
                if (tempGrid.isFilled && tempGrid.isInteractable)
                {
                    movingSword = tempGrid.transform.GetChild(1);
                    movingSwordGrid = tempGrid;
                    movingSwordGrid.isInteractable = false;
                    isMoving = true;
                }
            }
        }
    }

    private void MoveSword()
    {
        if (isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMaskMove))
            {
                movingSword.position = new Vector3(raycastHit.point.x, movingSword.position.y, raycastHit.point.z);
            }
        }
    }

    private void MouseUnclickEvents()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isMoving)
            {
                isMoving = false;
                if (tempGrid is null)//if tempGrid is nul
                {
                    movingSword.DOLocalMove(Vector3.zero, resetTime);
                    movingSwordGrid.WaitResetInteractable(resetTime);
                    movingSword = null;
                    movingSwordGrid = null;
                }
                else //if temp grid is not null
                {
                    if (tempGrid.isFilled) //if temp grid is filled
                    {
                        //Leveli farkli ise
                        movingSword.DOLocalMove(Vector3.zero, resetTime);
                        movingSwordGrid.WaitResetInteractable(resetTime);
                        movingSword = null;
                        movingSwordGrid = null;
                        //Level ayni ise merge olacak
                    }

                    else //if temp grid is not filled
                    {
                        movingSword.SetParent(tempGrid.transform);
                        movingSword.DOLocalMove(Vector3.zero, resetTime);
                        tempGrid.level = movingSword.GetComponent<SwordParentController>().level;

                        tempGrid.WaitResetInteractable(resetTime);
                        tempGrid.isFilled = true;

                        movingSwordGrid.isFilled = false;
                        movingSwordGrid.isInteractable = true;
                        movingSwordGrid.level = 0;

                        movingSword = null;
                        movingSwordGrid = null;
                    }
                }

                UIManager.Instance.mergeBuyButton.SaveGridLevel();
                DataManager.SaveData(GameManager.Instance.datas);
            }
        }
    }
}
