using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed = 5f;             // Constant speed to the right
    public float jumpForce = 10f;           // Jump force
    public int maxJumps = 2;                // 2 jumps allowed (double jump)

    private Rigidbody2D rb;
    private int jumpCount = 0;
    private bool isGrounded = false;

    public Transform groundCheck;           // Empty object at player's feet
    public float groundCheckRadius = 0.1f;  // Ground detection radius
    public LayerMask groundLayer;           // Layer for ground

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Ground check using Physics2D.OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            jumpCount = 0; // Reset jump count on ground
        }

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Reset vertical velocity before jump
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void FixedUpdate()
    {
        // Apply constant rightward velocity
        rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);
    }
}
