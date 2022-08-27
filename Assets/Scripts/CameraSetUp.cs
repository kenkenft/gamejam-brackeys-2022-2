using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetUp : MonoBehaviour
{
    public GameObject lowerLeftBound, upperRightBound;
    private Vector3 cameraPos = new Vector3(0f,0f,0f), mask = new Vector3(0f,0f,0f); 
    // private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        // mainCamera = GetComponentInChildren<Camera>();
        SetUpCamera();
    }

    void SetUpCamera()
    {
        mask[0] = upperRightBound.transform.position.x - lowerLeftBound.transform.position.x;
        mask[1] = upperRightBound.transform.position.y - lowerLeftBound.transform.position.y;
        cameraPos = Vector3.Lerp(lowerLeftBound.transform.position, upperRightBound.transform.position, 0.5f);
        // cameraPos[0] = mask[0]/2f;
        // cameraPos[1] = mask[1]/2f;
        cameraPos[2] = -10f;
        Camera.main.transform.position = cameraPos;

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = mask[1]/mask[0];

        if(screenRatio >= targetRatio)
            Camera.main.orthographicSize = mask[1] / 2f;
        else
        {
            float differenceInSize = targetRatio/screenRatio;
            Camera.main.orthographicSize = mask[1] / 2f * differenceInSize;
        }
    }
}
