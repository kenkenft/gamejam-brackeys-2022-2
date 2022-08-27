using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerMove classPlayerMove;

    public PlayerChange classPlayerChange;
    public PlayerVelocityDecay classPlayerVelocityDecay;
    public StackTotem classStackTotem;
    public ThrowTotem classThrowTotem;
    private Vector2 directionAttack;
    private UIManagerStage classUIManagerStage;
    private Rigidbody2D rig;


    void Start()
    {
        classPlayerMove = GetComponent<PlayerMove>();
        classStackTotem = GetComponentInChildren<StackTotem>();
        classThrowTotem = GetComponent<ThrowTotem>();
        classUIManagerStage = FindObjectOfType<UIManagerStage>();
        classPlayerChange = GetComponentInParent<PlayerChange>();
        classPlayerVelocityDecay = GetComponentInParent<PlayerVelocityDecay>();
        rig = GetComponent<Rigidbody2D>();
        // classPlayerMove.SetRigidbody();
        // classPlayerVelocityDecay.SetRigidbody();

    }

    void Update()
    {
        Debug.Log(gameObject.name + " enabled: " + classPlayerMove.enabled);
        if(classPlayerMove.enabled)
        {
            // directionAttack = classPlayerMove.Move();

            // if(Input.GetKeyDown(KeyCode.Space))
            //     classPlayerMove.Jump(classPlayerChange, classStackTotem);

            if(Input.GetKeyDown(KeyCode.K) && classStackTotem.GetCountStackedTotems() > 0)
                classThrowTotem.SetUpThrow(directionAttack);

            if(Input.GetKeyDown(KeyCode.J))
                classPlayerChange.ChangeCharacter();

            if(Input.GetKeyDown(KeyCode.P))
                classUIManagerStage.SetPauseOverlay();
        }
        
        // classPlayerVelocityDecay.VelocityDecay();                // Decays X, and Y velocities over time

        
    }
}
