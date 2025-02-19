using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f, jumpPower = 10f;
    private bool isGrounded;
    private float horizontalInput;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    //private void FixedUpdate()
    //{
    //    //RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 0.5f, LayerMask.GetMask("Ground"));

    //    //if (hit.collider != null) // Ensure the player is on the surface
    //    //{
    //    //    Vector2 surfaceNormal = hit.normal;
    //    //    Vector2 tangent = new Vector2(surfaceNormal.y, -surfaceNormal.x); // Correct orientation
    //    //    Vector2 moveDirection = horizontalInput * tangent;

    //    //    isGrounded = true;

    //    //    // Using velocity for controlled movement speed
    //    //    rb.velocity = moveDirection * moveSpeed; // No ForceMode.Impulse, just set velocity

    //    //    // Stick the player to the surface by applying a slight force inward
    //    //    float stickForce = 5f; // Less aggressive sticking force
    //    //    rb.AddForce(-surfaceNormal * stickForce, ForceMode2D.Force);

    //    //    // Debug visualization
    //    //    Debug.DrawRay(transform.position, surfaceNormal * 2, Color.red);
    //    //    Debug.DrawRay(transform.position, tangent * 2, Color.green);
    //    //}
    //    //else
    //    //{
    //    //    isGrounded = false;
    //    //    // Reduce speed if not grounded to simulate drag
    //    //    rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y); // Gradually slow down horizontal movement
    //    //}
    //    

    //}




    private void FixedUpdate()
    {
        
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Ground"))
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
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector2 surfaceNormal = collision.GetContact(i).normal;
                Vector2 tangent = new Vector2(surfaceNormal.y, -surfaceNormal.x);
                Vector2 moveDirection = horizontalInput * tangent * moveSpeed;

                rb.velocity = moveDirection;

                // Stick the player to the surface by applying a slight force inward
                float stickForce = 5f;
                rb.AddForce(-surfaceNormal * stickForce, ForceMode2D.Force);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Ground"))
        {
            rb.drag = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Ground"))
        {
            rb.drag = 0.2f;
        }
    }
}