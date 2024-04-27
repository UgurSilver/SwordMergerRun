using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SliceMoneyText : MonoBehaviour
{
    public Color startColor, endColor;
    private void OnEnable()
    {
        transform.GetComponent<TMPro.TextMeshPro>().color = startColor;
        MoveUp();
    }

    private void MoveUp()
    {
        transform.DOMoveY(transform.position.y + 1.25f, 1.5f).OnStepComplete(()=>ReplacePool());
        transform.GetComponent<TMPro.TextMeshPro>().DOColor(endColor, 1.2f);
            
    }
    void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }

   private void ReplacePool()
    {
        DOTween.Kill(this);
        PoolingManager.Instance.ReplacingSliceMoneyText(this.gameObject);
    }
}
