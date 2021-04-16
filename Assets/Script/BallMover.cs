using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallMover : MonoBehaviour, IDragHandler, IEndDragHandler{

    #region Singleton
    public static BallMover instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void BallMove(float pos);
    public BallMove ballMove;

    public delegate void BallShoot();
    public BallShoot ballShoot;

    private float movePos;

    private void Start()
    {
        ballMove = null;
        ballShoot = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        movePos = eventData.position.x * 6f / 1080 - 3f;
        ballMove(movePos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ballShoot();
    }

}
