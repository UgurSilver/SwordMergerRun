using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeRunButton : MonoBehaviour
{
    public void MergeRun()
    {
        CameraManager.Instance.SetPerspective();
    }
}
