
using System;
using System.Collections;

using UnityEngine;


public class ShootingController : MonoBehaviour
{
    public GameObject target;
    Vector3 targetPos;
    public Vector3 targetVelocity;

    public GameObject bulletPrefab;
    public GameObject missilePrefab;

    public float bulletSpeed = 5f;
    bool canLaunch = true;
    float timeToIncrease = 10f;
    float startTime;
    float nextFireTime;
    float waitTime = 3f;


    private void Start()
    {
        startTime = Time.time;
        nextFireTime = Time.time + waitTime;
        targetPos = target.transform.position;
    }


    void Update()
    {
        //velocity of the target over time = change of distance between target current pos and prev pos over change of time
        targetVelocity = (target.transform.position - targetPos) / Time.deltaTime;
        targetPos = target.transform.position;

        //Debug.Log(waitTime);
        if (Time.time >= nextFireTime)
        {
            //Debug.Log(Time.time);
            //Debug.Log("next fire time: " + nextFireTime);
            
           // ShootBullet();

            if (Time.time - startTime > timeToIncrease) // increase difficulty
            {
                ShootPredictedBullet();
                waitTime -= 0.1f;
                bulletSpeed += 0.8f;
                ShootMissile();

            }
            
           
            nextFireTime = Time.time + waitTime;
        }
        //if (waitTime <= 0.9f)
        //{
        //    //reset timer
        //    waitTime = 3f;
        //    startTime = Time.time;
        //    nextFireTime = Time.time + waitTime;
        //    bulletSpeed = 5f;
        //}
        Restart();

        if (canLaunch)
        {

            ShootBullet();
            canLaunch = false;
            StartCoroutine(WaitTime());
        }
      
    }      
        

    void ShootBullet()
    {
        GameObject bullet= Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<BasicBullet>().CreateBullet(target.transform.position, bulletSpeed);
        

    }

    void ShootPredictedBullet()
    {
        //Distance between turret and target
        float distance = Vector3.Distance(transform.position, targetPos);

        //find approximate time for the bullet to hit the player
        float timeToTarget = distance / bulletSpeed;

        // current pos + change of distance in time period = predicted pos
        Vector3 predictedPosition = targetPos + (targetVelocity * timeToTarget); 

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<BasicBullet>().CreateBullet(predictedPosition, bulletSpeed);
        
    }

    void ShootMissile()
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
        missile.GetComponent<HeatSeekingMissiles>().CreateMissile(target);
        
    }//Quaternion.Euler(targetPos))

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(waitTime);
        canLaunch = true;
    }


    public void Restart()
    {
        if (waitTime <= 0.9f)
        {
            //reset timer
            waitTime = 3f;
            startTime = Time.time;
            nextFireTime = Time.time + waitTime;
            bulletSpeed = 5f;
        }
    }

}
