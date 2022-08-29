using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIPauseMenu : MonoBehaviour
{
    private Button buttonMainMenu, buttonContinue, buttonRestart;
    private int nextLevel;
    void Awake()
    {
        Button[] allButtons = GetComponentsInChildren<Button>();
        foreach(Button button in allButtons)
        {
            switch(button.name)
            {
                case "buttonMainMenu":
                    buttonMainMenu = button;
                    break;
                case "buttonContinue":
                    buttonContinue = button;
                    break;
                case "buttonRestart":
                    buttonRestart = button;
                    break;
                default:
                    break;
            }
                
            button.interactable = false;
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        buttonMainMenu.interactable = true;
        buttonContinue.interactable = true;
        buttonRestart.interactable = true;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        buttonMainMenu.interactable = false;
        buttonContinue.interactable = false;
        buttonRestart.interactable = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainLevel()
    {
        Debug.Log("Return to main level button clicked!");
        SceneManager.LoadScene(0);
    }
}
