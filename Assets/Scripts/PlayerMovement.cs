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

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _moveSpeed = _baseSpeed;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && _timer < 0)
        {
            _timer = _dashCooldown;
            StartCoroutine(Dashed());
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
        _rb.linearVelocity = movement;
    }
}
