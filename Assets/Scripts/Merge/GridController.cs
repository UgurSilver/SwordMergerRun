using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public bool isFilled;
    public int level;
    public bool isInteractable;

    public void WaitResetInteractable(float time)
    {
        Invoke(nameof(SetInteractable), time);
    }

    private void SetInteractable()
    {
        isInteractable = true;
    }
}
