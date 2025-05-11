using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShotCapturer : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image _photoDisplayArea;
    [SerializeField] private GameObject _cameraUI;

    private Texture2D _screenCapture;
    private List<Sprite> _photoSprites = new();
    private int _currentPhotoIndex = 0;

    private void Start()
    {
        _screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        CameraController.PictureTakenEvent += CapturePhoto;
    }

    public void CapturePhoto(CameraController cameraController)
    {
        StartCoroutine(ReadRegion());
    }

    IEnumerator ReadRegion()
    {
        _cameraUI.SetActive(false);
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        _screenCapture.ReadPixels(regionToRead, 0, 0, false);
        _screenCapture.Apply();

        SavePhoto();
    }

    private void SavePhoto()
    {
        Texture2D newTexture = new Texture2D(_screenCapture.width, _screenCapture.height, _screenCapture.format, false);
        newTexture.SetPixels(_screenCapture.GetPixels());
        newTexture.Apply();

        Sprite photoSprite = Sprite.Create(newTexture, new Rect(0.0f, 0.0f, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        _photoSprites.Add(photoSprite);
        _currentPhotoIndex = _photoSprites.Count - 1;
        _photoDisplayArea.sprite = photoSprite;

        _cameraUI.SetActive(true);
    }

    public void ShowNextPhoto()
    {
        if (_photoSprites.Count == 0) return;
        _currentPhotoIndex = (_currentPhotoIndex + 1) % _photoSprites.Count;
        _photoDisplayArea.sprite = _photoSprites[_currentPhotoIndex];
    }

    public void ShowPreviousPhoto()
    {
        if (_photoSprites.Count == 0) return;
        _currentPhotoIndex = (_currentPhotoIndex - 1 + _photoSprites.Count) % _photoSprites.Count;
        _photoDisplayArea.sprite = _photoSprites[_currentPhotoIndex];
    }

    private void OnDestroy()
    {
        CameraController.PictureTakenEvent -= CapturePhoto;
    }
}
