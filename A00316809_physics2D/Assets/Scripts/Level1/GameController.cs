using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;
    // Start is called before the first frame update
    void Start()
    {
        progressAmount = 0;
        Collectables.OnGemCollect += IncreaseProgressAmount;
        progressSlider.value = 0;
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;
        if (progressAmount > 100)
        {
            Debug.Log("LEvel complete");
        }
    }
}
