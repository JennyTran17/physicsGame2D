using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeatSeekingMissiles : MonoBehaviour
{
    GameObject target;
    Vector3 targetPosition;
    float speed;
    float trackTime = 7f;
    float rotationSpeed = 720f;

    public void CreateMissile(GameObject player)
    {
        target = player;
        speed = 10f;
        StartCoroutine(StopFollowing());
    }
    void Update()
    {
        //Debug.Log(trackTime);
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

            
        }
    }

    IEnumerator StopFollowing()
    {
        yield return new WaitForSeconds(trackTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {

            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerHealth _playerhealth = collision.gameObject.GetComponent<PlayerHealth>();
                _playerhealth.health -= 10;
                _playerhealth.healthBar.value = _playerhealth.health;
                Debug.Log(_playerhealth.health);
                
            }
            Destroy(gameObject);
        }

    }


}
