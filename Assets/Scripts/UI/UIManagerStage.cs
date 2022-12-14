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
        Time.timeScale = 1;
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
        SetUpAudioManager();
        SetOverlayState("overlayInLevel", true);
        SetOverlayState("overlayEndgame", false);
        SetOverlayState("overlayPauseMenu", false); 
    }

    public void TriggerEndgame(int levelNum, bool isLastLevel)
    {
        isLevelFinished = false;  // To be used when user attempts to pause game on overlayPause.
        SetOverlayState("overlayInLevel", false);
        SetOverlayState("overlayEndgame", true);

        audioManager.Play("endgameTrigger");
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
