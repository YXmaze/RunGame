using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutScenePingPong : MonoBehaviour
{
    public Image cutsceneImage;        // Assign the UI Image component
    public Sprite[] frames;            // Drag all your PNG frames here in the Inspector
    public float frameRate = 10f;      // Frames per second
    public string nextSceneName;       // Scene to load after cutscene
    public float cutsceneDuration = 5f; // Duration of the cutscene before auto-skip

    private int currentFrame = 0;
    private int direction = 1;         // 1 for forward, -1 for backward
    private bool isPlaying = true;

    void Start()
    {
        if (frames.Length > 0)
        {
            StartCoroutine(PlayCutscenePingPong());
            StartCoroutine(AutoSkipCutscene());
        }
        else
        {
            Debug.LogError("No frames assigned to CutsceneLooper.");
        }
    }

    private IEnumerator PlayCutscenePingPong()
    {
        while (isPlaying)
        {
            cutsceneImage.sprite = frames[currentFrame];

            // Update frame index based on direction
            currentFrame += direction;

            // Check for ping-pong at ends
            if (currentFrame >= frames.Length)
            {
                currentFrame = frames.Length - 2; // step back one frame
                direction = -1; // reverse direction
            }
            else if (currentFrame < 0)
            {
                currentFrame = 1; // step forward one frame
                direction = 1; // reverse direction
            }

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
