
using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;


public class BasicBullet : MonoBehaviour
{
    private Vector3 direction;
    float speed;

    public void CreateBullet(Vector3 target_pos, float bulletSpeed)
    {
        direction = (target_pos - transform.position).normalized; // Normalize direction once
        speed = bulletSpeed;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

        StartCoroutine(waitDestroy()); // Start timer ONCE
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }


    IEnumerator waitDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerHealth _playerhealth = collision.gameObject.GetComponent<PlayerHealth>();
                _playerhealth.health -= 5;
                _playerhealth.healthBar.value = _playerhealth.health;
                Debug.Log(_playerhealth.health);
                
            }
            Destroy(gameObject);
        }

    }


}
