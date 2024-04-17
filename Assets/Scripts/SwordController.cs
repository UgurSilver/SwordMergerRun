using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public int level;

    private void OnEnable()
    {
        SetParentLevel();
    }

    public void SetParentLevel()
    {
        transform.parent.GetComponent<SwordParentController>().level = level;

    }

}
