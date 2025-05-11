using System;
using TMPro;
using UnityEngine;

public class PhotosCounter : MonoBehaviour
{
    private TextMeshProUGUI _photosCounter;
    private int _pictureCount;

    public static Action<int> PhotoTakenEvent;
    private void Start()
    {
        _photosCounter = GetComponent<TextMeshProUGUI>();
        CameraController.PictureTakenEvent += UpdateUI;
    }

    private void UpdateUI(CameraController ctr)
    {
        _pictureCount++;
        _photosCounter.text = SetCounterDisplay(_pictureCount);
        PhotoTakenEvent?.Invoke(1);
    }

    private string SetCounterDisplay(int pictureCount)
    {
        if (pictureCount < 10)
        {
            return $"00{pictureCount}";
        }
        else 
        {
            return $"0{pictureCount}";
        }
    }

    private void OnDestroy()
    {
        CameraController.PictureTakenEvent -= UpdateUI;
    }
}
