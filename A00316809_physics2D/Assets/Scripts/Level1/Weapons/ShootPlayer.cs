using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    public float hit = 3f;


    private void OnCollisionEnter2D(Collision2D obj)
    {

        if (obj.gameObject.CompareTag("Player"))
        {
            PlayerHealth pHealth = obj.gameObject.GetComponent<PlayerHealth>();
            pHealth.health -= hit;
            pHealth.healthBar.value = pHealth.health;
            
        }
        Destroy(gameObject, 0.1f);
    }
}
