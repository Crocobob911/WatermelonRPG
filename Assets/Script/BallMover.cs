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

    [SerializeField] private AudioSource dragStartSound;
    [SerializeField] private AudioSource dragEndSound;
    private float movePos;

    private void Start()
    {
        ballMove = null;
        ballShoot = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("DragStart");
        dragStartSound.Play();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log(eventData.position.x);
        movePos = eventData.position.x * 5f / 1080f - 2.5f;
        ballMove(movePos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragEndSound.Play();
        ballShoot();
    }

}
