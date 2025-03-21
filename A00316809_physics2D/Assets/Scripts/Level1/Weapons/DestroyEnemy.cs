using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public float hit = 3f;
  

    private void OnCollisionEnter2D(Collision2D obj)
    {
       
        if(obj.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth eHealth = obj.gameObject.GetComponent<EnemyHealth>();
            if(eHealth != null)
            {
                eHealth.enemyHealth -= hit;
                Destroy(gameObject, 0.1f);
            }
           
        }
    }


}
