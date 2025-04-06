using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
public class GroundCheck : MonoBehaviour
{
    CapsuleCollider2D capsuleCollider2D;
    public ContactFilter2D cast;
    public float groundDistance = 1.5F;
    RaycastHit2D[] raycastHit2D = new RaycastHit2D[5];
    public bool isGrounded = false;
   // private Animator playerAnimator;

    void Start()
    {
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        //playerAnimator = gameObject.GetComponent<Animator>(); 
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.green);
    }

    void FixedUpdate()
    {
        // Determine if colliding with anything 
        int collisions = capsuleCollider2D.Cast(Vector2.down, cast, raycastHit2D, groundDistance);
        
        // If collide with the ground
        if (collisions > 0)
        {
            isGrounded = true;
            //Debug.Log("on ground");
        }
        // If not colliding with the ground (jumping)
        else 
        {
            isGrounded = false;
        }
    }
}
