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

    [Header("Space Holding Settings")]
    public float spaceDecaySpeed = 0.5f; // How fast weight moves toward 0 when holding space

    [Header("Movement Settings")]
    public Transform playerTransform;
    public float movementSpeedMultiplier = 2f;
    public float dashSpeedMultiplier = 3f;
    public float movementThreshold = 0.01f;

    [Header("Fade Image Settings")]
    public CanvasGroup fadeImage;
    public float fadeSpeed = 2f;

    private float currentWeight = 0f;
    private float pauseTimer = 0f;

    private Vector3 lastPlayerPosition;

    void Start()
    {
        lastPlayerPosition = playerTransform.position;
    }

    void Update()
    {
        bool isHoldingSpace = Input.GetKey(KeyCode.Space);
        bool isDashing = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        Vector3 currentPos = playerTransform.position;
        float movedDistance = Vector3.Distance(currentPos, lastPlayerPosition);
        bool isMoving = movedDistance > movementThreshold;
        lastPlayerPosition = currentPos;

        if (pauseTimer > 0f)
        {
            pauseTimer -= Time.deltaTime;
        }
        else
        {
            if (isHoldingSpace && !isMoving)
            {
                // Only allow weight to decrease if stationary
                currentWeight = Mathf.MoveTowards(currentWeight, 0f, spaceDecaySpeed * Time.deltaTime);

                if (currentWeight <= 0f)
                {
                    currentWeight = 0f;
                    pauseTimer = pauseAfterZero;
                }
            }
            else
            {
                // Always rise when not holding space or moving
                float riseSpeed = 1f / riseDuration;

                if (isMoving)
                    riseSpeed *= movementSpeedMultiplier;
                if (isDashing)
                    riseSpeed *= dashSpeedMultiplier;

                currentWeight += riseSpeed * Time.deltaTime;
                currentWeight = Mathf.Clamp01(currentWeight);
            }
        }

        volume.weight = currentWeight;

        // Fade image
        if (fadeImage != null)
        {
            float targetAlpha = currentWeight < 0.3f ? 1f : 0f;
            fadeImage.alpha = Mathf.MoveTowards(fadeImage.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
        }
    }
}
