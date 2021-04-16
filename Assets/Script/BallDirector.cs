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
    }
    #endregion

    public List<Ball> ballInfo = new List<Ball>();

    private BallController[] balls = new BallController[100];

    [SerializeField] private int ballObjectIndex = 0;
    private int ballNum;


    private void Start()
    {
        GetBalls();
        StartCoroutine(CallBall());
    }


    private void GetBalls()
    {
        for(int i = 0; i < 100; i++)
        {
            balls[i] = GameObject.Find("Balls").gameObject.transform.GetChild(i).gameObject.GetComponent<BallController>();
            balls[i].gameObject.transform.position = new Vector3(0, 0, 0);
            //balls[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator CallBall()
    {
        yield return new WaitForSeconds(0.4f);

        ballNum = Random.Range(1, 5);
        balls[ballObjectIndex].GetComponent<BallController>().SetBall(ballInfo[ballNum]);

        do
        {
            ballObjectIndex++;
            if (ballObjectIndex > 99) ballObjectIndex = 0;
        } while (balls[ballObjectIndex].isOn);
        //Debug.Log(ballObjectIndex);

        yield return null;
    }
}

[System.Serializable]
public class Ball
{
    public int num;
    public float radius;
    //public Sprite image;
}