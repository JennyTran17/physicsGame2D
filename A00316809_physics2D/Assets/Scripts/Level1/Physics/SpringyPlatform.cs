using UnityEngine;

public class SpringyPlatform : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpringJoint2D spring;
    private GameObject anchorPoint;


    private void Start()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();

        // Create anchor point at current position
        anchorPoint = new GameObject("Anchor_" + gameObject.name);
        anchorPoint.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        // Setup spring joint with tight settings for subtle movement
        spring = GetComponent<SpringJoint2D>();
        spring.enabled = false; // Temporarily disable while setting up
        spring.connectedBody = anchorPoint.AddComponent<Rigidbody2D>();
        spring.connectedBody.bodyType = RigidbodyType2D.Static;
       // spring.connectedAnchor = anchorPoint.transform.position;

        // Configure spring for minimal bounce
        spring.distance = 5f;               
        spring.dampingRatio = 2f;        
        spring.frequency = 0.7f;             
        spring.autoConfigureDistance = false;
        spring.enabled = true; 
    }


    private void OnDestroy()
    {
        if (anchorPoint != null)
        {
            Destroy(anchorPoint);
        }
    }
}