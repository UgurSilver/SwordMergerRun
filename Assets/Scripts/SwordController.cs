using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public TMPro.TextMeshPro levelText;

    private void OnEnable()
    {
        SetLeveltext();
    }

    private void SetLeveltext()
    {
        levelText.text = transform.GetSiblingIndex().ToString();
    }
}
