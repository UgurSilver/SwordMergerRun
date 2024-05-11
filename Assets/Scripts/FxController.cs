using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FxController : MonoBehaviour
{
    public FxType fxType;
    public float delay;

    private void OnEnable()
    {
        Invoke(nameof(ReplacePool), delay);
    }

    

    private void ReplacePool()
    {
        if (fxType.Equals(FxType.SliceFx))
            PoolingManager.Instance?.ReplacingSliceFx(this.gameObject);
        if(fxType.Equals(FxType.SmokeFx))
            PoolingManager.Instance?.ReplacingSmokeFx(this.gameObject);
        if(fxType.Equals(FxType.PowerFx))
            PoolingManager.Instance?.ReplacingPowerFx(this.gameObject);

    }

}
