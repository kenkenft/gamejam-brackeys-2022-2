using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerStage : UIManager
{
    public GameObject overlayInLevel, overlayEndGame, overlayPauseMenu;

    public bool isLevelFinished;
    void Awake()
    {
        isLevelFinished = false;
        dictAllOverlays.Add("overlayInLevel", GetComponentInChildren<UIInLevel>().gameObject.GetComponent<Canvas>());
        dictAllOverlays.Add("overlayEndgame", GetComponentInChildren<UIEndgame>().gameObject.GetComponent<Canvas>());
        dictAllOverlays.Add("overlayPauseMenu", GetComponentInChildren<UIPauseMenu>().gameObject.GetComponent<Canvas>());
    }

    public void TriggerEndgame()
    {
        // Method called externally that disables the UIScoring canvas and then enables UIEndgame canvas
        isLevelFinished = false;  // To be used when user attempts to pause game on overlayPause.
        SetOverlayState("overlayLevelMessages", false);
        SetOverlayState("overlayEndgame", true);
        FindObjectOfType<UIEndgame>().SetEndgameMessage();
    }
}
