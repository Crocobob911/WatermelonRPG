using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDirector : MonoBehaviour
{
    #region Singleton
    public static BallDirector instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        GetBalls();
    }
    #endregion

    public List<Ball> ballInfo = new List<Ball>();

    private BallController[] balls = new BallController[100];
    [SerializeField] private int ballObjectIndex = 0;
    [SerializeField] private GameObject dragZone;
    private int ballNum;
    private int nextBallNum;

    [SerializeField] private SpriteRenderer nextBallSprite;


    public void ballDirectorGameStart()
    {
        CallNextBall();
        StartCoroutine(CallBall());
    }


    private void GetBalls()
    {
        for(int i = 0; i < 100; i++)
        {
            balls[i] = GameObject.Find("Balls").gameObject.transform.GetChild(i).gameObject.GetComponent<BallController>();
            balls[i].gameObject.transform.position = new Vector3(0, 0, 0);
            balls[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator CallBall()
    {
        if(GameDirector.instance.isGameOver == true)
        {
            dragZone.SetActive(false);
            yield break;
        }
        yield return new WaitForSeconds(0.4f);

        ballNum = nextBallNum;
        CallNextBall();
        balls[ballObjectIndex].GetComponent<BallController>().SetBall(ballInfo[ballNum]);

        do
        {
            ballObjectIndex++;
            if (ballObjectIndex > 99) ballObjectIndex = 0;
        } while (balls[ballObjectIndex].isOn);

        yield return null;
    }

    private void CallNextBall()
    {
        nextBallNum = Random.Range(0, 3);
        nextBallSprite.sprite = ballInfo[nextBallNum].image;
    }
}

[System.Serializable]
public class Ball
{
    public int num;
    public float radius;
    public Sprite image;
}