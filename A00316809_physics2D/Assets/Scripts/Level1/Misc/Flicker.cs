using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    

    public float minDisappearTime = 3f; // Minimum time before disappearing
    public float maxDisappearTime = 7f; // Maximum time before disappearing
    public float fadeSpeed = 2f;        // Speed of fading

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
       
        StartCoroutine(DisappearRoutine());
    }

    IEnumerator DisappearRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDisappearTime, maxDisappearTime));

            // Fade Out
            yield return StartCoroutine(FadeEnemy(1f, 0f));
           

            yield return new WaitForSeconds(Random.Range(2f, 5f)); // Stay invisible for a while

            // Fade In
            yield return StartCoroutine(FadeEnemy(0f, 1f));
          
        }
    }

    IEnumerator FadeEnemy(float startAlpha, float targetAlpha)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            spriteRenderer.color = color;
            yield return null;
        }
    }
}
