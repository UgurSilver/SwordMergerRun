using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GateController : MonoBehaviour
{
    [Header("General")]
    public GateType gateType;
    public int value;
    public TMPro.TextMeshPro valueText;
    public MeshRenderer mesh;

    private void Start()
    {
        SetDoorValues();
    }

    private void SetDoorValues()
    {
        if (gateType.Equals(GateType.Sum))
        {
            valueText.text = "+" + value;
        }
    }

    public void CloseCollider()
    {
        GetComponent<Collider>().enabled = false;
    }

    public void CloseMesh()
    {
        mesh.transform.DOMoveY(mesh.transform.position.y - 5, 1);
    }
}
