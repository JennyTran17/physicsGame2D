using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] private float grappleLength = 4f;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private LineRenderer rope;
    PlayerMovement3 player;
    private Vector2 swingMomentum;

    private Vector3 grapplePoint;
    private SpringJoint2D joint;

    private void Start()
    {
        joint = gameObject.GetComponent<SpringJoint2D>();
        joint.enabled = false;
        rope.enabled = false;
        player = FindFirstObjectByType<PlayerMovement3>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            RaycastHit2D hit = Physics2D.Raycast(
                origin: Camera.main.ScreenToWorldPoint(Input.mousePosition),
                direction: Vector2.zero,
                distance: Mathf.Infinity,
                layerMask: grappleLayer

            );

            if(hit.collider != null )
            {
                grapplePoint = hit.point;
                grapplePoint.z = 0;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
                rope.SetPosition(0, grapplePoint);
                rope.SetPosition(1, transform.position);
                rope.enabled = true;
               
                player.SetGrappling(true);
            }
            
        }

        if(Input.GetMouseButtonUp(0)) 
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();


            swingMomentum = rb.velocity;


            joint.enabled=false;
            rope.enabled=false;
            player.SetGrappling(false);

            rb.velocity = new Vector2(swingMomentum.x, swingMomentum.y);

        }

        if(rope.enabled == true)
        {
            rope.SetPosition(1, transform.position);
        }
    }
}
