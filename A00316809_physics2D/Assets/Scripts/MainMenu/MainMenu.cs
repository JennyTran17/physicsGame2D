using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Scene scene;
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
        LoadFirstLevel();
    }

    void LoadFirstLevel()
    {
        if (scene.name.Equals("MainMenu"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Level1");
            }
        }
    }
}
