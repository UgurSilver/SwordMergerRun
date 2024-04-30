using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    public Transform followObject;


    private void LateUpdate()
    {
        if (!PlayerManager.Instance.ishorizontal)
        {
        followObject = PlayerManager.Instance.rows.GetChild(transform.parent.GetSiblingIndex());
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followObject.position.x, GameManager.Instance.swordsXFollowSpeed * Time.smoothDeltaTime),
                followObject.position.y, followObject.position.z - GameManager.Instance.swordsZDistance);
        }
        else
        {
            followObject = PlayerManager.Instance.rows.GetChild(0);
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followObject.position.x, GameManager.Instance.swordsXFollowSpeed * Time.smoothDeltaTime),
        Mathf.Lerp(transform.position.y, (followObject.position.y-0.4f) + (transform.parent.GetSiblingIndex() * GameManager.Instance.swordsYDistance), GameManager.Instance.swordsYFollowSpeed * Time.smoothDeltaTime),
        followObject.position.z - GameManager.Instance.swordsZDistance);
                }
    }
}
