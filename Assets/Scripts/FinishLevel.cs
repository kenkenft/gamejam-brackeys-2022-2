using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public string nextSceneName;
    public bool lastLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("FinishLevel");
        if(other.CompareTag("Totem"))
        {
            
            //Load relevant scene
            if(lastLevel == true)
            {
                Debug.Log("Last Level completed: You Win!");
                // SceneManager.LoadScene(6); //Loads EndScreen
            }
            else
            {
                Debug.Log("Level completed: Load next level");
                // SceneManager.LoadScene(nextSceneName);
            }
            

        }
    }
}
