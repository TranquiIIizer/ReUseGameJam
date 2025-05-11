using UnityEngine;

public class VehicleCollision : MonoBehaviour
{
    private Collider _selfCollider;

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
        if (collision.gameObject.GetComponent<Player>())
        {
            CollidedWithPlayer();
        }
    }

    public void CollidedWithPlayer()
    {
        if (carHitSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(carHitSound);
        }

        Debug.Log("Śmierć");
    }
}