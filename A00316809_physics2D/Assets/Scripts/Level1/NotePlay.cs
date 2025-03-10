using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePlay : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.Play();
        }
    }
}
