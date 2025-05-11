using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private RectTransform blocker;
    [SerializeField] private CanvasGroup blockerCanvasGroup;
    [SerializeField] private CanvasGroup cinematicCanvasGroup;

    [Header("Movement Settings")]
    [SerializeField] private float firstMoveTargetX = 333f;
    [SerializeField] private float secondMoveTargetX = 722f;
    [SerializeField] private float moveDuration = 1f;

    [Header("Timing")]
    [SerializeField] private float delayBetweenMoves = 2f;
    [SerializeField] private float fadeOutDuration = 2f;

    private void Start()
    {
        StartCoroutine(PlayIntroSequence());
    }

    private IEnumerator PlayIntroSequence()
    {
        // Wait before first move
        yield return new WaitForSeconds(delayBetweenMoves);

        // Move from X = 0 → 333
        yield return StartCoroutine(MoveBlockerX(firstMoveTargetX));

        // Wait before second move
        yield return new WaitForSeconds(delayBetweenMoves);

        // Move from 333 → 722
        yield return StartCoroutine(MoveBlockerX(secondMoveTargetX));

        // Wait before fade
        yield return new WaitForSeconds(delayBetweenMoves);

        // Fade out both blocker and cinematic
        float elapsed = 0f;
        float startAlpha = 1f;

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeOutDuration);
            if (blockerCanvasGroup != null) blockerCanvasGroup.alpha = alpha;
            if (cinematicCanvasGroup != null) cinematicCanvasGroup.alpha = alpha;
            yield return null;
        }

        // Ensure alpha is zero
        if (blockerCanvasGroup != null) blockerCanvasGroup.alpha = 0f;
        if (cinematicCanvasGroup != null) cinematicCanvasGroup.alpha = 0f;
    }

    private IEnumerator MoveBlockerX(float targetX)
    {
        float elapsed = 0f;
        Vector2 startPos = blocker.anchoredPosition;
        Vector2 targetPos = new Vector2(targetX, startPos.y);

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            blocker.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsed / moveDuration);
            yield return null;
        }

        blocker.anchoredPosition = targetPos;
    }
}
