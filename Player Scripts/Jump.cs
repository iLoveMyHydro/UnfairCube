using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(LayerMask))]
[RequireComponent(typeof(LayerMask))]
public class Jump : MonoBehaviour
{
    [Header("Body")]
    [SerializeField] private new Rigidbody2D rigidbody;

    [Header("Jump")]
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private float maxJumps = 2f;
    [SerializeField] private float jumpsLeft = 0f;
    [SerializeField] private bool canJump = true;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter = 0f;

    [Header("Jump Buffer")]
    [SerializeField] private float jumpBuffer = 0.2f;
    [SerializeField] private float jumpBufferCounter = 0f;
    [SerializeField] private LayerMask jumpBufferLayer;
    [SerializeField] private Transform jumpBufferCheck;

    private void Awake()
    {
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();

        if (groundCheck == null) groundCheck = GetComponent<Transform>();

        if (jumpBufferCheck == null) jumpBufferCheck = GetComponent<Transform>();
    }

    private void Start()
    {
        jumpsLeft = maxJumps;
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            Landed();
        }
    }

    private void Landed()
    {
        if (IsGrounded())
        {
            jumpsLeft = maxJumps;
        }
    }

    private bool IsGrounded()
    {
        var radius = 0.2f;

        return Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
    }

    private bool CanJumpBuffer()
    {
        var radius = 0.2f;

        return Physics2D.OverlapCircle(jumpBufferCheck.position, radius, jumpBufferLayer);
    }

    private float CoyoteTime()
    {
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        return coyoteTimeCounter;
    }

    private float JumpBuffer()
    {
        if (CanJumpBuffer())
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        return jumpBufferCounter;
    }

    public void JumpAbility(InputAction.CallbackContext context)
    {
        var longerJump = 0.2f;

        var zeroJump = 0f;

        if (context.started)
        {
            if (canJump)
            {
                if (IsGrounded())
                {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
                }
                else
                {
                    if (CoyoteTime() > zeroJump && JumpBuffer() > zeroJump)
                    {
                        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
                        jumpsLeft--;
                    }
                    else
                    {
                        if (jumpsLeft > 0f)
                        {
                            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
                            jumpsLeft--;
                        }
                    }
                }
            }
        }
        if (context.canceled && rigidbody.velocity.y > zeroJump && canJump)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * longerJump);
        }
    }

}
