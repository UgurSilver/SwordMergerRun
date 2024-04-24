using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public int level;
    public int hp;

    private void OnEnable()
    {
        SetParentLevel();
        SetParentHp();
    }

    public void SetParentLevel()
    {
        transform.parent.GetComponent<SwordParentController>().level = level;
    }
    public void SetParentHp()
    {
        transform.parent.GetComponent<SwordParentController>().hp = hp;
    }

}
