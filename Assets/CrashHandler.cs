using UnityEngine;

public class CrashHandler : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup deathScreen;

    [Header("Fade Settings")]
    [SerializeField] private float fadeSpeed = 1f;
    [SerializeField] private float timeSlowSpeed = 1f;
    [SerializeField] private bool stopAudioAfterFade = true;

    private static CrashHandler instance;

    private bool hasCrashed = false;
    private AudioSource[] allAudioSources;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (deathScreen != null)
        {
            deathScreen.alpha = 0f;
            deathScreen.gameObject.SetActive(false);
        }
    }

    public static void TriggerCrash()
    {
        if (instance != null)
        {
            instance.StartCrashSequence();
        }
    }

    private void StartCrashSequence()
    {
        hasCrashed = true;
        allAudioSources = FindObjectsOfType<AudioSource>();

        if (deathScreen != null)
        {
            deathScreen.alpha = 0f;
            deathScreen.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (!hasCrashed) return;

        // Fade in black screen
        if (deathScreen != null && deathScreen.alpha < 1f)
        {
            deathScreen.alpha = Mathf.MoveTowards(deathScreen.alpha, 1f, fadeSpeed * Time.unscaledDeltaTime);
        }

        // Slow down game time
        if (Time.timeScale > 0f)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0f, timeSlowSpeed * Time.unscaledDeltaTime);
        }

        // Fade out all audio
        foreach (AudioSource source in allAudioSources)
        {
            if (source == null) continue;
            source.volume = Mathf.MoveTowards(source.volume, 0f, fadeSpeed * Time.unscaledDeltaTime);
            if (stopAudioAfterFade && source.volume <= 0.01f)
            {
                source.Stop();
            }
        }
    }
}
