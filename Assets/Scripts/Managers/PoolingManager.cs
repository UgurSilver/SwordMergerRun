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
    }

    #region Bullet Pooling Events
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
}
