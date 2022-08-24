using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemCollision : MonoBehaviour
{
    private StackTotem colClassStackTotem;
    public StackTotem classStackTotem;

    void Start()
    {
        classStackTotem = GetComponentInChildren<StackTotem>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        colClassStackTotem = col.gameObject.GetComponentInChildren<StackTotem>();
        if(col.gameObject.tag == "Totem" && colClassStackTotem.totemState == "inactive")
        {
            // Debug.Log("Collided into Totem: " + colClassPlayerMove.name);
            if(!colClassStackTotem.isTotemRecruited)
            {
                colClassStackTotem.isTotemRecruited = true;
                // colClassStackTotem.GetComponentInParent<PlayerMove>().enabled = true;
            }
            if(!colClassStackTotem.isTotemStacked)
            {
                Debug.Log("col.gameObject: " + col.gameObject.name + " Col Is not Null? " + (col.gameObject != null)+ " classStackTotem Is not Null? " + (classStackTotem != null));
                if(classStackTotem.totemState == "active")
                {
                    classStackTotem.StackTotemInPos(col.gameObject);
                    colClassStackTotem.isTotemStacked = true;
                }
                
            }
            
        }
    }
}
