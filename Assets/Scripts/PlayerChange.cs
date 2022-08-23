using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public List<GameObject> listTotems;
    private List<int> listTotemIDs = new List<int>{};
    private List<PlayerMove> listClassPlayerMove = new List<PlayerMove>{};
    private int currentTotemID = 0, counter = 0, maxTotemCount = 0;
    private bool nextTotemFound;
    
    void Start()
    {
        maxTotemCount = listTotems.Count;
        SetUpTotems();
    }
        
    public void ChangeCharacter()
    {
        Debug.Log("ChangeCharacter called!");
        if(maxTotemCount > 1)
        {
            listClassPlayerMove[counter].totemState = "inactive";
            listClassPlayerMove[counter].enabled = false; // Should disable current active totem
            FindNextAvailableTotem();
            Debug.Log("Controlling Totem " + listClassPlayerMove[counter].totemID);
        }
    }

    public int GetMaxTotemCount()
    {
        return maxTotemCount;
    }

    void SetUpTotems()
    {
        for(int i = 0; i < maxTotemCount; i++)
        {
            listClassPlayerMove.Add(listTotems[i].GetComponentInChildren<PlayerMove>());
            listTotemIDs.Add(listClassPlayerMove[i].totemID);
            if(listTotemIDs[i] != 0)
            {
                listClassPlayerMove[i].totemState = "inactive";
                listClassPlayerMove[i].isTotemRecruited = false;
                listClassPlayerMove[i].enabled = false;
            }
            else
            {
                listClassPlayerMove[i].totemState = "active";
                listClassPlayerMove[i].isTotemRecruited = true;
                listClassPlayerMove[i].enabled = true;
            }
        }
    }

    private void FindNextAvailableTotem()
    {
        nextTotemFound = false;
        while(!nextTotemFound)
        {
            if(counter >= maxTotemCount-1)
                counter = 0;
            else
                counter++;

            if(listClassPlayerMove[counter].isTotemRecruited == true)
            {
                listClassPlayerMove[counter].totemState = "active";
                listClassPlayerMove[counter].enabled = true;
                nextTotemFound = true;
            }
        }
        // return currCount;
    } 
}
