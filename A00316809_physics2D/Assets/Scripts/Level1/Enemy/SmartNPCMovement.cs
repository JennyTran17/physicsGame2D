using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WayPointPM
{
    public GameObject waypoint;
    public float speed = 2f;
}

//move with random waypoints and block player
public class SmartNPCMovement : MonoBehaviour
{
    public WayPointPM[] wpPattern;
    public Transform player;
    public Transform collectable; //npc protects the collectable

    public float detectionRadius = 6f;  // How close before NPC reacts to player
    public float guardPredictionDistance = 6f;  // How far ahead to predict player movement
    public float baseSpeed = 2.5f;
    

    private int patternIndex = 0;
    private bool playerDetected = false; //detect if player in range
   

    void Start()
    {
        ShuffleWaypoints();
    }

    void Update()
    {
        //check player distance to enemy
        playerDetected = Vector3.Distance(transform.position, player.position) <= detectionRadius;

        if (collectable != null) //check if collectable exists
        {
            if (playerDetected && PlayerCloseToCollectable()) //in range and close to collectable
            {

                BlockPlayerMovement();
            }
            else if (IsPlayerHeadingToCollectable()) //predict playermovement
            {
                PredictAndGuardCollectable();
            }
            else
            {
                FollowPatternMovement(); //normal pattern

            }
        }
    }

    void ShuffleWaypoints()
    {
        for (int i = 0; i < wpPattern.Length; i++)
        {
            int randomIndex = Random.Range(i, wpPattern.Length); //shuffle waypoints

            WayPointPM temp = wpPattern[i];

            wpPattern[i] = wpPattern[randomIndex]; //assigned waypoints randomly

            wpPattern[randomIndex] = temp; //swap points
        }
    }

    void FollowPatternMovement()
    {
        if (wpPattern.Length == 0) return; // stop moving if no waypoints exist

        WayPointPM currentWP = wpPattern[patternIndex];

        float speed = GetDynamicSpeed(); // move faster if player is heading towards the collectable

        MoveTowards(currentWP.waypoint.transform.position, speed);

        // Check if NPC is close enough to the waypoint to switch to the next one
        // speed * Time.deltaTime ensures movement is frame rate independent, prevent faster or slower movement on different frame rate

        if (Vector3.Distance(transform.position, currentWP.waypoint.transform.position) <= speed * Time.deltaTime)
        {
            patternIndex++;
            if (patternIndex >= wpPattern.Length)
            {
                ShuffleWaypoints(); //randomize waypoints
                patternIndex = 0;
            }
        }
    }

    bool IsPlayerHeadingToCollectable() //check if player heading towards and not away
    {
        Vector3 playerToCollectable = collectable.position - player.position;
        Vector3 playerForward = (player.position - transform.position).normalized;

        // Calculate the dot product to check if the player is moving toward the collectable
        float dotProduct = Vector3.Dot(playerToCollectable.normalized, playerForward); //check the facing of the player to the collectable. 1 if in front, -1 is behind

        return dotProduct > 0.5f;  
    }

    bool PlayerCloseToCollectable()
    {
        return Vector3.Distance(player.position, collectable.position) <= detectionRadius;
    }

    void PredictAndGuardCollectable() //slightly guarding
    {
        Vector3 playerDirection = (player.position - transform.position).normalized;
        Vector3 predictedPlayerPosition = player.position + playerDirection * guardPredictionDistance;

        Vector3 guardPosition = (collectable.position + predictedPlayerPosition) / 2; // move between player and collectable
        MoveTowards(guardPosition, GetDynamicSpeed() * 1.2f);  // Slightly faster
    }

    void BlockPlayerMovement()
    {
        // Predict player movement
        Vector3 playerDirection = (player.position - transform.position).normalized;
        Vector3 predictedPlayerPosition = player.position + playerDirection * guardPredictionDistance;

        // Move between player and collectable (closer to player to block path)
        Vector3 blockPosition = player.position + (player.position - collectable.position).normalized * 1f;
        MoveTowards(blockPosition, GetDynamicSpeed() * 1.5f);  // Run even faster to block
    }

    
    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    float GetDynamicSpeed()
    {
        float distanceToCollectable = Vector3.Distance(transform.position, collectable.position);

        //Math.Lerp - linear interpolation
        //transition value from basespeed to basespeed *2 with a percentage(0 to 1)      //normalize vector
        //1- vector = inverted check distance. if 0 -> base speed, 1 -> *2
        //float dynamicSpeed = Mathf.Lerp(baseSpeed, baseSpeed * 2f, 1f - (distanceToCollectable / detectionRadius));

        // The proportion of distance relative to detection radius.
        float distanceFactor = 1- (distanceToCollectable / detectionRadius);

        // When the distance is large, the speed is closer to baseSpeed (distanceFactor = 0).
        // When the distance is small, the speed is closer to baseSpeed * 2 (distanceFactor = 1).
        float dynamicSpeed = baseSpeed + (baseSpeed * distanceFactor);

        if (playerDetected)
        {
            dynamicSpeed *= 1.5f; // Speed boost when player is close
        }

        return dynamicSpeed;
    }

    void OnDrawGizmosSelected()
    {
        // Draw detection radius in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
