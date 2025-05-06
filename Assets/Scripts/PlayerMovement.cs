using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerControls playerControls;

    private Vector2 moveAmount;
    //private Animator animator;
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed, jumpSpeed;
    private int maxJumpCount, currentJumpCount;

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

    private bool isGrounded() {
        // TODO: Figure out grounding

        // TODO: Reset jump count when grounded
        currentJumpCount = maxJumpCount;

        return true;
    }

    private void Jump() {
        if(isGrounded() && currentJumpCount > 0) {
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
