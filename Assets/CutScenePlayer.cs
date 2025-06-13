using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutScenePlayer : MonoBehaviour
{
    public Image cutsceneImage; 
    public Sprite[] frames;                // Array of PNG frames
    public float frameRate = 10f;           // Frames per second
    public float cutsceneDuration = 5f;             // Time to wait after cutscene before changing scene
    public string nextSceneName;            // Next scene to load

    private void Start()
    {
        if (frames.Length > 0 && cutsceneImage != null)
        {
            StartCoroutine(PlayCutsceneOnce());
        }
    }

    private IEnumerator PlayCutsceneOnce()
    {
        float frameTime = 1f / frameRate;

        for (int i = 0; i < frames.Length; i++)
        {
            cutsceneImage.sprite = frames[i];
            yield return new WaitForSeconds(frameTime);
        }

        // Wait additional time after cutscene if desired
        yield return new WaitForSeconds(cutsceneDuration);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
