using UnityEngine;

public class BackgroundLooper : MonoBehaviour
{
    public Transform player;
    public float checkOffset = 2f; // A little buffer to trigger looping earlier

    private float backgroundWidth;
    private static BackgroundLooper[] allBackgrounds;

    private void Start()
    {
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;

        if (allBackgrounds == null)
            allBackgrounds = FindObjectsOfType<BackgroundLooper>();
    }

    private void Update()
    {
        float cameraLeftEdge = player.position.x - Camera.main.orthographicSize * Camera.main.aspect;

        // If this background is fully off screen to the left
        if (transform.position.x + backgroundWidth < cameraLeftEdge - checkOffset)
        {
            // Find the rightmost background piece
            float maxX = float.MinValue;
            foreach (var bg in allBackgrounds)
            {
                if (bg.transform.position.x > maxX)
                    maxX = bg.transform.position.x;
            }

            // Move this piece to the right of the rightmost one
            float newX = maxX + backgroundWidth;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}
