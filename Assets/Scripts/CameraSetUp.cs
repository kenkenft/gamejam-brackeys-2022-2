using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetUp : MonoBehaviour
{
    public GameObject lowerLeftBound, upperRightBound; 
    public GameObject[] levelBoundary; // 0 - Left; 1 - Right; 2 - Up; 3 - Bottom
    private Vector3 cameraPos = new Vector3(0f,0f,0f), mask = new Vector3(0f,0f,0f); 
    private BoxCollider2D targetCol;

    void Start()
    {
        mask[0] = upperRightBound.transform.position.x - lowerLeftBound.transform.position.x;
        mask[1] = upperRightBound.transform.position.y - lowerLeftBound.transform.position.y;
        SetUpCamera();
        SetUpBoundaries();
    }

    void SetUpCamera()
    {
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
    void SetUpBoundaries()
    {
        Vector3 scaleMaskX = new Vector3(mask[0], 1f, 0f);
        Vector3 scaleMaskY = new Vector3(1f, mask[1], 0f); 
        Vector3 posLeftMask = new Vector3(lowerLeftBound.transform.position.x, lowerLeftBound.transform.position.y + (mask[1]/2f), 0f);
        Vector3 posRightMask = new Vector3(upperRightBound.transform.position.x, lowerLeftBound.transform.position.y + (mask[1]/2f), 0f); 
        Vector3 posTopMask = new Vector3(0f, lowerLeftBound.transform.position.y + mask[1], 0f);
        Vector3 posBottomMask = new Vector3(0f, lowerLeftBound.transform.position.y, 0f); 

        for(int i = 0; i < levelBoundary.Length; i++)
        {
            targetCol = levelBoundary[i].GetComponent<BoxCollider2D>();
            switch(i)
            {
                case 0:
                {
                    targetCol.size = scaleMaskY;
                    targetCol.transform.position = posLeftMask;
                    break;
                }
                case 1:
                {
                    targetCol.size = scaleMaskY;
                    targetCol.transform.position = posRightMask;
                    break;
                }
                case 2:
                {
                    targetCol.size = scaleMaskX;
                    targetCol.transform.position = posTopMask;
                    break;
                }
                case 3:
                {
                    targetCol.size = scaleMaskX;
                    targetCol.transform.position = posBottomMask;
                    break;
                }
                default:
                {
                    Debug.Log("Error in setting boundary");
                    break;
                }            
            }
        }


    }
}
