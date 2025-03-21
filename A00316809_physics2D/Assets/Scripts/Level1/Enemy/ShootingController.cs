using System.Collections;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject target;
    public GameObject bulletPrefab;
    public GameObject missilePrefab;

    public float bulletSpeed = 15f;
    private float waitTime = 3f;
    private float timeToIncrease = 10f;
    private float startTime;
    private float nextFireTime;
    private bool canLaunch = true;

    private void Start()
    {
        startTime = Time.time;
        nextFireTime = Time.time + waitTime;
    }

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            ShootBullet();

            if (Time.time - startTime > timeToIncrease) // Increase difficulty over time
            {
                waitTime = Mathf.Max(0.5f, waitTime - 0.1f); // Prevents waitTime from going negative
                bulletSpeed += 0.1f;
                ShootMissile();
            }

            nextFireTime = Time.time + waitTime;
        }

        Restart();

        if (canLaunch)
        {
            canLaunch = false;
            StartCoroutine(WaitTime());
        }
    }

    void ShootBullet()
    {
        Vector3 spawnPosition = transform.position ; // Spawns bullet slightly in front
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        bullet.GetComponent<BasicBullet>().CreateBullet(target.transform.position, bulletSpeed);
    }

    void ShootMissile()
    {
        Vector3 spawnPosition = transform.position;
        GameObject missile = Instantiate(missilePrefab, spawnPosition, Quaternion.identity);
        missile.GetComponent<HeatSeekingMissiles>().CreateMissile(target);
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(waitTime);
        canLaunch = true;
    }

    public void Restart()
    {
        if (waitTime <= 1f)
        {
            waitTime = 3f;
            startTime = Time.time;
            nextFireTime = Time.time + waitTime;
            bulletSpeed = 15f;
        }
    }
}
