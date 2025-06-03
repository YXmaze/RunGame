using UnityEngine;
using System.Collections;

public class BackgroundPan : MonoBehaviour
{
    public RectTransform backgroundTransform;  // Drag the background RectTransform here
    public Vector2 targetPosition;            // Set the target position you want to pan to
    public float panDuration = 2f;

    public void StartPan()
    {
        StartCoroutine(PanBackground());
    }

    private IEnumerator PanBackground()
    {
        Vector2 startPos = backgroundTransform.anchoredPosition;
        Vector2 endPos = targetPosition;
        float elapsed = 0f;

        while (elapsed < panDuration)
        {
            elapsed += Time.deltaTime;
            backgroundTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsed / panDuration);
            yield return null;
        }

        backgroundTransform.anchoredPosition = endPos;
    }
}
