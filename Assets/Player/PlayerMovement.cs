using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //Speed JumForce and Max Jump Control
    public float runSpeed = 5f;
    public float jumpForce = 10f;
    public int maxJumps = 2;

    //Animation Control
    private Rigidbody2D rb;
    private Animator animator;
    private int jumpCount = 0;
    private bool isGrounded = false;

    //Check Ground
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    //Slide Control
    public BoxCollider2D standingCollider;
    public BoxCollider2D slideCollider;
    private bool isSliding = false;

    private bool canControl = true;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        // Reset jump count when grounded
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < maxJumps - 1))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            animator.SetTrigger("Jump");
        }

        if ((Input.GetKey(KeyCode.LeftShift)) && (isGrounded)){
           
            if (!isSliding)
            {
                isSliding = true;
                standingCollider.enabled = false;
                slideCollider.enabled = true;
                animator.SetBool("isSliding", true);
                Debug.Log("Started Slide");
            }
        }
         else
        {
            if (isSliding)
            {
                isSliding = false;
                standingCollider.enabled = true;
                slideCollider.enabled = false;
                animator.SetBool("isSliding", false);
                Debug.Log("Stopped Slide");
            }
        }

        // Update Speed parameter for idle/run blend
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);
    }
    
     public void StopInput()
    {
        canControl = false;
    }
}
