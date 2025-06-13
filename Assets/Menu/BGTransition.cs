using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackgroundPanDownAndFade : MonoBehaviour
{
    [Header("Background Settings")]
    public Transform background;            // Background object to move
    public float panDistance = 12.8f;         // How far to pan DOWN
    public float panSpeed = 4.5f;            // Speed of panning

    [Header("UI Fade Settings")]
    public CanvasGroup menuCanvasGroup;     // CanvasGroup to fade out
    public float fadeDuration = 1.7f;         // Duration of fade out

    [Header("Scene Settings")]
    public string nextSceneName;            // Scene to load after pan and wait
    public float waitAfterPan = 4f;         // Seconds to wait after pan before loading scene

    private bool hasStarted = false;
    private Vector3 targetPosition;

    void Start()
    {
        if (background == null || menuCanvasGroup == null)
        {
            Debug.LogError("Assign background Transform and CanvasGroup in inspector.");
            enabled = false;
            return;
        }

        // Target position is current position moved down by panDistance
        targetPosition = background.position + new Vector3(0, panDistance, 0);
    }

    void Update()
    {
        if (!hasStarted && Input.GetKeyDown(KeyCode.Return))
        {
            hasStarted = true;
            StartCoroutine(FadeThenPan());
        }
    }

    private IEnumerator FadeThenPan()
    {
        // Fade out UI first
        yield return StartCoroutine(FadeOutCanvasGroup(menuCanvasGroup, fadeDuration));

        // Then pan background down smoothly
        while (Vector3.Distance(background.position, targetPosition) > 0.01f)
        {
            background.position = Vector3.MoveTowards(background.position, targetPosition, panSpeed * Time.deltaTime);
            yield return null;
        }

        // Wait after pan
        yield return new WaitForSeconds(waitAfterPan);

        // Load next scene
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator FadeOutCanvasGroup(CanvasGroup canvasGroup, float duration)
    {
        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
