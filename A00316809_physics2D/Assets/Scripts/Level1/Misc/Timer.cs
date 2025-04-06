using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    float elapsedTime;
    int minutes = 0;
    int seconds = 0;
    public PlayerScriptable playerData;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        minutes = Mathf.FloorToInt(elapsedTime / 60);
        seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if(elapsedTime>=0) playerData.time = elapsedTime;
    }

    //[SerializeField] TextMeshProUGUI timerText;
    //[SerializeField] float remainingTime;

    //private void Update()
    //{
    //    if(remainingTime > 0)
    //    {
    //        remainingTime -= Time.deltaTime;
    //    }
    //    else
    //    {
    //        remainingTime = 0;
    //    }
    //        remainingTime -= Time.deltaTime;
    //    int minutes = Mathf.FloorToInt(remainingTime / 60);
    //    int seconds = Mathf.FloorToInt(remainingTime % 60);
    //    timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    //}
}
