using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _mouseSensitivity = 1f;
    [SerializeField] private float secondsToPhotoShot = 3f;
    [SerializeField] private Volume blurVolume;
    [SerializeField] private Transform playerTransform;

    [Header("Photo UI and Effects")]
    [SerializeField] private CanvasGroup photoReadyIndicator; // fades from 0 to 1 gradually
    [SerializeField] private float photoReadyFadeSpeed = 2f;
    [SerializeField] private CanvasGroup photoCanBeTakenIcon; // shows when fully ready
    [SerializeField] private CanvasGroup flashImage;
    [SerializeField] private float flashFadeSpeed = 3f;

    [Header("Black Overlay Settings")]
    [SerializeField] private CanvasGroup blackScreenOverlay; // fades based on player movement
    [SerializeField] private float maxBlackOpacity = 0.2f;
    [SerializeField] private float blackFadeSpeed = 2f;

    [Header("Audio")]
    [SerializeField] private AudioSource photoSound;

    private Vector2 _lookPos;
    private float _timeSinceLastMovement;
    private bool _hasMovedSinceLastPhoto = true;
    private Vector3 _lastPlayerPosition;
    private bool _canTakePicture;

    public static Action<CameraController> PictureTakenEvent;

    private void Start()
    {
        _lookPos = Vector2.zero;
        //Cursor.lockState = CursorLockMode.Locked;
        _lastPlayerPosition = playerTransform.position;
    }

    private void Update()
    {
        // Mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        bool isCameraMoving = Mathf.Abs(mouseX) > 0.01f || Mathf.Abs(mouseY) > 0.01f;

        // Player movement detection
        Vector3 currentPosition = playerTransform.position;
        bool isPlayerMoving = Vector3.Distance(currentPosition, _lastPlayerPosition) > 0.001f;
        _lastPlayerPosition = currentPosition;

        // Reset still timer if moving camera or walking
        if (isCameraMoving || isPlayerMoving)
        {
            _timeSinceLastMovement = 0f;
            _hasMovedSinceLastPhoto = true;
        }
        else
        {
            _timeSinceLastMovement += Time.deltaTime;
        }

        // Camera look rotation
        _lookPos.x += mouseX * _mouseSensitivity;
        _lookPos.y += mouseY * _mouseSensitivity;
        _lookPos.x = Mathf.Clamp(_lookPos.x, -45f, 45f);
        _lookPos.y = Mathf.Clamp(_lookPos.y, -45f, 45f);
        transform.rotation = Quaternion.Euler(-_lookPos.y, _lookPos.x, 0f);

        // Determine if camera is ready to take a photo
        bool isStillLongEnough = _timeSinceLastMovement >= secondsToPhotoShot;
        bool isBlurLow = blurVolume.weight < 0.3f;
        _canTakePicture = isStillLongEnough && isBlurLow && _hasMovedSinceLastPhoto;

        // Photo shot trigger
        if (_canTakePicture && Input.GetMouseButtonDown(0))
        {
            PictureTakenEvent?.Invoke(this);
            photoSound?.Play();
            TriggerFlash();
            _hasMovedSinceLastPhoto = false;
        }

        // Readiness fill UI with smooth fade
        if (photoReadyIndicator != null)
        {
            float targetAlpha = Mathf.Clamp01(_timeSinceLastMovement / secondsToPhotoShot);
            photoReadyIndicator.alpha = Mathf.MoveTowards(photoReadyIndicator.alpha, targetAlpha, photoReadyFadeSpeed * Time.deltaTime);
        }

        // Show "can take photo" icon only when fully ready
        if (photoCanBeTakenIcon != null)
        {
            photoCanBeTakenIcon.alpha = _canTakePicture ? 1f : 0f;
        }

        // Flash fade-out
        if (flashImage != null && flashImage.alpha > 0f)
        {
            flashImage.alpha = Mathf.MoveTowards(flashImage.alpha, 0f, flashFadeSpeed * Time.deltaTime);
        }

        // Black screen overlay fades out as player stands still
        if (blackScreenOverlay != null)
        {
            float progress = Mathf.Clamp01(_timeSinceLastMovement / secondsToPhotoShot);
            float targetBlackAlpha = Mathf.Lerp(maxBlackOpacity, 0f, progress);
            blackScreenOverlay.alpha = Mathf.MoveTowards(blackScreenOverlay.alpha, targetBlackAlpha, blackFadeSpeed * Time.deltaTime);
        }
    }

    private void TriggerFlash()
    {
        if (flashImage != null)
        {
            flashImage.alpha = 1f;
        }
    }
}
