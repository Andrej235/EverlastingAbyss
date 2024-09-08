using ModestTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;
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

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody playerRigidbody;

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
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            playerRigidbody.velocity = new Vector3(limitedVelocity.x, playerRigidbody.velocity.y, limitedVelocity.z);
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
