using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth = 9;
    private void Update()
    {
        if(enemyHealth <= 0)
        {
            enemyHealth = 0;
            Destroy(gameObject, 0.1f);
        }
    }
}
