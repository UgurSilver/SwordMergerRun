using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordParentController : MonoBehaviour
{
    public int level;
    private Transform followObject;

    public void ReplacePool()
    {
        PoolingManager.Instance.ReplacingSword(this.gameObject);
    }

    private void Update()
    {
        if (GameManager.Instance.isGame)
        {
            followObject = transform.parent.GetChild(transform.GetSiblingIndex() - 1);
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, followObject.position.x, GameManager.Instance.swordsXFollowSpeed * Time.deltaTime),
             followObject.position.y,
            Mathf.Lerp(transform.position.z, followObject.position.z-GameManager.Instance.swordsZDistance, GameManager.Instance.swordsZfollowSpeed * Time.deltaTime));
        }
    }
}
