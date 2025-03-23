using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalInput;

    //[SerializeField] private float moveSpeed = 15f;
    public float jumpPower = 17f;
    public bool canJump = true;
    public bool gravityFlip = true;
    public bool canGrapple = false;

    public bool canDash = false;
    public bool isDashing;
    private float dashingPower = 100f;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 1f;

    private float currentSpeed = 0f;
    public float maxSpeed = 23f;
    public float acceleration = 23f;
    public float deceleration = 10f;
    public float swingForce = 10f;

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
        if(isDashing)
        {
            return;
        }
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            animator.SetBool("walk", true);
            isReleasingGrapple = false; // Reset the grapple release state
            if (facingRight == false && horizontalInput > 0)
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

        if((Input.GetKeyDown(KeyCode.W) && canJump) || (Input.GetKeyDown(KeyCode.S) && canJump))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (gravityFlip)
        {
            ChangeGravity();
        }
        Attack();

        //attack point rotation
        Vector3 displacement = gun.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(0f, 0f, angle + offset);

        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
       

        ApplyMovement();
    }

    void ApplyMovement()
    {
        if (isDashing)
        {
            return;
        }
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
            //float targetVelocityX = horizontalInput * currentSpeed;
            //float velocityChange = targetVelocityX - rb.velocity.x;

            //rb.AddForce(new Vector2(velocityChange * 5, 0), ForceMode2D.Force);
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
        if(Input.GetKeyDown(KeyCode.E))
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
            
           
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > nextShotTime)
            {
                nextShotTime = Time.time + timeBetweenShots;
                Instantiate(projectile, shotPoint.position, shotPoint.rotation);
            }
            // animator.SetTrigger("attack");

        }
    }

    //Dash
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }

}
