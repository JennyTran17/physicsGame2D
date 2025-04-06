using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f, jumpPower = 10f;
    private bool isGrounded;
    private float horizontalInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 currentNormal = Vector2.up; // Stores the latest surface normal
    private float stickForce = 8f; // Reduced for smoother balancing
    private float rotationLerpSpeed = 10f; // Controls smooth rotation adjustments


    [SerializeField] public GameObject bullet;
    public Transform shootPos;
    public GameObject gunPivot;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            animator.SetBool("walk", true);
            spriteRenderer.flipX = horizontalInput > 0; // Keep sprite flipping correct
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = currentNormal * jumpPower;
        }

       
    }

    private void FixedUpdate()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, LayerMask.GetMask("Ground"));

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                isGrounded = true;
                break;
            }
        }

        if (!isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Find the best normal for smoother movement
            currentNormal = Vector2.zero;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                currentNormal += contact.normal;
            }
            currentNormal.Normalize();

            // Calculate tangent movement along the surface
            Vector2 tangent = new Vector2(currentNormal.y, -currentNormal.x);
            Vector2 moveDirection = horizontalInput * tangent * moveSpeed;

            rb.velocity = moveDirection;

            // Stick the player to the ground more naturally
            rb.AddForce(-currentNormal * stickForce, ForceMode2D.Force);

            // **SMOOTH ROTATION FIX**
            float targetAngle = Mathf.Atan2(currentNormal.y, currentNormal.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), Time.deltaTime * rotationLerpSpeed);
        }
    }

    
}
