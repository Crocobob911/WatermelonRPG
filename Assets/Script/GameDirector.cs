using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    #region Singleton
    public static GameDirector instance;
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

    public bool isGameOver;

    private int score;
    [SerializeField] private Text scoreText;

    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject mainSceneUI;

    private void Start()
    {
        CameraController.instance.MoveCam(1);
        inGameUI.SetActive(false);
    }


    public void GameStart()
    {
        Debug.Log("Game Start");
        isGameOver = false;

        CameraController.instance.MoveCam(2);

        mainSceneUI.SetActive(false);
        inGameUI.SetActive(true);

        score = 0;
        scoreText.text = "" + score;

        BallDirector.instance.ballDirectorGameStart();
    }

    public void GetScore(int point)
    {
        score += point;
        scoreText.text = "" + score;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        isGameOver = true;
    }
}
