using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIStageSelect : MonoBehaviour
{
    [SerializeField] Button buttonBack, buttonStage01;
    [SerializeField] List<Button> listStageButtons;
    public UIManagerMainMenu classUIManagerMainMenu;
    private IDictionary<string, Button> dictAllUIButtons = new Dictionary<string, Button>();

    void Start()
    {
        Button[] allButtons = GetComponentsInChildren<Button>();

        for(int i = 0; i < allButtons.Length; i++)
        {
            SetUpButtonReference(allButtons[i]);
        }
    }

    public void SetUpButtonReference(Button targetButton)
    {
        // Method that sets up button references and dictionary string-Button pairs
        switch(targetButton.name)
        {
            case "buttonBack":
                buttonBack = targetButton.GetComponent<Button>();
                buttonBack.interactable = true;
                dictAllUIButtons.Add("buttonBack", buttonBack);
                break;
            case "buttonStage01":
                buttonStage01 = targetButton.GetComponent<Button>();
                buttonStage01.interactable = true;
                dictAllUIButtons.Add("buttonStage01", buttonStage01);
                break;
            default:
                break;
        }
    }

    public void NavigateToMainMenu()
    {
        Debug.Log("Navigate to Main Menu");
        classUIManagerMainMenu.SwitchUIOverlay(classUIManagerMainMenu.overlayTitle.name, classUIManagerMainMenu.overlayStageSelect.name);
    }
}
