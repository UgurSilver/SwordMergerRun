using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{

    private void Start()
    {
        GetComponent<Renderer>().sharedMaterials[1].mainTextureScale = new Vector2(transform.localScale.z / -1.5f, -4f);
    }
}
