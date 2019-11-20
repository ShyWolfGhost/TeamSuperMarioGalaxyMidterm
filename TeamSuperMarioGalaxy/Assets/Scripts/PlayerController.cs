using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State { NORMAL, DISABLED, HURT, SPINNING };
    public State state;

    public Transform moveTarget;

    [Header("Jump")]
    public bool onGround;
    public float groundCheckDist;
    public float maxJumpTime;
    public float jumpTimer;
    public enum JumpState { ON_GROUND, JUMPING, FALLING };
    public JumpState jumpState;
    public AnimationCurve jumpCurve;

    [Header("Down Raycasting")]
    public Vector3 raycastOriginOffset;
    public float raycastDist;
    public Vector3 up;

    [Header("Gravity")]
    public float fallTime;
    public float timeToMaxFall;
    public float maxFallSpeed;
    public AnimationCurve fallCurve;
    public float gravityCheckDist;

    [Header("Forward Movement Values")]
    public float maxForwardSpeed;
    public float timeToMaxForward;
    public AnimationCurve forwardCurve;
    [Header("Backward Movement Values")]
    public float maxBackwardSpeed;
    public float timeToMaxBackward;
    public AnimationCurve backwardCurve;
    [Header("Rotation Values")]
    public float rotationSpeed;
    [Header("Other Movement Values")]
    public float directionChangeMultiplier;
    public float movementDecayMultiplier;

    private Rigidbody rb;
    private float forwardTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ForwardInputs();

        TurningInputs();

        JumpInput();
    }

    private void FixedUpdate()
    {
        FindUp();

        Gravity();

        Movement();
    }

    void VertMove()
    {
        // Reset all transform data
        moveTarget.localPosition = Vector3.zero;
        moveTarget.localRotation = Quaternion.identity;
        if (jumpState == JumpState.JUMPING)
        {
            float movement = jumpCurve.Evaluate(jumpTimer / maxJumpTime) * Time.deltaTime;

            moveTarget.Translate(0, movement, 0);

            rb.MovePosition(moveTarget.position);
        }
        else if(jumpState == JumpState.FALLING)
        {
            float movement = fallCurve.Evaluate(fallTime / timeToMaxFall) * -maxFallSpeed * Time.deltaTime;

            moveTarget.Translate(0, movement, 0);

            rb.MovePosition(moveTarget.position);
        }
    }

    /// <summary>
    /// Handles the inputs for jumping.
    /// If you press space while on the ground you'll start jumping.
    /// </summary>
    void JumpInput()
    {
        // If you're on the ground and press the spacebar you start jumping
        if(jumpState == JumpState.ON_GROUND && Input.GetKeyDown(KeyCode.Space))
        {
            jumpTimer = 0f;
            jumpState = JumpState.JUMPING;
        }
        // If at any point you are jumping and stop holding space you start falling
        else if(jumpState == JumpState.JUMPING && !Input.GetKey(KeyCode.Space))
        {
            jumpState = JumpState.FALLING;
            fallTime = 0f;
        }
        // If you are jumping for too long you stop jumping
        else if(jumpState == JumpState.JUMPING && jumpTimer > maxJumpTime)
        {
            jumpState = JumpState.FALLING;
            fallTime = 0f;
        }
        // If you're still jumping just increase the timer;
        else if(jumpState == JumpState.JUMPING && jumpTimer < maxJumpTime)
        {
            jumpTimer += Time.deltaTime;
        }
        else if(!onGround && jumpState == JumpState.ON_GROUND)
        {
            jumpState = JumpState.FALLING;
            fallTime = 0f;
        }
        else if(onGround)
        {
            jumpState = JumpState.ON_GROUND;
        }
    }

    /// <summary>
    /// Determines the direction of up
    /// </summary>
    private void FindUp()
    {
        Ray ray = new Ray(transform.position , -up * raycastDist);
        // Debug.DrawRay(r.origin,r.direction);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, raycastDist))
        {
            if(hit.collider.tag == "Ground")
                up = hit.normal;
        }

        up = up.normalized;

        Vector3 r = transform.TransformVector(Vector3.right);

        r = r.normalized;

        Vector3 f = Vector3.Cross(r, up);

        f = f.normalized;

        Debug.DrawRay(transform.position, f, Color.blue);
        Debug.DrawRay(transform.position, up, Color.green);
        Debug.DrawRay(transform.position, r, Color.red);

        transform.rotation = Quaternion.LookRotation(f, up);
    }

    /// <summary>
    /// Checks if the player is on the ground and records how long they have
    /// been off the ground
    /// </summary>
    private void Gravity()
    {
        Ray ray = new Ray(transform.position, -up);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, gravityCheckDist))
        {
            fallTime = 0;
        }
        else if(jumpState != JumpState.JUMPING)
        {
            fallTime += Time.deltaTime;
        }

        if(Physics.Raycast(ray, out hit, groundCheckDist))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    /// <summary>
    /// Checks inputs for forward movement and sets values to allow the player
    /// to move
    /// </summary>
    private void ForwardInputs()
    {
        // Forward movement
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            // Increase the time the player has been forward
            forwardTime += forwardTime < 0 ? Time.deltaTime * directionChangeMultiplier : Time.deltaTime;
            // Max out forward time
            if (forwardTime > timeToMaxForward)
                forwardTime = timeToMaxForward;
        }
        // Backward movement
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            forwardTime -= forwardTime > 0 ? Time.deltaTime * directionChangeMultiplier : Time.deltaTime;

            if (forwardTime < -timeToMaxBackward)
                forwardTime = -timeToMaxBackward;
        }
        // If no input is being held for forward movement the player slows and stops
        else
        {
            // If the player was moving forward
            if (forwardTime > 0)
            {
                // We decrease their forward time
                forwardTime -= Time.deltaTime * movementDecayMultiplier;
                // If we passed 0 it just stops
                if (forwardTime < 0)
                    forwardTime = 0;
            }
            // If the player was moving backward
            else if (forwardTime < 0)
            {
                forwardTime += Time.deltaTime * movementDecayMultiplier;
                if (forwardTime > 0)
                    forwardTime = 0;
            }
        }
    }

    /// <summary>
    /// Checks inputs for turning
    /// </summary>
    private void TurningInputs()
    {
        // Turning
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        }
    }

    /// <summary>
    /// Controls the physics movement of the player.
    /// This includes their movement forward, backwards, and downwards from
    /// gravity.
    /// 
    /// </summary>
    private void Movement()
    {
        VertMove();

        // Forward movement
        if (forwardTime > 0)
        {
            /* Moves the target to the correct position
             * The X is always 0 because the player doesn't strafe
             * The Y is gravity so it's based on how long the player has been falling
             * The Z is forward so it's based on how long the key has been pressed
             */
            moveTarget.Translate(0,0, maxForwardSpeed * forwardCurve.Evaluate(forwardTime / timeToMaxForward) * Time.deltaTime );
            rb.MovePosition(moveTarget.position);
        }
        // Backward movement
        else if (forwardTime < 0)
        {
            /* Moves the target to the correct position
             * The X is always 0 because the player doesn't strafe
             * The Y is gravity so it's based on how long the player has been falling
             * The Z is forward so it's based on how long the key has been pressed
             */
            moveTarget.Translate(0,0,-maxBackwardSpeed * backwardCurve.Evaluate(-forwardTime / timeToMaxBackward) * Time.deltaTime);
            
            rb.MovePosition(moveTarget.position);
        }
    }
}
