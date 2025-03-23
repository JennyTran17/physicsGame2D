using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public float health = 100f;
    public string sceneName;

    Animator animator;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        //healthBar.value = health;
        if(health <= 0)
        {
            health = 0f;
            Debug.Log("player dead");
            animator.SetTrigger("death");
            StartCoroutine(Replay());
        }
        
    }

  

    IEnumerator Replay()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}
