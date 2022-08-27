using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerStage : UIManager
{
    public GameObject overlayInLevel, overlayEndGame, overlayPauseMenu;
    private UIInLevel classUIInLevel;
    private UIEndgame classUIEndgame;
    private UIPauseMenu classUIPauseMenu;
    private bool isGamePaused;

    public bool isLevelFinished;
    void Awake()
    {
        isLevelFinished = false;
        isGamePaused = false;
        classUIInLevel = GetComponentInChildren<UIInLevel>();
        classUIEndgame = GetComponentInChildren<UIEndgame>();
        classUIPauseMenu = GetComponentInChildren<UIPauseMenu>();
        dictAllOverlays.Add("overlayInLevel", classUIInLevel.gameObject.GetComponent<Canvas>());
        dictAllOverlays.Add("overlayEndgame", classUIEndgame.gameObject.GetComponent<Canvas>());
        dictAllOverlays.Add("overlayPauseMenu", classUIPauseMenu.gameObject.GetComponent<Canvas>());
    }

    void Start()
    {
        SetOverlayState("overlayInLevel", true);
        SetOverlayState("overlayEndgame", false);
        SetOverlayState("overlayPauseMenu", false);
    }

    public void TriggerEndgame(int levelNum, bool isLastLevel)
    {
        // Method called externally that disables the UIScoring canvas and then enables UIEndgame canvas
        isLevelFinished = false;  // To be used when user attempts to pause game on overlayPause.
        SetOverlayState("overlayInLevel", false);
        SetOverlayState("overlayEndgame", true);
        classUIEndgame.SetEndgameScreen(levelNum, isLastLevel);
    }

    public void SetPauseOverlay()
    {
        if(!isLevelFinished)
        {
            isGamePaused = !isGamePaused;
            SetOverlayState("overlayInLevel", !isGamePaused);
            SetOverlayState("overlayPauseMenu", isGamePaused);
            if(isGamePaused)
                classUIPauseMenu.PauseGame();
            else
                classUIPauseMenu.ContinueGame();
        }
    }
}
