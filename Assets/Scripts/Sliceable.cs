using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    public int hp;
    public Material insideMat;
    public Material woodInsideMat1, woodInsideMat2, woodInsideMat3, woodInsideMat4, woodInsideMat5;
    public Color sliceFxColor;
    public Color wood1, wood2, wood3, wood4, wood5;
    public bool isWood;
    public WoodType woodType;
    public float rotateSpeed;
    private bool isMoveDown;
    public float moveUpPos, moveDownPos;
    public float moveSpeed;

    private void OnEnable()
    {
        if (isWood)
        {
            if (woodType.Equals(WoodType.Wood1))
            {
                sliceFxColor = wood1;
                insideMat = woodInsideMat1;
            }
            if (woodType.Equals(WoodType.Wood2))
            {
                sliceFxColor = wood2;
                insideMat = woodInsideMat2;
            }
            if (woodType.Equals(WoodType.Wood3))
            {
                sliceFxColor = wood3;
                insideMat = woodInsideMat3;
            }
            if (woodType.Equals(WoodType.Wood4))
            {
                sliceFxColor = wood4;
                insideMat = woodInsideMat4;
            }
            if (woodType.Equals(WoodType.Wood5))
            {
                sliceFxColor = wood5;
                insideMat = woodInsideMat5;
            }
        }

        //RandomRot
       /* else
        {
            float rndRot = Random.Range(0, 180);
            transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, rndRot, transform.eulerAngles.z));
        }*/
    }

    private void Update()
    {
        //RotateAnim();
        CheckMovePos();
        MoveUpDown();
    }
    public void SetSliceable()
    {
        Invoke(nameof(CloseSliceable), 0.1f);
    }

    private void CloseSliceable()
    {
        GameManager.Instance.UseSliceMoneyText(transform.GetComponent<Rigidbody>().centerOfMass, transform);
        GameManager.Instance.earnedMoney += GameManager.Instance.sliceMoney;
        GameManager.Instance.datas.money += GameManager.Instance.sliceMoney;
        UIManager.Instance.SetMoneyText();
        print("Add Money");
        transform.tag = "Untagged";
    }

    public void RotateAnim()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime,Space.World);
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
