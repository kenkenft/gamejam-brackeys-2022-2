using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public List<GameObject> listTotems;
    private List<int> listTotemIDs = new List<int>{};
    private List<PlayerMove> listClassPlayerMove = new List<PlayerMove>{};
    private List<Rigidbody2D> listRigs = new List<Rigidbody2D>{};
    private List<StackTotem> listClassStackTotem = new List<StackTotem>{};
    private List<SpriteRenderer> listTotemSprites = new List<SpriteRenderer>{};
    private List<Color> listTotemColors = new List<Color>{}, listTotemColorsInactive = new List<Color>{};
    private int counter = 0, maxTotemCount = 0;
    private bool nextTotemFound;
    private AudioManager audioManager;
    private Color inactiveColor = new Color(0.5f,0.5f,0.5f,1);
    
    void Awake()
    {
        try
        {
            listTotems[0].name = "totem (0)";
        }
        catch
        {
            Debug.Log("totems have not been set in PlayerChange.listTotems");
        }

        maxTotemCount = listTotems.Count;
        SetUpTotems();
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    
    void SetUpTotems()
    {
        for(counter = 0; counter < maxTotemCount; counter++)
        {
            listClassPlayerMove.Add(listTotems[counter].GetComponentInChildren<PlayerMove>());
            listClassStackTotem.Add(listTotems[counter].GetComponentInChildren<StackTotem>());
            
            listRigs.Add(listClassPlayerMove[counter].gameObject.GetComponent<Rigidbody2D>());

            listTotemSprites.Add(listTotems[counter].GetComponentInChildren<SpriteRenderer>());
            listTotemColors.Add(listTotemSprites[counter].color);

            listTotemColorsInactive.Add(new Color(listTotemColors[counter].r, listTotemColors[counter].g, listTotemColors[counter].b, 0.3f));

            listClassStackTotem[counter].totemID = counter;

            listTotemIDs.Add(listClassStackTotem[counter].totemID);
            if(listTotemIDs[counter] != 0)
            {
                listClassStackTotem[counter].isTotemRecruited = false;
                
                SetTotemState(false);
                listTotemSprites[counter].color = inactiveColor;
            }
            else
            {
                listClassStackTotem[counter].isTotemRecruited = true;
                SetTotemState(true);
            }
            listClassStackTotem[counter].isTotemStacked = false;
            // PrintTotemState();
        }
        counter = 0;
    }

    public void ChangeCharacter()
    {
        if(maxTotemCount > 1)
        {
            SetTotemState(false);
            SwitchNextAvailableTotem();
        }
    }

    public int GetMaxTotemCount()
    {
        return maxTotemCount;
    }

    

    private void SwitchNextAvailableTotem()
    {
        nextTotemFound = false;
        while(!nextTotemFound)
        {
            if(counter >= maxTotemCount-1)
                counter = 0;
            else
                counter++;

            if(listClassStackTotem[counter].isTotemRecruited && !listClassStackTotem[counter].isTotemStacked)
            {
                SetTotemState(true);
                nextTotemFound = true;
                audioManager.Play("switchTotem");
            }
        }
    }

    private void SetTotemState(bool state)
    {
        listClassStackTotem[counter].totemState = state ? "active" : "inactive";
        listClassPlayerMove[counter].enabled = state;
        if(state)
            SetColourState(counter, listTotemColors[counter]);
        else
            SetColourState(counter, listTotemColorsInactive[counter]);
    }

    private void PrintTotemState()
    {
        Debug.Log(listClassPlayerMove[counter].name + " totemState: " + listClassStackTotem[counter].totemState);
        Debug.Log(listClassPlayerMove[counter].name + " isKinematic: " + listRigs[counter].isKinematic);
        Debug.Log(listClassPlayerMove[counter].name + " enabled: " + listClassPlayerMove[counter].enabled);
    }

    public void ChangeToTargetTotem(StackTotem currTotem, StackTotem nextTotem)
    {
        counter = listTotemIDs.IndexOf(currTotem.totemID);
        SetTotemState(false);

        counter = listTotemIDs.IndexOf(nextTotem.totemID);
        listRigs[counter].isKinematic = false;
        listClassStackTotem[counter].isTotemStacked = false;
        SetTotemState(true);
    }

    public void SetTotemColorInactive(int totemID)
    {
        SetColourState(totemID, listTotemColorsInactive[totemID]);
    }

    public void SetColourState(int totemID, Color targetColor)
    {
        listTotemSprites[totemID].color = targetColor;
    }
}
