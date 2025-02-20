using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityScale, groundRadius, gravityMinRange, gravityMaxRange;
    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            // Reduce gravity influence if player is on inner collider
            if (Vector2.Distance(obj.transform.position, transform.position) < groundRadius + gravityMinRange)
            {
                return; // No gravity applied if too close (presumably on the inner collider)
            }
        }
        // Continue with normal gravity application for other objects
        float gravitionalPower = gravityScale;
        float dist = Vector2.Distance(obj.transform.position, transform.position);

        if (dist > (groundRadius + gravityMinRange))
        {
            float min = groundRadius + gravityMinRange;
            gravitionalPower = (((min + gravityMinRange) - dist) / gravityMaxRange) * gravitionalPower;
        }

        Vector3 dir = (transform.position - obj.transform.position) * gravityScale;
        obj.GetComponent<Rigidbody2D>().AddForce(dir);
        if (obj.CompareTag("Player"))
        {
            obj.transform.up = Vector3.MoveTowards(obj.transform.up, -dir, gravityScale * Time.deltaTime);
        }
    }
}
