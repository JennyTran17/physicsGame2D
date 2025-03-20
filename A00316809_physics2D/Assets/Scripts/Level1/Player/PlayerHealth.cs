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
    private void Update()
    {
        //healthBar.value = health;
        if(health <= 0)
        {
            health = 100f;
            Debug.Log("player dead");
            StartCoroutine(Replay());
        }
        
    }

  

    IEnumerator Replay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }
}
