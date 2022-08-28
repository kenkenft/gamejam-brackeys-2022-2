using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTotem : MonoBehaviour
{
    public int totemID;
    public string totemState;
    public bool isTotemStacked, isTotemRecruited;

    private PlayerChange classPlayerChange;
    private int maxTotemCount, countStackedTotems = 0, listIndex;
    private List<GameObject> listStackedTotems = new List<GameObject>{};
    private List<GameObject> listStackPos = new List<GameObject>{};
    private GameObject targetObject;
    private StackTotem targetClassStackTotem, donorStack, recipientStack;
    private Vector3 posVector = new Vector3();
    private Rigidbody2D targetRig;
    private float offsetTotemHeight;

    private AudioManager audioManager;
    void Start()
    {
        SetUpPositionMarkers();
        audioManager = FindObjectOfType<AudioManager>();
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
        totem.transform.position = listStackPos[countStackedTotems].transform.position;
        totem.transform.parent = listStackPos[countStackedTotems].transform;

        targetRig = totem.GetComponent<Rigidbody2D>();
        targetRig.velocity = new Vector3(0,0,0);
        targetRig.isKinematic = true;

        targetClassStackTotem = totem.GetComponentInChildren<StackTotem>();
       
        listStackedTotems.Add(totem);
        totem.GetComponentInChildren<StackTotem>().isTotemStacked = true; 
        countStackedTotems++;

        // ToDoCheck for nested totems
        if(targetClassStackTotem.GetCountStackedTotems() > 0)
            TransferListToTargetTotem(targetClassStackTotem, this);

        if(totemState == "active")
            audioManager.Play("totemStack");
    }

    public int GetCountStackedTotems()
    {
        return countStackedTotems;
    }

    public GameObject GetNextTotem(string positionInStack)
    {
        listIndex = positionInStack == "bottom" ? 0 : countStackedTotems-1;
        targetObject = listStackedTotems[listIndex]; // Element 0 should be the "bottom" of the stack (excluding the active totem)
        targetObject.transform.parent = classPlayerChange.gameObject.transform;
        targetClassStackTotem = targetObject.GetComponentInChildren<StackTotem>();
        RemoveTotemFromStackList(listIndex, this); 
        

        if(positionInStack == "bottom")
            TransferListToTargetTotem(this, targetClassStackTotem);

        return targetObject;
    }

    void TransferListToTargetTotem(StackTotem donorStack, StackTotem recipientStack)
    {
        while(donorStack.countStackedTotems > 0)
        {
            recipientStack.StackTotemInPos(donorStack.listStackedTotems[0]);
            RemoveTotemFromStackList(0, donorStack);
        }
    }

    void RemoveTotemFromStackList(int index, StackTotem donorStack)
    {
        donorStack.isTotemStacked = false;
        donorStack.listStackedTotems.RemoveAt(index);
        donorStack.countStackedTotems--;
    }

    
}
