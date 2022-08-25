using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerStage : UIManager
{
    public GameObject overlayScoring, overlayEndGame, overlayPause;

    private bool isEndgameTriggered;
    void Awake()
    {
        isEndgameTriggered = false;
        // dictAllOverlays.Add("overlayScoring", GetComponentInChildren<UIScoring>().gameObject.GetComponent<Canvas>());
        // dictAllOverlays.Add("overlayEndgame", GetComponentInChildren<UIEndgame>().gameObject.GetComponent<Canvas>());
    }

    public void TriggerEndgame(float finalScore)
    {
        // Method called externally that disables the UIScoring canvas and then enables UIEndgame canvas
        isEndgameTriggered = true;  // To be used when user attempts to pause game on overlayPause.
        SetOverlayState("overlayScoring", false);
        SetOverlayState("overlayEndgame", true);
        // FindObjectOfType<UIEndgame>().SetEndgameMessage(finalScore);
    }
}
