using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public float health = 100f;
    public GameObject endGame;
    public string sceneName;
    private void Update()
    {
        if (health <= 0)
        {
            // health = 100;
            healthText.text = "Player is dead. Game over!";
            
            endGame.SetActive(true);
           
            
            
        }
        else
        {
            healthText.text = "Health: " + health.ToString();
        }
        
    }

    public void Replay()
    {
        SceneManager.LoadScene(sceneName);
    }
}
