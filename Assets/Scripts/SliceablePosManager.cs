using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceablePosManager : MonoBehaviour
{
    public Vector3 initialPos;
    public Vector3 initialRot;
    public float xDistance;
    public float zDistance;
    public int isXMove;
    public float maxXPos;


    public void SetPos()
    {
        transform.GetChild(0).position = new Vector3(initialPos.x, transform.GetChild(0).position.y, initialPos.z);
        transform.GetChild(0).rotation = Quaternion.Euler(initialRot);
        for (int i = 1; i < transform.childCount; i++)
        {
            if (isXMove == 1)
            {
                transform.GetChild(i).position = transform.GetChild(i - 1).position + new Vector3(xDistance, 0, zDistance);
                transform.GetChild(i).rotation = Quaternion.Euler(initialRot);
                if (transform.GetChild(i).position.x == maxXPos)
                    xDistance = -xDistance;
                if (transform.GetChild(i).position.x == -maxXPos)
                    xDistance = -xDistance;
            }
            else
            {
                transform.GetChild(i).position = transform.GetChild(i - 1).position + new Vector3(0, 0, zDistance);
                transform.GetChild(i).rotation = Quaternion.Euler(initialRot);
            }
        }
    }
}
