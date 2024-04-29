using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerManager.CollisionDetection collisionDetection;
    private float xPos;
    private Transform swords;
    private int openSwordsNum;

    private void Start()
    {
        collisionDetection = PlayerManager.Instance?.collisionDetection;
        swords = GameObject.FindGameObjectWithTag("Swords").transform;
    }


    private void LateUpdate()
    {
        if (GameManager.Instance.isGame)
        {
            xPos = 0;
            for (int i = 0; i < swords.childCount; i++)
            {
                if (swords.GetChild(i).childCount != 1)
                {
                    openSwordsNum++;
                    xPos += swords.GetChild(i).GetChild(1).position.x;
                }
            }

            if (openSwordsNum > 0)
                transform.position = new Vector3(xPos / openSwordsNum, transform.position.y, transform.position.z);
            xPos = 0;
            openSwordsNum = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            other.transform.parent.GetChild(0).GetComponent<GateController>().CloseCollider();
            other.transform.parent.GetChild(1).GetComponent<GateController>().CloseCollider();

            GateController gateSc = other.transform.GetComponent<GateController>();
            gateSc.CloseMesh();

            if (gateSc.gateType.Equals(GateType.Sum))
                PlayerManager.Instance.AddSwords(other.GetComponent<GateController>().value);
            if (gateSc.gateType.Equals(GateType.Rotate))
                PlayerManager.Instance.RotateSwords();
        }

        if (other.CompareTag("FinishPlane"))
        {
            PlayerManager.Instance.ishorizontal = true;
            PlayerManager.Instance.RotateSwords();
            GameManager.Instance.isLevelEnd = true;
        }
    }
}
