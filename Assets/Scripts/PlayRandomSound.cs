using UnityEngine;
using System.Collections;

public class PlayRandomSound : MonoBehaviour
{
    public AudioClip[] dogBarks;
    public AudioClip[] carHorns;
    public AudioSource audioSource;

    [Header("Pies – interwa³ czasowy")]
    public float dogMinDelay = 3f;
    public float dogMaxDelay = 8f;

    [Header("Auto – interwa³ czasowy")]
    public float carMinDelay = 5f;
    public float carMaxDelay = 12f;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        StartCoroutine(PlayRandomDogBarks());
        StartCoroutine(PlayRandomCarHorns());
    }

    IEnumerator PlayRandomDogBarks()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(dogMinDelay, dogMaxDelay));

            if (dogBarks.Length > 0)
            {
                AudioClip clip = dogBarks[Random.Range(0, dogBarks.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
    }

    IEnumerator PlayRandomCarHorns()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(carMinDelay, carMaxDelay));

            if (carHorns.Length > 0)
            {
                AudioClip clip = carHorns[Random.Range(0, carHorns.Length)];
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
