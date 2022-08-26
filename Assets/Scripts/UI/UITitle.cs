using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UITitle : MonoBehaviour
{  
    public UIManagerMainMenu classUIManagerMainMenu;

    public void OnPlayButton ()
    {
        //Load level 1
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton ()
    {
        //When testing in Unity, it will not actually quit the Unity application itself
        Application.Quit();
    }

    public void OnReturnButton ()
    {
        //Returns player to Main menu from End screen
        SceneManager.LoadScene(0);
    }

    public void NavigateToStageSelect()
    {
        Debug.Log("Navigate to Stage Select Overlay");
        classUIManagerMainMenu.SwitchUIOverlay(classUIManagerMainMenu.overlayStageSelect.name, classUIManagerMainMenu.overlayTitle.name);
    }

    public void NavigateToRandomizer()
    {
        Debug.Log("Navigate to Randomizer Overlay");
    }
}
