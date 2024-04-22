using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{

    private void Start()
    {
        GetComponent<Renderer>().materials[1].mainTextureScale = new Vector2(transform.localScale.z / -3, -4f);
    }
}
