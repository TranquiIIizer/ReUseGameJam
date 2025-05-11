using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScreenShotCapturer : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image _photoDisplayArea;
    private Texture2D _screenCapture;
    [SerializeField] private GameObject _cameraUI;
    [SerializeField] private GameObject _photoContainer;
    [SerializeField] private List<Image> images = new();

    private void Start()
    {
        _screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        CameraController.PictureTakenEvent += CapturePhoto;
        images.AddRange(_photoContainer.GetComponentsInChildren<Image>());
    }

    public void CapturePhoto(CameraController cameraController)
    {
        StartCoroutine(ReadRegion());
    }

    IEnumerator ReadRegion()
    {
        _cameraUI.SetActive(false);
        yield return new WaitForEndOfFrame();
        Rect regionToRead = new (0, 0, Screen.width, Screen.height);

        _screenCapture.ReadPixels(regionToRead, 0, 0, false);
        _screenCapture.Apply();

        SavePhoto();
    }

    private void SavePhoto()
    {
        Texture2D newTexture = new (_screenCapture.width, _screenCapture.height, _screenCapture.format, false);
        newTexture.SetPixels(_screenCapture.GetPixels());
        newTexture.Apply();

        Sprite photoSprite = Sprite.Create(newTexture, new Rect(0.0f, 0.0f, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        _photoDisplayArea.sprite = photoSprite;

        foreach (Image image in images)
        {
            if (image.sprite == null)
            {
                image.sprite = photoSprite;
                break;
            }
        }

        _cameraUI.SetActive(true);
    }

    private void OnDestroy()
    {
        CameraController.PictureTakenEvent -= CapturePhoto;
    }
}
