using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private int frameCount = 60;

    private void Awake()
    {
        setupCamera();
        setupFrame();
    }

    private void setupCamera()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        float targetWidthAspect = 9f;
        float targetHeightAspect = 18f;

        Camera mainCamera = Camera.main;

        mainCamera.aspect = targetWidthAspect / targetHeightAspect;

        float widthRatio = (float)Screen.width / targetWidthAspect;
        float heightRatio = (float)Screen.height / targetHeightAspect;

        float heightadd = ((widthRatio / (heightRatio / 100)) - 100) / 200;
        float widthadd = ((heightRatio / (widthRatio / 100)) - 100) / 200;

        if (heightRatio > widthRatio)
            widthadd = 0.0f;
        else
            heightadd = 0.0f;

        mainCamera.rect = new Rect(
            mainCamera.rect.x + Mathf.Abs(widthadd),
            mainCamera.rect.y + Mathf.Abs(heightadd),
            mainCamera.rect.width + (widthadd * 2),
            mainCamera.rect.height + (heightadd * 2));

        //Debug.Log("Screen Fixed : " + targetWidthAspect + " : " + targetHeightAspect);
    }

    private void setupFrame()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = frameCount;

        //Debug.Log("Frame Fixed : " + frameCount);
    }

}

