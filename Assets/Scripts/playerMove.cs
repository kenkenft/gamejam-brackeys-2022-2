using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rig;
    public bool isPlayerActive, canJumpAgain = false;

    public int totemID;

    private float playerSpeed = 3f, speedDecayMultiplier = 0.95f, playerJump = 8.0f, jumpVelDecayHigh = 1.4f, jumpVelDecayLow = 1.7f, 
    playerColliderWidth, playerColliderWidthOffset, faceDirection, playerSpeedMax, jumpTierFallReduction = 1f;       
    private BoxCollider2D playerCollider;
    private Vector2 directionAttack = Vector2.right;
    private SpriteRenderer playerSprite;

    public LayerMask groundLayerMask;
    public PlayerChange classPlayerChange;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<BoxCollider2D>(); // Get player collider width for use positioning the rays for the IsGrounded function 
        playerColliderWidth = playerCollider.size[0];
        playerColliderWidthOffset = playerColliderWidth + 0.1f;

        // Spawns totem characters to preset positions per level
        // doorManager = FindObjectOfType<DoorManager>();
        // transform.position = doorManager.GetSpawnPosition();
        playerSprite = GetComponent<SpriteRenderer>();

        playerSpeedMax = playerSpeed; 
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Space))
            Jump();

        if(Input.GetKeyDown(KeyCode.C))
            classPlayerChange.ChangeCharacter(totemID);
        
        VelocityDecay();                // Decays X, and Y velocities over time
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
        // float jump = playerJump * (1 + (1 * unlockedTraits[0,1] * tier1JumpBonus));    // Calculate jump power. Player jumps higher if Tier 1 jump is unlocked

        if(IsGrounded())    // Jump whilst on ground
        {
            rig.velocity = Vector2.up * playerJump;
            canJumpAgain = true; 
            //audioManager.Play("playerJump");
        }
        // else if(canJumpAgain)    // Jump a second time if in at least 2-totem stack
        // {
        //     rig.velocity = Vector2.up * playerJump;
        //     canJumpAgain = false;
        //     //audioManager.Play("playerDoubleJump");
        // }

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
