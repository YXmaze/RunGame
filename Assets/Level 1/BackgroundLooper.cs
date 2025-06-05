using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public Transform player;            // Reference to player
    private float backgroundWidth;      // Width of one background piece

    private void Start()
    {
        // Get the width of the sprite renderer attached to this object
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        float cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;

        // Check if this piece is behind the player
        if (transform.position.x + backgroundWidth < player.position.x - cameraHalfWidth)
        {
            // Move this piece to the right side of the furthest piece
            float newX = transform.position.x + backgroundWidth * 2f;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}
