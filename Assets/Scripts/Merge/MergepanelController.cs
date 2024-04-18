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
    public Transform movingSword, mergedSword;
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
                tempSword = GameManager.Instance.UseSword(grids[i].transform.position, grids[i].transform, Vector3.one * 0.15f, Quaternion.Euler(0, -45, 0)).transform;
                tempSword.GetChild(GameManager.Instance.datas.gridLevels[i] - 1).gameObject.SetActive(true);
                grids[i].isFilled = true;
            }
            grids[i].isInteractable = true;
            grids[i].level = GameManager.Instance.datas.gridLevels[i];
            grids[i].SetLevelText();
            DataManager.SaveData(GameManager.Instance.datas);
        }

        CameraManager.Instance.SetOrthographic();
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
                    movingSword = tempGrid.transform.GetChild(tempGrid.transform.childCount - 1);
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
                if (tempGrid is null)//Hedef grid degil ise
                {
                    movingSword.DOLocalMove(Vector3.zero, resetTime);
                    movingSwordGrid.WaitResetInteractable(resetTime);
                    movingSword = null;
                    movingSwordGrid = null;
                }
                else //Hedef grid ise
                {
                    if (tempGrid.isFilled) //Hedef grid dolu ise
                    {

                        if (tempGrid.level != movingSwordGrid.level) //Hedef grid ile level farkli ise
                        {
                            movingSword.DOLocalMove(Vector3.zero, resetTime);
                            movingSwordGrid.WaitResetInteractable(resetTime);
                            movingSword = null;
                            movingSwordGrid = null;
                        }
                        else //hedef grid ile level ayni ise 
                        {
                            if (movingSwordGrid== tempGrid) //Kendi gridi ise
                            {
                                movingSword.DOLocalMove(Vector3.zero, resetTime);
                                movingSwordGrid.WaitResetInteractable(resetTime);
                                movingSword = null;
                                movingSwordGrid = null;
                            }
                            else //Kendi gridi degil ise 
                            {
                                if (movingSwordGrid.level < swordIcons.Count)//Son Level degil ise Merge
                                {
                                    SetMerge(movingSword, tempGrid.transform.GetChild(tempGrid.transform.childCount - 1), tempGrid);
                                   
                                    movingSwordGrid.isFilled = false;
                                    movingSwordGrid.isInteractable = true;
                                    movingSwordGrid.level = 0;
                                    movingSwordGrid.SetLevelText();
                                    movingSwordGrid = null;
                                    
                                    movingSword = null;
                                }

                                else //Son level ise
                                {
                                    movingSword.DOLocalMove(Vector3.zero, resetTime);
                                    movingSwordGrid.WaitResetInteractable(resetTime);
                                    movingSword = null;
                                    movingSwordGrid = null;
                                }
                            }
                        }
                    }
                    else //Hedef grid bos ise
                    {
                        movingSwordGrid.isFilled = false;
                        movingSwordGrid.isInteractable = true;
                        movingSwordGrid.level = 0;
                        movingSwordGrid.SetLevelText();

                        movingSword.SetParent(tempGrid.transform);
                        movingSword.DOLocalMove(Vector3.zero, resetTime);

                        tempGrid.isFilled = true;
                        tempGrid.level = movingSword.GetComponent<SwordParentController>().level;
                        tempGrid.SetLevelText();

                        tempGrid.WaitResetInteractable(resetTime);

                        movingSword = null;
                        movingSwordGrid = null;
                    }
                }

                UIManager.Instance.mergeBuyButton.SaveGridLevel();
                DataManager.SaveData(GameManager.Instance.datas);
            }
        }
    }

    #region Merge
    public void SetMerge(Transform sword1,Transform sword2,GridController grid)
    {
        float mergeTime = 0.5f;
        grid.isInteractable = false;
        int level = grid.level;
        grid.level = 0;
        grid.SetLevelText();

        sword1.transform.position = grid.transform.position + new Vector3(0.25f, 0, 0);
        sword2.transform.position = grid.transform.position + new Vector3(-0.25f, 0, 0);

        sword1.DOLocalRotate(new Vector3(0, 360, 0), mergeTime, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        sword2.DOLocalRotate(new Vector3(0, 360, 0), mergeTime, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);

        sword1.DOMove(grid.transform.position, mergeTime);
        sword2.DOMove(grid.transform.position, mergeTime).OnStepComplete(()=> EndMerge(sword1,sword2,grid, level));

    }

    public void EndMerge(Transform sword1, Transform sword2, GridController grid,int level)
    {
        sword1.GetComponent<SwordParentController>().ReplacePool();
        sword2.GetComponent<SwordParentController>().ReplacePool();
        grid.level=level+1;
        grid.SetLevelText();
        grid.isInteractable = true;

        mergedSword = GameManager.Instance.UseSword(grid.transform.position, grid.transform, Vector3.one * 0.15f, Quaternion.Euler(0, -45, 0)).transform;
        mergedSword.GetChild(grid.level - 1).gameObject.SetActive(true);

        UIManager.Instance.mergeBuyButton.CheckActivate();

        UIManager.Instance.mergeBuyButton.SaveGridLevel();
        DataManager.SaveData(GameManager.Instance.datas);
    }
    #endregion
}
