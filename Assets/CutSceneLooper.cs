using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutsceneLooper : MonoBehaviour
{
    public Image cutsceneImage;        // Assign the UI Image component
    public Sprite[] frames;            // Drag all your PNG frames here in the Inspector
    public float frameRate = 10f;      // Frames per second
    public string nextSceneName;       // Scene to load after cutscene
    public float cutsceneDuration = 5f; // Duration of the cutscene before auto-skip

    private int currentFrame = 0;
    private bool isPlaying = true;

    void Start()
    {
        if (frames.Length > 0)
        {
            StartCoroutine(PlayCutscene());
            StartCoroutine(AutoSkipCutscene());
        }
        else
        {
            Debug.LogError("No frames assigned to CutsceneLooper.");
        }
    }

    private IEnumerator PlayCutscene()
    {
        while (isPlaying)
        {
            cutsceneImage.sprite = frames[currentFrame];
            currentFrame = (currentFrame + 1) % frames.Length;
            yield return new WaitForSeconds(1f / frameRate);
        }
    }

    private IEnumerator AutoSkipCutscene()
    {
        yield return new WaitForSeconds(cutsceneDuration);
        SkipCutscene();
    }

    public void SkipCutscene()
    {
        isPlaying = false;
        SceneManager.LoadScene(nextSceneName);
    }
}
