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
    // private List<Collider2D> listCols = new List<Collider2D>{};
    private int currentTotemID = 0, counter = 0, maxTotemCount = 0;
    private bool nextTotemFound;
    
    void Awake()
    {
        maxTotemCount = listTotems.Count;
        SetUpTotems();
    }
        
    public void ChangeCharacter()
    {
        Debug.Log("ChangeCharacter called!");
        if(maxTotemCount > 1)
        {
            // listRigs[counter].isKinematic = true;
            // listClassPlayerMove[counter].totemState = "inactive";
            // listClassPlayerMove[counter].enabled = false; // Should disable current active totem
            SetTotemState(false);
            SwitchNextAvailableTotem();
            Debug.Log("Controlling Totem " + listClassStackTotem[counter].totemID);
        }
    }

    public int GetMaxTotemCount()
    {
        return maxTotemCount;
    }

    void SetUpTotems()
    {
        for(counter = 0; counter < maxTotemCount; counter++)
        {
            listClassPlayerMove.Add(listTotems[counter].GetComponentInChildren<PlayerMove>());
            listClassStackTotem.Add(listTotems[counter].GetComponentInChildren<StackTotem>());
            listRigs.Add(listClassPlayerMove[counter].gameObject.GetComponent<Rigidbody2D>());
            // listCols.Add(listClassPlayerMove[i].gameObject.GetComponent<Collider2D>());

            listTotemIDs.Add(listClassStackTotem[counter].totemID);
            if(listTotemIDs[counter] != 0)
            {
                Debug.Log(listClassPlayerMove[counter].name + ": Totem ID not 0");                // listRigs[i].isKinematic = true;
                listClassStackTotem[counter].isTotemRecruited = false;
                SetTotemState(false);
            }
            else
            {
                Debug.Log(listClassPlayerMove[counter].name + ": Totem ID is 0");                // listRigs[i].isKinematic = true;
                listClassStackTotem[counter].isTotemRecruited = true;
                SetTotemState(true);
            }
            listClassStackTotem[counter].isTotemStacked = false;
            PrintTotemState();
        }
        counter = 0;
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
            }
        }
        // return currCount;
    }

    private void SetTotemState(bool state)
    {
        listClassStackTotem[counter].totemState = state ? "active" : "inactive";
        listRigs[counter].isKinematic = !state;
        // listCols[counter].isTrigger = !state;
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
        Debug.Log("ChangeToTargetTotem called!");
    }
}
