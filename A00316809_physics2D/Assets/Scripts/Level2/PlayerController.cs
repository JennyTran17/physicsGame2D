using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int moveSpeed, jumpPower;
    public bool useTransformMovement;
    private bool isGround;
    private float horizontal;
   

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //float x = Input.GetAxis("Horizontal");
        //Debug.Log(x);
        //if(useTransformMovement == false)
        //{
        //    rb.velocity = new Vector3(x, rb.velocity.y, 0);
        //}
        //else
        //{

        //    transform.position = new Vector3(transform.position.x + x * Time.deltaTime * moveSpeed, transform.position.y, transform.position.z);

        //}
        horizontal = Input.GetAxisRaw("Horizontal");
        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        }


    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.right * horizontal * moveSpeed);
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if(obj.CompareTag("Ground"))
        {
            rb.drag = 1.2f;

            float distance = Mathf.Abs(obj.GetComponent<Gravity>().groundRadius - Vector2.Distance(transform.position, obj.transform.position));
            if(distance < 1f)
            {
                isGround = distance < 0.1f;
            }
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
