using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    #region Singleton
    public static ScoreManager instance;
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

    private int score;

    [SerializeField] private Text scoreUI;

    private void Start()
    {
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
}
