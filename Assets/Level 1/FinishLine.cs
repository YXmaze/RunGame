using UnityEngine;
using Unity.Cinemachine;  // ✅ Correct namespace for Cinemachine 3.1.3
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class FinishLine : MonoBehaviour
{
    [Header("Cinemachine Setup")]
    public CinemachineVirtualCameraBase virtualCamera; 
    // You can reference CinemachineVirtualCamera, CinemachineCamera, or the base class

    [Header("Light Sprite (PNG)")]
    public GameObject lightSprite; // Optional visual at finish line

    private bool isActivated = false;

    public string nextSceneName;
    public float waitAfterFinish;

    void Start()
    {
        // Ensure the trigger collider is set up correctly
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;

        // Light sprite should be pre-placed in the scene and not disabled
        // (Player just runs past it; no need to toggle activation)
    }

    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated && other.CompareTag("Player"))
        {
            isActivated = true;
            
            // Stop camera follow
            if (virtualCamera != null)
            {
                virtualCamera.Follow = null;
                Debug.Log("Camera stopped following player on finish line.");

                yield return new WaitForSeconds(waitAfterFinish);
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogWarning("FinishLine: virtualCamera not assigned!");
            }

            // Optional: interact with PNG, like adding animation or sound here
        }
    }
}
