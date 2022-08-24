using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTotem : MonoBehaviour
{
    private PlayerChange classPlayerChange;
    private int maxTotemCount, countStackedTotems = 0;
    private List<GameObject> listStackedTotems = new List<GameObject>{};
    private List<GameObject> listStackPos = new List<GameObject>{};
    private GameObject emptyObject;
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
            emptyObject = new GameObject();
            emptyObject.transform.parent = transform;
            emptyObject.gameObject.name = "position " + i;

            posVector[0] = startX;
            posVector[1] = startY + ((i + 1f) * offsetTotemHeight);
            posVector[2] = 0f;
            emptyObject.transform.position = posVector;

            listStackPos.Add(emptyObject);
        }
    }

    public void StackTotemInPos(GameObject totem)
    {
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
        emptyObject = listStackedTotems[0]; // Element 0 should be the "bottom" of the stack (excluding the active totem)

        return emptyObject;
    }
}
