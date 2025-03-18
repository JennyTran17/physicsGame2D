
using UnityEngine;


public class BasicBullet : MonoBehaviour
{
    Vector3 targetPosition;
    float speed;
    bool hasDamage = false;

    public void CreateBullet(Vector3 target_pos, float bulletSpeed)
    {
        targetPosition = target_pos;
        speed = bulletSpeed;
        
    }
    void Update()
    {

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            //transform.position = Vector2.zero;
            Explode();
        }

        Vector3 distanceToTarget = targetPosition - transform.position;
        
        Vector3 vectorNormalise = distanceToTarget.normalized;


        //calculate speed to travel this frame
        float speedToTravelThisFrame = speed * Time.deltaTime;

        //Calculate how much to move
        Vector3 movement = vectorNormalise * speedToTravelThisFrame;
        // Debug.Log(transform.position);
        transform.position += movement;

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
                    Explode();
                }
            }
        }

    }

    void Explode()
    {
        Destroy(gameObject);
    }


}
