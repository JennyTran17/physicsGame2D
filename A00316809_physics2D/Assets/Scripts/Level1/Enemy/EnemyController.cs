using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 3f;
    public float jumpForce = 6f;  // Increased jump force for better jumping
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    //private bool isGrounded;
    private bool shouldJump;
    private float direction;

    GroundCheck groundCheck;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<GroundCheck>();
    }

    private void Update()
    {
        

        // Calculate movement direction (-1 left, 1 right)
        direction = player.position.x > transform.position.x ? 1f : -1f;

        // Check for ground in front
        Vector3 forwardCheckPos = transform.position + new Vector3(direction * 0.6f, 0, 0);
        RaycastHit2D groundInFront = Physics2D.Raycast(forwardCheckPos, Vector2.down, 1.5f, groundLayer);

        // Check for gap ahead
        RaycastHit2D gapAhead = Physics2D.Raycast(forwardCheckPos + new Vector3(0.5f * direction, 0, 0), Vector2.down, 2f, groundLayer);

        // Check if the player is above
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 7f, LayerMask.GetMask("Player"));

        // Check if there is a platform above
        RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 4f, groundLayer);

        // If there's no ground in front and a gap ahead, jump
        if (groundCheck.isGrounded && !groundInFront.collider && !gapAhead.collider)
        {
            shouldJump = true;
        }
        // Jump if the player is above and there's a platform
        else if (isPlayerAbove && platformAbove.collider)
        {
            shouldJump = true;
        }

        Debug.Log("Direction: " + direction);
        Debug.Log("Velocity: " + rb.velocity);
        Debug.Log("IsGrounded: " + groundCheck.isGrounded);
    }

    private void FixedUpdate()
    {
        if (groundCheck.isGrounded)
        {
            // Move the enemy towards the player
            rb.velocity = new Vector2(direction * chaseSpeed, rb.velocity.y);
        }

        // Jump if needed
        if (groundCheck.isGrounded && shouldJump)
        {
            shouldJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Jump upward
        }
    }
}
