using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class WarningZone : MonoBehaviour
{
    #region Singleton
    public static WarningZone instance;
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

    public bool isGameOver = false;

    private int ballInZoneCount = 0;
    [SerializeField] private SpriteRenderer zoneSprite;

    private void Start()
    {
        ballInZoneCount = 0;
        isGameOver = false;
        zoneSprite.color = new Color(1, 1, 1, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballInZoneCount++;
        Debug.Log("ballInZoneCount : "+ballInZoneCount);
        GameOverLineOnOff();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ballInZoneCount--;
        Debug.Log("ballInZoneCount : " + ballInZoneCount);
    }


    public void GameOverLineOnOff()
    {
        if (ballInZoneCount == 0) //Off
        {
            Debug.Log("GameOverLineOff");
            zoneSprite.DOColor(new Color(1, 1, 1, 0), 0.3f);
        }
        else //On
        {
            Debug.Log("GameOverLineOn");
            zoneSprite.DOColor(new Color(1, 1, 1, 1), 0.3f);
        }
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
        isGameOver = true;
    }
}
