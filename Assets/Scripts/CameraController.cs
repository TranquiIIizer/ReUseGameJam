using System;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 1f;
    [SerializeField] private float secondsToPhotoShot = 3f;
    [SerializeField] private Transform _playerTransform;

    private Vector2 _lookPos;
    private float _timeSinceLastMovement;

    public static Action<CameraController> PictureTakenEvent; 

    private void Start()
    {
        _lookPos = Vector2.zero;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(CheckCameraStillness());
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(mouseX) > 0.01f || Mathf.Abs(mouseY) > 0.01f)
        {
            _timeSinceLastMovement = 0f;
        }

        _lookPos.x += mouseX * _mouseSensitivity;
        _lookPos.y += mouseY * _mouseSensitivity;

        _lookPos.x = Mathf.Clamp(_lookPos.x, -45f, 45f);
        _lookPos.y = Mathf.Clamp(_lookPos.y, -45f, 45f);

        transform.rotation = Quaternion.Euler(-_lookPos.y, _lookPos.x, 0f);
    }

    private IEnumerator CheckCameraStillness()
    {
        while (true)
        {
            yield return null;
            _timeSinceLastMovement += Time.deltaTime;

            if (_timeSinceLastMovement >= secondsToPhotoShot)
            {
                Debug.Log("Screenshot");
                PictureTakenEvent?.Invoke(this);
                _timeSinceLastMovement = 0f;
            }
        }
    }
}
