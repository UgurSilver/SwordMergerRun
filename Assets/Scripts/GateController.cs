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
    public GameObject rotateIcon,repairIcon,FireIcon;
    public MeshRenderer mesh;
    public Material positiveMat, negativeMat,rotateMat;

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
            SetColor(rotateMat);

            rotateIcon.SetActive(true);
        }

        if (gateType.Equals(GateType.Repair))
        {
            nameText.gameObject.SetActive(true);
            nameText.text = "Repair";
            SetColor(rotateMat);
            repairIcon.SetActive(true);
        }

        if (gateType.Equals(GateType.Fire))
        {
            nameText.gameObject.SetActive(true);
            nameText.text = "Fire";
            SetColor(rotateMat);
            FireIcon.SetActive(true);
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
