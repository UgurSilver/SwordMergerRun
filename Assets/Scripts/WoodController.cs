using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodController : MonoBehaviour
{
    private void OnEnable()
    {
        transform.SetParent(null);
    }
}
