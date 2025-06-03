using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackgroundPan : MonoBehaviour
{
    [Header("Background Settings")]
    public Transform background;            // Background object to move
    public float panDistance = 60f;         // How far to pan UP (move background UP)
    public float panSpeed = 30f;            // Speed of panning

    [Header("UI Fade Settings")]
    public CanvasGroup menuCanvasGroup;     // CanvasGroup for title and press enter texts
    public float fadeDuration = 1f;         // Duration of fade out

    [Header("Scene Settings")]
    public string nextSceneName = "FirstScene";            // Scene to load after pan and wait
    public float waitAfterPan = 5f;         // Seconds to wait after pan before loading scene

    private bool hasStarted = false;
    private Vector3 targetPosition;

    void Start()
    {
        // Calculate the target position: move background UP by panDistance units
        targetPosition = background.position + new Vector3(0, panDistance, 0);
    }

    void Update()
    {
        if (!hasStarted && Input.GetKeyDown(KeyCode.Return))
        {
            hasStarted = true;
            StartCoroutine(StartPanAndFade());
        }
    }

    private IEnumerator StartPanAndFade()
    {
        // Start fading out UI
        yield return StartCoroutine(FadeOutCanvasGroup(menuCanvasGroup, fadeDuration));

        // Pan background UP smoothly
        while (Vector3.Distance(background.position, targetPosition) > 0.01f)
        {
            background.position = Vector3.MoveTowards(background.position, targetPosition, panSpeed * Time.deltaTime);
            yield return null;
        }

        // Wait for a few seconds after pan
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
    }
}
