using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    // Reference to the Rigidbody component
    private Rigidbody2D rb;

    // Gravity scale value you want to set
    public float gravityScaleValue = 0.1f;

    public GameObject obj;

    private Renderer objectRenderer;

    bool isPlayerInside;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to the GameObject
        rb = GetComponent<Rigidbody2D>();

        // Get the Renderer component attached to the GameObject
        objectRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerInside && obj != null)
        {
            //// Keep player inside bubble
            //obj.transform.position = new Vector3(
            //    Mathf.Clamp(obj.transform.position.x, transform.position.x - 0.5f, transform.position.x + 0.5f),
            //    Mathf.Clamp(obj.transform.position.y, transform.position.y - 0.5f, transform.position.y + 0.5f),
            //    obj.transform.position.z
            //);

            transform.position = obj.GetComponent<Rigidbody2D>().transform.position;
        }
    }

    // This method is called when another collider enters the trigger collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            liftBubble();
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            destroyBubble();
        }
    }

    private void destroyBubble()
    {
        isPlayerInside = false;
        objectRenderer.enabled = false;
        obj.GetComponent<Rigidbody2D>().gravityScale = 3f;
        obj.GetComponent<Rigidbody2D>().mass = 1f;
        obj.GetComponent<CapsuleCollider2D>().enabled = true;
        obj.GetComponent<PlayerMovement3>().canJump = true;
        obj.GetComponent<PlayerMovement3>().gravityFlip = true;
        Destroy(gameObject);
    }

    private void liftBubble()
    {
        isPlayerInside = true;
        obj.GetComponent<Rigidbody2D>().gravityScale = - 0.3f;
        
        transform.position = obj.GetComponent<Rigidbody2D>().transform.position;
        rb.gravityScale = obj.GetComponent<Rigidbody2D>().gravityScale;
        obj.GetComponent<Rigidbody2D>().mass = 0;
        obj.GetComponent<CapsuleCollider2D>().enabled = false;
        obj.GetComponent<PlayerMovement3>().canJump = false;
        obj.GetComponent<PlayerMovement3>().gravityFlip = false;    
    }
}
