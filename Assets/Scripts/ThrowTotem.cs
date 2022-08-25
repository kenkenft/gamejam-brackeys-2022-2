using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowTotem : MonoBehaviour
{
    public StackTotem classStackTotem;
    private GameObject targetTotem;
    private float throwPower = 6f;
    private Vector2 throwVector = new Vector2(1f,0.5f);
    private Rigidbody2D rig;
    
    void Start()
    {
        classStackTotem = GetComponentInChildren<StackTotem>();
    }


    public void SetUpThrow(Vector2 directionAttack)
    {
        targetTotem = classStackTotem.GetNextTotem("Top");
        throwVector[0] = directionAttack[0]; // Throw Leftwards if -1, else assume throw towards Rightwards
        TossTopTotem();
    }

    public void TossTopTotem()
    {
        targetTotem.GetComponentInChildren<StackTotem>().isTotemStacked = false;
        rig = targetTotem.GetComponent<Rigidbody2D>();
        rig.isKinematic = false;
        rig.velocity = throwVector * throwPower;
    }
}
