using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalInput;

    //[SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpPower = 15f;
    public bool canJump = true;

    private float currentSpeed = 0f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float acceleration = 30f;
    [SerializeField] private float deceleration = 40f;
    [SerializeField] private float swingForce = 5f;

    private Animator animator;
   

    private bool top;
    private bool facingRight = true;
    private bool isGrappling = false;
    private float grappleMomentumX = 0f;
    private bool isReleasingGrapple = false;

    public Transform gun;
    public float offset;


    public Transform shotPoint;
    public GameObject projectile;

    public float timeBetweenShots;
    float nextShotTime;


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

        if(Input.GetButtonDown("Jump") && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        ChangeGravity();
        Attack();

        //attack point rotation
        Vector3 displacement = gun.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(0f, 0f, angle + offset);
    }

    private void FixedUpdate()
    {
       

        ApplyMovement();
    }

    void ApplyMovement()
    {
        if (isGrappling)
        {
            grappleMomentumX = rb.velocity.x;

            // Add force instead of setting velocity to allow momentum and swinging
            rb.AddForce(new Vector2(horizontalInput * swingForce, 0), ForceMode2D.Force);
        }
        else if (isReleasingGrapple)
        {
            // When releasing the grapple, keep the momentum going
            rb.velocity = new Vector2(grappleMomentumX, rb.velocity.y);

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
        isReleasingGrapple = true;
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            rb.AddForce(Vector2.down, ForceMode2D.Impulse);
            isReleasingGrapple = false; // Reset the grapple release state
           
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Time.time > nextShotTime)
            {
                nextShotTime = Time.time + timeBetweenShots;
                Instantiate(projectile, shotPoint.position, shotPoint.rotation);
            }
            // animator.SetTrigger("attack");

        }
    }

}
