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
    #region Parameter

    #region Const

    private const string BodyHeaderText = "Body";

    private const string JumpHeaderText = "Jump";

    private const string GroundCheckHeaderText = "Ground Check";

    private const string WallCheckHeaderText = "Wall Check";

    private const string JumpBufferCheckHeaderText = "Jump Buffer";

    private const string CoyoteTimeHeaderText = "Coyote Time";

    private const string WallSlidingHeaderText = "Wall Sliding";

    private const string WallJumpingHeaderText = "Wall Jumping";

    private const string MoveHeaderText = "Move";

    #endregion

    #region Body

    [Header(BodyHeaderText)]
    [SerializeField] private new Rigidbody2D rigidbody;
    private bool isFacingRight = true;
    [SerializeField] private SpriteRenderer spriteRenderer;

    #endregion

    #region Jump

    [Header(JumpHeaderText)]
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private float maxJumps = 2f;
    [SerializeField] private float jumpsLeft = 0f;
    [SerializeField] private bool canJump = true;

    #endregion

    #region Check Stuff

    #region Ground Check

    [Header(GroundCheckHeaderText)]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    #endregion

    #region Wall Check

    [Header(WallCheckHeaderText)]
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private LayerMask wallLayer;

    #endregion

    #region Jump Buffer Check

    [Header(JumpBufferCheckHeaderText)]
    [SerializeField] private float jumpBuffer = 0.2f;
    [SerializeField] private float jumpBufferCounter = 0f;
    [SerializeField] private LayerMask jumpBufferLayer;
    [SerializeField] private Transform jumpBufferCheck;

    #endregion

    #endregion

    #region Coyote Time

    [Header(CoyoteTimeHeaderText)]
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float coyoteTimeCounter = 0f;

    #endregion

    #region Wall Sliding

    [Header(WallSlidingHeaderText)]
    [SerializeField] private float wallSlidingSpeed = 1.5f;
    [field: SerializeField] public bool CanWallSlide { get; set; } = true;
    private bool isWallSliding = true;

    #endregion

    #region Wall Jumping

    [Header(WallJumpingHeaderText)]
    [SerializeField] private float wallJumpingTime = 0.2f;
    [SerializeField] private float wallJumpingDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpingPower = new();
    [field: SerializeField] public bool CanWallJump { get; set; } = true;
    private float wallJumpingCounter = 0;
    private float wallJumpingDirection = 0;
    private bool isWallJumping = false;

    #endregion

    #region Move

    [Header(MoveHeaderText)]
    [SerializeField] private Move move = null;

    #endregion

    #endregion


    #region Awake

    private void Awake()
    {
        if (rigidbody == null) rigidbody = GetComponent<Rigidbody2D>();

        if (groundCheck == null) groundCheck = GetComponent<Transform>();

        if (jumpBufferCheck == null) jumpBufferCheck = GetComponent<Transform>();
    }

    #endregion

    #region Start
    
    /// <summary>
    /// Sets how many often the player can jump (max double jump in this game) 
    /// </summary>
    private void Start()
    {
        jumpsLeft = maxJumps;
    }

    #endregion

    #region FixedUpdate

    /// <summary>
    /// If the player is grounded the Landed() will be started
    /// </summary>
    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            Landed();
        }
    }

    #endregion

    #region Update

    /// <summary>
    /// Starts the flip method if the player change their move Direction
    /// Also starts the WallSlide method
    /// </summary>
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

    #endregion

    #region Flip

    /// <summary>
    /// Flips the Charakter 
    /// </summary>
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    #endregion

    #region Landed

    /// <summary>
    /// If the player isGrounded the jumps will be reseted
    /// So the player can double Jump again
    /// </summary>
    private void Landed()
    {
        if (IsGrounded())
        {
            jumpsLeft = maxJumps;
        }
    }

    #endregion

    #region CheckStuff

    #region IsWalled

    /// <summary>
    /// Looks if the Charakter isWalled
    /// </summary>
    /// <returns></returns>
    private bool IsWalled()
    {
        #region Fields

        var radius = 0.2f;

        #endregion

        if (!spriteRenderer.flipX)
        {
            return Physics2D.OverlapCircle(wallCheckRight.position, radius, wallLayer);
        }
        else
        {
            return Physics2D.OverlapCircle(wallCheckLeft.position, radius, wallLayer);
        }
    }

    #endregion

    #region CanJumpBuffer

    /// <summary>
    /// Checks if the player can jump even if he already jumped 2 times 
    /// </summary>
    /// <returns></returns>
    private bool CanJumpBuffer()
    {
        #region Fields

        var radius = 0.2f;

        #endregion

        return Physics2D.OverlapCircle(jumpBufferCheck.position, radius, jumpBufferLayer);
    }

    #endregion

    #region IsGrounded

    /// <summary>
    /// Checks if the player is Grounded
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        #region Fields

        var radius = 0.2f;

        #endregion

        return Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
    }

    #endregion

    #endregion

    #region Coyote Time

    /// <summary>
    /// If the player is Grounded the Coyote Time will be set 
    /// </summary>
    /// <returns></returns>
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

    #endregion

    #region Jump Buffer

    /// <summary>
    /// If the player can Jump Buffer then the BufferTime will be set
    /// </summary>
    /// <returns></returns>
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

    #endregion

    #region Jump Ability

    /// <summary>
    /// Here are all Jump Abilities 
    /// </summary>
    /// <param name="context"></param>
    public void JumpAbility(InputAction.CallbackContext context)
    {
        #region Fields

        var longerJump = 0.2f;

        var zeroJump = 0f;

        #endregion


        if (context.started)
        {
            if (canJump)
            {
                //if the player is just grounded then he can jump normally
                if (IsGrounded())
                {
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
                }
                else
                { //if he isnt grounded but got the coyote time or the jumpbuffer time then he still can jump
                    if (CoyoteTime() > zeroJump && JumpBuffer() > zeroJump)
                    {
                        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
                        jumpsLeft--;
                    }
                    else
                    {   //if he got jumps left then the player can jump again
                        if (jumpsLeft > zeroJump)
                        {
                            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpPower);
                            jumpsLeft--;
                        }
                    }
                }
            }
        }//if the player presses the spacebar longer - the player can jump higher
        if (context.canceled && rigidbody.velocity.y > zeroJump && canJump)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * longerJump);
        }
    }

    #endregion

    #region Wall Slide

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

    #endregion

    #region Wall Jump

    #region Wall Jump Ability

    /// <summary>
    /// Ability for the Wall JumpAbility
    /// Code für den Wall jump -> https://youtu.be/O6VX6Ro7EtA
    /// Code wurde angepasst dass man die Funktion an un ausschalten kann
    /// 
    /// </summary>
    public void WallJumpAbility(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        DirectionWallJump();

        WallJump();
    }

    #endregion

    #region DirectionWallJump
    /// <summary>
    /// In this method the Direction of the wall jump will be set
    /// if he went to the right - then he will jump to the left
    /// if he went to the left - then he will jump to the right
    /// </summary>
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

    #endregion

    #region WallJump
    
    /// <summary>
    /// if the player can wall jump, got the walljump counter and iswalled then he is able to jump off a wall
    /// </summary>
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

    #endregion

    #region Stop Wall Jump

    /// <summary>
    /// Stops the Wall JumpAbility
    /// </summary>
    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    #endregion

    #endregion
}
