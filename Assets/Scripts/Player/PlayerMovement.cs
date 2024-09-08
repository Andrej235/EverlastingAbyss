using ModestTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float drag;

    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    [SerializeField] private float fallMultiplier;
    private bool isJumpReady;


    [Header("Ground check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask ground;
    private bool isGrounded;

    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody playerRigidbody;

    [SerializeField] private Transform orientation;
    private MovementState movementState;

    private enum MovementState
    {
        walking,
        sprinting,
        air
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
        isJumpReady = true;
    }

    private void Update()
    {
        // Check if player is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        GetInputs();
        SpeedControl();
        MovementStateHandler();

        // Handle drag
        playerRigidbody.drag = (isGrounded) ? drag : 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        ApplyExtraGravity();
    }

    private void GetInputs()
    {
        // Get player inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && isJumpReady && isGrounded)
        {
            isJumpReady = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovementStateHandler()
    {
        // Mode - Sprinting
        if(isGrounded && Input.GetKey(sprintKey))
        {
            movementState = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        // Mode - Walking
        else if(isGrounded)
        {
            movementState = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        // Mode - Air
        else
        {
            movementState= MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // Move player
        if (isGrounded)
            playerRigidbody.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);
        else
            playerRigidbody.AddForce(10f * moveSpeed * airMultiplier * moveDirection.normalized, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);

        // Limit velocity if needed
        if (!isGrounded)
        {
            if (flatVelocity.magnitude * airMultiplier > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed * airMultiplier;
                playerRigidbody.velocity = new Vector3(limitedVelocity.x, playerRigidbody.velocity.y, limitedVelocity.z);
            }
        }
        else
        {
            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                playerRigidbody.velocity = new Vector3(limitedVelocity.x, playerRigidbody.velocity.y, limitedVelocity.z);
            }
        }
    }

    private void Jump()
    {
        // Reset y velocity
        playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);

        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ApplyExtraGravity()
    {
        // Apply additional gravity when player is falling
        if (!isGrounded && playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.AddForce(fallMultiplier * Physics.gravity.y * Vector3.up, ForceMode.Acceleration);
        }
    }

    private void ResetJump()
    {
        isJumpReady = true;
    }
}
