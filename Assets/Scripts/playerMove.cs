using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    // private bool canJumpAgain = false;
    
    private float playerSpeed = 3f, speedDecayMultiplier = 0.95f, playerJump = 5.5f, jumpVelDecayHigh = 1.4f, jumpVelDecayLow = 1.7f, 
    playerColliderWidth, playerColliderWidthOffset, faceDirection, playerSpeedMax, jumpTierFallReduction = 1f;       
    private Rigidbody2D rig;
    private BoxCollider2D playerCollider;
    private Vector2 directionAttack = Vector2.right;
    private SpriteRenderer playerSprite;
    public LayerMask groundLayerMask;
    public PlayerChange classPlayerChange;
    public StackTotem classStackTotem;
    public ThrowTotem classThrowTotem;
    private GameObject targetTotem; 

    private UIManagerStage classUIManagerStage;
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<BoxCollider2D>(); // Get player collider width for use positioning the rays for the IsGrounded function 
        playerColliderWidth = playerCollider.size[0];
        playerColliderWidthOffset = playerColliderWidth + 0.1f;
        playerSprite = GetComponent<SpriteRenderer>();

        playerSpeedMax = playerSpeed; 
        classPlayerChange = GetComponentInParent<PlayerChange>();
        groundLayerMask = LayerMask.GetMask("Ground");
        classStackTotem = GetComponentInChildren<StackTotem>();
        classThrowTotem = GetComponent<ThrowTotem>();
        classUIManagerStage = FindObjectOfType<UIManagerStage>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Space))
            Jump();

        if(Input.GetKeyDown(KeyCode.K) && classStackTotem.GetCountStackedTotems() > 0)
                classThrowTotem.SetUpThrow(directionAttack);

        if(Input.GetKeyDown(KeyCode.J))
            classPlayerChange.ChangeCharacter();
        
        VelocityDecay();                // Decays X, and Y velocities over time

        if(Input.GetKeyDown(KeyCode.P))
            classUIManagerStage.SetPauseOverlay();
    }

    void Move()
    {
        faceDirection = Input.GetAxisRaw("Horizontal");
        if(faceDirection < 0f)
            directionAttack =  Vector2.left;        
        else if(faceDirection > 0f)
            directionAttack =  Vector2.right;
        // if exactly 0, keep facing in the current direction

        float moveAmount;
        if(faceDirection == 0)
        {
            float x = rig.velocity.x;
            Vector3 mask = rig.velocity;    
            mask.x = x;
            rig.velocity = mask;
        }
        else
        {
            moveAmount = faceDirection * playerSpeed;
            ClampSpeed(moveAmount, playerSpeedMax);
        }
    }

    void Jump()
    {
        if(IsGrounded())    // Jump whilst on ground
        {
            rig.velocity = Vector2.up * playerJump;
            audioManager.Play("playerJump");
        }
        else if(classStackTotem.GetCountStackedTotems() > 0)    // Jump a second time if in at least 2-totem stack
        {
            targetTotem = classStackTotem.GetNextTotem("bottom");
            targetTotem.GetComponent<Rigidbody2D>().velocity = Vector2.up * playerJump * 1.2f;
            classPlayerChange.ChangeToTargetTotem(classStackTotem, targetTotem.GetComponentInChildren<StackTotem>());
            audioManager.Play("playerDoubleJump");
        }

    }
    bool IsGrounded ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayerMask);
        if (hit.collider != null) 
            return true;
        return false;
    }//// End of IsGrounded()

    void ClampSpeed(float moveAmount, float speedLimit)
    {
            Vector2 mask = new Vector2(moveAmount, 0); 
            rig.AddForce(mask, ForceMode2D.Impulse);
            mask = rig.velocity;
            mask.x = Mathf.Clamp( rig.velocity.x, -speedLimit, speedLimit);             // Limit player's velocity
            mask.y = Mathf.Clamp( rig.velocity.y, -20f, 40f);
            rig.velocity = mask;
    }

    void VelocityDecay()
    {
        float x = rig.velocity.x;
        Vector3 mask = rig.velocity;

        if( x !=0.0f )              // Gradually reduce x-axis velocity (Unless being boosted by ramps)
        {
            mask.x *= speedDecayMultiplier;
            rig.velocity = mask;
        }

        if(rig.velocity.y < 0)              // Reduces floatiness of jumps
            rig.velocity += Vector2.up * Physics2D.gravity.y * jumpVelDecayHigh * jumpTierFallReduction * Time.deltaTime;    
        else if(rig.velocity.y > 0 && !Input.GetButton("Jump"))     // For low jumps
            rig.velocity += Vector2.up * Physics2D.gravity.y * jumpVelDecayLow * jumpTierFallReduction * Time.deltaTime;                // Start increasing downward velocity once player lets go of jump input
        
    }//// End of VelocityDecay()
}
