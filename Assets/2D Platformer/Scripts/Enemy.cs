using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform wallCheck;
    public float checkDistance = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private int direction = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Move constantly
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        // Raycast forward
        bool hit = Physics2D.Raycast(
            wallCheck.position,
            Vector2.right * direction,
            checkDistance,
            groundLayer
        );

        // Flip every time it detects ground in front
        if (hit)
        {
            Flip();
        }
    }

    void Flip()
    {
        direction *= -1;
        sr.flipX = direction < 0;
    }

    void OnDrawGizmosSelected()
    {
        if (wallCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            wallCheck.position,
            wallCheck.position + Vector3.right * direction * checkDistance
        );
    }
}