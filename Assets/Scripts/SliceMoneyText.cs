using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SliceMoneyText : MonoBehaviour
{
    public Color startColor, endColor;
    private void OnEnable()
    {
        DOTween.Kill(transform.GetComponent<TMPro.TextMeshPro>());
        DOTween.Kill(this);
        transform.GetComponent<TMPro.TextMeshPro>().color = startColor;
        MoveUp();
    }

    private void MoveUp()
    {
        transform.DOMoveY(transform.position.y + 1.2f, 1.5f).OnStepComplete(()=>ReplacePool());
        transform.DOMoveZ(transform.position.z + 3, 1.3f);
        transform.GetComponent<TMPro.TextMeshPro>().DOColor(endColor, 2.9f);
            
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
