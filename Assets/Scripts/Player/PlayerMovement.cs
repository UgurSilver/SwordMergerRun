using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables for General
    private PlayerManager.Movement movement;
    private Transform swords;
    #endregion

    #region Variables for Movement
    private bool isTouch;
    private float xDifference;
    private Vector3 currentTouch, firstTouch;
    public float border;
    private float negativeBorderClamp, positiveBorderClamp;
    #endregion

    private void Start()
    {
        movement = PlayerManager.Instance?.movement;
        swords = GameObject.FindGameObjectWithTag("Swords").transform;
    }

    private void Update()
    {
        if (GameManager.Instance.isGame)
            Move();
    }

    private void Move()
    {
        //ForwardMovement
        transform.Translate(transform.forward * movement.forwardSpeed * Time.smoothDeltaTime);

        //SideMovement
        if (isTouch)
        {
            currentTouch = Input.mousePosition;
            xDifference = (currentTouch.x - firstTouch.x) * 100f / Screen.width;
            xDifference = Mathf.Clamp(xDifference, -1, 1); //Clamp Side acceleration
            Vector3 newPos = transform.position + new Vector3(xDifference, 0, 0);
            transform.position = Vector3.Lerp(transform.position, new Vector3(newPos.x, transform.position.y, transform.position.z), movement.sideSpeed * Time.deltaTime);// MoveSpeed = 11
           
            //Border Control
            SetBorder();
            if (transform.position.x <= negativeBorderClamp)
                transform.position = new Vector3(negativeBorderClamp, transform.position.y, transform.position.z);
            if (transform.position.x > positiveBorderClamp)
                transform.position = new Vector3(positiveBorderClamp, transform.position.y, transform.position.z);
        }

        if (Input.GetMouseButton(0))
        {
            isTouch = true;
            firstTouch = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isTouch = false;
        }
    }

    private void SetBorder()
    {
        for (int i = 0; i < swords.childCount; i++)
        {
            if (swords.GetChild(i).childCount != 1)
            {
                positiveBorderClamp = border - Mathf.Abs(swords.GetChild(i).position.x);
                negativeBorderClamp = -positiveBorderClamp;
            }

        }
    }
}
