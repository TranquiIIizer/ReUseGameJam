using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float elapsedTime;
    private int seconds;
    private int minutes;

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f)
        {
            seconds += Mathf.FloorToInt(elapsedTime);
            elapsedTime -= Mathf.Floor(elapsedTime);

            if (seconds >= 60)
            {
                minutes += seconds / 60;
                seconds %= 60;
            }

            timerText.text = GetTimerText(minutes, seconds);
        }
    }

    private string GetTimerText(int minutes, int seconds)
    {
        return $"{minutes:D2}:{seconds:D2}";
    }
}
