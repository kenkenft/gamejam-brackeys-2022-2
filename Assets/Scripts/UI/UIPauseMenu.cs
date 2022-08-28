using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    private Button buttonMainMenu, buttonContinue;
    private int nextLevel;
    void Awake()
    {
        Button[] allButtons = GetComponentsInChildren<Button>();
        foreach(Button button in allButtons)
        {
            if(button.name == "buttonMainMenu")
                buttonMainMenu = button;
            
            if(button.name == "buttonContinue")
                buttonContinue = button;
            
            button.interactable = false;
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        buttonMainMenu.interactable = true;
        buttonContinue.interactable = true;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        buttonMainMenu.interactable = false;
        buttonContinue.interactable = false;
    }

    // public void RestartLevel()
    // {
    //     //Bonus ToDo Just load the current scene?
    // }

    public void ReturnToMainLevel()
    {
        Debug.Log("Return to main level button clicked!");
        SceneManager.LoadScene(0);
    }
}
