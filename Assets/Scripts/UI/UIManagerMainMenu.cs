using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerMainMenu : UIManager
{
    public GameObject overlayTitle, overlayStageSelect, overlayRandomizer;
    
    void Awake()
    {
        overlayTitle = GetComponentInChildren<UITitle>().gameObject;
        dictAllOverlays.Add(overlayTitle.name, overlayTitle.GetComponent<Canvas>());


        // overlayStageSelect = GetComponentInChildren<UIStageSelect>().gameObject;
        // dictAllOverlays.Add(overlayStageSelect.name, overlayStageSelect.GetComponent<Canvas>());

        // SetOverlayState(overlayStageSelectName, false);
        // SetOverlayState(overlayStageSelect.name, false);
    }

    void Start()
    {
        SetUpAudioManager();
    }

    public void SwitchUIOverlay(string overlayTarget, string overlayPrev)
    {
        // Method that enables target overlay, and disables previous overlay
        Debug.Log("SwitchUIOverlay called");
        SetOverlayState(overlayTarget, true);
        SetOverlayState(overlayPrev, false);
    } 
}
