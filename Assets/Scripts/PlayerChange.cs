using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    public List<GameObject> listTotems;
    private List<int> listTotemIDs = new List<int>{};
    private List<PlayerMove> listClassPlayerMove = new List<PlayerMove>{};
    private int currentTotemID = 0, counter = 0, maxTotemCount = 0;
    
    void Start()
    {
        maxTotemCount = listTotems.Count;
        for(int i = 0; i < maxTotemCount; i++)
        {
            listClassPlayerMove.Add(listTotems[i].GetComponentInChildren<PlayerMove>());
            listTotemIDs.Add(listClassPlayerMove[i].totemID);
            if(listTotemIDs[i] != 0)
                listClassPlayerMove[i].enabled = false;
            else
                listClassPlayerMove[i].enabled = true;
        }
    }
    public void ChangeCharacter()
    {
        Debug.Log("ChangeCharacter called!");
        if(maxTotemCount > 1)
        {
            listClassPlayerMove[counter].enabled = !listClassPlayerMove[counter].enabled; // Should disable current active totem
            if(counter >= maxTotemCount-1)
                counter = 0;
            else
                counter++;
            listClassPlayerMove[counter].enabled = !listClassPlayerMove[counter].enabled; 
        
            Debug.Log("Controlling Totem " + listClassPlayerMove[counter].totemID);
        }
    }

    public int GetMaxTotemCount()
    {
        return maxTotemCount;
    }
}
