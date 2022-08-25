using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTotem : MonoBehaviour
{
    public StackTotem classStackTotem;
    
    void Start()
    {
        classStackTotem = GetComponentInChildren<StackTotem>();
    }


    public void TossTopTotem()
    {
        Debug.Log("TossTopTotem called!");
    }
}
