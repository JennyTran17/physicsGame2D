using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public GameObject target;
    public float damage = 5f;
    bool isHit = false;

    private void Update()
    {
        target.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        if (Vector2.Distance(transform.position,target.transform.position) < 1.5f && !isHit)
        {
            PlayerHealth _playerhealth = target.GetComponent<PlayerHealth>();
            _playerhealth.health -= damage;
            _playerhealth.healthBar.value = _playerhealth.health; 
            Debug.Log(_playerhealth.health);
            isHit = true;
        }

        if(Vector2.Distance(transform.position, target.transform.position) > 1.7f)
        {
            isHit = false;
        }
       // Debug.Log(Vector2.Distance(transform.position, target.transform.position));
       
    }
}
