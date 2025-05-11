using UnityEngine;

public class StatsTracker : MonoBehaviour
{
    public int moneyEarned;
    public int photosTook;
    public float survivedTime;

    private void Start()
    {
        PhotosCounter.PhotoTakenEvent += AddPhotos;
        CameraTimer.TimeLeftEvent += SetTimeLeft;
    }

    public void AddPhotos(int i)
    {
        photosTook += i;
    }

    public void SetTimeLeft(int i)
    {
        survivedTime = i;
    }

    private void OnDestroy()
    {
        PhotosCounter.PhotoTakenEvent -= AddPhotos;
        CameraTimer.TimeLeftEvent -= SetTimeLeft;
    }
}
