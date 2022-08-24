using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTotem : MonoBehaviour
{
    public int totemID;
    public string totemState;
    public bool isTotemStacked, isTotemRecruited;

    private PlayerChange classPlayerChange;
    private int maxTotemCount, countStackedTotems = 0;
    private List<GameObject> listStackedTotems = new List<GameObject>{};
    private List<GameObject> listStackPos = new List<GameObject>{};
    private GameObject targetObject;
    private StackTotem targetClassStackTotem;
    private Vector3 posVector = new Vector3();
    private float offsetTotemHeight;
    void Start()
    {
        SetUpPositionMarkers();
    }

    void SetUpPositionMarkers()
    {
        classPlayerChange = GetComponentInParent<PlayerChange>();
        maxTotemCount = classPlayerChange.GetMaxTotemCount();
        offsetTotemHeight = 1.05f * GetComponentInParent<BoxCollider2D>().size[1];

        float startX = transform.position.x;    
        float startY = transform.position.y;
        for(int i = 0; i < maxTotemCount-1; i++)
        {
            targetObject = new GameObject();
            targetObject.transform.parent = transform;
            targetObject.gameObject.name = "position " + i;

            posVector[0] = startX;
            posVector[1] = startY + ((i + 1f) * offsetTotemHeight);
            posVector[2] = 0f;
            targetObject.transform.position = posVector;

            listStackPos.Add(targetObject);
        }
    }

    public void StackTotemInPos(GameObject totem)
    {
        totem.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
        totem.GetComponent<Rigidbody2D>().isKinematic = true;
        totem.transform.position = listStackPos[countStackedTotems].transform.position;
        totem.transform.parent = listStackPos[countStackedTotems].transform;
        listStackedTotems.Add(totem);
        countStackedTotems++;
    }

    public int GetCountStackedTotems()
    {
        return countStackedTotems;
    }

    public GameObject GetNextTotem()
    {
        targetObject = listStackedTotems[0]; // Element 0 should be the "bottom" of the stack (excluding the active totem)
        listStackedTotems.RemoveAt(0);
        countStackedTotems--;
        targetObject.transform.parent = classPlayerChange.gameObject.transform;
        targetClassStackTotem = targetObject.GetComponentInChildren<StackTotem>();
        // if(listStackedTotems.Count > 0)
        // {
        //     targetClassStackTotem = targetObject.GetComponentInChildren<StackTotem>();
        // }
        classPlayerChange.ChangeToTargetTotem(this, targetClassStackTotem);
        // MonoBehaviour thisComponent = this;
        return targetObject;
    }
}
