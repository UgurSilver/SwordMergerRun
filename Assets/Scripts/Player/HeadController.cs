using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = PlayerManager.Instance.rows.GetChild(transform.GetSiblingIndex()).position;
    }
}
