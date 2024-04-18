using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public bool isFilled;
    public int level;
    public bool isInteractable;
    public TMPro.TextMeshPro gridLevelText;

    public void WaitResetInteractable(float time)
    {
        Invoke(nameof(SetInteractable), time);
    }

    private void SetInteractable()
    {
        isInteractable = true;
    }

   public void SetLevelText()
    {
        if (level == 0)
            gridLevelText.text = "";
        else
        {
            gridLevelText.text = level.ToString();
        }
    }

    private void LateUpdate()
    {
        if (isFilled)
        {
            gridLevelText.transform.localPosition = new Vector3(transform.GetChild(transform.childCount - 1).localPosition.x, gridLevelText.transform.localPosition.y, transform.GetChild(transform.childCount - 1).localPosition.z) + new Vector3(0.075f, 0, 0.075f);
        }
    }
}
