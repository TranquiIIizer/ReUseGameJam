using System;
using UnityEngine;

public class VehicleCollision : MonoBehaviour
{
    [SerializeField] private AudioClip carHitSound;
    private AudioSource _audioSource;
    public static Action<bool> GameEnded;
    public GameObject gameOverPanel;

    private bool _hasCrashed = false;

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.volume = 0.6f;
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCrashed) return;

        if (collision.gameObject.GetComponent<Player>())
        {
            _hasCrashed = true;
            _audioSource.PlayOneShot(carHitSound);

            CrashHandler.TriggerCrash();
            GameEnded?.Invoke(true);
        }
    }
}
