using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private SwordParentController swordParentController;
    public int level;
    public int hp;
    public bool isdead;
    private Sliceable sliceable;

    #region Variables for Rotation
    public bool isLookForward;
    private float zeroRotSpeed = 5;
    #endregion

    private void OnEnable()
    {
        swordParentController = transform.parent.GetComponent<SwordParentController>();
        isdead = false;
        SetparentSword();
        SetParentLevel();
        SetParentHp();
        SetParentPower();
    }

    private void Update()
    {
        if (!PlayerManager.Instance.ishorizontal)
            LookAtForward();
    }

    private void LookAtForward()
    {
        Vector3 angle = transform.localEulerAngles;
        angle.x = 0;
        angle.y = 0;
        angle.z = (angle.z > 180) ? angle.z - 360 : angle.z;
        angle.z = Mathf.Clamp(angle.z, -45, 45);
        transform.localRotation = Quaternion.Euler(angle);

        //saga veya sola gittiginde rotasyonunu degistiriyor
        if (PlayerManager.Instance.GetComponent<PlayerMovement>().xDifference != 0)
            transform.Rotate(Vector3.up * PlayerManager.Instance.GetComponent<PlayerMovement>().xDifference * PlayerManager.Instance.movement.sideSpeed, Space.World);

        //Saga ve sola gitmediginde rotasyonu sifirliyor
        else
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Vector3.zero), zeroRotSpeed * Time.deltaTime);

        //Ekrana tiklamadigimizda modelin rotasyonunu sifirliyor
        if (!PlayerManager.Instance.GetComponent<PlayerMovement>().isTouch)
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(Vector3.zero), zeroRotSpeed * Time.deltaTime);

    }

    public void SetparentSword()
    {
        swordParentController.currentSword = transform;
    }
    public void SetParentLevel()
    {
        swordParentController.level = level;
    }
    public void SetParentHp()
    {
        swordParentController.startHp = hp;
        swordParentController.currentHp = hp;
    }

    public void SetParentPower()
    {
        if (!swordParentController.isGetPower)
        {
            swordParentController.power = GameManager.Instance.swordPower[transform.GetSiblingIndex()];
            swordParentController.isGetPower = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sliceable"))
        {
            sliceable = other.GetComponent<Sliceable>();
            if (!isdead && sliceable.isObstacle && !GameManager.Instance.isFire)
            {
                isdead = true;
                other.GetComponent<Obstacle>().SetSwordNum();
                GetComponentInParent<SwordParentController>().BrokenEvents(true);
            }
        }

        if (other.CompareTag("BonusObject"))
        {
            GameManager.Instance.SetBonusBool();
            other.transform.parent.GetComponent<BonusObjectController>().CloseObject();
        }
    }

}
