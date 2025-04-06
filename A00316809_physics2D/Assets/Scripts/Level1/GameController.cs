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

    public GameObject[] areas;
    public GameObject celebrateParticle;
    public TextMeshProUGUI message;
    public GameObject menuPanel;
    public PlayerScriptable playerData;
    public AudioSource gameAudio;
    public AudioClip[] gameClip;
    PlayerHealth playerHealth;

    private bool isGrappleEnabled = false;
    private bool isDashEnabled = false;
    bool isPaused = false;
    bool isFinal;
    bool isWining;
    public string sceneName;
    public float counter;
    private static GameController instance;
    private void Awake()
    {
        foreach (GameObject a in areas)
        {
            a.SetActive(false);
        }



    }

    void Start()
    {
        Collectables.OnGemCollect -= IncreaseProgressAmount;
        progressAmount = 0;
        playerData.health = 100;
        playerData.kill = 0;
        playerData.gems = progressAmount;
        playerData.score = 0;
        Collectables.OnGemCollect += IncreaseProgressAmount;
        progressSlider.value = 0;
        player = FindFirstObjectByType<PlayerMovement3>();
        gameAudio = gameObject.GetComponent<AudioSource>();
        message.text = "";
        PauseGame();

    }

    void IncreaseProgressAmount(int amount)
    {
        if (this == null) return;
        progressAmount += amount;
        progressSlider.value = progressAmount;

        playerData.gems = progressAmount;

        if (progressAmount >= 120 && !isWining)
        {
            InstantiateCelebrateParticle();
            
            //ChangeAudio(1);

            //Debug.Log("Level complete");
            //isWining = true;
            message.text = "Exit opened!";
            StartCoroutine(waitTime(4f));

        }
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        // Handle upgrades & celebrate only once per milestone
        if (progressAmount >= 20 && progressAmount < 50 && !isGrappleEnabled)
        {
            player.canGrapple = true;
            InstantiateCelebrateParticle();
            ChangeAudio(0);
            areas[0].SetActive(true);
            isGrappleEnabled = true;
            message.text = "New Area opened\nGrapple unlocked\n Health boost";
            Debug.Log(message.text);
            if (playerHealth != null)
            {
                playerHealth.health += 10;
                playerData.health = playerHealth.health;
            }

            StartCoroutine(waitTime(4f));
        }
        if (progressAmount >= 50 && progressAmount < 110 && !isDashEnabled)
        {
            player.canDash = true;
            InstantiateCelebrateParticle();
            ChangeAudio(0);
            areas[1].SetActive(true);
            isDashEnabled = true;
            message.text = "New Area opened\nDashing unlocked\n Health boost";
            if (playerHealth != null)
            {
                playerHealth.health += 10;
                playerData.health = playerHealth.health;
            }

            StartCoroutine(waitTime(4f));
        }
        if (progressAmount >= 110 && progressAmount < 120 && !isFinal)
        {
            InstantiateCelebrateParticle();
            ChangeAudio(0);
            areas[2].SetActive(true);
            message.text = "New Area unlocked";
            if (playerHealth != null)
            {
                playerHealth.health += 30;
                playerData.health = playerHealth.health;
            }

            StartCoroutine(waitTime(4f));
            isFinal = true;
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
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
        // if Q pressed, menu pop up appear
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isPaused)
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
            ChangeAudio(2);
            counter = 0;
        }

    }

    public void ChangeAudio(int i)
    {
        if (gameAudio == null) { Debug.Log("no audio"); return; }
        gameAudio.clip = gameClip[i];
        gameAudio.Play();
    }

    void OnDestroy()
    {

        Collectables.OnGemCollect -= IncreaseProgressAmount;
    }
}
