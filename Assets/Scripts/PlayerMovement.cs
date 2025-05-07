using System.Collections.Generic;
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

    private int currentJumpCount;
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
        currentJumpCount = AnimalManager.instance.GetMaxJumpCount();

        GetComponent<SpriteRenderer>().material.color = AnimalManager.instance.GetAnimalColor();
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

    private bool CanMove() {
        return true;
    }

    private bool CanMoveVertical() {
        // Cast circles to the left and right of the player and check if it collides with a wall
        return Physics2D.CircleCast(transform.position, groundColliderRadius, -transform.right, groundColliderCastDistance, groundLayer)
            || Physics2D.CircleCast(transform.position, groundColliderRadius, transform.right, groundColliderCastDistance, groundLayer);
    }

    private void Move() {
        if(CanMove()) {
            //animator.SetFloat("Speed", moveAmount.x);

            // Get the new horizontal movement
            Vector2 newVelocity = rb.linearVelocity;
            newVelocity.x = moveAmount.x * moveSpeed;

            if(AnimalManager.instance.CanWallWalk() && CanMoveVertical()) {
                // If the player can move vertically,
                // get the new vertical movement
                newVelocity.y = moveAmount.y * moveSpeed;
            }

            // Set the player's rigidbody's new velocity
            rb.linearVelocity = newVelocity;
        }
    }

    private bool IsGrounded() {
        // Cast a small circle at the base of the player and check if it collides with the ground
        if(Physics2D.CircleCast(transform.position, groundColliderRadius, -transform.up, groundColliderCastDistance, groundLayer)) {
            // Reset jump count when grounded
            currentJumpCount = AnimalManager.instance.GetMaxJumpCount();
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
