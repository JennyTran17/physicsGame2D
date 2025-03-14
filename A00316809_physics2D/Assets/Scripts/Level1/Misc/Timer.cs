using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    float elapsedTime;

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
