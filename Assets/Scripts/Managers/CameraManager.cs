using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{

    #region Variables for General
    public static CameraManager Instance;
    #endregion

    #region Variables for Follow
    private Vector3 targetDistance;
    public Vector3 offSet;
    public float followSpeed;
    private Transform target;
    public bool isFollow = true;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = null;
        else
            Destroy(this);

    }
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetDistance = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        if (isFollow)
            FollowTarget();
    }

    #region Follow
    private void FollowTarget()
    {
        transform.position = targetDistance + target.transform.position + offSet;
    }
    #endregion

    #region Shake
    public void Shake()
    {
        DOTween.Complete(this.transform);
        transform.DOShakePosition(/*Duration*/0.1f,/*Strength*/3,/*Vibration*/10);
        transform.DOShakeRotation(/*Duration*/0.1f,/*Strength*/3,/*Vibration*/10);
    }


    #endregion
}
