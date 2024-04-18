using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParentController : MonoBehaviour
{
    public int level;

    public void ReplacePool()
    {
        PoolingManager.Instance.ReplacingSword(this.gameObject);
    }
}
