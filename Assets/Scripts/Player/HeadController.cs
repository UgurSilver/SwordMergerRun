using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{

    public Transform followObject;
    private void Update()
    {
        followObject = PlayerManager.Instance.rows.GetChild(transform.parent.GetSiblingIndex());
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, followObject.position.x, GameManager.Instance.swordsXFollowSpeed * Time.deltaTime),
            followObject.position.y,
           Mathf.Lerp(transform.position.z, followObject.position.z - GameManager.Instance.swordsZDistance, GameManager.Instance.swordsZfollowSpeed * Time.deltaTime));

        //transform.position = PlayerManager.Instance.rows.GetChild(transform.parent.GetSiblingIndex()).position;
    }
}
