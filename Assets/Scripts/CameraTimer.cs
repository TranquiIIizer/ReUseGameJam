using System;
using TMPro;
using UnityEngine;

public class CameraTimer : MonoBehaviour

{
    public TextMeshProUGUI timerText;
    private float timer = 90f;
    private int lastDisplayedTime;
    public static Action<int> TimeLeftEvent;
    public GameObject GameOverPanel;

    void Start()
    {
        VehicleCollision.GameEnded += EndGame;
        lastDisplayedTime = Mathf.CeilToInt(timer);
        timerText.text = lastDisplayedTime.ToString();
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            int currentTime = Mathf.CeilToInt(timer);

            if (currentTime != lastDisplayedTime)
            {
                lastDisplayedTime = currentTime;
                timerText.text = currentTime.ToString();
                TimeLeftEvent?.Invoke(currentTime);
            }
        }
        else if (lastDisplayedTime != 0)
        {
            lastDisplayedTime = 0;
            GameOverPanel.SetActive(true);
        }
    }

    private void EndGame(bool ended)
    {
        GameOverPanel?.SetActive(ended);
    }
}

