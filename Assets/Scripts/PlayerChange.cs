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
    private int counter = 0, maxTotemCount = 0;
    private bool nextTotemFound;
    private AudioManager audioManager;
    
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

            listClassStackTotem[counter].totemID = counter;

            listTotemIDs.Add(listClassStackTotem[counter].totemID);
            if(listTotemIDs[counter] != 0)
            {
                listClassStackTotem[counter].isTotemRecruited = false;
                SetTotemState(false);
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
}
