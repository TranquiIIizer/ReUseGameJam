using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity;
    private Vector2 _lookPos;
    [SerializeField] private Transform _playerTransform;

    private void Start()
    {
        _lookPos = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _lookPos.x += Input.GetAxis("Mouse X");
        _lookPos.y += Input.GetAxis("Mouse Y");

        _lookPos.x = Mathf.Clamp(_lookPos.x, -45f, 45f);
        _lookPos.y = Mathf.Clamp(_lookPos.y, -45f, 45f);

        transform.rotation = Quaternion.Euler(-_lookPos.y, _lookPos.x, 0f);
    }
}
