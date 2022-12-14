using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public int nextLevelNum;
    public bool isLastLevel;
    public UIManagerStage classUIManagerStage;

    void Start()
    {
        classUIManagerStage = FindObjectOfType<UIManagerStage>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Totem"))
            classUIManagerStage.TriggerEndgame(nextLevelNum, isLastLevel);
    }
}
