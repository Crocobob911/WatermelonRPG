using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public int ballNum;
    public bool isOn=false;

    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer spriteRenderer;

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
        gameObject.transform.localScale = new Vector3(info.radius, info.radius, 1f);
        //spriteRenderer.sprite = info.image;

        gameObject.SetActive(true);
        isOn = true;
    }

    private void ShootBall()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1 * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            if(ballNum == collision.gameObject.GetComponent<BallController>().ballNum)
            {
                collision.gameObject.SetActive(false);

                ballNum++;
                gameObject.transform.localScale 
                    = new Vector3(
                        gameObject.transform.localScale.x + 0.2f,
                        gameObject.transform.localScale.y + 0.2f, 1f);
            }
        }
    }
}
