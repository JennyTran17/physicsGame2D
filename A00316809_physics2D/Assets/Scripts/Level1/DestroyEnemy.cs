using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public float hit = 3f;
  

    private void OnTriggerEnter2D(Collider2D obj)
    {
       
        if(obj.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth eHealth = obj.GetComponent<EnemyHealth>();
            eHealth.enemyHealth -= hit;
            Destroy(gameObject, 0.1f);
        }
    }


}
