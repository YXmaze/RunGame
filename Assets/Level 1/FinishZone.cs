using UnityEngine;
using Unity.Cinemachine;  // Updated namespace

public class FinishZone : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;       // Reference to player Transform
    public float exitSpeed = 5f;   // Speed player runs out

    [Header("Camera Settings")]
    public CinemachineCamera virtualCamera;  // Cinemachine 3.x uses CinemachineCamera
    private bool playerInFinishZone = false;

    private void Start()
    {
        // Find the first CinemachineCamera in the scene automatically if not assigned
        if (virtualCamera == null)
        {
            virtualCamera = FindFirstObjectByType<CinemachineCamera>();
        }
    }

    private void Update()
    {
        if (playerInFinishZone)
        {
            // Move player to the right to exit the screen
            player.position += Vector3.right * exitSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInFinishZone = true;
            Debug.Log("Player entered the finish zone!");

            // Detach the camera follow target so it stops following the player
            if (virtualCamera != null)
            {
                virtualCamera.Follow = null;
                virtualCamera.LookAt = null;
            }
        }
    }
}
