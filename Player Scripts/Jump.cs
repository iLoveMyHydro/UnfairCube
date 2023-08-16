using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(LayerMask))]
[RequireComponent(typeof(LayerMask))]
[RequireComponent(typeof(Move))]
public class Jump : MonoBehaviour
{
    [Header("Body")]
    [SerializeField] private new Rigidbody2D rigidbody;
    private bool isFacingRight = true;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Jump")]
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private float maxJumps = 2f;
    [SerializeField] private float jumpsLeft = 0f;
    [SerializeField] private bool canJump = true;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    [Header("Wall Check")]
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private LayerMask wallLayer;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter = 0f;

    [Header("Jump Buffer")]
    [SerializeField] private float jumpBuffer = 0.2f;
    [SerializeField] private float jumpBufferCounter = 0f;
    [SerializeField] private LayerMask jumpBufferLayer;
    [SerializeField] private Transform jumpBufferCheck;

    [Header("Wall Sliding")]
    [SerializeField] private float wallSlidingSpeed = 1.5f;
    [field: SerializeField] public bool CanWallSlide { get; set; } = true;
    private bool isWallSliding = true;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpingTime = 0.2f;
    [SerializeField] private float wallJumpingDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpingPower = new();
    [field: SerializeField] public bool CanWallJump { get; set; } = true;
    private float wallJumpingCounter = 0;
    private float wallJumpingDirection = 0;
    private bool isWallJumping = false;

    [Header("Move")]
    [SerializeField] private Move move = null;

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

    private void Update()
    {
        var speedZero = 0;

        if (!isFacingRight && move.MoveSpeed > speedZero && !isWallJumping)
        {
            Flip();
        }
        else if (isFacingRight && move.MoveSpeed < speedZero && !isWallJumping)
        {
            Flip();
        }

        WallSlide();
    }

    /// <summary>
    /// Flips the Charakter 
    /// </summary>
    private void Flip()
    {

        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
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

    /// <summary>
    /// Looks if the Charakter isWalled
    /// </summary>
    /// <returns></returns>
    private bool IsWalled()
    {
        var radius = 0.2f;

        if (!spriteRenderer.flipX)
        {
            return Physics2D.OverlapCircle(wallCheckRight.position, radius, wallLayer);
        }
        else
        {
            return Physics2D.OverlapCircle(wallCheckLeft.position, radius, wallLayer);
        }
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

    /// <summary>
    /// Ability for the Wall Slide -> Looks if the Charakter is on a wall 
    /// Is not grounded 
    /// the x Achse is not = 0f
    /// and if the player selected that he can wall slide
    /// 
    /// Code für den Wallslide -> https://youtu.be/O6VX6Ro7EtA
    /// Code wurde dahingehend angepasst dass man die Funktion an und ausschalten kann 
    /// </summary>
    private void WallSlide()
    {
        #region Fields

        var speedZero = 0f;

        #endregion

        if (IsWalled() && !IsGrounded() && move.MoveSpeed != speedZero && CanWallSlide)
        {
            isWallSliding = true;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, Mathf.Clamp(rigidbody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }

        if (IsWalled() && !IsGrounded() && move.MoveSpeed != speedZero && !CanWallSlide)
        {
            isWallSliding = true;
        }
    }

    /// <summary>
    /// Ability for the Wall JumpAbility
    /// Code für den Wall jump -> https://youtu.be/O6VX6Ro7EtA
    /// Code wurde angepasst dass man die Funktion an un ausschalten kann
    /// 
    /// </summary>
    public void WallJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        DirectionWallJump();

        WallJump();
    }

    private void DirectionWallJump()
    {
        #region Fields

        var rightDirection = 1f;

        var leftDirection = -1f;

        #endregion

        if (isWallSliding)
        {
            isWallJumping = false;
            if (spriteRenderer.flipX)
            {
                wallJumpingDirection = rightDirection;
                move.MoveSpeed *= -rightDirection;
            }
            else
            {
                wallJumpingDirection = leftDirection;
                move.MoveSpeed *= leftDirection;
            }
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
    }

    private void WallJump()
    {
        #region Fields

        var noWallJumpLeft = 0f;

        #endregion

        if (CanWallJump)
        {
            if (wallJumpingCounter > noWallJumpLeft && IsWalled())
            {
                isWallJumping = true;


                rigidbody.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
                wallJumpingCounter = noWallJumpLeft;

                Flip();



                Invoke(nameof(StopWallJumping), wallJumpingDuration);
            }
        }
    }

    /// <summary>
    /// Stops the Wall JumpAbility
    /// </summary>
    private void StopWallJumping()
    {
        isWallJumping = false;
    }

}
