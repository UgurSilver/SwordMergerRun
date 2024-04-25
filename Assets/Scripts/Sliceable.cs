using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    public int hp;
    
    public void SetSliceable()
    {
        Invoke(nameof(CloseSliceable), 0.05f);
    }

    private void CloseSliceable()
    {
        print("Add Money");
        transform.tag = "Untagged";
    }
}
