using System.Collections;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float pullStrength = 1f; // Pull force remains strong
    public float orbitSpeed = 1f; // Slower orbiting speed
    public float decayRate = 0.995f; // Slower decay (player orbits longer)
    public float destroyThreshold = 0.1f; // Distance where player is teleported

    private Transform player;
    private bool isPulling = false;
    private float orbitRadius;
    private float angle = 0f;

    [SerializeField] GameObject destination;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            isPulling = true;
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb)
            {
                rb.velocity = Vector2.zero; // Stop player movement
                rb.gravityScale = 0; // Disable gravity (if any)
            }

            // Set initial orbit distance
            orbitRadius = Vector2.Distance(player.position, transform.position);
        }
    }

    private void Update()
    {
        if (player != null)
        {
            Debug.Log(Vector2.Distance(player.position, transform.position));
        }

        if (isPulling && player != null)
        {
            Vector2 blackHolePos = transform.position;
            Vector2 playerPos = player.position;

            // Orbiting movement (slower, more persistent orbit)
            angle += orbitSpeed * Time.fixedDeltaTime; // Slower orbit speed
            orbitRadius *= decayRate; // Reduce orbit radius very gradually

            // Convert polar coordinates to Cartesian for Fibonacci-like orbit
            float x = Mathf.Cos(angle) * orbitRadius;
            float y = Mathf.Sin(angle) * orbitRadius;
            Vector2 orbitPosition = blackHolePos + new Vector2(x, y);

            // Apply movement smoothly
            player.position = Vector2.Lerp(player.position, orbitPosition, pullStrength * Time.fixedDeltaTime);

            // Teleport player when very close to center
            if (Vector2.Distance(playerPos, blackHolePos) < destroyThreshold)
            {
                StartCoroutine(TeleportPlayer());
            }
        }
    }
    void FixedUpdate()
    {
        
    }

    IEnumerator TeleportPlayer()
    {
        isPulling = false; // Stop pulling effect
        yield return new WaitForSeconds(0.1f); // Small delay for safety
        player.position = destination.transform.position;

        // Reactivate physics if needed
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.gravityScale = 2.5f;
        }
    }
}
