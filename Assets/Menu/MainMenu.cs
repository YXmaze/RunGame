using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public CanvasGroup menuCanvasGroup;       // Drag your menu CanvasGroup here
    public BackgroundPan backgroundPan;       // Drag the BackgroundPan script here
    public float fadeDuration = 1f;

    public void StartGame()
    {
        StartCoroutine(FadeOutMenu());
    }

    private IEnumerator FadeOutMenu()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            menuCanvasGroup.alpha = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        // Disable interactions
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;


    }
}
