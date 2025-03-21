using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;
    PlayerMovement3 player;
    [SerializeField] GameObject[] areas;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach(GameObject a in areas)
        {
            a.SetActive(false);
        }
    }
    void Start()
    {
        progressAmount = 0;
        Collectables.OnGemCollect += IncreaseProgressAmount;
        progressSlider.value = 0;
        player = GameController.FindFirstObjectByType<PlayerMovement3>();
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;
        if (progressAmount > 120)
        {
            Debug.Log("Level complete");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            //collectable > 20 -> grapple enabled 
            if (progressAmount >= 15 && progressAmount < 40)
            {
                player.canGrapple = true;
                if(progressAmount > 20)
                {
                    areas[0].SetActive(true);
                }
            }
            //collectable > 60 -> running increase or jumping enabled
            else if (progressAmount >= 40 && progressAmount < 60)
            {
                player.maxSpeed = 27;
                player.acceleration = 27;
                player.jumpPower = 23;
                areas[1].SetActive(true);
            }
            //collectable > 80 -> shooting enabled 
            else if (progressAmount >= 60)
            {
                player.canShoot = true;
                if (progressAmount > 120)
                {
                    areas[2].SetActive(true);
                }
             
            }
        }
    }
}
