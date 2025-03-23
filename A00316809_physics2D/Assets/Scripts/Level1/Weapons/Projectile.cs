using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up *speed * Time.deltaTime);
        StartCoroutine(waitDestroy());
    }

    IEnumerator waitDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject, 1f);
    }

}
