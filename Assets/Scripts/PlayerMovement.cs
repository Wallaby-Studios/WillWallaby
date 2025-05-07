using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerControls playerControls;

    [SerializeField]
    private float groundColliderRadius, groundColliderCastDistance;
    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float moveSpeed, jumpSpeed;
    private int maxJumpCount, currentJumpCount;

    private Vector2 moveAmount;
    //private Animator animator;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxJumpCount = 2;
        currentJumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        moveAmount = playerControls.GetMove();

        IsGrounded();

        if(playerControls.GetJump()) {
            Jump();
        }
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        //animator.SetFloat("Speed", moveAmount.x);

        Vector2 velocity = rb.linearVelocity;
        velocity.x = moveAmount.x * moveSpeed;
        rb.linearVelocity = velocity;
    }

    private bool IsGrounded() {
        // Cast a small circle at the base of the player and check if it collides with the ground
        if(Physics2D.CircleCast(transform.position, groundColliderRadius, -transform.up, groundColliderCastDistance, groundLayer)) {
            // Reset jump count when grounded
            currentJumpCount = maxJumpCount;
            return true;
        } else {
            return false;
        }
    }

    private void Jump() {
        if(currentJumpCount > 0) {
            //animator.SetTrigger("Jump");

            // Using rb.AddForce() is not helpful here because of double jump.
            // We want the y-vel to be set to the jump force instead of
            // being cancelled out by any negative y-vel from falling.
            Vector2 velocity = rb.linearVelocity;
            velocity.y = jumpSpeed;
            rb.linearVelocity = velocity;

            currentJumpCount--;
        }
    }
}
