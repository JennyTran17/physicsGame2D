using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    Scene scene;
    [SerializeField] PlayerScriptable playerData;
    public GameObject highScorePanel;
    public TextMeshProUGUI highScoretext;
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
      
    }
    private void Update()
    {
        LoadIntro();
        LoadFirstLevel();
    }

    void LoadIntro()
    {
        if (scene.name.Equals("MainMenu"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Intro");
            }
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    void LoadFirstLevel()
    {
        if (scene.name.Equals("Intro"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Level1");
            }
        }
    }

    public void UpdateHighScore()
    {
        if (playerData.highScore.Count == 0 || playerData.score > playerData.highScore[0])
        {
            if (playerData.highScore.Count == 0)
            {
                playerData.highScore.Add(playerData.score);
            }
            else
            {
                playerData.highScore[0] = playerData.score; // Update only if it's a new high score
            }
        }

        highScoretext.text = playerData.highScore[0].ToString(); // Ensure text updates every time

        highScorePanel.SetActive(!highScorePanel.activeSelf); // Toggle the panel visibility
    }
}

