using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerManager.Movement movement;

    private void Start()
    {
        movement = PlayerManager.Instance?.movement;
    }
}
