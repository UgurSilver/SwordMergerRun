using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    #region variables for General
    public static PoolingManager Instance;
    #endregion


    #region Variables for Fx
    public GameObject fx;
    private GameObject tempfx;
    public Queue<GameObject> fxQue = new();
    public int fxCount;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = null;
        else
            Destroy(this);
    }

    private void Start()
    {
        FxPooling();
    }

    #region Bullet Pooling Events
    private void FxPooling()
    {
        for (int i = 0; i < fxCount; i++)
        {
            tempfx = Instantiate(fx);
            tempfx.transform.SetParent(transform.GetChild(0)); //Bullet Parent
            tempfx.transform.localPosition = Vector3.zero;

            fxQue.Enqueue(tempfx);
        }
    }

    public GameObject UseFx()
    {
        if (fxQue.Count <= 1)
            FxPooling();
        return fxQue.Dequeue();
    }

    public void ReplacingFx(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform.GetChild(0)); //Bullet Parent
        go.transform.localPosition = Vector3.zero;
        fxQue.Enqueue(go);
    }
    #endregion
}
