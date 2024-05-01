using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GateController : MonoBehaviour
{
    [Header("General")]
    public GateType gateType;
    public int value;
    public TMPro.TextMeshPro valueText,nameText;
    public GameObject rotateIcon,repairIcon;
    public MeshRenderer mesh;
    public Material positiveMat, negativeMat;

    private void Start()
    {
        SetDoorValues();
    }

    private void SetDoorValues()
    {
        if (gateType.Equals(GateType.Sum))
        {
            valueText.gameObject.SetActive(true);
            valueText.text = "+" + value;
            SetColor(positiveMat);
        }
        if (gateType.Equals(GateType.Multiplier))
        {
            valueText.gameObject.SetActive(true);
            valueText.text = "x" + value;
            SetColor(positiveMat);
        }

        if (gateType.Equals(GateType.Minus))
        {
            valueText.gameObject.SetActive(true);
            valueText.text = "-" + value;
            SetColor(negativeMat);
        }
        if (gateType.Equals(GateType.Divider))
        {
            valueText.gameObject.SetActive(true);
            valueText.text = "÷" + value;

            SetColor(negativeMat);
        }

        if (gateType.Equals(GateType.Rotate))
        {
            nameText.gameObject.SetActive(true);
            nameText.text = "Rotate";

            rotateIcon.SetActive(true);
        }

        if (gateType.Equals(GateType.Repair))
        {
            nameText.gameObject.SetActive(true);
            nameText.text = "Repair";

            repairIcon.SetActive(true);
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

    public void SetColor(Material _material)
    {
        mesh.material = _material;
    }
}
