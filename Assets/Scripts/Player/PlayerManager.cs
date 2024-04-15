using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    #region Variables for General
    public static PlayerManager Instance;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = null;
        else
            Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    #region 

    public void Win()
    {
        UIManager.Instance?.OpenWinpanel();
    }

    public void Fail()
    {
        UIManager.Instance?.OpenFailPanel();
    }
    #endregion

    [System.Serializable]
    public class Movement
    {
        public float forwardSpeed;
    }
    public Movement movement;

    [System.Serializable]
    public class CollisionDetection
    {
        public int adana;
        public void CollectableCollision()
        {

        }
    }
    public CollisionDetection collisionDetection;
}


