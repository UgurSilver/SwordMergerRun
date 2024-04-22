using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class RowController : MonoBehaviour
{
    public Transform row;

    private void OnEnable()
    {
        transform.position = row.position;
    }
}
