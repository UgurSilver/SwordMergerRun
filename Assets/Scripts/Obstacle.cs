using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    public int maxSwordNum;
    public float rotateSpeed;
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
    public void Update()
    {
        RotateAnim();
    }

    public void RotateAnim()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
    }
}
