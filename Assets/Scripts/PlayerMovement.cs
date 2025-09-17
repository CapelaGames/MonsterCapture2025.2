using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public Camera cam;
    
    private Rigidbody rb;
    private Vector2 input;
    private bool isGrounded = false;

    private float jumpSpeed = 300;
    private float moveSpeed = 20;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (cam == null) { cam = Camera.main; }
        if (cam == null) { cam = FindFirstObjectByType<Camera>(); }
    }

    void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed);
        }
    }
    private void FixedUpdate()
    {
        if (isGrounded)
        {
            // Movement relative to the camera
            Vector3 movement = new Vector3(input.x, 0f, input.y);
            movement = cam.transform.TransformDirection(movement);
            movement.y = 0f; // flatten direction parallel to ground
            movement = movement.normalized;

            // Add the force to the player
            rb.AddForce(movement * moveSpeed);
            
            // Movement speed lerping
            Vector3 movementGoal = movement * moveSpeed;
            Vector3 smoothedVelocity = Vector3.Lerp(rb.linearVelocity, movementGoal, Time.deltaTime * 5f);
            rb.linearVelocity = smoothedVelocity;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        Vector3 floorNormal = other.contacts[0].normal.normalized;
        // dot product returns
        // -1 to 1
        // 1 means the vectors are pointing exactly the same direction
        // 0 means they are pointing at a right angle
        // -1 means exact opposite direction
        if (Vector3.Dot(floorNormal, Vector3.up) > 0.5f) 
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isGrounded = false;
    }
}
