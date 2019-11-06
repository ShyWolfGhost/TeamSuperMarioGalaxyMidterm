using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform moveTarget;

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
    }

    private void FixedUpdate()
    {
        FindUp();

        Gravity();

        Movement();
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
            Debug.Log("Found Up");
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
            Debug.Log("On ground");
            fallTime = 0;
        }
        else
        {
            fallTime += Time.deltaTime;
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
        // Forward movement
        if (forwardTime > 0)
        {
            /* Moves the target to the correct position
             * The X is always 0 because the player doesn't strafe
             * The Y is gravity so it's based on how long the player has been falling
             * The Z is forward so it's based on how long the key has been pressed
             */
            moveTarget.localPosition = new Vector3(0,
                                                   -maxFallSpeed * fallCurve.Evaluate(fallTime / timeToMaxFall),
                                                   maxForwardSpeed * forwardCurve.Evaluate(forwardTime / timeToMaxForward)) * Time.deltaTime;

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
            moveTarget.localPosition = new Vector3(0,
                                                   -maxFallSpeed * fallCurve.Evaluate(fallTime / timeToMaxFall),
                                                   -maxBackwardSpeed * backwardCurve.Evaluate(-forwardTime / timeToMaxBackward)) * Time.deltaTime;

            rb.MovePosition(moveTarget.position);
        }
        else
        {
            if (fallTime > 0)
            {
                moveTarget.localPosition += -up * maxFallSpeed * fallCurve.Evaluate(fallTime / timeToMaxFall) * Time.deltaTime;
                rb.MovePosition(moveTarget.position);
            }

        }
    }
}
