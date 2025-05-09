using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText1;
    [SerializeField] TextMeshProUGUI scoreText2;
    [SerializeField] PlayerScriptable playerData;
    float scoreBefore;
    float totalScore;
    float gemsCollected;
    float enemyKills;
    float gemPointValue = 10f;
    float killPointValue = 20f;
    float timeTaken;
    float timePointValue = 20;
    float healthRemaining;
    float health;
    float timeScore;

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
        timeScore = Mathf.Max(0, 1 / (timeTaken * timePointValue)); // Make sure it doesn't go negative
        calculateFinal();
    }

    public void calculateScore()
    {

        // Calculate score for gems collected
        float gemScore = gemsCollected * gemPointValue;

        // Calculate score for enemy kills
        float killScore = enemyKills * killPointValue;

        // Calculate time score: Reward for faster completion
       

        // Calculate health score: Reward for more health remaining
        float healthScore = (float)((healthRemaining / 100) * 100);

        // Combine all scores
        scoreBefore = gemScore + killScore + healthScore;
        scoreText1.text = "Score: " + scoreBefore.ToString();
    }
    public void calculateFinal()
    {
        totalScore = scoreBefore + timeTaken;
        playerData.score = (int)Mathf.Round(totalScore);
        scoreText2.text = "Total Score\n" + playerData.score.ToString();
    }
}
