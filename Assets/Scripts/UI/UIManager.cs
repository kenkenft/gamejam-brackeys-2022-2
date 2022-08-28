using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public IDictionary<string, Canvas> dictAllOverlays = new Dictionary<string, Canvas>();
    
    public AudioManager audioManager;

    public void SetUpAudioManager()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void SetOverlayState(string dictKey, bool buttonState)
    {
        dictAllOverlays[dictKey].enabled = buttonState;
        audioManager.Play("switchOverlays");
    }

}
