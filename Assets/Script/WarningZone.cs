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


    [SerializeField] private int ballInZoneCount = 0;
    [SerializeField] private SpriteRenderer zoneSprite;

    private void Start()
    {
        ballInZoneCount = 0;
        zoneSprite.color = new Color(1, 1, 1, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballInZoneCount++;
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BallController>().isShooted)
        {
            GameOverLineSetActive(true);
        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        ballInZoneCount--;
        if(ballInZoneCount == 0)
        {
            GameOverLineSetActive(false);
        }
    }


    public void GameOverLineSetActive(bool active)
    {
        if (active) //On
        {
            zoneSprite.DOColor(new Color(1, 1, 1, 1), 0.3f);
        }
        else //Off
        {
            zoneSprite.DOColor(new Color(1, 1, 1, 0), 0.3f);
        }
    }
}
