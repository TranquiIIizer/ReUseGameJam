using UnityEngine;

public class VehicleCollision : MonoBehaviour
{
    [SerializeField] private AudioClip carHitSound;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip carHitSound;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
        _audioSource.volume = 0.6f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasCrashed) return;

        if (collision.gameObject.GetComponent<Player>())
        {
            _hasCrashed = true;
            _audioSource.PlayOneShot(carHitSound);

    public void CollidedWithPlayer()
    {
        if (carHitSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(carHitSound);
        }

            Debug.Log("Śmierć");
        }
    }
}