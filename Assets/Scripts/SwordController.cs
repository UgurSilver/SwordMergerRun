using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private SwordParentController swordParentController;
    public int level;
    public int hp;

    private void OnEnable()
    {
        swordParentController = transform.parent.GetComponent<SwordParentController>();
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

}
