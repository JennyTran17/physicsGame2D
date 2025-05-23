using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public float hit = 3f;
    public PlayerScriptable playerData;

    private void OnCollisionEnter2D(Collision2D obj)
    {
       
        if(obj.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth eHealth = obj.gameObject.GetComponent<EnemyHealth>();
            if(eHealth != null)
            {
                playerData.kill += 1;
                eHealth.enemyHealth -= hit;
                Destroy(gameObject, 0.1f);
            }
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth eHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (eHealth != null)
            {
                playerData.kill += 1;
                eHealth.enemyHealth -= hit;
                Destroy(gameObject, 0.1f);
               // Debug.Log("enemy2");
            }
           // Debug.Log("Enemy found");
        }
    }


}
