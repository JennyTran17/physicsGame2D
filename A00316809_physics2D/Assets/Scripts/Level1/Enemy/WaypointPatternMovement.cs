using System.Collections;
using UnityEngine;

[System.Serializable]
public class WayPointCD
{
    public GameObject waypoint;
    public float speed = 2f; 
    public float waitTime = 1f; 
}

//move with random speed and wait time
public class WaypointPatternMovement : MonoBehaviour
{
    public WayPointCD[] wpPattern;
    private int patternIndex = 0;

    private bool isPaused = false; //track pause state
    private float pauseTimer = 0f;

    private float pauseDuration; //take in waitTime value

    [Header("Random Speed Settings")]
    public float minSpeed = 4f;
    public float maxSpeed = 9f;

    [Header("Random Wait Time Settings")]
    public float minWaitTime = 0.1f;
    public float maxWaitTime = 2f;

    void Start()
    {
        // Initialize first waypoint random settings
        RandomizeWaypointSpeedTime();
    }

    void Update()
    {
        if (isPaused) // whenever StartPause() is called isPause = true
        {
            HandlePause();
        }
        else
        {
            MoveAlongWaypoints();
        }
    }

    void RandomizeWaypointSpeedTime() //Randomize speed and waitTime
    {
        WayPointCD current = wpPattern[patternIndex];

        current.speed = Random.Range(minSpeed, maxSpeed);
        current.waitTime = Random.Range(minWaitTime, maxWaitTime);
    }

    void MoveAlongWaypoints() // move npc towards the current waypoint
    {
        WayPointCD wayPointCD = wpPattern[patternIndex];

        Vector3 directionToWaypoint = wayPointCD.waypoint.transform.position - transform.position;
        float distance = directionToWaypoint.magnitude;
        float step = wayPointCD.speed * Time.deltaTime;

        if (distance <= 1)
        {
            
            StartPause(wayPointCD.waitTime); // Arrived at waypoint - start pause with random duration

            patternIndex++; // Advance to next waypoint and randomize its properties

            if (patternIndex >= wpPattern.Length) 
            {
                patternIndex = 0; // Loop back to the first waypoints
            }

            RandomizeWaypointSpeedTime();
        }
        else
        {
            // Move toward waypoint
            Vector3 move = step * directionToWaypoint.normalized;
            transform.Translate(move);
        }
    }

    void StartPause(float duration) //Pauses the NPC before moving to the next waypoint.
    {
        isPaused = true;
        pauseTimer = 0f;
        
        pauseDuration = duration; //duration is assigned the waitTime
    }

    

    void HandlePause()  //Pause Time
    {
        pauseTimer += Time.deltaTime;
        if (pauseTimer >= pauseDuration)
        {
            isPaused = false; // Resume moving
        }
    }

    
}
