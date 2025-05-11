using System.Collections;
using TMPro;
using UnityEngine;

public class SequentialTypewriter : MonoBehaviour
{
    [Header("TextMeshPro Components")]
    public TextMeshProUGUI timeSurvivedText;
    public TextMeshProUGUI moneyEarnedText;
    public TextMeshProUGUI photosTookText;

    [Header("Text Values")]
    public float timeSurvivedValue;
    public int moneyEarnedValue;
    public int photosTookValue;

    public StatsTracker tracker;

    [Header("Typing Speeds")]
    public float timeTypingSpeed = 0.05f;
    public float moneyTypingSpeed = 0.03f;
    public float photosTypingSpeed = 0.04f;

    private void Start()
    {
        timeSurvivedValue = tracker.survivedTime;
        moneyEarnedValue = tracker.photosTook * 15;
        photosTookValue = tracker.photosTook;
        StartCoroutine(PlayAllTypewriters());
    }

    IEnumerator PlayAllTypewriters()
    {
        //yield return StartCoroutine(TypeTimeSurvived());
        yield return StartCoroutine(TypeMoneyEarned());
        yield return StartCoroutine(TypePhotosTook());
    }

    IEnumerator TypeTimeSurvived()
    {
        string label = "Seconds remaining:";
        string dots = new string('.', 10);
        string value = $"{timeSurvivedValue}s";
        string fullText = $"{label}{dots}{value}";

        timeSurvivedText.text = "";
        foreach (char c in fullText)
        {
            timeSurvivedText.text += c;
            yield return new WaitForSeconds(timeTypingSpeed);
        }
    }

    IEnumerator TypeMoneyEarned()
    {
        string label = "Money Earned:";
        string dots = new string('.', 10);
        string value = $"${moneyEarnedValue}";
        string fullText = $"{label}{dots}{value}";

        moneyEarnedText.text = "";
        foreach (char c in fullText)
        {
            moneyEarnedText.text += c;
            yield return new WaitForSeconds(moneyTypingSpeed);
        }
    }

    IEnumerator TypePhotosTook()
    {
        string label = "Photos Took:";
        string dots = new string('.', 10);
        string value = $"{photosTookValue}";
        string fullText = $"{label}{dots}{value}";

        photosTookText.text = "";
        foreach (char c in fullText)
        {
            photosTookText.text += c;
            yield return new WaitForSeconds(photosTypingSpeed);
        }
    }
}
