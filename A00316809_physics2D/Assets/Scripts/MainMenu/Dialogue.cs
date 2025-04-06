using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index = 0;
    public float typingSpeed = 0.05f;
    public float sentencePause = 2f; // Pause time before next sentence
    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
        textDisplay.text = "";

        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(sentencePause); // Pause before the next sentence
        NextSentence();
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            StartCoroutine(TypeSentence()); // Start the next sentence typing
        }
        else
        {
            textDisplay.text = ""; // Clear text after all sentences
            audioSource.enabled = false;
        }
    }
}
