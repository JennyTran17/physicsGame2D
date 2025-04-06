using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public GameObject destination;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Move the player to the Destination
        collision.gameObject.transform.position = destination.transform.position;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerMovement3 player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement3>();
            if (player != null)
            {
                collision.gameObject.transform.position = new Vector2(player.transform.position.x + 10f, player.transform.position.y + 10f);


            }
        }
    }
}
