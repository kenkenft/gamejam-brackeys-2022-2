using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTotem : MonoBehaviour
{
    private PlayerChange classPlayerChange;
    private int maxTotemCount;
    private List<GameObject> listStackedTotems;
    private GameObject posStackedTotem;
    private Vector3 posVector = new Vector3();
    // Start is called before the first frame update
    private float offsetTotemHeight;   //ToDo Set offsetTotemHeight to be whatever the sprite height is
    void Start()
    {
        classPlayerChange = GetComponentInParent<PlayerChange>();
        maxTotemCount = classPlayerChange.GetMaxTotemCount();
        offsetTotemHeight = 1.02f * GetComponentInParent<BoxCollider2D>().size[1];

        float startX = transform.position.x;    
        float startY = transform.position.y;
        for(int i = 0; i < maxTotemCount-1; i++)
        {

            posStackedTotem = new GameObject();
            posStackedTotem.transform.parent = transform;
            posStackedTotem.gameObject.name = "position " + i;

            posVector[0] = startX;
            posVector[1] = startY + ((i + 1f) * offsetTotemHeight);
            posVector[2] = 0f;
            posStackedTotem.transform.position = posVector;

        }
        
    }

    
}
