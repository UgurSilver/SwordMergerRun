using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerManager.CollisionDetection collisionDetection;

    private void Start()
    {
        collisionDetection = PlayerManager.Instance?.collisionDetection;
    }

}
