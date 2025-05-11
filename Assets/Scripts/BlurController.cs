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
    public float spaceDecaySpeed = 0.5f;

    [Header("Movement Settings")]
    public Transform playerTransform;
    public float movementSpeedMultiplier = 2f;
    public float dashSpeedMultiplier = 3f;
    public float movementThreshold = 0.01f;

    [Header("Fade Image Settings")]
    public CanvasGroup fadeImage;
    public float fadeSpeed = 2f;

    [Header("Audio Settings")]
    public AudioSource clickLoopSource;
    public AudioSource oneShotSource;
    public AudioClip clickLoopClip;
    public AudioClip shutterClip;

    private float currentWeight = 0f;
    private float previousWeight = 0f;
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
                currentWeight = Mathf.MoveTowards(currentWeight, 0f, spaceDecaySpeed * Time.deltaTime);

                if (!clickLoopSource.isPlaying && clickLoopClip != null)
                {
                    clickLoopSource.clip = clickLoopClip;
                    clickLoopSource.loop = true;
                    clickLoopSource.Play();
                }

                if (currentWeight <= 0f)
                {
                    currentWeight = 0f;
                    pauseTimer = pauseAfterZero;

                    if (clickLoopSource.isPlaying)
                        clickLoopSource.Stop();

                    // Do NOT play shutterClip here
                }
            }
            else
            {
                if (clickLoopSource.isPlaying)
                    clickLoopSource.Stop();

                float riseSpeed = 1f / riseDuration;
                if (isMoving)
                    riseSpeed *= movementSpeedMultiplier;
                if (isDashing)
                    riseSpeed *= dashSpeedMultiplier;

                currentWeight += riseSpeed * Time.deltaTime;
                currentWeight = Mathf.Clamp01(currentWeight);
            }
        }

        // Check if we crossed the 0.3 threshold in either direction (excluding 0)
        if ((previousWeight > 0.3f && currentWeight <= 0.3f && currentWeight > 0f) ||
            (previousWeight < 0.3f && currentWeight >= 0.3f))
        {
            if (shutterClip != null && oneShotSource != null)
                oneShotSource.PlayOneShot(shutterClip);
        }

        previousWeight = currentWeight;
        volume.weight = currentWeight;

        if (fadeImage != null)
        {
            float targetAlpha = currentWeight < 0.3f ? 1f : 0f;
            fadeImage.alpha = Mathf.MoveTowards(fadeImage.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
        }
    }
}
