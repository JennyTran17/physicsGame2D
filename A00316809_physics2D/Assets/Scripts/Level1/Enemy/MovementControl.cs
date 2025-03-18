using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public GameObject target;
    public float speed = 2.5f;
    public float stopRange = 1f;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        if ( distance > stopRange)
        {
            Vector3 directionToPlayer = (target.transform.position - transform.position).normalized;
            transform.position += directionToPlayer * speed * Time.deltaTime;
        }
    }
}
