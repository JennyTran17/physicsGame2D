using System.Collections;
using UnityEngine;



public class OrbitGuardNPC : MonoBehaviour
{

    public GameObject bulletPrefab;      // Assign a simple bullet prefab
    public int bulletsToShoot = 8;
    bool isShot= true;

    void Update()
    {
        if (isShot)
        {
            ShootRadialBullets();
            StartCoroutine(shootTime());
            
        }
    }



    void ShootRadialBullets()
    {
        for (int i = 0; i < bulletsToShoot; i++)
        {
            float angle = (360f / bulletsToShoot) * i; // angle to direct each bullet

            //convert to direction
            //e.g: angle = 0: Cos(0) = 1, Sin(0) = 0  direction = (1, 0, 0) (right).

            Vector3 direction = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad),
                0);

            // Instantiate the bullet
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            //Rotate the bullet so that "up" points in the desired direction
            
            //calculate angle in radians between x axis and direction vector, and convert to degrees
            //return value between -3.14 to 3.14. eg.direction = (1, 0) (right), Atan2(0, 1) = 0 
            //float angleDegrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //atan2 aligns bullet local right direction with direction.
            //subtract 90 rotates the bullet counterclockwise so its up direction aligns with direction
            //bullet.transform.rotation = Quaternion.Euler(0, 0, angleDegrees - 90f);  // "-90" to align 'up' to the direction

            
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction); //up direction aligns with direction found

            Projectile bulletScript = bullet.GetComponent<Projectile>();
            isShot = false;
        }
    }

    IEnumerator shootTime()
    {
        yield return new WaitForSeconds(3);
        isShot = true;
        
    }
}
