using TMPro;
using UnityEngine;

public class PhotosCounter : MonoBehaviour
{
    private TextMeshProUGUI _photosCounter;
    private int _pictureCount;
    private void Start()
    {
        _photosCounter = GetComponent<TextMeshProUGUI>();
        CameraController.PictureTakenEvent += UpdateUI;
    }

    private void UpdateUI(CameraController ctr)
    {
        _pictureCount++;
        _photosCounter.text = SetCounterDisplay(_pictureCount);
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
