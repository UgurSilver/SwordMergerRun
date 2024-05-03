using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    public int maxSwordNum;
    [HideInInspector] public int swordNum;

   public void SetSwordNum()
    {
        swordNum++;
        if (swordNum == maxSwordNum)
        {
            GetComponent<Collider>().enabled = false;
            transform.DOMoveY(transform.position.y - 2, 2);
        }
    }
}
