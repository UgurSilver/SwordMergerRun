using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{

    #region Variables for General
    public static CameraManager Instance;
    private Transform swords;
    #endregion

    #region Variables for Follow
    private Vector3 targetDistance;
    public Vector3 offSet;
    public float followSpeed;
    private Transform target;
    public bool isFollow = true;

    private float targetZOffset;
    public float zoomSpeed;
    #endregion

    private Transform startMergePos, startRunnerPos;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        startMergePos = GameObject.FindGameObjectWithTag("StartMergePos").transform;
        startRunnerPos = GameObject.FindGameObjectWithTag("StartRunnerPos").transform;
        swords = GameObject.FindGameObjectWithTag("Swords").transform;
    }

    void LateUpdate()
    {
        if (isFollow)
            FollowTarget();

        if (swords.GetChild(0).childCount > 10)
            targetZOffset = -0.5f;
        else
            targetZOffset = 0;

        if (offSet.z != targetZOffset)
            offSet.z =Mathf.MoveTowards(offSet.z, targetZOffset, zoomSpeed*Time.deltaTime);
    }

    #region SetPos

    public void SetMergePos()
    {
        transform.position = startMergePos.position;
        transform.rotation = startMergePos.rotation;
    }

    public void SetRunnerPos(float time)
    {
        transform.DOMove(startRunnerPos.position, time).OnStepComplete(() => GameManager.Instance.EndMerge());
        transform.DORotate(startRunnerPos.eulerAngles, time);
    }
    #endregion

    #region Follow

    public void SetTarget()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform; //Smooth
        target = GameObject.FindGameObjectWithTag("Swords").transform.GetChild(0).GetChild(0);
        targetDistance = transform.position - target.transform.position;
    }
    private void FollowTarget()
    {
        Vector3 posTarget;
        posTarget = targetDistance + target.transform.position + offSet;
        transform.position =new Vector3 (Mathf.MoveTowards(transform.position.x,posTarget.x,followSpeed*Time.deltaTime),posTarget.y,posTarget.z);
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
