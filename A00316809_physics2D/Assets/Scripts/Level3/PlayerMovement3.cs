using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalInput;

    //[SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpPower = 15f;

    private float currentSpeed = 0f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float acceleration = 30f;
    [SerializeField] private float deceleration = 40f;
    [SerializeField] private float swingForce = 10f;

    private Animator animator;
   

    private bool top;
    private bool facingRight = true;
    private bool isGrappling = false;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
       
    }

    private void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            animator.SetBool("walk", true);
            if(facingRight == false && horizontalInput > 0)
            {
                Flip();
            }
            else if(facingRight == true && horizontalInput < 0) 
            {
                Flip();
            }
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if(Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        ChangeGravity();
    }

    private void FixedUpdate()
    {
       // rb.velocity = new Vector2 (horizontalInput * moveSpeed, rb.velocity.y);

        ApplyMovement();
    }

    void ApplyMovement()
    {
        if (isGrappling)
        {
            // Add force instead of setting velocity to allow momentum and swinging
            rb.AddForce(new Vector2(horizontalInput * swingForce, 0), ForceMode2D.Force);
        }
        else
        {
            // Acceleration
            if (horizontalInput != 0)
            {
                currentSpeed += acceleration * Time.fixedDeltaTime;
                currentSpeed = Mathf.Min(currentSpeed, maxSpeed);
            }
            // Deceleration when no input
            else
            {
                currentSpeed -= deceleration * Time.fixedDeltaTime;
                currentSpeed = Mathf.Max(currentSpeed, 0); // Prevent negative speed
            }

            // Apply the movement speed
            rb.velocity = new Vector2(horizontalInput * currentSpeed, rb.velocity.y);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void ChangeGravity()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            rb.gravityScale *= -1;
            Rotation();
        }
        
    }

    void Rotation()
    {
        if(top == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        facingRight = !facingRight;
        top = !top;
        jumpPower *= -1;
    }

    public void SetGrappling(bool state)
    {
        isGrappling = state;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            rb.AddForce(Vector2.down, ForceMode2D.Impulse);
        }
    }

}
