using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _endPoints;
    [SerializeField] private float _dashCooldown = 0.75f;
    [SerializeField] private float _dashDuration = 0.3f;
    private float _timer;
    [SerializeField] private float _baseSpeed;
    private float _moveSpeed;

    private Rigidbody _rb;

    [Header("Z Position Clamp")]
    [SerializeField] private float minZ = -5f;
    [SerializeField] private float maxZ = 5f;

    [Header("Footstep Sound")]
    [SerializeField] private AudioClip footstepsSound;
    private AudioSource _footstepsSource;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _moveSpeed = _baseSpeed;

        _footstepsSource = gameObject.AddComponent<AudioSource>();
        _footstepsSource.clip = footstepsSound;
        _footstepsSource.loop = true;
        _footstepsSource.playOnAwake = false;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && _timer < 0)
        {
            _timer = _dashCooldown;
            StartCoroutine(Dashed());
        }

        bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        if (isMoving && !_footstepsSource.isPlaying)
        {
            _footstepsSource.Play();
        }
        else if (!isMoving && _footstepsSource.isPlaying)
        {
            _footstepsSource.Stop();
        }
    }

    IEnumerator Dashed()
    {
        _moveSpeed = _baseSpeed * 3;
        yield return new WaitForSeconds(_dashDuration);
        _moveSpeed = _baseSpeed;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical) * _moveSpeed;
        Vector3 newPosition = _rb.position + movement * Time.fixedDeltaTime;

        // Clamp the Z position
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        _rb.MovePosition(newPosition);
    }
}
