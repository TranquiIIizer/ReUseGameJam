using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BlurController : MonoBehaviour
{
    [Header("Volume Settings")]
    public Volume volume;

    [Header("Timing Settings")]
    public float riseDuration = 10f;
    public float pauseAfterZero = 3f;

    [Header("Focus Oscillation Settings")]
    public float oscillationSpeed = 2f; // Time (in seconds) for full up-down cycle

    [Header("Fade Image Settings")]
    public CanvasGroup fadeImage;
    public float fadeSpeed = 2f;

    private float currentWeight = 0f;
    private float pauseTimer = 0f;

    private bool isOscillating = false;
    private float oscillationTimer = 0f;
    private float oscillationStartOffset = 0f;
    private bool wasHoldingSpaceLastFrame = false;

    void Update()
    {
        bool isHoldingSpace = Input.GetKey(KeyCode.Space);

        if (pauseTimer > 0f)
        {
            pauseTimer -= Time.deltaTime;
        }
        else if (isHoldingSpace)
        {
            // Detect fresh press of space
            if (!wasHoldingSpaceLastFrame)
            {
                // Calculate starting point of oscillation based on current weight
                float t = Mathf.Clamp01(currentWeight);
                // Avoid domain error in Asin by clamping within [-1, 1]
                float normalized = Mathf.Clamp(2f * t - 1f, -1f, 1f);
                oscillationStartOffset = Mathf.Asin(normalized) / Mathf.PI + 0.5f;
                oscillationTimer = oscillationStartOffset * oscillationSpeed;
            }

            isOscillating = true;
            oscillationTimer += Time.deltaTime;
            currentWeight = Mathf.PingPong(oscillationTimer / oscillationSpeed, 1f);
        }
        else
        {
            isOscillating = false;
            oscillationTimer = 0f;

            float riseSpeed = 1f / riseDuration;
            currentWeight += riseSpeed * Time.deltaTime;
            currentWeight = Mathf.Clamp01(currentWeight);
        }

        // If weight hits 0, trigger pause
        if (currentWeight <= 0f && isOscillating)
        {
            currentWeight = 0f;
            pauseTimer = pauseAfterZero;
        }

        volume.weight = currentWeight;
        wasHoldingSpaceLastFrame = isHoldingSpace;

        // Fade image based on weight threshold
        if (fadeImage != null)
        {
            float targetAlpha = currentWeight < 0.3f ? 1f : 0f;
            fadeImage.alpha = Mathf.MoveTowards(fadeImage.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
        }
    }
}