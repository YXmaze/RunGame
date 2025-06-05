using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    void Start()
    {
        if (player == null)
        {
            GameObject found = GameObject.FindGameObjectWithTag("Player");
            if (found != null)
                player = found.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector2 currentPos = transform.position;
        Vector2 targetPos = new Vector2(player.position.x, currentPos.y);

        transform.position = Vector2.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);

        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
    }
}
