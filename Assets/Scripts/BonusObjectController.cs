using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BonusObjectController : MonoBehaviour
{
    public GameObject colliderObj;
    public Animator animator;

    public void CloseObject()
    {
        colliderObj.SetActive(false);
        animator.enabled = true;
    }
}
