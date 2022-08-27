using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    public string nextSceneName;
    public bool isLastLevel;
    public UIManagerStage classUIManagerStage;

    void Start()
    {
        classUIManagerStage = FindObjectOfType<UIManagerStage>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("FinishLevel");
        if(other.CompareTag("Totem"))
        {
            Debug.Log("Level completed: Load next level");
            classUIManagerStage.TriggerEndgame(isLastLevel);
        }
    }
}
