using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeatSeekingMissiles : MonoBehaviour
{
    GameObject target;
    Vector3 targetPosition;
    float speed;
    float trackTime = 4f;
    bool hasDamage = false;
    float rotationSpeed = 720f;

    public void CreateMissile(GameObject player)
    {
        target = player;
        speed = 4f;
        StartCoroutine(StopFollowing());
    }
    void Update()
    {
        Debug.Log(trackTime);
        if (target != null)
        {
            targetPosition = target.transform.position;
            Vector3 distanceToTarget = targetPosition - transform.position;

            Vector3 vectorNormalise = distanceToTarget.normalized;


            //calculate speed to travel this frame
            float speedToTravelThisFrame = speed * Time.deltaTime;

            //Calculate how much to move
            Vector3 movement = vectorNormalise * speedToTravelThisFrame;
            // Debug.Log(transform.position);

            //add to movement
            transform.position += movement;
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, vectorNormalise );
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            if (!hasDamage)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, vectorNormalise, 0.1f);
                if (hit)
                {
                    PlayerHealth player = hit.collider.GetComponent<PlayerHealth>();
                    if (player != null)
                    {
                        player.health -= 5;
                        Debug.Log("health: " + player.health);
                        hasDamage = true;
                    }
                }
            }
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                transform.position = Vector2.zero;
                Destroy(gameObject);
            }
        }
    }

    IEnumerator StopFollowing()
    {
        yield return new WaitForSeconds(trackTime);
        Destroy(gameObject);
    }

    void Rotate()
    {
       
    }

}
