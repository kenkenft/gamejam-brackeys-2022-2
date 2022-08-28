using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIEndgame : MonoBehaviour
{
    private Button buttonMainMenu, buttonNext;
    private int nextLevel;
    void Awake()
    {
        Button[] allButtons = GetComponentsInChildren<Button>();
        foreach(Button button in allButtons)
        {
            if(button.name == "buttonMainMenu")
                buttonMainMenu = button;
            
            if(button.name == "buttonNext")
                buttonNext = button;
        }

    }

    public void SetEndgameScreen(int levelNum, bool isLastLevel)
    {
        if(isLastLevel)
        {    
            buttonNext.enabled = false;
            buttonNext.gameObject.SetActive(false);
            buttonMainMenu.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f,0f);
        }
        else
            nextLevel = levelNum;
        
        Time.timeScale = 0;
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void ReturnToMainLevel()
    {
        SceneManager.LoadScene(0);
    }
}
