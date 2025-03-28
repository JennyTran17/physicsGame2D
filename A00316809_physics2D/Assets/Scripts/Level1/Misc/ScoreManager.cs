using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
public class ScoreManager : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] PlayerScriptable playerData;
    float totalScore;
    float gemsCollected;
    float enemyKills;
    float gemPointValue = 10f;
    float killPointValue = 20f;
    float timeTaken;
    float timePointValue = 1;
    float healthRemaining;
    float health;

    private void Start()
    {
        totalScore = 0;
    }
    private void Update()
    {
        gemsCollected = playerData.gems;
        enemyKills = playerData.kill;
        health = playerData.health;
        timeTaken = playerData.time;
        healthRemaining = 100 - health;

       // scoreText.text = "Score: " + ((int)totalScore).ToString();
        calculateScore();
    }

    public void calculateScore()
    {

        // Calculate score for gems collected
        float gemScore = gemsCollected * gemPointValue;

        // Calculate score for enemy kills
        float killScore = enemyKills * killPointValue;

        // Calculate time score: Reward for faster completion
        float timeScore = Mathf.Max(0, 1 /  (timeTaken * timePointValue)); // Make sure it doesn't go negative

        // Calculate health score: Reward for more health remaining
        float healthScore = (float)((healthRemaining / 100) * 100);

        // Combine all scores
        totalScore = gemScore + killScore + timeScore + healthScore;
        playerData.score = (int)Mathf.Round(totalScore);
    }
}
