using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceablePosManager : MonoBehaviour
{
    public Vector3 initialPos;
    public float xDistance;
    public float zDistance;
   
    public int isXMove;


    public void SetPos()
    {
        transform.GetChild(0).position = new Vector3(initialPos.x, transform.GetChild(0).position.y, initialPos.z);
        for (int i = 1; i < transform.childCount; i++)
        {
            if (isXMove == 1)
            {
                transform.GetChild(i).position = transform.GetChild(i - 1).position + new Vector3(xDistance, 0, zDistance);
                if (transform.GetChild(i).position.x == 1)
                    xDistance = -xDistance;
                if (transform.GetChild(i).position.x == -1)
                    xDistance = -xDistance;
            }
            else
                transform.GetChild(i).position += new Vector3(0, 0, zDistance) * i;
        }
    }
}
