using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    public int ballNum;
    public bool isOn=false;

    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private BallController collisionBallController;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        { 
            ShootBall();
        }
    }
    public void SetBall(Ball info)
    {
        ballNum = info.num;
        gameObject.transform.position = new Vector3(0f, 4f, 0);
        gameObject.transform.DOScale(new Vector3(info.radius, info.radius, 1f), 0.3f);
        //spriteRenderer.sprite = info.image;

        gameObject.SetActive(true);
        BallMover.instance.ballMove += AimBall;
        BallMover.instance.ballShoot += ShootBall;
        isOn = true;
    }

    public void AimBall(float pos)
    {
        gameObject.transform.position = new Vector3(pos, 4f, 0f);
    }

    private void ShootBall()
    {
        BallMover.instance.ballMove = null;
        BallMover.instance.ballShoot = null;

        rigidBody.gravityScale = 1;
        rigidBody.AddForce(new Vector2(0, -1 * speed));
        StartCoroutine(BallDirector.instance.CallBall());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionBallController = collision.gameObject.GetComponent<BallController>();
        if (collision.gameObject.layer == 8)
        {
            if (ballNum ==  collisionBallController.ballNum)
            {
                if (gameObject.transform.position.y >= collision.transform.position.y)
                {
                    collisionBallController.isOn = false;
                    
                    rigidBody.AddForce(3 * (collision.transform.position - gameObject.transform.position));
                    collision.gameObject.SetActive(false);
                    //collision.transform.DOMove(collision.gameObject.transform.position, 0.4f);

                    ScoreManager.instance.GetScore(ballNum);
                    ballNum++;
                    Invoke("BallScaler", 0.4f);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        collisionBallController = collision.gameObject.GetComponent<BallController>();
        if (collision.gameObject.layer == 8)
        {
            if (ballNum == collisionBallController.ballNum)
            {
                if (gameObject.transform.position.y >= collision.transform.position.y)
                {
                    collisionBallController.isOn = false;

                    rigidBody.AddForce(3 * (collision.transform.position - gameObject.transform.position));
                    collision.gameObject.SetActive(false);
                    //collision.transform.DOMove(collision.gameObject.transform.position, 0.4f);

                    ScoreManager.instance.GetScore(ballNum);
                    ballNum++;
                    Invoke("BallScaler", 0.4f);
                }
            }
        }
    }


    private void BallScaler()
    { 
        gameObject.transform.DOScale(
            new Vector3(0.1f + 0.2f* ballNum, 0.1f + 0.2f*ballNum, 1f), 0.3f);
    }
}
