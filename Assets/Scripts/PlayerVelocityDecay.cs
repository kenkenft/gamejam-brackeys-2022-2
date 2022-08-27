using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocityDecay : MonoBehaviour
{
    private float speedDecayMultiplier = 0.95f, jumpVelDecayHigh = 1.4f, jumpVelDecayLow = 1.7f;       
    
    private Rigidbody2D rig;
    
    public void VelocityDecay()
    {
        float x = rig.velocity.x;
        Vector3 mask = rig.velocity;

        if( x !=0.0f )              // Gradually reduce x-axis velocity (Unless being boosted by ramps)
        {
            mask.x *= speedDecayMultiplier;
            rig.velocity = mask;
        }

        if(rig.velocity.y < 0)              // Reduces floatiness of jumps
            rig.velocity += Vector2.up * Physics2D.gravity.y * jumpVelDecayHigh * Time.deltaTime;    
        else if(rig.velocity.y > 0 && !Input.GetButton("Jump"))     // For low jumps
            rig.velocity += Vector2.up * Physics2D.gravity.y * jumpVelDecayLow  * Time.deltaTime;                // Start increasing downward velocity once player lets go of jump input
        
    }//// End of VelocityDecay()

    public void SetRigidbody(Rigidbody2D targetRig)
    {
        rig = targetRig;
    }
}
