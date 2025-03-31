using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class GameController : MonoBehaviour
{
    int progressAmount;
    public Slider progressSlider;
    PlayerMovement3 player;

    [SerializeField] GameObject[] areas;
    [SerializeField] GameObject celebrateParticle;
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] GameObject menuPanel;
    [SerializeField] PlayerScriptable playerData;

    private bool isGrappleEnabled = false;
    private bool isDashEnabled = false;
    bool isPaused = false;

    [SerializeField] float counter;
    private void Awake()
    {
        foreach (GameObject a in areas)
        {
            a.SetActive(false);
        }

    }

    void Start()
    {
        progressAmount = 0;
        playerData.health = 100;
        playerData.kill = 0;
        playerData.gems = progressAmount;
        playerData.score = 0;
        Collectables.OnGemCollect += IncreaseProgressAmount;
        progressSlider.value = 0;
        player = FindFirstObjectByType<PlayerMovement3>();
        message.text = "";
        PauseGame();
    }

    void IncreaseProgressAmount(int amount)
    {
        progressAmount += amount;
        progressSlider.value = progressAmount;

        playerData.gems = progressAmount;

        if (progressAmount > 120)
        {
            Debug.Log("Level complete");
           
        }

        // Handle upgrades & celebrate only once per milestone
        if (progressAmount >= 20 && !isGrappleEnabled)
        {
            player.canGrapple = true;
            InstantiateCelebrateParticle();
            areas[0].SetActive(true);
            isGrappleEnabled = true;
            message.text = "New Area opened\nGrapple unlocked";
            Debug.Log(message.text);
            StartCoroutine(waitTime(4f));
        }
        if (progressAmount >= 50 && !isDashEnabled)
        {
            player.canDash = true;
            InstantiateCelebrateParticle();
            areas[1].SetActive(true);
            isDashEnabled = true;
            message.text = "New Area opened\nDashing unlocked";
            StartCoroutine(waitTime(4f));
        }
        if (progressAmount >= 110)
        {
            InstantiateCelebrateParticle();
            areas[2].SetActive(true);
            message.text = "New Area unlocked";
            StartCoroutine(waitTime(4f));
        }
    }

    void InstantiateCelebrateParticle()
    {
        player = FindFirstObjectByType<PlayerMovement3>();
        GameObject particle = Instantiate(celebrateParticle, player.transform.position, Quaternion.identity);
        Destroy(particle, 1.5f);
        
    }

    

    IEnumerator waitTime(float time)
    {
        yield return new WaitForSeconds(time);
        message.text = "";
    }

 


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        // if Q pressed, menu pop up appear
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
                
            }
        }

        //StartCoroutine(shakeInterval(10f));

        CallIntervalShake();
    }

    //pause game
    public void PauseGame()
    {

        //open menu
        menuPanel.SetActive(true);

        isPaused = true;
        Time.timeScale = 0;
    }

    //resume game
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        //close menu
        menuPanel.SetActive(false);
    }

    void CallIntervalShake()
    {
        counter += Time.deltaTime;
        if (counter > 100)
        {
            CinemachineShake.instance.ShakeCamera(3f, 1.5f);
            counter = 0;
        }
        
    }
}
