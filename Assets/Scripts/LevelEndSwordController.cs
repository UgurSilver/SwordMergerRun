using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndSwordController : MonoBehaviour
{
    private bool isMoveDown;
    public float moveUpPos, moveDownPos;
    public float moveSpeed;

    private void Update()
    {
        //RotateAnim();
        CheckMovePos();
        MoveUpDown();
    }

    public void MoveUpDown()
    {
        if (isMoveDown)
            transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, moveDownPos, moveSpeed * Time.deltaTime), transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, moveUpPos, moveSpeed * Time.deltaTime), transform.position.z);
    }

    public void CheckMovePos()
    {
        if (transform.position.y == moveUpPos)
            isMoveDown = true;
        if (transform.position.y == moveDownPos)
            isMoveDown = false;
    }
}
