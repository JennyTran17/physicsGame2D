using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public GameObject target;
    public float speed = 2.5f;
    public float stopRange = 1f;
   // public float rotationSpeed = 100f; // Controls rotation speed

    void Update()
    {
        // Rotate around itself (Clockwise)
       // transform.Rotate(0f, 0f, -rotationSpeed * Time.deltaTime);

        // Calculate distance to the player
        float distance = Vector3.Distance(transform.position, target.transform.position);

        // Move toward player without affecting rotation
        if (distance > stopRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
}
