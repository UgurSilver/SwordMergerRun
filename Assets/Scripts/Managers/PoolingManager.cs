using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    #region variables for General
    public static PoolingManager Instance;
    #endregion

    #region Variables for sword
    public GameObject sword;
    private GameObject tempSword;
    public Queue<GameObject> swordQue = new();
    public int swordCount;
    #endregion

    #region Variables for SliceMoneyText
    public GameObject sliceMoneyText;
    private GameObject tempSliceMoneyText;
    public Queue<GameObject> sliceMoneyTextQue = new();
    public int sliceMoneyTextCount;
    #endregion

    #region Variables for SliceFx
    public GameObject sliceFx;
    private GameObject tempSliceFx;
    public Queue<GameObject> sliceFxQue = new();
    public int sliceFxCount;
    #endregion

    #region Variables for SmokeFx
    public GameObject smokeFx;
    private GameObject tempSmokeFx;
    public Queue<GameObject> smokeFxQue = new();
    public int smokeFxCount;
    #endregion

    #region Variables for PowerFx
    public GameObject powerFx;
    private GameObject tempPowerFx;
    public Queue<GameObject> powerFxQue = new();
    public int powerFxCount;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        SwordPooling();
        SliceMoneyTextPooling();
        SliceFxPooling();
        SmokeFxPooling();
        PowerFxPooling();
    }
    #region Sword Pooling Events
    private void SwordPooling()
    {
        for (int i = 0; i < swordCount; i++)
        {
            tempSword = Instantiate(sword);
            tempSword.transform.SetParent(transform.GetChild(0)); //Sword Parent
            tempSword.transform.localPosition = Vector3.zero;

            swordQue.Enqueue(tempSword);
        }
    }

    public GameObject UseSword()
    {
        if (swordQue.Count <= 1)
            SwordPooling();
        return swordQue.Dequeue();
    }

    public void ReplacingSword(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform.GetChild(0)); //Sword Parent
        go.transform.localPosition = Vector3.zero;
        swordQue.Enqueue(go);
    }
    #endregion
    #region Slice Money Text Pooling Events
    private void SliceMoneyTextPooling()
    {
        for (int i = 0; i < sliceMoneyTextCount; i++)
        {
            tempSliceMoneyText = Instantiate(sliceMoneyText);
            tempSliceMoneyText.transform.SetParent(transform.GetChild(1)); //sliceMoneyText Parent
            tempSliceMoneyText.transform.localPosition = Vector3.zero;

            sliceMoneyTextQue.Enqueue(tempSliceMoneyText);
        }
    }

    public GameObject UseSliceMoneyText()
    {
        if (sliceMoneyTextQue.Count <= 1)
            SliceMoneyTextPooling();
        return sliceMoneyTextQue.Dequeue();
    }

    public void ReplacingSliceMoneyText(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform.GetChild(1)); //sliceMoneyText Parent
        go.transform.localPosition = Vector3.zero;
        sliceMoneyTextQue.Enqueue(go);
    }
    #endregion

    #region Slice Fx Pooling Events
    private void SliceFxPooling()
    {
        for (int i = 0; i < sliceFxCount; i++)
        {
            tempSliceFx = Instantiate(sliceFx);
            tempSliceFx.transform.SetParent(transform.GetChild(2)); //sliceFx Parent
            tempSliceFx.transform.localPosition = Vector3.zero;

            sliceFxQue.Enqueue(tempSliceFx);
        }
    }

    public GameObject UseSliceFx()
    {
        if (sliceFxQue.Count <= 1)
            SliceFxPooling();
        return sliceFxQue.Dequeue();
    }

    public void ReplacingSliceFx(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform.GetChild(2)); //sliceFx Parent
        go.transform.localPosition = Vector3.zero;
        sliceFxQue.Enqueue(go);
    }
    #endregion

    #region Smoke Fx Pooling Events
    private void SmokeFxPooling()
    {
        for (int i = 0; i < smokeFxCount; i++)
        {
            tempSmokeFx = Instantiate(smokeFx);
            tempSmokeFx.transform.SetParent(transform.GetChild(3)); //SmokeFx Parent
            tempSmokeFx.transform.localPosition = Vector3.zero;

            smokeFxQue.Enqueue(tempSmokeFx);
        }
    }

    public GameObject UseSmokeFx()
    {
        if (smokeFxQue.Count <= 1)
            SmokeFxPooling();
        return smokeFxQue.Dequeue();
    }

    public void ReplacingSmokeFx(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform.GetChild(3)); //SmokeFx Parent
        go.transform.localPosition = Vector3.zero;
        smokeFxQue.Enqueue(go);
    }
    #endregion

    #region Power Fx Pooling Events
    private void PowerFxPooling()
    {
        for (int i = 0; i < powerFxCount; i++)
        {
            tempPowerFx = Instantiate(powerFx);
            tempPowerFx.transform.SetParent(transform.GetChild(4)); //PowerFx Parent
            tempPowerFx.transform.localPosition = Vector3.zero;

            powerFxQue.Enqueue(tempPowerFx);
        }
    }

    public GameObject UsePowerFx()
    {
        if (powerFxQue.Count <= 1)
            PowerFxPooling();
        return powerFxQue.Dequeue();
    }

    public void ReplacingPowerFx(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform.GetChild(4)); //PowerFx Parent
        go.transform.localPosition = Vector3.zero;
        powerFxQue.Enqueue(go);
    }
    #endregion
}
