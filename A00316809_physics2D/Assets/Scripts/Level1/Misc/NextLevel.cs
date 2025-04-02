using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] string sceneName;
    public GameObject celebrateParticle;
    AudioSource audioS;
    [SerializeField] PlayerScriptable playerData;
    private void Start()
    {
        audioS = gameObject.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Player"))
        {
            
           
            Debug.Log("get playerdata");
            if (playerData.gems >=120)
            {
                audioS.Play();
                Debug.Log("audio play");
                
                InstantiateCelebrateParticle();
                StartCoroutine(ChangeScene());
            }
            
           
        }
    }
    IEnumerator ChangeScene()
    {
        
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(sceneName);
    }

    void InstantiateCelebrateParticle()
    {
        PlayerMovement3 player = FindFirstObjectByType<PlayerMovement3>();
        GameObject particle = Instantiate(celebrateParticle, player.transform.position, Quaternion.identity);
        Destroy(particle, 1.5f);

    }

}
