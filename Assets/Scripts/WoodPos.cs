using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class WoodPos : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localPosition = new Vector3(0, 1.2f, transform.GetSiblingIndex() * 1);
    }
}
