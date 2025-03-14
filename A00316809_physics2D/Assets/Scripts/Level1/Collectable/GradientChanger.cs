using UnityEngine;

public class GradientChanger : MonoBehaviour
{
    public Gradient gradient; // Holds the gradient
    public float changeSpeed = 1f; // Speed of gradient changes

    private SpriteRenderer spriteRenderer;
    private float time;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        time += Time.deltaTime / changeSpeed;
        if (time >= 1f)
        {
            time = 0f;
            GenerateNewGradient(); // Generate a new gradient every second
        }

        // Get color from gradient based on time
        spriteRenderer.color = gradient.Evaluate(time);
    }

    private void GenerateNewGradient()
    {
        gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];

        // Generate vibrant colors (High saturation and brightness)
        colorKeys[0] = new GradientColorKey(RandomVibrantColor(), 0f);
        colorKeys[1] = new GradientColorKey(RandomVibrantColor(), 1f);

        alphaKeys[0] = new GradientAlphaKey(1f, 0f);
        alphaKeys[1] = new GradientAlphaKey(1f, 1f);

        gradient.SetKeys(colorKeys, alphaKeys);
    }

    private Color RandomVibrantColor()
    {
        // Generate colors with high saturation (0.7 - 1) and high brightness (0.8 - 1)
        return Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.8f, 1f);
    }
}
