using UnityEngine;
using UnityEngine.Rendering;

public class BlurController : MonoBehaviour
{
    public Volume volume; 
    public float riseDuration = 10f;
    public float fallDuration = 1f;
    public float pauseAfterFall = 3f;

    private float currentWeight = 0f;
    private float pauseTimer = 0f;
    private bool isFalling = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFalling = true;
            pauseTimer = pauseAfterFall;
        }

        if (isFalling)
        {
            currentWeight -= Time.deltaTime / fallDuration;
            if (currentWeight <= 0f)
            {
                currentWeight = 0f;
                isFalling = false;
            }
        }
        else if (pauseTimer > 0f)
        {
            pauseTimer -= Time.deltaTime;
        }
        else
        {
            currentWeight += Time.deltaTime / riseDuration;
            currentWeight = Mathf.Clamp01(currentWeight);
        }

        volume.weight = currentWeight;
    }
}
