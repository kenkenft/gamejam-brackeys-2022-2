using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIEndgame : MonoBehaviour
{
    private TextMeshProUGUI messageEndgame;
    void Awake()
    {
        TextMeshProUGUI[] allText = GetComponentsInChildren<TextMeshProUGUI>();
        foreach(TextMeshProUGUI textUI  in allText)
        {
            if(textUI.name == "endgameMessage")
                messageEndgame = textUI;
        }
        // messageEndgame.SetText("Default Message");
    }

    public void SetEndgameMessage()
    {
        Debug.Log("You beat the level!");
        // messageEndgame.SetText("You beat the level!");
    }

    public void RetryLevel()
    {
        // Method that resets current level and re-enables UIScoring overlays.
        Debug.Log("Retry level button clicked!");
    }

    public void GoToNextLevel()
    {
        // Method that clears current level and loads the next level.
        Debug.Log("Go to next level button clicked!");
        // SceneManager.LoadScene(nextSceneName);

    }

    public void ReturnToMainLevel()
    {
        // Method that navigates to main level scene.
        Debug.Log("Return to main level button clicked!");
    }
}
