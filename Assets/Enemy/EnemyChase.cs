using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    private Vector2 startPos;
    private Vector2 playerStartPos;

    void Start()
    {
        startPos = transform.position;

        if (player == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag("Player");
            if (found != null)
                player = found.transform;
        }

        if (player != null)
            playerStartPos = player.position;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 currentPos = transform.position;
        Vector2 targetPos = new Vector2(player.position.x, currentPos.y);
        transform.position = Vector2.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);

        Vector3 localScale = transform.localScale;
        localScale.x = player.position.x < transform.position.x ? -Mathf.Abs(localScale.x) : Mathf.Abs(localScale.x);
        transform.localScale = localScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered with: " + other.name); // 👈 Helps debug
        if (other.CompareTag("Player"))
        {
            transform.position = startPos;
            player.position = playerStartPos;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }


}
