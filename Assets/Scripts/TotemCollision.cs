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
            if(!colClassStackTotem.isTotemRecruited)
                colClassStackTotem.isTotemRecruited = true;

            if(!colClassStackTotem.isTotemStacked)
            {
                if(classStackTotem.totemState == "active")
                {
                    classStackTotem.StackTotemInPos(col.gameObject);
                    colClassStackTotem.isTotemStacked = true;
                } 
            }
        }
    }
}
