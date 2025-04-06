using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D obj)
    {
        PlayerMovement3 audioS = gameObject.GetComponent<PlayerMovement3>();
        IItem item = obj.gameObject.GetComponent<IItem>();
        if(item != null )
        {
            
            if (obj.gameObject.CompareTag("Health"))
            {
                PlayerHealth playerHealth = gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.health += 10;
                    playerHealth.healthBar.value = playerHealth.health;
                    playerHealth.playerData.health = playerHealth.health;
                }
            }
            item.Collect();
            audioS.SetAudio(3);
        }

        
    }
}
