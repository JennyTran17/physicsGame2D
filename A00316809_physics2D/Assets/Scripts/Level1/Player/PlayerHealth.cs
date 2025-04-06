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
    public PlayerScriptable playerData;
    PlayerMovement3 audioS;
    Animator animator;
    bool isPlay;
    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioS = gameObject.GetComponent<PlayerMovement3>();
    }
    private void Update()
    {
        //healthBar.value = health;
        if(health <= 0)
        {
            health = 0f;
            Debug.Log("player dead");
            animator.SetTrigger("death");
            if(!isPlay)
            {
                isPlay = true;
                audioS.SetAudio(4);
            }
            StartCoroutine(Replay());
        }
        if (health > 100)
        {
            health = 100;
        }

        healthBar.value = health;
        playerData.health = health;
    }

  

    IEnumerator Replay()
    {
        
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(sceneName);
    }
}
