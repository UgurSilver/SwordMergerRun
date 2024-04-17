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

        if (fxType.Equals(FxType.HitFx))
            HitFxSettings();

      

        Invoke(nameof(ReplacePool), delay);
    }

    private void HitFxSettings()
    {
       
    }

    private void ReplacePool()
    {
        //if (fxType.Equals(FxType.HitFx))
        //    PoolingManager.Instance?.ReplacingFx(this.gameObject);

    }

    private void OnDisable()
    {
        if (fxType.Equals(FxType.HitFx))
            ResetHitFx();
    }

    private void ResetHitFx()
    {
        DOTween.Kill(this);
    }
}
