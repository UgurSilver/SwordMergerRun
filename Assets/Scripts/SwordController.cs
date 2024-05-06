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

    private void OnEnable()
    {
        swordParentController = transform.parent.GetComponent<SwordParentController>();
        isdead = false;
        SetParentLevel();
        SetParentHp();
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
    }

}
