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
    [SerializeField] private Text scoreUI;

    private void Start()
    {
        isGameOver = false;
        score = 0;
        SetScoreUI();
    }

    public void GetScore(int point)
    {
        score += point;
        SetScoreUI();
    }

    private void SetScoreUI()
    {
        scoreUI.text = "" + score;
        //Debug.Log(score);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        isGameOver = true;
    }
}
