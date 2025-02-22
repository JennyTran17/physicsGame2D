using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalInput;

    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpPower = 15f;

    private Animator animator;
   

    private bool top;
    private bool facingRight = true;
    

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
        rb.velocity = new Vector2 (horizontalInput * moveSpeed, rb.velocity.y);

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
    }
   
}
