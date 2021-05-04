using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    public int ballNum;
    public bool isOn=false;

    public bool isShooted = false;
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioSource ballSound;
    private BallController collisionBallController;
    private Rigidbody2D rigidBody;
    private bool isGettingBig = false;

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
    public void SetBall(Ball info) //공에 정보들을 집어넣음
    {
        ballNum = info.num;
        gameObject.transform.position = new Vector3(0f, 4f, 0);
        gameObject.transform.DOScale(new Vector3(info.radius, info.radius, 1f), 0.3f);
        spriteRenderer.sprite = info.image;

        gameObject.SetActive(true);
        BallMover.instance.ballMove += AimBall;
        BallMover.instance.ballShoot += ShootBall;
        isOn = true;
        isGettingBig = false;
    }

    public void AimBall(float pos) //화면 드래그해서 공 위치 조절
    {
        gameObject.transform.position = new Vector3(pos, 4f, 0f);
    }

    private void ShootBall() //터치아웃 시 공 떨어짐
    {
        BallMover.instance.ballMove = null;
        BallMover.instance.ballShoot = null;

        rigidBody.gravityScale = 1;
        rigidBody.AddForce(new Vector2(0, -1 * speed));
        StartCoroutine(BallDirector.instance.CallBall());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer != 9)
            isShooted = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isGettingBig == false)
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

                        GameDirector.instance.GetScore(ballNum);
                        ballNum++;
                        StartCoroutine("BallScaler");
                    }
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isShooted && collision.gameObject.layer == 10)
        {
            WarningZone.instance.GameOverLineSetActive(true);
        }

        if (isShooted && collision.gameObject.layer == 11)
        {
            GameDirector.instance.GameOver();
        }

    }

    private IEnumerator BallScaler()
    {
        isGettingBig = true;
        yield return new WaitForSeconds(0.4f);

        gameObject.transform.DOScale(
            new Vector3(0.1f* ballNum, 0.1f*ballNum, 1f), 0.3f);
        ballSound.Play();

        spriteRenderer.sprite = BallDirector.instance.ballInfo[ballNum-1].image;
        yield return new WaitForSeconds(0.3f);
        isGettingBig = false;
        //Debug.Log(isGettingBig);
    }
}
